using System;
using Xunit;
using Luc.Util.Templates;
using System.Collections.Generic;

namespace Luc.Util.Tests;

public class TemplateTests
{
    [Fact]
    public void SimplePlaceholderReplacement()
    {
        var tpl = new Template("Hello, {{name}}!");
        var r = tpl.Renderer();
        r.Set("name", "Alice");
        Assert.Equal("Hello, Alice!", r.Render());
    }

    [Fact]
    public void NamedSubTemplate_WithVar_AppendRows()
    {
        string source = "<lwx:tpl name=\"row-template\" var=\"rows\">\n<tr><td>{{A}}</td></tr>\n</lwx:tpl>\n<table>{{rows}}</table>";
        var tpl = new Template(source);
        var doc = tpl.Renderer();
        var row = tpl.GetTemplate("row-template");

        for (int i = 0; i < 3; i++)
        {
            doc.Append("rows", row.Render(new { A = i }));
        }

        var outStr = doc.Render();
        Assert.Contains("<table>", outStr);
        Assert.Contains("<tr><td>0</td></tr>", outStr);
        Assert.Contains("<tr><td>1</td></tr>", outStr);
        Assert.Contains("<tr><td>2</td></tr>", outStr);
    }

    [Fact]
    public void UnknownVariableThrows()
    {
        var tpl = new Template("{{known}} and {{missing}} ");
        var r = tpl.Renderer();
        r.Set("known", "X");
        // setting a var not declared in template should throw
        Assert.False(r.HasVariable("not_declared"));
        Assert.True(r.HasVariable("missing"));
        var caught = false;
        try { r.Set("not_declared", "Y"); }
        catch (UnknownVariableException) { caught = true; }
        Assert.True(caught);
    }

    [Fact]
    public void VarTagFormatting()
    {
        var s = "<lwx:var name=\"title\" format=\"upper\" />\n<h1>{{title}}</h1>";
        var tpl = new Template(s);
        var r = tpl.Renderer();
        r.Set("title", "welcome");
        var outStr = r.Render();
        Assert.Contains("<h1>WELCOME</h1>", outStr);
    }
}
