using System;
using Luc.Util.Tests.Samples;
using Luc.Util;
using Luc.Util.UUID;
using Xunit;
using Luc.Util.Encoding;


namespace Luc.Util.Tests;

public class UUIDTests
{
    /// <summary>
    /// Tests that a UUID can be created from valid 16 bytes and matches the input bytes.
    /// </summary>
    [Fact]
    public void UUID_ValidBytes_ShouldCreateUUID()
    {
        // Run a few deterministic random buffers to ensure constructor copies the bytes correctly.
        var rng = new Random(123456);
        for (int i = 0; i < 10; i++)
        {
            byte[] bytes = new byte[16];
            rng.NextBytes(bytes);

            // No need to force version/variant here; the purpose of this test is
            // to verify the UUID constructor preserves the provided bytes.
            var uuid = new Uuid(bytes);
            Assert.Equal(bytes, uuid.Bytes.ToArray());
        }
    }

    /// <summary>
    /// Ensures the constructor copies bytes passed by ReadOnlySpan and is immune
    /// to later modifications of the original array.
    /// </summary>
    [Fact]
    public void UUID_ReadOnlySpan_ShouldCreateUUIDAndPreserveBytes()
    {
        var rng = new Random(112233);

        for (int i = 0; i < 10; i++)
        {
            byte[] bytes = new byte[16];
            rng.NextBytes(bytes);
            var original = (byte[])bytes.Clone();

            var uuid = new Uuid(bytes.AsSpan());

            // Mutate the source after the constructor — constructor should copy
            // so uuid.Bytes remains equal to the original contents.
            bytes[0] = (byte)(bytes[0] + 1);

            Assert.Equal(original, uuid.Bytes.ToArray());
        }
    }

    /// <summary>
    /// Tests that creating a UUID with invalid byte length throws an ArgumentException.
    /// </summary>
    [Fact]
    public void UUID_InvalidBytes_ShouldThrowArgumentException()
    {
        // Try many different lengths (including 0) and assert all invalid lengths throw.
        var rng = new Random(654321);
        for (int i = 0; i < 100; i++)
        {
            int length = rng.Next(0, 33); // test 0..32
            if (length == 16) continue;

            var bytes = new byte[length];
            Random.Shared.NextBytes(bytes);
            Assert.Throws<ArgumentException>(() => new Uuid(bytes));
        }
    }

    /// <summary>
    /// Tests that the UUID string representation matches the expected format.
    /// </summary>
    [Fact]
    public void UUID_ToString_ShouldReturnCorrectFormat()
    {
        foreach (var sample in UuidTestSamples.Samples)
        {
            var uuid = Uuid.Parse(sample.Canonical);
            Assert.Equal(sample.Canonical, uuid.ToString());
        }
    }




    // Base32 tests are defined below; we removed Base31 references earlier.



    /// <summary>
    /// Tests equality and inequality operators for UUIDs.
    /// </summary>
    [Fact]
    public void UUID_Equality_ShouldWorkCorrectly()
    {
        byte[] bytes = new byte[16];
        Random.Shared.NextBytes(bytes);

        // Equality compares raw bytes; no need to set version/variant for this test.

        var uuid1 = new Uuid(bytes);
        var uuid2 = new Uuid(bytes);
        Assert.Equal(uuid1, uuid2);
        Assert.True(uuid1 == uuid2);
        Assert.False(uuid1 != uuid2);
    }

    /// <summary>
    /// Tests the comparison logic between two UUIDs.
    /// </summary>
    [Fact]
    public void UUID_Comparison_ShouldWorkCorrectly()
    {
        byte[] bytes1 = new byte[16];
        byte[] bytes2 = new byte[16];
        // Use deterministic RNG so this test is repeatable
        var rng = new Random(987654);
        rng.NextBytes(bytes1);
        rng.NextBytes(bytes2);
        
        // The comparison relies on byte ordering. No need to set versions/variants.
        
        var uuid1 = new Uuid(bytes1);
        var uuid2 = new Uuid(bytes2);
        int comparison = uuid1.CompareTo(uuid2);
        Assert.Equal(comparison < 0, uuid1.Bytes.SequenceCompareTo(uuid2.Bytes) < 0);
        Assert.Equal(comparison > 0, uuid1.Bytes.SequenceCompareTo(uuid2.Bytes) > 0);
    }

    /// <summary>
    /// Tests that a newly generated UUIDv4 has correct version and variant bits.
    /// </summary>
    [Fact]
    public void UUID_NewV4_ShouldCreateValidV4()
    {
        var uuid = Uuid.NewV4();
        var bytes = uuid.Bytes.ToArray();
        
        // Check version bits (should be 0x4X)
        Assert.Equal(0x40, bytes[6] & 0xF0);
        // Check variant bits (should be 0x8X, 0x9X, 0xAX, or 0xBX)
        Assert.Equal(0x80, bytes[8] & 0xC0);
    }

