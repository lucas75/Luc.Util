using System;
using Xunit;
using Luc.Util;
using Luc.Util.Tests.Samples;
using System.Linq;
using Luc.Util.Encoding;

namespace Luc.Util.Tests.Encoding;

#pragma warning disable CS8601

public class BaseEncodingRoundTripTests
{
    private static readonly Type[] Types = SharedTestTypes.Types;

    [Fact]
    public void Base36_AllSizes_RoundTrip()
    {
        var rng = new Random(12345);
        for (int size = 1; size <= 64; size++)
        {
            for (int i = 0; i < 8; i++)
            {
                var randomInstance = SharedTestTypes.Random(size, rng);
                string encoded = Base36Sortable.Encode((IEncodingInput)randomInstance);
                var type = Types[size - 1];
                var decodeMethod = typeof(Base36Sortable).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(m => m.Name == "Decode" && m.IsGenericMethod && m.GetParameters().Length == 2).First()!.MakeGenericMethod(type);
                var recreated = decodeMethod.Invoke(null, new object[] { encoded, size * 8 });
                Assert.Equal(randomInstance, recreated);
                Assert.Equal((int)Math.Ceiling(size * 8 / Math.Log2(36)), encoded.Length);
            }
        }
    }

    [Fact]
    public void Base32_AllSizes_RoundTrip()
    {
        var rng = new Random(12346);
        for (int size = 1; size <= 64; size++)
        {
            for (int i = 0; i < 8; i++)
            {
                var randomInstance = SharedTestTypes.Random(size, rng);
                string encoded = Base32Sortable.Encode((IEncodingInput)randomInstance);
                var type = Types[size - 1];
                var decodeMethod = typeof(Base32Sortable).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(m => m.Name == "Decode" && m.IsGenericMethod && m.GetParameters().Length == 2).First()!.MakeGenericMethod(type);
                var recreated = decodeMethod.Invoke(null, new object[] { encoded, size * 8 });
                Assert.Equal(randomInstance, recreated);
                Assert.Equal((int)Math.Ceiling(size * 8 / Math.Log2(32)), encoded.Length);
            }
        }
    }

    [Fact]
    public void Base64_AllSizes_RoundTrip()
    {
        var rng = new Random(12347);
        for (int size = 1; size <= 64; size++)
        {
            for (int i = 0; i < 8; i++)
            {
                var randomInstance = SharedTestTypes.Random(size, rng);
                var type = Types[size - 1];
                var encodeMethod = typeof(Base64).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(m => m.Name == "Encode" && m.IsGenericMethod && m.GetParameters().Length == 1).First()!.MakeGenericMethod(type);
                var enc = encodeMethod.Invoke(null, new object[] { randomInstance });
                var decodeMethod = typeof(Base64).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(m => m.Name == "Decode" && m.IsGenericMethod && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(string)).First()!.MakeGenericMethod(type);
                var dec = decodeMethod.Invoke(null, new object[] { enc });
                Assert.Equal(randomInstance, dec);
            }
        }
    }
}
#pragma warning restore CS8601