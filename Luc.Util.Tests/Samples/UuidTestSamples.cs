namespace Luc.Util.Tests.Samples;

using System;

/// <summary>
/// Collection of sample UUIDs for testing purposes.
/// </summary>
public static class UuidTestSamples
{
  public record Sample
  {
    public required string Name { get; init; }
    public required string Canonical { get; init; }
    public required string Hex { get; init; }
    public byte[] Bytes => Convert.FromHexString(Hex);
  }

  /// <summary>
  /// General-purpose sample UUIDs for testing encoding/decoding.
  /// </summary>
  public static readonly Sample[] Samples = [
    new Sample {
      Name = "S1",
      Canonical = "019a8ee5-25a4-735a-a76c-e14671bc38aa",
      Hex = "019a8ee525a4735aa76ce14671bc38aa"
    },
      new Sample
      {
        Name = "S1",
        Canonical = "019a8ee5-25a4-735a-a76c-e14671bc38aa",
        Hex = "019a8ee525a4735aa76ce14671bc38aa",
      },
      new Sample
      {
        Name = "S2",
        Canonical = "019a8ee5-2620-74da-b9a3-535c89c446b3",
        Hex = "019a8ee5262074dab9a3535c89c446b3",
      },
      new Sample
      {
        Name = "S3",
        Canonical = "12345678-90ab-4def-9234-567890abcdef",
        Hex = "1234567890ab4def9234567890abcdef",
      },
      new Sample
      {
        Name = "S4",
        Canonical = "00112233-4455-4677-8899-aabbccddeeff",
        Hex = "00112233445546778899aabbccddeeff",
      },
      new Sample
      {
        Name = "S5",
        Canonical = "ffffffff-ffff-4fff-8fff-ffffffffffff",
        Hex = "ffffffffffff4fff8fffffffffffffff",
      },
      new Sample
      {
        Name = "S6",
        Canonical = "00000000-0000-4000-8000-000000000000",
        Hex = "00000000000040008000000000000000",
      },
      new Sample
      {
        Name = "S7",
        Canonical = "7f7f7f7f-7f7f-4f7f-9f7f-7f7f7f7f7f7f",
        Hex = "7f7f7f7f7f7f4f7f9f7f7f7f7f7f7f7f",
      },
      new Sample
      {
        Name = "S8",
        Canonical = "abcdef01-2345-4678-89ab-cdef01234567",
        Hex = "abcdef012345467889abcdef01234567",
      },
      new Sample
      {
        Name = "S9",
        Canonical = "deadbeef-0000-4bad-80de-adbeef000000",
        Hex = "deadbeef00004bad80deadbeef000000",
      },
      new Sample
      {
        Name = "S10",
        Canonical = "01020304-0506-4070-8090-a0b0c0d0e0f0",
        Hex = "01020304050640708090a0b0c0d0e0f0",
      },
];

  /// <summary>
  /// Sample of UUIDs that are known to be version 4 (random) UUIDs.
  /// </summary>
  public static readonly Sample[] SamplesType4 = [
    new Sample
    {
      Name = "Type4-1",
      Canonical = "aaaaaaaa-bbbb-4ccc-8ddd-eeeeeeeeeeee",
      Hex = "aaaaaaaabbbb4ccc8dddeeeeeeeeeeee",
    },
    new Sample
    {
      Name = "Type4-2",
      Canonical = "01234567-89ab-4cde-8f01-234567890abc",
      Hex = "0123456789ab4cde8f01234567890abc",
    },
    new Sample
    {
      Name = "Type4-3",
      Canonical = "deadbeef-dead-4bee-8dad-beefdeadbeef",
      Hex = "deadbeefdead4bee8dadbeefdeadbeef",
    },
];

  /// <summary>
  /// Sample UUID with defined variants for variant testing
  /// </summary>
  public static readonly Sample[] SamplesVariantAbc = [
    new Sample
    {
      Name = "Variant-a",
      Canonical = "12345678-90ab-4def-ac34-567890abcdef",
      Hex = "1234567890ab4defac34567890abcdef",
    },
    new Sample
    {
      Name = "Variant-b",
      Canonical = "12345678-90ab-4def-bc34-567890abcdef",
      Hex = "1234567890abbc34bc34567890abcdef",
    },
    new Sample
    {
      Name = "Variant-c",
      Canonical = "12345678-90ab-4def-cd34-567890abcdef",
      Hex = "1234567890abcd34cd34567890abcdef",
    }
  ];

  /// <summary>
  /// Samples strings that resemble canonical UUIDs but are not parseable as UUID
  /// </summary>
  public static readonly string[] SamplesWithBrokenCanonical = [      
    "12345678-90ab-4def-9234-567890abcde",    // <- Too short      
    "12345678-90ab-4def-9234-567890abcdef00", // <- Too long      
    "1234-5678-90ab-4def-9234-567890abcdef",  // <- Wrong hyphen placement      
    "1234567890ab4def9234567890abcdef",       // <- Missing hyphens      
    "12345678-90ab-4def-9z34-567890abcdef"    // <- Invalid hex character within canonical string
  ];

  /// <summary>
  /// Sample hex strings that are not parseable as UUID
  /// </summary>
  public static readonly string[] SamplesWithBrokenHex = [
    "1234567890ab4def9234567890abcde",        // <- Too short
    "1234567890ab4def9234567890abcdefg",      // <- Too long with non-hex char 'g'
    "1234567890ab4def9234567890abcdefa",      // <- Too long 
    "thisisnothexatall",                      // <- Not hex at all
  ];
}

