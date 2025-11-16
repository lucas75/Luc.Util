using System;
using Xunit;

namespace Luc.Util.Tests;

public class UUIDTests
{
    [Fact]
    public void Constructor_ValidBytes_ShouldCreateUUID()
    {
        byte[] bytes = new byte[16];
        new Random().NextBytes(bytes);

        var uuid = new UUID(bytes);

        Assert.Equal(bytes, uuid.Bytes.ToArray());
    }

    [Fact]
    public void Constructor_InvalidBytes_ShouldThrowArgumentException()
    {
        byte[] bytes = new byte[15]; // Invalid length

        Assert.Throws<ArgumentException>(() => new UUID(bytes));
    }

    [Fact]
    public void ToString_ShouldReturnCorrectFormat()
    {
        byte[] bytes = new byte[16]
        {
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        };

        var uuid = new UUID(bytes);
        string expected = "12345678-90ab-cdef-1234-567890abcdef";

        Assert.Equal(expected, uuid.ToString());
    }

    [Fact]
    public void ToBase36_ShouldReturnCorrectFormat()
    {
        byte[] bytes = new byte[16]
        {
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        };

        var uuid = new UUID(bytes);
        string base36 = uuid.ToBase36();

        Assert.Equal(25, base36.Length);
    }

    [Fact]
    public void FromBase36_ShouldRecreateUUID()
    {
        byte[] bytes = new byte[16]
        {
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        };

        var uuid = new UUID(bytes);
        string base36 = uuid.ToBase36();
        var recreatedUuid = UUID.FromBase36(base36);

        Assert.Equal(uuid, recreatedUuid);
    }

    [Fact]
    public void ToBase25_ShouldReturnCorrectFormat()
    {
        byte[] bytes = new byte[16]
        {
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        };

        var uuid = new UUID(bytes);
        string base25 = uuid.ToBase25();

        Assert.Equal(28, base25.Length);
    }

    [Fact]
    public void FromBase25_ShouldRecreateUUID()
    {
        byte[] bytes = new byte[16]
        {
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        };

        var uuid = new UUID(bytes);
        string base25 = uuid.ToBase25();
        var recreatedUuid = UUID.FromBase25(base25);

        Assert.Equal(uuid, recreatedUuid);
    }

    [Fact]
    public void Equality_ShouldWorkCorrectly()
    {
        byte[] bytes = new byte[16];
        new Random().NextBytes(bytes);

        var uuid1 = new UUID(bytes);
        var uuid2 = new UUID(bytes);

        Assert.True(uuid1 == uuid2);
        Assert.False(uuid1 != uuid2);
        Assert.True(uuid1.Equals(uuid2));
    }

    [Fact]
    public void Comparison_ShouldWorkCorrectly()
    {
        byte[] bytes1 = new byte[16];
        byte[] bytes2 = new byte[16];
        new Random().NextBytes(bytes1);
        new Random().NextBytes(bytes2);

        var uuid1 = new UUID(bytes1);
        var uuid2 = new UUID(bytes2);

        int comparison = uuid1.CompareTo(uuid2);

        Assert.Equal(comparison < 0, uuid1.Bytes.SequenceCompareTo(uuid2.Bytes) < 0);
        Assert.Equal(comparison > 0, uuid1.Bytes.SequenceCompareTo(uuid2.Bytes) > 0);
    }
}
