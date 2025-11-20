# Luc.Util.Template 

This package provides a small, lightweight template processor used by the Luc.Util solution.

Syntax is intentionally tiny. It supports nested templates and a minimal variable system:

- Named template blocks: <lwx:tpl name="my-template">...</lwx:tpl>
- Optional collection binding on a template via `var`: <lwx:tpl name="row-template" var="row-var">...</lwx:tpl>
- Explicit variable slot declaration: <lwx:var name="title" format="upper" />
- Simple placeholder replacement using `{{name}}`.

Notes:
- `lwx:tpl` blocks may be nested and nesting is supported. This enables hierarchical composition of templates.
- Named templates without `var` are *not* inserted into the parent document; they are available by name via `GetTemplate(name)`.
- Named templates with `var` are replaced in-place by a `{{var}}` token or by a `lwx:var` declaration and can be fed via `Append(var, value)`. This is the primary mechanism for repeating rows, etc.







# PLAN

```c#

var tpl = new Template( """

  Hello, {{name}}!

  <lwx:tpl name="title-template">
    This is the title block.
  </lwx:tpl>

  <table>
  <thead>
    <tr>
      <td>Abc</td>
      <td>Cde</td>
    </tr>
  </thead>
  <tbody>
    <lwx:tpl var="row-template-var" name="row-template">
    </lwx:tpl>
  </tbody>
  </table>

""" );

var out = tpl.Renderer();
// If you wish to manipulate a named sub-template as a separate renderer, call `.GetTemplate(name).Renderer()`.
var titleTpl = tpl.GetTemplate("title-template").Renderer();
var rowTpl = tpl.GetTemplate("row-template");
// Renderer returns an object backed by a `Dictionary<string,string>` that can be `Set` with variables and `Append` to named `var` bindings,
// and finally `Render()` produces the final output string.

// If the template is nested (see the Nested templates example below), you can render the nested template
// using `tpl.GetTemplate(name).Renderer()` and then `doc.Set("varName", nestedRenderer.Render())` to insert
// the rendered nested content into the parent document.

// The following line will throw: `out.Set("title-template", ...)` because `title-template` must be
// used via `GetTemplate("title-template")` and not via `Set`, it has no `var` binding in the parent.

out.Set( "name", "World" );

// this should throw a unknow variable error because <lwx:tpl> without var cannot be used as a variable
out.Set( "title-template", titleTpl.Render() );

for(int i = 0; i < 10; i++ )
{
  out.Append( "row-template-var", rowTpl.Render( new { Abc = i, Cde = i * 2 } );
}

return out.Render();
```

Nested templates example

```c#
var s = """
<lwx:tpl name="outer-template" var="outterTpl">
  <h1>{{title}}</h1>
  <ul>
    <lwx:tpl name="inner-template" var="innerRows">
      <li>{{value}}</li>
    </lwx:tpl>
  </ul>
</lwx:tpl>
""";

var tpl = new Template(s);

var doc = tpl.Renderer();
var outer = tpl.GetTemplate("outer-template").Renderer();

outer.Set("title", "My List");

var inner = outer.GetTemplate("inner-template");
for (int i = 0; i < 3; i++)
{
    outer.Append("innerRows", inner.Render(new { value = "Item " + i }));
}

doc.Set("outterTpl", outer.Render());

// Render the page; outer-template will expand using the appended inner rows
return doc.Render();
```

## API sketch

- `var tpl = new Template(source)` — parse the source and discover `lwx:tpl` blocks, `lwx:var` declarations and `{{...}}` tokens. The parser builds a structured model composed of text, var, and tpl parts.
- `var out = tpl.Renderer()` — create a renderer that is used to produce a final output. The renderer is backed by a dictionary-like store mapping variable names to their string values.
 - `var out = tpl.Renderer()` — create a renderer that is used to produce a final output. The renderer is backed by a dictionary-like store mapping variable names to their string values. 
 - `out.GetTemplate(string name)` — you can also call `GetTemplate` on a renderer to obtain a nested template that's scoped to the renderer (useful for building sub-renderers that you will later append into the parent renderer using a `var` slot).
