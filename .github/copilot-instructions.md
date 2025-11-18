# INSTRUCTION TO AGENTS
- run tests as `dotnet build && dotnet test`
- do not modify files that have &lt;generated&gt; on the start of the file

# CODE STYLE GUIDELINES
- use interpolated multiline strings when needed 
- multiline interpolated strings are escaped like $$""".  {regula text} {{variable}}  .."""
- use file-scoped namespaces
