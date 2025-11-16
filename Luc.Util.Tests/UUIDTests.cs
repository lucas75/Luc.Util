using System;
using Luc.Util.Tests.Generated;
using Xunit;

namespace Luc.Util.Tests;

public class UUIDTests
{

    [Fact]
    public void UUIDv4_ValidBytes_ShouldCreateUUIDv4()
    {
        byte[] bytes = new byte[16];
        Random.Shared.NextBytes(bytes);
        // Set version and variant bits for v4
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);

        var uuid = new UUIDv4(bytes);
        Assert.Equal(bytes, uuid.Bytes.ToArray());
    }


    [Fact]
    public void UUIDv4_InvalidBytes_ShouldThrowArgumentException()
    {
        byte[] bytes = new byte[15]; // Invalid length
        Assert.Throws<ArgumentException>(() => new UUIDv4(bytes));
    }


    [Fact]
    public void UUIDv4_ToString_ShouldReturnCorrectFormat()
    {
        byte[] bytes =
        [
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        ];
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
        var uuid = new UUIDv4(bytes);
        string expected = "12345678-90ab-4def-9234-567890abcdef";
        Assert.Equal(expected, uuid.ToString());
    }


    [Fact]
    public void UUIDv4_ToBase36_ShouldReturnCorrectFormat()
    {
        byte[] bytes =
        [
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        ];
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
        var uuid = new UUIDv4(bytes);
        string base36 = uuid.ToBase36();
        Assert.Equal(25, base36.Length);
    }


    [Fact]
    public void UUIDv4_FromBase36_ShouldRecreateUUIDv4()
    {
        byte[] bytes =
        [
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        ];
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
        var uuid = new UUIDv4(bytes);
        string base36 = uuid.ToBase36();
        var recreatedRecord = UUIDRecord.FromBase36(base36);
        var recreatedUuid = new UUIDv4(recreatedRecord.Bytes);
        if (!uuid.Equals(recreatedUuid))
        {
            Console.WriteLine($"Original:   {BitConverter.ToString(uuid.Bytes.ToArray())}");
            Console.WriteLine($"Recreated:  {BitConverter.ToString(recreatedUuid.Bytes.ToArray())}");
        }
        Assert.Equal(uuid, recreatedUuid);
    }


    [Fact]
    public void UUIDv4_ToBase25_ShouldReturnCorrectFormat()
    {
        byte[] bytes =
        [
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        ];
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
        var uuid = new UUIDv4(bytes);
        string base25 = uuid.ToBase25();
        Assert.Equal(28, base25.Length);
    }


    [Fact]
    public void UUIDv4_FromBase25_ShouldRecreateUUIDv4()
    {
        byte[] bytes =
        [
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
        ];
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
        var uuid = new UUIDv4(bytes);
        string base25 = uuid.ToBase25();
        var recreatedRecord = UUIDRecord.FromBase25(base25);
        var recreatedUuid = new UUIDv4(recreatedRecord.Bytes);
        if (!uuid.Equals(recreatedUuid))
        {
            Console.WriteLine($"Original:   {BitConverter.ToString(uuid.Bytes.ToArray())}");
            Console.WriteLine($"Recreated:  {BitConverter.ToString(recreatedUuid.Bytes.ToArray())}");
        }
        Assert.Equal(uuid, recreatedUuid);
    }


    [Fact]
    public void UUIDv4_Equality_ShouldWorkCorrectly()
    {
        byte[] bytes = new byte[16];
        Random.Shared.NextBytes(bytes);
        bytes[6] = (byte)((bytes[6] & 0x0F) | 0x40);
        bytes[8] = (byte)((bytes[8] & 0x3F) | 0x80);
        var uuid1 = new UUIDv4(bytes);
        var uuid2 = new UUIDv4(bytes);
            Assert.Equal(uuid1.ToString(), uuid2.ToString());
    }


    [Fact]
    public void UUIDv4_Comparison_ShouldWorkCorrectly()
    {
        byte[] bytes1 = new byte[16];
        byte[] bytes2 = new byte[16];
        Random.Shared.NextBytes(bytes1);
        Random.Shared.NextBytes(bytes2);
        bytes1[6] = (byte)((bytes1[6] & 0x0F) | 0x40);
        bytes1[8] = (byte)((bytes1[8] & 0x3F) | 0x80);
        bytes2[6] = (byte)((bytes2[6] & 0x0F) | 0x40);
        bytes2[8] = (byte)((bytes2[8] & 0x3F) | 0x80);
        var uuid1 = new UUIDv4(bytes1);
        var uuid2 = new UUIDv4(bytes2);
        int comparison = uuid1.Bytes.SequenceCompareTo(uuid2.Bytes);
        Assert.Equal(comparison < 0, uuid1.Bytes.SequenceCompareTo(uuid2.Bytes) < 0);
        Assert.Equal(comparison > 0, uuid1.Bytes.SequenceCompareTo(uuid2.Bytes) > 0);
    }


    [Fact]
    public void Uuid7Samples_ToBase36_And_FromBase36_ShouldRoundTrip()
    {
        foreach (var sample in Uuid7TestSamples.Uuids)
        {
            var guid = Guid.Parse(sample.UUID);
            if (!guid.AsUUIDv7(out var uuid7))
                continue;
            var base36 = uuid7.ToBase36();
            var recreatedRecord = UUIDRecord.FromBase36(base36);
            var recreated = new UUIDv7(recreatedRecord.Bytes);
            if (!uuid7.Equals(recreated))
            {
                Console.WriteLine($"Original:   {BitConverter.ToString(uuid7.Bytes.ToArray())}");
                Console.WriteLine($"Recreated:  {BitConverter.ToString(recreated.Bytes.ToArray())}");
            }
            Assert.Equal(uuid7, recreated);
        }
    }
    [Fact]
    public void Uuid7Samples_ToBase25_And_FromBase25_ShouldRoundTrip()
    {
        foreach (var sample in Uuid7TestSamples.Uuids)
        {
            var guid = Guid.Parse(sample.UUID);
            if (!guid.AsUUIDv7(out var uuid7))
                continue;
            var base25 = uuid7.ToBase25();
            var recreatedRecord = UUIDRecord.FromBase25(base25);
            var recreated = new UUIDv7(recreatedRecord.Bytes);
            if (!uuid7.Equals(recreated))
            {
                Console.WriteLine($"Original:   {BitConverter.ToString(uuid7.Bytes.ToArray())}");
                Console.WriteLine($"Recreated:  {BitConverter.ToString(recreated.Bytes.ToArray())}");
            }
            Assert.Equal(uuid7, recreated);
        }
    }
}
