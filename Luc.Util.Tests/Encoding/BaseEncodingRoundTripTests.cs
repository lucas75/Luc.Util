using System;
using Xunit;
using Luc.Util;
using Luc.Util.Tests.Samples;
using System.Linq;
using System.Reflection;
using Luc.Util.Encoding;

namespace Luc.Util.Tests.Encoding;

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
                string encoded = BaseEncodingUtils.InvokeEncode(typeof(Base36Sortable), (IEncodingInput)randomInstance);
                object? recreated = BaseEncodingUtils.InvokeDecodeWithLength(typeof(Base36Sortable), (IEncodingInput)randomInstance, encoded, size * 8);
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
                string encoded = BaseEncodingUtils.InvokeEncode(typeof(Base32Sortable), (IEncodingInput)randomInstance);
                object? recreated = BaseEncodingUtils.InvokeDecodeWithLength(typeof(Base32Sortable), (IEncodingInput)randomInstance, encoded, size * 8);
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
                string encStr = BaseEncodingUtils.InvokeEncode(typeof(Base64), (IEncodingInput)randomInstance);
                object? dec = BaseEncodingUtils.InvokeDecode(typeof(Base64), (IEncodingInput)randomInstance, encStr);
                Assert.Equal(randomInstance, dec);
            }
        }
    }
}