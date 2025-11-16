// execute isso no diretório da solução Luc.Util...
// dotnet run --project Luc.Util.Tests.SampleGenerator/Luc.Util.Tests.SampleGenerator.csproj

using Medo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;


var srcRecords = new StringBuilder();

for (int i = 0; i < 100; i++)
{
  var ts1 = DateTime.UtcNow;
  var uuid = Uuid7.NewUuid7();
  Thread.Sleep(Random.Shared.Next(1, 200));
  var ts2 = DateTime.UtcNow;

  srcRecords.AppendLine(
      $$"""         
            new()
            {
                UUID = "{{uuid}}", 
                TS1 = DateTimeOffset.Parse("{{ts1:O}}", null, System.Globalization.DateTimeStyles.RoundtripKind), 
                TS2 = DateTimeOffset.Parse("{{ts2:O}}", null, System.Globalization.DateTimeStyles.RoundtripKind),
                MedoId25 = "{{uuid.ToId25String()}}",
                MedoId26 = "{{uuid.ToId26String()}}",
                MedoId22 = "{{uuid.ToId22String()}}",
            },
      """);
}

var path = Path.Combine("Luc.Util.Tests", "Luc.Util.Tests.csproj");
if( !File.Exists(path) )
{
  throw new Exception("Esse projeto tem que ser executado no diretório Luc.Util (raiz da solução) com o comando dotnet run --project Luc.Util.Tests.SampleGenerator/Luc.Util.Tests.SampleGenerator.csproj");
}

var outputDir = "Luc.Util.Tests/Generated";
System.IO.Directory.CreateDirectory(outputDir);

System.IO.File.WriteAllText
(
  "Luc.Util.Tests/Generated/Uuid7TestSamples.cs",
  $$"""
  namespace Luc.Util.Tests.Generated;
  
  public static class Uuid7TestSamples
  {
    public record UuidRecord
    {
        public required string UUID { get; init; }
        public required string MedoId25 { get; init; }
        public required string MedoId26 { get; init; }
        public required string MedoId22 { get; init; }
        public required DateTimeOffset TS1 { get; init; }
        public required DateTimeOffset TS2 { get; init; }
    }

    public static readonly UuidRecord[] Uuids = [
  {{srcRecords}}

    ];
  }
  """
);
