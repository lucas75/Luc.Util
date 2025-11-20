namespace Luc.Util.Templates;

using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

public sealed partial class Template
{
    public Part[] Parts { get; }
    public IReadOnlyDictionary<string, Template> NamedTemplates { get; }

    private Template(Part[] parts, Dictionary<string, Template> namedTemplates)
    {
        Parts = parts;
        NamedTemplates = namedTemplates;
    }

    public Template(string source)
    {
        var parts = new List<Part>();
        var namedTemplates = new Dictionary<string, Template>();

        int i = 0;
        while (i < source.Length)
        {
            // detect <lwx:tpl
            var tplOpen = source.IndexOf("<lwx:tpl", i, System.StringComparison.Ordinal);
            var varTag = source.IndexOf("<lwx:var", i, System.StringComparison.Ordinal);
            var token = source.IndexOf("{{", i, System.StringComparison.Ordinal);

            int nextSpecial = source.Length;
            if (tplOpen >= 0) nextSpecial = System.Math.Min(nextSpecial, tplOpen);
            if (varTag >= 0) nextSpecial = System.Math.Min(nextSpecial, varTag);
            if (token >= 0) nextSpecial = System.Math.Min(nextSpecial, token);

            if (nextSpecial > i)
            {
                // append text up to next special
                parts.Add(new TextPart(source.Substring(i, nextSpecial - i)));
                i = nextSpecial;
                continue;
            }

            if (nextSpecial == token)
            {
                // parse variable token until '}}'
                int end = source.IndexOf("}}", i + 2, System.StringComparison.Ordinal);
                if (end < 0) throw new TemplateParseException("Unterminated {{ token");
                var name = source.Substring(i + 2, end - (i + 2)).Trim();
                // support simple escape ({{{{) => literal '{{'
                if (name.Length == 0 && source.Substring(i).StartsWith("{{{{"))
                {
                    parts.Add(new TextPart("{{"));
                    i += 4;
                    continue;
                }

                if (!Regex.IsMatch(name, "^[a-zA-Z0-9_\\-]+$"))
                    throw new TemplateParseException($"Invalid placeholder '{name}'");

                parts.Add(new VarPart(new Var(name)));
                i = end + 2;
                continue;
            }

            if (nextSpecial == varTag)
            {
                // parse <lwx:var name="title" format="upper" />
                var end = source.IndexOf("/>", i + 1, System.StringComparison.Ordinal);
                if (end < 0) throw new TemplateParseException("Unterminated <lwx:var tag");
                var tagText = source.Substring(i, end - i + 2);
                var attrs = ParseAttributes(tagText);
                if (!attrs.TryGetValue("name", out var varName)) throw new TemplateParseException("<lwx:var> missing name");
                attrs.TryGetValue("format", out var format);

                // `<lwx:var>` is a declaration; it should not emit output but registers formatting
                if (!string.IsNullOrEmpty(varName) && !string.IsNullOrEmpty(format))
                {
                    _varFormats[varName] = format;
                }
                i = end + 2;
                continue;
            }

            if (nextSpecial == tplOpen)
            {
                // find end of open tag '>'
                var openEnd = source.IndexOf('>', tplOpen);
                if (openEnd < 0) throw new TemplateParseException("Unterminated <lwx:tpl tag");

                var openTag = source.Substring(tplOpen, openEnd - tplOpen + 1);
                var attrs = ParseAttributes(openTag);
                attrs.TryGetValue("name", out var name);
                attrs.TryGetValue("var", out var varName);

                // find matching </lwx:tpl> with nested depth
                var closeTag = "</lwx:tpl>";
                int depth = 0;
                int bodyStart = openEnd + 1;
                int j = bodyStart;
                int foundIndex = -1;
                while (j < source.Length)
                {
                    var nextOpen = source.IndexOf("<lwx:tpl", j, System.StringComparison.Ordinal);
                    var nextClose = source.IndexOf(closeTag, j, System.StringComparison.Ordinal);
                    if (nextClose < 0) throw new TemplateParseException("Missing </lwx:tpl>");
                    if (nextOpen >= 0 && nextOpen < nextClose)
                    {
                        depth++;
                        j = nextOpen + 1;
                        continue;
                    }
                    if (depth == 0)
                    {
                        foundIndex = nextClose;
                        break;
                    }
                    // consume close that matched deeper nesting
                    depth--;
                    j = nextClose + closeTag.Length;
                }

                if (foundIndex < 0) throw new TemplateParseException("Missing matching </lwx:tpl>");

                var body = source.Substring(bodyStart, foundIndex - bodyStart);
                var subTpl = new Template(body);
                parts.Add(new TemplatePart(subTpl, name, varName));
                if (!string.IsNullOrEmpty(name)) namedTemplates[name] = subTpl;

                i = foundIndex + closeTag.Length;
                continue;
            }
        }

        Parts = parts.ToArray();
        // Build format map from var declarations
        foreach (var p in Parts.OfType<VarPart>())
        {
            if (!string.IsNullOrEmpty(p.Variable.Format))
            {
                _varFormats[p.Variable.Name] = p.Variable.Format!;
            }
        }
        NamedTemplates = new System.Collections.ObjectModel.ReadOnlyDictionary<string, Template>(namedTemplates);
    }

