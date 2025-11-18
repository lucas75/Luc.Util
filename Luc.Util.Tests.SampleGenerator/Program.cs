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
  "Luc.Util.Tests/Samples/Uuid7TestSamples.cs",
  $$"""
  // <generated>Do not alter this class</generated>

  namespace Luc.Util.Tests.Samples;
  
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


var srcSharedTypes = new StringBuilder();
var srcSharedTypeInstances = new StringBuilder();
var srcSharedRandomCases = new StringBuilder();
var srcSharedEmptyCases = new StringBuilder();
var srcTypes = new StringBuilder();


for( int i = 1; i <= 64; i++ )
{
  var srcBytes = new StringBuilder();
  for( int b = 0; b < i; b++ )
  {
    srcBytes.Append($"B{b},");
  }
  srcBytes.Length -= 1;

  srcSharedTypes.AppendLine(
      $$"""
        [StructLayout(LayoutKind.Sequential, Pack=1)]
        public struct S{{i:D2}} : IEncodingInput, IEncodingOutput<S{{i:D2}}>
        {
          byte {{srcBytes}};

          readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
          {
              var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
              return (bytes, bytes.Length * 8);
          }
          public static S{{i:D2}} DecodeFromBytes(ReadOnlyMemory<byte> bytes)
          {
              if(bytes.Length < Unsafe.SizeOf<S{{i:D2}}>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S{{i:D2}}>()} byte.");
              return MemoryMarshal.Read<S{{i:D2}}>(bytes.Span);
          }
        }
      """);
 
  srcSharedTypeInstances.AppendLine(
      $$"""
            new S{{i:D2}}(),         
      """);

  srcSharedRandomCases.AppendLine(
      $$"""       
            case {{i}}: b = new byte[{{i}}]; rng.NextBytes(b); return S{{i:D2}}.DecodeFromBytes(b); 
      """);
  srcSharedEmptyCases.AppendLine(
      $$"""       
            case {{i}}: return new S{{i:D2}}(); 
      """);

  srcTypes.AppendLine(
      $$"""
            typeof(SharedTestTypes.S{{i:D2}}),
      """);
}



System.IO.File.WriteAllText
(
  "Luc.Util.Tests/Samples/SharedTestTypes.cs",
  $$"""
  // <generated>Do not alter this class</generated>
  using System;
  using System.Runtime.InteropServices;
  using System.Runtime.CompilerServices;
  using Luc.Util;
  using Luc.Util.Encoding;
  
  namespace Luc.Util.Tests.Samples;
  
  public static class SharedTestTypes
  {
  {{srcSharedTypes}}

    public static readonly IEncodingInput[] Samples = [ 
  {{srcSharedTypeInstances}}
    ];

    public static readonly Type[] Types = [
  {{srcTypes}}
    ];

    public static IEncodingInput Empty(int size)
    {
      switch(size) {
  {{srcSharedEmptyCases}}
        default: throw new ArgumentException("Size not supported.");
      }  
    }

    public static IEncodingInput Random(int size, Random rng)
    {
      byte[] b;
      switch(size) {
  {{srcSharedRandomCases}}
        default: throw new ArgumentException("Size not supported.");
      }  
    } 
  }
  """
);
