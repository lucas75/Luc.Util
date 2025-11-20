# Luc.Util.Template 

This package provides a small, lightweight template processor used by the Luc.Util solution.

Syntax is intentionally tiny. It supports:

- Named template blocks: <lwx:tpl name="my-template">...</lwx:tpl>
- Optional collection binding on a template via `var`: <lwx:tpl name="row-template" var="row-var">...</lwx:tpl>
- Simple placeholder replacement using `{{name}}`.

Notes:
- Named templates without `var` are *not* inserted into the parent document; they are available by name via `GetTemplate(name)`.
- Named templates with `var` are replaced in-place by a `{{var}}` token and can be fed via `Append(var, value)`. This is the primary mechanism for repeating rows, etc.
 - Named templates with `var` are replaced in-place by a `{{var}}` token and can be fed via `Append(var, value)`. This is the primary mechanism for repeating rows, etc.
 - Variables can also be declared using `<tpl:var name="cde"/>` which is equivalent to inserting `{{cde}}`.







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

var out = tpl.Render();
var titleTpl = tpl.GetTemplate("title-template");
var rowTpl = tpl.GetTemplate("row-template");
// Render returns an object that can be `Set` with variables and `Append` to named `var` bindings,
// and finally `Render()` produces the final output string.

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

## API sketch

- `var tpl = new Template(source)` — parse the source and discover `lwx:tpl` blocks.
- `var out = tpl.Render()` — create a renderer that is used to produce a final output.
- `out.Set(string key, object value)` — replace `{{key}}` with `value.ToString()`. Accepts strings or objects; objects are searched for properties.
- `out.Append(string key, string value)` — append `value` to a named `var` slot inserted by a `<lwx:tpl var="...">` block.
- `tpl.GetTemplate(string name)` — returns a template object that can be `Render()`ed separately (optionally with a model object).

### Examples

Simple replacement

```
var tpl = new Template("Hello, {{name}}!");
var out = tpl.Render();
out.Set("name", "Alice");
return out.Render(); // "Hello, Alice!"
```

Templates with collection binding and named sub-templates

```
var tpl = new Template("<lwx:tpl name='row-template' var='rows'>\n<tr><td>{{A}}</td></tr>\n</lwx:tpl>\n<table>{{rows}}</table>");
var table = tpl.Render();
var row = tpl.GetTemplate("row-template");
for(int i = 0; i < 10; i++) table.Append("rows", row.Render(new { A = i }));
return table.Render();
```

Behavior notes / edge cases
---------------------------

- Escaping `{{` — to allow literal `{{` in templates, use `{{{` or a double escape `{{{{` depending on context (implementation detail). We'll implement a minimal escape sequence `{{{{` to render `{{` unchanged.
- Nested named templates are not supported; do not put `<lwx:tpl>` blocks inside other `<lwx:tpl>` blocks. The parser treats each `lwx:tpl` top-level block in the source.
- Only discovered `{{...}}` tokens present in the top-level document can be `Set` or `Append` on the renderer. Attempting to set an undefined token throws an exception.
 - `Set` and `Append` operate on variables discovered anywhere in the template tree: tokens in the main content or inside named templates and `tpl:var` declarations are all valid targets. Attempting to set an undefined token throws an exception.
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
- Template block parsing: find `lwx:tpl` blocks using a regex that captures `name`, optional `var` and inner text. The parser will run a first pass to extract `lwx:tpl` definitions and strip them from the main content before tokenization.
- Rendering behavior: `Renderer.Render()` replaces `{{key}}` with provided values. For `var` placeholders, each appended value is concatenated. If a value is null or missing, it prints an empty string.
- Model binding: `GetTemplate(name).Render(model)` uses public properties of `model` (via reflection) to resolve placeholder values inside that template.
- Thread-safety: `Template` instances are immutable. `Renderer` is not thread-safe and intended for single use.

Limitations
-----------

- There is no HTML auto-escaping, nor expression evaluation. The focus is simple string substitution.
- Nested lwx:tpl blocks are discouraged and not explicitly supported — behavior is undefined in those cases.
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