- `out.Set(string key, object value)` — replace `{{key}}` with `value.ToString()`. Accepts strings or objects; objects are searched for properties.
- `out.Append(string key, string value)` — append `value` to a named `var` slot inserted by a `<lwx:tpl var="...">` block.
- `tpl.GetTemplate(string name)` — returns a template object that can be `Render()`ed separately (optionally with a model object).
- `tpl.Render(IDictionary<string,string> dict)` — shorthand for `var r = tpl.Renderer(); r.Set(dict); return r.Render();`.

### Examples

Simple replacement

```c#
var tpl = new Template("Hello, {{name}}!");
var out = tpl.Renderer();
out.Set("name", "Alice");
return out.Render(); // "Hello, Alice!"
```

Templates with collection binding and named sub-templates

```c#
var tpl = new Template("<lwx:tpl name='row-template' var='rows'>\n<tr><td>{{A}}</td></tr>\n</lwx:tpl>\n<table>{{rows}}</table>");
var table = tpl.Renderer();
var row = tpl.GetTemplate("row-template");
for(int i = 0; i < 10; i++) table.Append("rows", row.Render(new { A = i }));
return table.Render();
```

Convenience: Render from a dictionary

```c#
var tpl = new Template("Hello, {{name}}! Today is {{day}}.");
var dict = new Dictionary<string,string>{{"name","Bob"},{"day","Friday"}};
// This is a shorthand for creating a Renderer() and prepopulating it
var output = tpl.Render(dict);
return output; // "Hello, Bob! Today is Friday."
```

`lwx:var` and formatting

```c#
var s = "<lwx:var name=\"title\" format=\"upper\" />\n<h1>{{title}}</h1>";
var tpl = new Template(s);
var r = tpl.Renderer();
r.Set("title", "welcome");
// With a simple "upper" formatter the renderer outputs uppercase for this var
return r.Render(); // "\n<h1>WELCOME</h1>"
```

Behavior notes / edge cases
---------------------------

- Escaping `{{` — to allow literal `{{` in templates, use `{{{` or a double escape `{{{{` depending on context (implementation detail). We'll implement a minimal escape sequence `{{{{` to render `{{` unchanged.
- Nested named templates are supported; you may place `<lwx:tpl>` blocks inside other `<lwx:tpl>` blocks. This allows inner templates to act as sub-templates (or `var`-backed repeaters) that the outer template can reference using `{{name}}` or `{{var}}` placeholders.
- Only discovered `{{...}}` tokens present in the top-level document can be `Set` or `Append` on the renderer. Attempting to set an undefined token throws an exception.
- Named templates can be `GetTemplate(name)` and `Render(model)` to render them standalone with a given model object. Model properties are resolved via public properties (reflection) and re-used during token replacement.
- Render output is built by concatenating appended items for the same `var` in the order they were appended.

Next steps in implementation
----------------------------

1. Implement a parser for the `lwx:tpl` blocks and tokenizing of `{{...}}` placeholders.
2. Implement `Template` type with `.Render()` to produce a `Renderer` object with `Set`, `Append` and final `Render()`.
3. Add unit tests validating: simple replacement, collection binding append, unknown variable errors, model property resolution.

Implementation details
----------------------

