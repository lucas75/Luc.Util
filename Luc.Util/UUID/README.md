# Luc.Util

A high-performance .NET library for working with UUIDs. Provides fast, zero-allocation helpers for UUIDv4 (random) and UUIDv7 (time-ordered) formats — plus compact string encodings and conversion helpers.

## Features

- **UUID**: Core UUID struct with multiple encoding formats (hex, base36, base35, base32, base31)
- **UUIDv4**: Random UUID generation (RFC 4122)
- **UUIDv7**: Time-ordered UUID generation (RFC 9562)
- **Zero-allocation**: Stack-allocated operations for optimal performance
- **Multiple encodings**: Standard hex, base36 (25 chars), base35 (25 chars), base32 (26 chars), and base31 (26 chars)
- **Interop**: Extension methods for `System.Guid` conversion

## Usage

### Creating UUIDs

```csharp
using Luc.Util;

// Create a random UUIDv4
var uuid4 = UUID.NewV4();

// Create a time-ordered UUIDv7
var uuid7 = UUID.NewV7();
```