    /// <summary>
    /// Tests that GetVersion returns the correct version for various UUIDs.
    /// </summary>
    [Fact]
    public void UUID_GetVersion_ShouldReturnCorrectValue()
    {
        var uuidV4 = Uuid.NewV4();
        Assert.Equal(4, uuidV4.GetVersion());

        var uuidV7 = Uuid.NewV7();
        Assert.Equal(7, uuidV7.GetVersion());

        // Create custom uuid with version 1 in byte[6]
        byte[] bytes = new byte[16];
        Random.Shared.NextBytes(bytes);
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x10); // version 1
        var uuidV1 = new Uuid(bytes);
        Assert.Equal(1, uuidV1.GetVersion());
    }

    /// <summary>
    /// Ensure known Type-4 samples contain the version nibble and report version 4.
    /// </summary>
    [Fact]
    public void UUID_SamplesType4_ShouldBeVersion4()
    {
        foreach (var sample in UuidTestSamples.SamplesType4)
        {
            // Version nibble is the first char of the 3rd hyphen-delimited section.
            Assert.Equal('4', sample.Canonical[14]);
            var uuid = Uuid.Parse(sample.Canonical);
            Assert.Equal(4, uuid.GetVersion());
        }
    }

    /// <summary>
    /// Assert we can parse canonical strings that contain unusual variant nibble values.
    /// We also validate that those values appear in the expected canonical position.
    /// </summary>
    [Fact]
    public void UUID_SamplesVariantAbc_ShouldParseAndPreserveVariantNibble()
    {
        foreach (var sample in UuidTestSamples.SamplesVariantAbc)
        {
            // Variant nibble is the first char of the 4th hyphen-delimited section.
            var variantNibble = sample.Canonical[19];
            Assert.Contains(variantNibble, "abc");

            var uuid = Uuid.Parse(sample.Canonical);
            Assert.Equal(sample.Canonical, uuid.ToString());
        }
    }

    /// <summary>
    /// The malformed canonical samples should not round-trip or parse.
    /// </summary>
    [Fact]
    public void UUID_SamplesWithBrokenCanonical_ShouldNotParse()
    {
        foreach (var bad in UuidTestSamples.SamplesWithBrokenCanonical)
        {
            Assert.False(Uuid.TryParse(bad, out _));
            Assert.Throws<FormatException>(() => Uuid.Parse(bad));
        }
    }

    /// <summary>
    /// Ensure invalid hex strings fail `Convert.FromHexString` since they are not valid hex.
    /// </summary>
    [Fact]
    public void UuidSamplesWithBrokenHex_ShouldNotConvertFromHex()
    {
        foreach (var hex in UuidTestSamples.SamplesWithBrokenHex)
        {
            Assert.Throws<FormatException>(() => Convert.FromHexString(hex));
            Assert.False(Uuid.TryParse(hex, out _));
        }
    }

    /// <summary>
    /// Tests that GetVariant returns the correct variant for various UUIDs.
    /// </summary>
    [Fact]
    public void UUID_GetVersionVariant_ShouldReturnEnum()
    {
        var uuidV4 = Uuid.NewV4();
        Assert.Equal(Uuid.UuidVariant.Rfc4122, uuidV4.GetVariant());

        var uuidV7 = Uuid.NewV7();
        Assert.Equal(Uuid.UuidVariant.Rfc4122, uuidV7.GetVariant());

        // Manually create a UUID with variant bits 0xC0 (Microsoft variant needs 0b1110xxxx = 0xE0)
        byte[] bytes = new byte[16];
        Random.Shared.NextBytes(bytes);
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x1F) | 0xC0);
        var custom = new Uuid(bytes);
        Assert.Equal(Uuid.UuidVariant.Microsoft, custom.GetVariant());
    }

    /// <summary>
    /// Tests that a newly generated UUIDv7 has correct version and variant bits.
    /// </summary>
    [Fact]
    public void UUID_NewV7_ShouldCreateValidV7()
    {
        var uuid = Uuid.NewV7();
        var bytes = uuid.Bytes.ToArray();
        
        // Check version bits (should be 0x7X)
        Assert.Equal(0x70, bytes[6] & 0xF0);
        // Check variant bits (should be 0x8X, 0x9X, 0xAX, or 0xBX)
        Assert.Equal(0x80, bytes[8] & 0xC0);
    }

    /// <summary>
    /// Tests that the timestamp extracted from a UUIDv7 is within the expected range.
    /// </summary>
    [Fact]
    public void UUID_V7GetTimestamp_ShouldReturnValidTimestamp()
    {
        var before = DateTimeOffset.UtcNow;
        var uuid = Uuid.NewV7();
        var after = DateTimeOffset.UtcNow;
        
        var timestamp = uuid.V7GetTimestamp();
        
        Assert.True(timestamp >= before.AddMilliseconds(-1));
        Assert.True(timestamp <= after.AddMilliseconds(1));
    }



    /// <summary>
    /// Tests differences between custom encodings and Medo's Id26 (Base31 removed).
    /// </summary>
    
    // This alphabet comparison moved to `BaseEncodingCornerCasesTests`.



    // BaseXX string/roundtrip tests are covered by the test suite in
    // `Luc.Util.Tests.Encoding.BaseEncodingRoundTripTests` and
    // `Luc.Util.Tests.Encoding.BaseEncodingCornerCasesTests` which exercise
    // all sizes including the 16-byte UUID size (equivalent to S16).
    //
    // We keep only tests here that check UUID-specific integrations or compare
    // with external Medo id formats (MedoId22/25/26).  The general encode/decode
    // and length behavior is validated by the shared tests in `Encoding`.

}