- Token matching: we will treat placeholder tokens as `{{identifier}}` where `identifier` matches `[a-zA-Z0-9_\-]+`.
 - Template block parsing: the parser discovers `lwx:tpl` (supporting nested blocks), `lwx:var` declarations and `{{identifier}}` tokens, and builds an ordered `Template` array composed of `<text>`, `<var>` and `<tpl>` parts.
  - Algorithm (recursive block collection): when the parser finds a `<lwx:tpl ...>` opening tag, it begins collecting text into a new buffer and keeps an integer `depth` counter initialized at 0. As it scans further:
    1. Each time it finds another opening `<lwx:tpl` the parser increments `depth`.
    2. Each time it finds a closing `</lwx:tpl>` it decrements `depth`.
    3. When it finds a `</lwx:tpl>` and `depth == 0`, the collected buffer contains the full sub-template body for that `lwx:tpl` and the parser stops collecting for that sub-template.
    4. The sub-template text (the collected buffer) is then parsed recursively via `new Template(subTemplateText)` to discover its own `lwx:tpl`, `lwx:var`, and `{{...}}` tokens, and then the sub-template is recorded as a `<tpl>` part in the parent template's parts array.
    5. The parent parser continues scanning text after the `</lwx:tpl>` that ended the sub-template, appending any intervening raw text as `<text>` parts.

  - This approach is fully recursive and tolerates arbitrarily nested `lwx:tpl` blocks.

  - Pseudocode (high level):

    ```text
    function parseTemplate(source):
      parts = []
      i = 0
      while i < len(source):
        if source at i starts with "<lwx:tpl":
          tagStart = i
          // find end of the tag open
          i = findEndOfOpenTag(source, i)
          depth = 0
          bufferStart = i
          i = i + 1
          while i < len(source):
            if source at i starts with "<lwx:tpl":
              depth += 1
            else if source at i starts with "</lwx:tpl>":
              if depth == 0:
                // found matching close for the original tpl
                subText = source[bufferStart:i]
                tpl = new Template(subText) // recursive parse
                parts.append( tplPart(tpl) )
                i = i + len("</lwx:tpl>")
                break
              else:
                depth -= 1
            i += 1
        else:
          text = collectUntilNextOpenTplOrEof(source, i)
          parts.append( textPart(text) )
          i += len(text)
      return parts
    ```

  - Notes:
    - The parser reads the `lwx:tpl` opening tag and its attributes in order. Attributes such as `name` and `var` are captured and stored on the parent part, but any `{{...}}` tokens or `lwx:var` instances found in the sub-template body are parsed by the new `Template(subTemplateText)` instance — i.e., each sub-template is parsed in order by its own parser and handles its own variables.
    - The `depth` counter excludes the original tpl: when you first encounter the opening `lwx:tpl`, it sets `depth` to 0 and starts scanning for nested opens/closes; subsequent `lwx:tpl` increments reflect additional layers of nesting.
    - This algorithm avoids a fragile regex-only parsing approach and is robust to nested content.
- Rendering behavior: `Renderer.Render()` replaces `{{key}}` with provided values. For `var` placeholders, each appended value is concatenated. If a value is null or missing, it prints an empty string.
- Model binding: `GetTemplate(name).Render(model)` uses public properties of `model` (via reflection) to resolve placeholder values inside that template.
- Thread-safety: `Template` instances are immutable. `Renderer` is not thread-safe and intended for single use.

