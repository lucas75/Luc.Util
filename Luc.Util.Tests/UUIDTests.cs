using System;
using Luc.Util.Tests.Generated;
using Xunit;

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
            var uuid = new UUID(bytes);
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

            var uuid = new UUID(bytes.AsSpan());

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
            Assert.Throws<ArgumentException>(() => new UUID(bytes));
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
            var uuid = UUID.Parse(sample.Canonical);
            Assert.Equal(sample.Canonical, uuid.ToString());
        }
    }

    /// <summary>
    /// Tests that the Base36 encoding of a UUID returns a string of correct length.
    /// </summary>
    [Fact]
    public void UUID_ToBase36_ShouldReturnCorrectFormat()
    {
        foreach (var sample in UuidTestSamples.Samples)
        {
            var uuid = UUID.Parse(sample.Canonical);
            string base36 = uuid.ToBase36();
            Assert.Equal(25, base36.Length);
        }
    }

    /// <summary>
    /// Tests that a UUID can be recreated from its Base36 string representation.
    /// </summary>
    [Fact]
    public void UUID_FromBase36_ShouldRecreateUUID()
    {
        foreach (var sample in UuidTestSamples.Samples)
        {
            var uuid = UUID.Parse(sample.Canonical);
            string base36 = uuid.ToBase36();
            var recreatedUuid = UUID.FromBase36(base36);
            Assert.Equal(uuid, recreatedUuid);
        }
    }

    /// <summary>
    /// Tests that the Base31 encoding of a UUID returns a string of correct length.
    /// </summary>
    [Fact]
    public void UUID_ToBase31_ShouldReturnCorrectFormat()
    {
        foreach (var sample in UuidTestSamples.Samples)
        {
            var uuid = UUID.Parse(sample.Canonical);
            string base31 = uuid.ToBase31();
            Assert.Equal(26, base31.Length);
        }
    }

    /// <summary>
    /// Tests that a UUID can be recreated from its Base31 string representation.
    /// </summary>
    [Fact]
    public void UUID_FromBase31_ShouldRecreateUUID()
    {
        foreach (var sample in UuidTestSamples.Samples)
        {
            var uuid = UUID.Parse(sample.Canonical);
            string base31 = uuid.ToBase31();
            var recreatedUuid = UUID.FromBase31(base31);
            Assert.Equal(uuid, recreatedUuid);
        }
    }

    /// <summary>
    /// Tests that the Base32_MedoId26 encoding of a UUID returns a string of correct length.
    /// </summary>
    [Fact]
    public void UUID_ToBase32_ShouldReturnCorrectFormat()
    {
        foreach (var sample in UuidTestSamples.Samples)
        {
            var uuid = UUID.Parse(sample.Canonical);
            string base32 = uuid.ToBase32();
            Assert.Equal(26, base32.Length);
        }
    }

    /// <summary>
    /// Tests that a UUID can be recreated from its Base32_MedoId26 string representation.
    /// </summary>
    [Fact]
    public void UUID_FromBase32_ShouldRecreateUUID()
    {
        foreach (var sample in UuidTestSamples.Samples)
        {
            var uuid = UUID.Parse(sample.Canonical);
            string base32 = uuid.ToBase32();
            var recreatedUuid = UUID.FromBase32(base32);
            Assert.Equal(uuid, recreatedUuid);
        }
    }

    /// <summary>
    /// Tests equality and inequality operators for UUIDs.
    /// </summary>
    [Fact]
    public void UUID_Equality_ShouldWorkCorrectly()
    {
        byte[] bytes = new byte[16];
        Random.Shared.NextBytes(bytes);

        // Equality compares raw bytes; no need to set version/variant for this test.

        var uuid1 = new UUID(bytes);
        var uuid2 = new UUID(bytes);
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
        
        var uuid1 = new UUID(bytes1);
        var uuid2 = new UUID(bytes2);
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
        var uuid = UUID.NewV4();
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
        var uuidV4 = UUID.NewV4();
        Assert.Equal(4, uuidV4.GetVersion());

        var uuidV7 = UUID.NewV7();
        Assert.Equal(7, uuidV7.GetVersion());

        // Create custom uuid with version 1 in byte[6]
        byte[] bytes = new byte[16];
        Random.Shared.NextBytes(bytes);
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x10); // version 1
        var uuidV1 = new UUID(bytes);
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
            var uuid = UUID.Parse(sample.Canonical);
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

            var uuid = UUID.Parse(sample.Canonical);
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
            Assert.False(UUID.TryParse(bad, out _));
            Assert.Throws<FormatException>(() => UUID.Parse(bad));
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
            Assert.False(UUID.TryParse(hex, out _));
        }
    }

    /// <summary>
    /// Tests that GetVariant returns the correct variant for various UUIDs.
    /// </summary>
    [Fact]
    public void UUID_GetVersionVariant_ShouldReturnEnum()
    {
        var uuidV4 = UUID.NewV4();
        Assert.Equal(UUID.UuidVariant.Rfc4122, uuidV4.GetVariant());

        var uuidV7 = UUID.NewV7();
        Assert.Equal(UUID.UuidVariant.Rfc4122, uuidV7.GetVariant());

        // Manually create a UUID with variant bits 0xC0 (Microsoft variant needs 0b1110xxxx = 0xE0)
        byte[] bytes = new byte[16];
        Random.Shared.NextBytes(bytes);
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x1F) | 0xC0);
        var custom = new UUID(bytes);
        Assert.Equal(UUID.UuidVariant.Microsoft, custom.GetVariant());
    }

    /// <summary>
    /// Tests that a newly generated UUIDv7 has correct version and variant bits.
    /// </summary>
    [Fact]
    public void UUID_NewV7_ShouldCreateValidV7()
    {
        var uuid = UUID.NewV7();
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
        var uuid = UUID.NewV7();
        var after = DateTimeOffset.UtcNow;
        
        var timestamp = uuid.V7GetTimestamp();
        
        Assert.True(timestamp >= before.AddMilliseconds(-1));
        Assert.True(timestamp <= after.AddMilliseconds(1));
    }

    /// <summary>
    /// Tests round-trip conversion of UUIDv7 samples to Base36 and back.
    /// </summary>
    [Fact]
    public void Uuid7Samples_ToBase36_And_FromBase36_ShouldRoundTrip()
    {
        foreach (var sample in Uuid7TestSamples.Uuids)
        {
            var uuid = UUID.Parse(sample.UUID);
            var base36 = uuid.ToBase36();
            var recreated = UUID.FromBase36(base36);
            Assert.Equal(uuid, recreated);
        }
    }

    /// <summary>
    /// Tests round-trip conversion of UUIDv7 samples to Base31 and back.
    /// </summary>
    [Fact]
    public void Uuid7Samples_ToBase31_And_FromBase31_ShouldRoundTrip()
    {
        foreach (var sample in Uuid7TestSamples.Uuids)
        {
            var uuid = UUID.Parse(sample.UUID);
            var base31 = uuid.ToBase31();
            var recreated = UUID.FromBase31(base31);
            Assert.Equal(uuid, recreated);
        }
    }

    /// <summary>
    /// Tests that our Base31 encoding differs from Medo's Id26 (they use different alphabets).
    /// </summary>
    [Fact]
    public void UUID_Base31_DiffersFromMedoId26()
    {
        // Our Base31 uses alphabet: 23456789abcdefghjkmnpqrstuvwxyz (31 chars)
        // Medo Id26 uses alphabet: 0123456789bcdefghjkmnpqrstuvwxyz (32 chars)
        // Both produce 26-character strings but with different encodings
        
        var uuid = UUID.Parse(Uuid7TestSamples.Uuids[0].UUID);
        var base31 = uuid.ToBase31();
        var medoId26 = Uuid7TestSamples.Uuids[0].MedoId26;
        
        Assert.Equal(26, base31.Length);
        Assert.Equal(26, medoId26.Length);
        Assert.NotEqual(base31, medoId26); // Different alphabets = different output
    }

    /// <summary>
    /// Tests that Base32_MedoId26 uses the same alphabet as Medo's Id26 but produces different encodings.
    /// While both use the same 32-character alphabet (0-9, b-z excluding a,i,l,o), the encoding
    /// algorithm differs (BigInteger-based vs Medo's implementation).
    /// </summary>
    [Fact]
    public void Uuid7Samples_ToBase32_UsesSameAlphabetButDiffersFromMedoId26()
    {
        foreach (var sample in Uuid7TestSamples.Uuids)
        {
            var uuid = UUID.Parse(sample.UUID);
            var ourBase32 = uuid.ToBase32();
            var medoId26 = sample.MedoId26;
            
            // Verify both are 26 characters
            Assert.Equal(26, ourBase32.Length);
            Assert.Equal(26, medoId26.Length);
            
            // Verify our output uses only alphabet characters
            foreach (char c in ourBase32)
            {
                Assert.Contains(c, "0123456789bcdefghjkmnpqrstuvwxyz");
            }
            
            // Verify the encodings differ (different algorithms)
            Assert.NotEqual(medoId26, ourBase32);
        }
    }

    /// <summary>
    /// Tests round-trip conversion of UUIDv7 samples to Base32_MedoId26 and back.
    /// </summary>
    [Fact]
    public void Uuid7Samples_ToBase32_And_FromBase32_ShouldRoundTrip()
    {
        foreach (var sample in Uuid7TestSamples.Uuids)
        {
            var uuid = UUID.Parse(sample.UUID);
            var base32 = uuid.ToBase32();
            var recreated = UUID.FromBase32(base32);
            Assert.Equal(uuid, recreated);
        }
    }

    /// <summary>
    /// Tests that the Base35_MedoId25 encoding of a UUID returns a string of correct length.
    /// </summary>
    [Fact]
    public void UUID_ToBase35_ShouldReturnCorrectFormat()
    {
        foreach (var sample in UuidTestSamples.Samples)
        {
            var uuid = UUID.Parse(sample.Canonical);
            string base35 = uuid.ToBase35();
            Assert.Equal(25, base35.Length);
            // Verify no 'l' character
            Assert.DoesNotContain('l', base35);
        }
    }

    /// <summary>
    /// Tests that a UUID can be recreated from its Base35_MedoId25 string representation.
    /// </summary>
    [Fact]
    public void UUID_FromBase35_MedoId25_ShouldRecreateUUID()
    {
        foreach (var sample in UuidTestSamples.Samples)
        {
            var uuid = UUID.Parse(sample.Canonical);
            string base35 = uuid.ToBase35();
            var recreatedUuid = UUID.FromBase35_MedoId25(base35);
            Assert.Equal(uuid, recreatedUuid);
        }
    }

    /// <summary>
    /// Tests that the Base64 encoding of a UUID returns a string of correct length.
    /// </summary>
    [Fact]
    public void UUID_ToBase64_ShouldReturnCorrectFormat()
    {
        foreach (var sample in UuidTestSamples.Samples)
        {
            var uuid = UUID.Parse(sample.Canonical);
            string base64 = uuid.ToBase64();
            Assert.Equal(22, base64.Length);
            Assert.DoesNotContain('+', base64);
            Assert.DoesNotContain('/', base64);
            Assert.DoesNotContain('=', base64);
        }
    }

    /// <summary>
    /// Tests that our Base35_MedoId25 encoding matches Medo's Id25 format from test samples.
    /// </summary>
    [Fact]
    public void Uuid7Samples_ToBase35_MatchesMedoId25()
    {
        foreach (var sample in Uuid7TestSamples.Uuids)
        {
            var uuid = UUID.Parse(sample.UUID);
            var ourBase35 = uuid.ToBase35();
            var medoId25 = sample.MedoId25;
            
            // Verify our encoding matches Medo's Id25 exactly
            Assert.Equal(25, ourBase35.Length);
            Assert.Equal(medoId25, ourBase35);
        }
    }

    /// <summary>
    /// Tests round-trip conversion of UUIDv7 samples to Base35_MedoId25 and back.
    /// </summary>
    [Fact]
    public void Uuid7Samples_ToBase35_And_FromBase35_MedoId25_ShouldRoundTrip()
    {
        foreach (var sample in Uuid7TestSamples.Uuids)
        {
            var uuid = UUID.Parse(sample.UUID);
            var base35 = uuid.ToBase35();
            var recreated = UUID.FromBase35_MedoId25(base35);
            Assert.Equal(uuid, recreated);
        }
    }

    /// <summary>
    /// Tests that a UUID can be recreated from its Base64 string representation.
    /// </summary>
    [Fact]
    public void UUID_FromBase64_ShouldRecreateUUID()
    {
        foreach (var sample in UuidTestSamples.Samples)
        {
            var uuid = UUID.Parse(sample.Canonical);
            string base64 = uuid.ToBase64();
            var recreatedUuid = UUID.FromBase64(base64);
            Assert.Equal(uuid, recreatedUuid);
        }
    }

    /// <summary>
    /// Tests that Base32_MedoId26 cannot decode Medo's Id26 strings due to different encoding algorithms.
    /// While both use the same alphabet, the encoding differs.
    /// </summary>
    [Fact]
    public void Uuid7Samples_MedoId26_CannotBeDecoded()
    {
        foreach (var sample in Uuid7TestSamples.Uuids)
        {
            var expectedUuid = UUID.Parse(sample.UUID);
            
            // Decode Medo's Id26 using our method - should produce different UUID
            var decodedUuid = UUID.FromBase32(sample.MedoId26);
            
            // Verify decoded UUID differs (different encoding algorithm)
            Assert.NotEqual(expectedUuid, decodedUuid);
        }
    }

    /// <summary>
    /// Tests that V7GetTimestamp extracts a timestamp that falls between TS1 and TS2 from test samples.
    /// Note: UUIDv7 only stores millisecond precision, so we allow 1ms tolerance for comparison.
    /// </summary>
    [Fact]
    public void Uuid7Samples_V7GetTimestamp_ShouldBeBetweenTS1AndTS2()
    {
        foreach (var sample in Uuid7TestSamples.Uuids)
        {
            var uuid = UUID.Parse(sample.UUID);
            
            var extractedTimestamp = uuid.V7GetTimestamp();
            
            // Convert to UTC for comparison
            var ts1Utc = sample.TS1.ToUniversalTime();
            var ts2Utc = sample.TS2.ToUniversalTime();
            
            // UUIDv7 stores milliseconds, so extracted timestamp may be truncated
            // Allow 1ms tolerance: timestamp should be >= (TS1 - 1ms) and <= TS2
            Assert.True(extractedTimestamp >= ts1Utc.AddMilliseconds(-1), 
                $"Timestamp {extractedTimestamp:O} should be >= TS1-1ms {ts1Utc.AddMilliseconds(-1):O}");
            Assert.True(extractedTimestamp <= ts2Utc, 
                $"Timestamp {extractedTimestamp:O} should be <= TS2 {ts2Utc:O}");
        }
    }

    /// <summary>
    /// Tests round-trip conversion of UUIDv7 samples to Base64 and back.
    /// </summary>
    [Fact]
    public void Uuid7Samples_ToBase64_And_FromBase64_ShouldRoundTrip()
    {
        foreach (var sample in Uuid7TestSamples.Uuids)
        {
            var uuid = UUID.Parse(sample.UUID);
            var base64 = uuid.ToBase64();
            var recreated = UUID.FromBase64(base64);
            Assert.Equal(uuid, recreated);
        }
    }

    [Fact]
    public void UUID_ParseAndTryParse_CanonicalAndAllEncodings()
    {
        foreach (var sample in UuidTestSamples.Samples)
        {
            var uuid = UUID.Parse(sample.Canonical);

            // Canonical (with hyphens)
            string canonical = uuid.ToString();
            
            Assert.Equal(uuid, UUID.Parse(canonical));
            Assert.True(UUID.TryParse(canonical, out var parsedCanonical));
            Assert.Equal(uuid, parsedCanonical);

        // Plain hex 32 chars
        var hex = BitConverter.ToString(uuid.Bytes.ToArray()).Replace("-", "").ToLowerInvariant();
        Assert.Equal(32, hex.Length);
        // Sanity check: Guid.ParseExact should accept 32-digit hex
        var guidViaGuid = Guid.ParseExact(hex, "N");
        var uuidViaGuid = guidViaGuid.AsUUID();
        Assert.Equal(uuid, uuidViaGuid);
        // Debug: ensure hex contains only hex characters
        foreach (char c in hex) Assert.True((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f'));
        

        // Debug output
        
        // We only allow TryParse for canonical form. Use `FromBaseXX` helpers for encoded forms.
        Assert.False(UUID.TryParse(hex, out _));

        // Try span-based parsing for canonical and hex
        Assert.True(UUID.TryParse(canonical.AsSpan(), out var parsedCanonicalSpan));
        Assert.Equal(uuid, parsedCanonicalSpan);
        // Non-canonical hex should not parse with TryParse (only canonical supported)
        Assert.False(UUID.TryParse(hex.AsSpan(), out _));

        // Base64
        var base64 = uuid.ToBase64();
        // Use FromBase64 for non-canonical encoded forms
        // TryParse only accepts canonical form: encoded formats should not parse
        Assert.False(UUID.TryParse(base64, out _));

        // Base36
        var base36 = uuid.ToBase36();
        // Use FromBase36 for non-canonical encoded forms
        Assert.False(UUID.TryParse(base36, out _));

        // Base31
        var base31 = uuid.ToBase31();
        // Use FromBase31 for non-canonical encoded forms
        Assert.False(UUID.TryParse(base31, out _));

        // Base32
        var base32 = uuid.ToBase32();
        // Use FromBase32 for non-canonical encoded forms
        Assert.False(UUID.TryParse(base32, out _));

        // Base35 (Medo Id25)
        var base35 = uuid.ToBase35();
        // Use FromBase35_MedoId25 for non-canonical encoded forms
        Assert.False(UUID.TryParse(base35, out _));

            // Negative - invalid format
            Assert.False(UUID.TryParse("not-a-uuid", out _));
        }
    }
}
