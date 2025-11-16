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
        byte[] bytes = new byte[16];
        Random.Shared.NextBytes(bytes);
        // Set version and variant bits for v4
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);

        var uuid = new UUID(bytes);
        Assert.Equal(bytes, uuid.Bytes.ToArray());
    }

    /// <summary>
    /// Tests that creating a UUID with invalid byte length throws an ArgumentException.
    /// </summary>
    [Fact]
    public void UUID_InvalidBytes_ShouldThrowArgumentException()
    {
        byte[] bytes = new byte[15]; // Invalid length
        Assert.Throws<ArgumentException>(() => new UUID(bytes));
    }

    /// <summary>
    /// Tests that the UUID string representation matches the expected format.
    /// </summary>
    [Fact]
    public void UUID_ToString_ShouldReturnCorrectFormat()
    {
        byte[] bytes =
        [
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        ];
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
        var uuid = new UUID(bytes);
        string expected = "12345678-90ab-4def-9234-567890abcdef";
        Assert.Equal(expected, uuid.ToString());
    }

    /// <summary>
    /// Tests that the Base36 encoding of a UUID returns a string of correct length.
    /// </summary>
    [Fact]
    public void UUID_ToBase36_ShouldReturnCorrectFormat()
    {
        byte[] bytes =
        [
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        ];
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
        var uuid = new UUID(bytes);
        string base36 = uuid.ToBase36();
        Assert.Equal(25, base36.Length);
    }

    /// <summary>
    /// Tests that a UUID can be recreated from its Base36 string representation.
    /// </summary>
    [Fact]
    public void UUID_FromBase36_ShouldRecreateUUID()
    {
        byte[] bytes =
        [
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        ];
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
        var uuid = new UUID(bytes);
        string base36 = uuid.ToBase36();
        var recreatedUuid = UUID.FromBase36(base36);
        Assert.Equal(uuid, recreatedUuid);
    }

    /// <summary>
    /// Tests that the Base25 encoding of a UUID returns a string of correct length.
    /// </summary>
    [Fact]
    public void UUID_ToBase25_ShouldReturnCorrectFormat()
    {
        byte[] bytes =
        [
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        ];
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
        var uuid = new UUID(bytes);
        string base25 = uuid.ToBase25();
        Assert.Equal(28, base25.Length);
    }

    /// <summary>
    /// Tests that a UUID can be recreated from its Base25 string representation.
    /// </summary>
    [Fact]
    public void UUID_FromBase25_ShouldRecreateUUID()
    {
        byte[] bytes =
        [
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        ];
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
        var uuid = new UUID(bytes);
        string base25 = uuid.ToBase25();
        var recreatedUuid = UUID.FromBase25(base25);
        Assert.Equal(uuid, recreatedUuid);
    }

    /// <summary>
    /// Tests equality and inequality operators for UUIDs.
    /// </summary>
    [Fact]
    public void UUID_Equality_ShouldWorkCorrectly()
    {
        byte[] bytes = new byte[16];
        Random.Shared.NextBytes(bytes);
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
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
        Random.Shared.NextBytes(bytes1);
        Random.Shared.NextBytes(bytes2);
        bytes1[6] = (byte)((bytes1[6] & 0x0F) | 0x40);
        bytes1[8] = (byte)((bytes1[8] & 0x3F) | 0x80);
        bytes2[6] = (byte)((bytes2[6] & 0x0F) | 0x40);
        bytes2[8] = (byte)((bytes2[8] & 0x3F) | 0x80);
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
    /// Tests that GetVariant returns the correct variant for various UUIDs.
    /// </summary>
    [Fact]
    public void UUID_GetVersionVariant_ShouldReturnEnum()
    {
        var uuidV4 = UUID.NewV4();
        Assert.Equal(UUID.UuidVariant.Rfc4122, uuidV4.GetVariant());

        var uuidV7 = UUID.NewV7();
        Assert.Equal(UUID.UuidVariant.Rfc4122, uuidV7.GetVariant());

        // Manually create a UUID with variant bits 0xC0
        byte[] bytes = new byte[16];
        Random.Shared.NextBytes(bytes);
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0xC0);
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
            var guid = Guid.Parse(sample.UUID);
            var uuid = guid.AsUUID();
            var base36 = uuid.ToBase36();
            var recreated = UUID.FromBase36(base36);
            Assert.Equal(uuid, recreated);
        }
    }

    /// <summary>
    /// Tests round-trip conversion of UUIDv7 samples to Base25 and back.
    /// </summary>
    [Fact]
    public void Uuid7Samples_ToBase25_And_FromBase25_ShouldRoundTrip()
    {
        foreach (var sample in Uuid7TestSamples.Uuids)
        {
            var guid = Guid.Parse(sample.UUID);
            var uuid = guid.AsUUID();
            var base25 = uuid.ToBase25();
            var recreated = UUID.FromBase25(base25);
            Assert.Equal(uuid, recreated);
        }
    }
}
