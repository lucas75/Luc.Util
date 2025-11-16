# Luc.Util Solution

This repository contains high-performance .NET utilities for working with UUIDs, including generation, encoding, and testing tools.

## Modules

### [Luc.Util](/Luc.Util/README.md)
- **Description:** Core library for UUID operations. Implements fast, zero-allocation helpers for UUIDv4 (random) and UUIDv7 (time-ordered) formats, plus compact string encodings and conversion helpers.
- **Key files:**
  - `UUID.cs`: Main struct for UUIDs, supporting multiple formats and versions.
  - `UUIDExtensions.cs`: Extension methods for converting `System.Guid` to `UUID`.

### [Luc.Util.Tests](/Luc.Util.Tests/README.md)
- **Description:** Unit tests for the core library. Validates UUID generation, encoding, parsing, and conversion.
- **Key files:**
  - `UUIDTests.cs`: Test cases for UUID functionality.
  - `Generated/Uuid7TestSamples.cs`: Sample data for UUIDv7 tests.

### [Luc.Util.Tests.SampleGenerator](/Luc.Util.Tests.SampleGenerator/README.md)
- **Description:** Utility for generating sample UUIDv7 data for tests. Can be run to regenerate test samples.
- **Key files:**
  - `Program.cs`: Main entry point for sample generation.

## License

This project is licensed under the terms of the MIT License. See [LICENSE.md](LICENSE.md) for details.
