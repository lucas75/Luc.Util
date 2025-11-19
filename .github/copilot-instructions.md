Main Instructions:
- test after changes with `dotnet build && dotnet test`
- do not modify files that have &lt;generated&gt; on the start of the file

Coding Conventions:
- use interpolated multiline strings when needed 
- use file-scoped namespaces
- use collection expressions when possible.

Language resources you may not know:
- multiline interpolated strings are escaped like $$""".  {regula text} {{variable}}  .."""
- collection expressions: new[] {a, b}or new Ex[] {a,b} is the same as [a, b]`