    private static Dictionary<string, string> ParseAttributes(string text)
    {
        var ret = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var m = Regex.Matches(text, @"([a-zA-Z_\-:]+)\s*=\s*""([^""]*)""");
        foreach (Match mm in m)
        {
            var k = mm.Groups[1].Value;
            var v = mm.Groups[2].Value;
            ret[k] = v;
        }
        return ret;
    }

    public Renderer Renderer()
    {
        return new Renderer(this);
    }

    public Template GetTemplate(string name)
    {
        if (!NamedTemplates.TryGetValue(name, out var tpl)) throw new System.ArgumentException($"Template '{name}' not found");
        return tpl;
    }

    public string Render(IDictionary<string, string> dict)
    {
        var r = Renderer();
        foreach (var kv in dict) r.Set(kv.Key, kv.Value);
        return r.Render();
    }

    public string Render(object model)
    {
        var r = Renderer();
        r.SetFromModel(model);
        return r.Render();
    }
}

public partial class Template
{
    // var formats declared via <lwx:var>
    private readonly Dictionary<string, string> _varFormats = new();
    public IReadOnlyDictionary<string, string> VarFormats => _varFormats;
}

public class UnknownVariableException : Exception
{
    public UnknownVariableException(string name) : base($"Unknown variable '{name}'") { }
}

public class TemplateParseException : Exception
{
    public TemplateParseException(string message) : base(message) { }
}

public sealed class Renderer
{
    private readonly Template _tpl;
    private readonly Dictionary<string, string> _scalars = new();
    private readonly Dictionary<string, List<string>> _collections = new();
    private readonly HashSet<string> _knownVars;
    private readonly HashSet<string> _knownCollections;

    public Renderer(Template tpl)
    {
        _tpl = tpl;
        _knownVars = new HashSet<string>(CollectVars(tpl));
        _knownCollections = new HashSet<string>(_tpl.Parts.OfType<TemplatePart>().Where(x => x.VarName is not null).Select(x => x.VarName!));

        // initialize collections
        foreach (var c in _knownCollections) _collections[c] = new List<string>();
    }

    private static IEnumerable<string> CollectVars(Template tpl)
    {
        foreach (var p in tpl.Parts)
        {
            if (p is VarPart vp) yield return vp.Variable.Name;
            // named template with no var will not introduce a variable at top-level
            // nested var tokens in subtemplates are not available on the parent renderer
        }
    }

    public void Set(string key, string value)
    {
        if (!_knownVars.Contains(key)) throw new UnknownVariableException(key);
        _scalars[key] = value ?? string.Empty;
    }

    // Expose for tests: whether the variable exists in this renderer's top-level scope
    public bool HasVariable(string name) => _knownVars.Contains(name);

    public void SetFromModel(object model)
    {
        if (model == null) return;
        var props = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        foreach (var p in props)
        {
            var name = p.Name;
            if (_knownVars.Contains(name))
            {
                var val = p.GetValue(model);
                _scalars[name] = val?.ToString() ?? string.Empty;
            }
        }
    }

    public void Append(string varName, string value)
    {
        if (!_knownCollections.Contains(varName)) throw new UnknownVariableException(varName);
        _collections[varName].Add(value ?? string.Empty);
    }

    public Template GetTemplate(string name)
    {
        var sub = _tpl.GetTemplate(name);
        return sub;
    }

    private string ApplyFormat(string val, string? format)
    {
        if (string.IsNullOrEmpty(format)) return val;
        return format.ToLowerInvariant() switch
        {
            "upper" => val.ToUpperInvariant(),
            "lower" => val.ToLowerInvariant(),
            _ => val
        };
    }

    public string Render()
    {
        var sb = new StringBuilder();
        foreach (var p in _tpl.Parts)
        {
            switch (p)
            {
                case TextPart t:
                    sb.Append(t.Text);
                    break;
                case VarPart v:
                    if (!_scalars.TryGetValue(v.Variable.Name, out var s)) s = string.Empty;
                    var format = v.Variable.Format ?? (_tpl.VarFormats.TryGetValue(v.Variable.Name, out var fmt) ? fmt : null);
                    sb.Append(ApplyFormat(s, format));
                    break;
                case TemplatePart tp:
                    if (!string.IsNullOrEmpty(tp.VarName))
                    {
                        if (_collections.TryGetValue(tp.VarName, out var list))
                        {
                            foreach (var item in list) sb.Append(item);
                        }
                    }
                    // templates without var are not inserted automatically
                    break;
            }
        }
        return sb.ToString();
    }
}

public abstract class Part { }

public sealed class TextPart : Part
{
    public string Text { get; }
    public TextPart(string text) { Text = text; }
}

public sealed class VarPart : Part
{
    public Var Variable { get; }
    public VarPart(Var v) { Variable = v; }
}

    public sealed class TemplatePart : Part
{
    public Template Template { get; }
    public string? Name { get; }
    public string? VarName { get; }

    public TemplatePart(Template tpl, string? name, string? varName)
    {
        Template = tpl;
        Name = name;
        VarName = varName;
    }
}

public sealed class Var
{
    public Var(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public string? Format { get; set; }
}
