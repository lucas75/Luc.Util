#pragma warning disable CS8601

using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Reflection;
using Xunit;
using Luc.Util.Encoding;
using Luc.Util.UUID;
using Luc.Util.Tests.Samples;
using System.Linq;

namespace Luc.Util.Tests.Encoding;


public class BaseEncodingCornerCasesTests
{
  [Fact]
  public void Structs_AllZero_RoundTrip_Base32_36_64()
  {

    for (int size = 1; size <= 64; size++)
    {
      var instance = SharedTestTypes.Empty(size);
      var type = SharedTestTypes.Types[size - 1];

      // Base36
      { 
        var encodeMethod = GetEncodeMethod(typeof(Base36Sortable), type);
        var enc = encodeMethod.Invoke(null, new object[] { instance });
        var decodeMethod = GetDecodeMethod(typeof(Base36Sortable), type, 2);
        var dec = decodeMethod.Invoke(null, new object[] { enc, size * 8 });
        Assert.Equal(instance, dec);
      }

      // Base32
      {
        var encodeMethod = GetEncodeMethod(typeof(Base32Sortable), type);
        var enc = encodeMethod.Invoke(null, new object[] { instance });
        var decodeMethod = GetDecodeMethod(typeof(Base32Sortable), type, 2);
        var dec = decodeMethod.Invoke(null, new object[] { enc, size * 8 });
        Assert.Equal(instance, dec);
      }

      // Base64
      {
        var encodeMethod = GetEncodeMethod(typeof(Base64), type);
        var enc = encodeMethod.Invoke(null, new object[] { instance });
        var decodeMethod = GetDecodeMethod(typeof(Base64), type, 1, typeof(string));
        var dec = decodeMethod.Invoke(null, new object[] { enc });
        Assert.Equal(instance, dec);
      }
    }
  }

  [Fact]
  public void Base32_Uuid7Samples_UsesSameAlphabetAsMedoId26()
  {
    foreach (var sample in Uuid7TestSamples.Uuids)
    {
      var uuid = Uuid.Parse(sample.UUID);
      var ourBase32 = Base32Sortable.Encode(uuid);
      var medoId26 = sample.MedoId26;

      // Verify both are 26 characters
      Assert.Equal(26, ourBase32.Length);
      Assert.Equal(26, medoId26.Length);

      // Verify our output uses only alphabet characters
      foreach (char c in ourBase32)
      {
        Assert.Contains(c, "0123456789bcdefghjkmnpqrstuvwxyz");
      }
    }
  }
  [Fact]
  public void Structs_AllOnes_RoundTrip_Base32_36_64()
  {
    var b8 = new byte[8];
    var b16 = new byte[16];
    var b24 = new byte[24];
    for (int i = 0; i < b8.Length; i++) b8[i] = 0xFF;
    for (int i = 0; i < b16.Length; i++) b16[i] = 0xFF;
    for (int i = 0; i < b24.Length; i++) b24[i] = 0xFF;

    var s8 = SharedTestTypes.S08.DecodeFromBytes(b8);
    var s16 = SharedTestTypes.S16.DecodeFromBytes(b16);
    var s24 = SharedTestTypes.S24.DecodeFromBytes(b24);

    // Base36
    {
      var enc = Base36Sortable.Encode(s8);
      var dec = Base36Sortable.Decode<SharedTestTypes.S08>(enc, 64);
      Assert.Equal(s8, dec);
    }

    // Base32
    {
      var enc = Base32Sortable.Encode(s8);
      var dec = Base32Sortable.Decode<SharedTestTypes.S08>(enc, 64);
      Assert.Equal(s8, dec);
    }

    {
      var enc = Base32Sortable.Encode(s16);
      var dec = Base32Sortable.Decode<SharedTestTypes.S16>(enc, 128);
      Assert.Equal(s16, dec);
    }
    {
      var enc = Base32Sortable.Encode(s24);
      var dec = Base32Sortable.Decode<SharedTestTypes.S24>(enc, 192);
      Assert.Equal(s24, dec);
    }
  }

  [Fact]
  public void Base64_Structs_RoundTrip()
  {
    var rng = new Random(56789);
    for (int size = 1; size <= 64; size++)
    {
      for (int i = 0; i < 16; i++)
      {
        var randomInstance = SharedTestTypes.Random(size, rng);
        var type = Types[size - 1];
        var encodeMethod = GetEncodeMethod(typeof(Base64), type);
        var enc = encodeMethod.Invoke(null, new object[] { randomInstance })!;
        var decodeMethod = GetDecodeMethod(typeof(Base64), type, 1, typeof(string));
        var dec = decodeMethod.Invoke(null, new object[] { enc })!;
        Assert.Equal(randomInstance, dec);
      }
    }
  }

  private static MethodInfo GetEncodeMethod(Type classType, Type type) =>
    classType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
      .Where(m => m.Name == "Encode" && m.IsGenericMethod && m.GetParameters().Length == 1)
      .Single()!
      .MakeGenericMethod(type);

  private static MethodInfo GetDecodeMethod(Type classType, Type type, int paramCount, Type? paramType = null) =>
    classType.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
      .Where(m => m.Name == "Decode" && m.IsGenericMethod && m.GetParameters().Length == paramCount &&
                  (paramType == null || m.GetParameters()[0].ParameterType == paramType))
      .Single()!
      .MakeGenericMethod(type);

  private static readonly Type[] Types = SharedTestTypes.Types;
}
