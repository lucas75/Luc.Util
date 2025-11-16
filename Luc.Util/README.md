# Luc.Util

A high-performance .NET library for working with UUIDs, including support for UUIDv4 (random) and UUIDv7 (time-ordered) formats.

## Features

- **UUID**: Core UUID struct with multiple encoding formats (hex, base36, base25)
- **UUIDv4**: Random UUID generation (RFC 4122)
- **UUIDv7**: Time-ordered UUID generation (RFC 9562)
- **Zero-allocation**: Stack-allocated operations for optimal performance
- **Multiple encodings**: Standard hex, base36 (25 chars), and base25 (28 chars)
- **Interop**: Extension methods for `System.Guid` conversion

## Usage

### Creating UUIDs

```csharp
using Luc.Util;

// Create a random UUIDv4
var uuid4 = UUIDv4.Create();

// Create a time-ordered UUIDv7
var uuid7 = UUIDv7.Create();

// Create UUIDv7 with custom timestamp and randomness
var customUuid7 = UUIDv7.CreateV7(
    unixEpochMs: DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
    randomBytes: myRandomBytes,
    seqBytes: mySequenceBytes
);
```

### Working with UUIDs

```csharp
// Get the timestamp from UUIDv7
DateTimeOffset timestamp = uuid7.GetDateTimeOffset();

// Convert to different formats
string hex = uuid7.ToString();           // Standard hyphenated format
string base36 = uuid7.ToBase36();        // 25-character base36
string base25 = uuid7.ToBase25();        // 28-character base25

// Access raw bytes
ReadOnlySpan<byte> bytes = uuid7.Bytes;

// Convert to base UUID
UUID baseUuid = uuid7.AsUUID();
```

### Converting from System.Guid

```csharp
Guid systemGuid = Guid.NewGuid();

// Try to convert to UUIDv4
if (systemGuid.AsUUIDv4(out var uuid4))
{
    Console.WriteLine($"Valid UUIDv4: {uuid4}");
}

// Try to convert to UUIDv7
if (systemGuid.AsUUIDv7(out var uuid7))
{
    Console.WriteLine($"Valid UUIDv7: {uuid7}");
}
```

### Parsing from Strings

```csharp
// Parse from base36
var uuid = UUID.FromBase36("00000000000000000000abcde");

// Parse from base25
var uuid = UUID.FromBase25("2222222222222222222222222222");
```

## Performance

All UUID operations are designed for zero-allocation performance:
- Stack-allocated byte buffers
- `ReadOnlySpan<byte>` for data access
- Struct-based types for value semantics
- Optimized string conversion

## License

[Your License Here]