Implementation sketch (C#)
---------------------------

Below is an implementation-level sketch describing the major types, their responsibilities and example pseudocode for the parser and renderer. The goal is to document exactly how to turn the plan into code.

Types & responsibilities
- `Template` — immutable, parsed representation of a source string; exposes:
  - Constructor: `public Template(string source)` — parse and build the `Parts[]` model.
  - `GetTemplate(string name)` — return a sub-template by name.
  - `Renderer()` — create a fresh `Renderer` instance for this template.
  - `Render(IDictionary<string,string> dict)` — shorthand for `Renderer()` + `Set(dict)` + `Render()`.

- `Renderer` — the rendering context/backing dictionary for a `Template` instance. Responsible for replacements and collections.
  - Backing store: `IDictionary<string, string> scalars` and `IDictionary<string, List<string>> collections`
  - `Set(string key, object value)` — set scalar or model-backed values.
  - `Append(string varName, string chunk)` — append to named collection bound to a `<lwx:tpl var="...">` slot.
  - `Render()` — walk `Template.Parts` producing final string.
  - `GetTemplate(string name)` — access a sub-template relative to the renderer's owner template. (Returns `Template` object or a renderer for it.)

- `Part` (abstract) — base representation of a template part.
  - `TextPart` — plain text
  - `VarPart` — a `Var` object with `Name` & `Format`
  - `TemplatePart` — holds a reference to a sub-`Template` object and optionally its `var` attribute.

- `Var` — small class that represents variables discovered by either `{{name}}` tokens or `lwx:var` attributes. Has `Name` and optional `Format`.

Parser pseudocode (C# style)
---------------------------

The parser performs a single scan of the source and constructs `Part` instances. For `lwx:tpl` it uses the counter algorithm discussed in this document.

```csharp
public Template(string source)
{
  var parts = new List<Part>();
  int i = 0;
  while (i < source.Length)
  {
    if (MatchesAt(source, i, "<lwx:tpl"))
    {
      var openAttrText = ReadUntil(source, ref i, '>'); // capture attributes
      var (name, varName) = ParseAttributes(openAttrText);

      int depth = 0;
      int bodyStart = i; // after the '>' char

      for (; i < source.Length; i++)
      {
        if (MatchesAt(source, i, "<lwx:tpl")) depth++;
        else if (MatchesAt(source, i, "</lwx:tpl>"))
        {
          if (depth == 0)
          {
            var subText = source.Substring(bodyStart, i - bodyStart);
            var subTpl = new Template(subText); // recursion
            parts.Add(new TemplatePart(subTpl, name, varName));
            i += "</lwx:tpl>".Length;
            break;
          }
          depth--;
        }
      }
      continue;
    }

    if (MatchesAt(source, i, "{{"))
    {
      var token = ReadUntil("}}", ref i);
      parts.Add(new VarPart(new Var(token)));
      i += 2; // skip '}}'
      continue;
    }

    // fallback: collect raw text until next special construct
    var text = ReadUntilNextSpecial(source, ref i);
    parts.Add(new TextPart(text));
  }

  this.Parts = parts.ToArray();
}
```

Renderer pseudocode
-------------------

The renderer walks the parts array and produces the output. For `<tpl>` parts bound to `var` the renderer uses its `collections` map. For sub-templates that are not var-bound, you can use `GetTemplate(name)` to render them as standalone templates.

```csharp
public string Render()
{
  var sb = new StringBuilder();
  foreach (var p in Template.Parts)
  {
    switch (p)
    {
      case TextPart t:
        sb.Append(t.Text);
        break;
      case VarPart v:
        var raw = scalars.TryGetValue(v.Var.Name, out var s) ? s : String.Empty;
        sb.Append(ApplyFormat(raw, v.Var.Format));
        break;
      case TemplatePart tplPart:
        if (!String.IsNullOrEmpty(tplPart.VarName))
        {
          // If the var is present, append all items
          if (collections.TryGetValue(tplPart.VarName, out var items))
            sb.Append(string.Join("", items));
        }
        else
        {
          // nested template with no var binding is not inserted automatically
          // it can be obtained with GetTemplate(name) and rendered explicitly
        }
        break;
    }
  }

  return sb.ToString();
}
```

Notes:
- The parser is intentionally lenient about whitespace; attribute parsing should use a small attribute parser rather than a complex regex. Attributes on `lwx:tpl` are recorded with the `TemplatePart` object so the renderer can recognize var bindings.
- For `GetTemplate(name)` the lookup returns a `Template` instance which can be rendered with `tpl.Render(model)` or `tpl.Renderer()`.
- Tests should assert nested templates, `lwx:var` formatting, and `Render(dict)` shorthand behave correctly.

Template model
--------------

When the source is parsed, the result is represented as an ordered array (list) of parts. These parts appear in the final rendering order and may be one of:

- `<text>` — a string literal to be emitted as-is.
- `<var>` — a variable slot, parsed from either `{{name}}` or a `lwx:var` declaration. The `<var>` is represented by a `Var` class that contains at least `Name` and `Format` properties:
  - `string Name` — variable identifier
  - `string Format` — optional format instruction (e.g., `upper`, `lower`, `trim`).
- `<tpl>` — a nested `lwx:tpl` block; it itself contains a list of parts and can be recursively rendered.

This ordered model (e.g., `<text> <var> <text> <tpl> <var>`) allows the renderer to walk the array, looking up `<var>` values in the backing dictionary and recursing into `<tpl>` blocks as needed.

Limitations
-----------

- There is no HTML auto-escaping, nor expression evaluation. The focus is simple string substitution.
 - Nested lwx:tpl blocks are supported and encouraged for composing hierarchical templates.
- The parser prioritizes simplicity and clarity over complicated templating features.

Planned exceptions and types
----------------------------

- `UnknownVariableException` for attempts to Set or Append a variable that is not defined in the root content.
- `TemplateParseException` for invalid `lwx:tpl` syntax during parsing.

Testing plan
------------

Write unit tests under `Luc.Util.Tests` (new file: `TemplateTests.cs`) that cover:

- Basic placeholder replacement using `Set`.
- `GetTemplate(name).Render(model)` with a small model object.
- Collection rendering: `Append` followed by `Render()` results.
- Unknown variable `Set`/`Append` throws `UnknownVariableException`.

