# Luc.Util.Tests

Unit tests for the Luc.Util core library. This project ensures the correctness and reliability of UUID generation, encoding, parsing, and conversion features.

## Overview

- Validates UUIDv4 and UUIDv7 creation and encoding
- Tests conversion between different UUID formats
 - Ensures round-trip integrity for Base36, Base35, Base32, Base31, and Base64 encodings
- Includes sample-based tests for UUIDv7 with Medo.Uuid7 test data
- Validates timestamp extraction from UUIDv7 against sample data
 - Validates URL-safe Base64 encode/decode (22-char) independently of Medo Id22

## Key Files

- `UUIDTests.cs`: Main test suite for UUID functionality
- `Generated/Uuid7TestSamples.cs`: Sample UUIDv7 data for round-trip tests

## Running Tests

You can run the tests using the .NET CLI:

```bash
# From the solution root
 dotnet test Luc.Util.Tests
```

## Contributing

Contributions to test coverage and new test cases are welcome. Please ensure all tests pass before submitting a pull request.

## License

This project is licensed under the MIT License. See the root LICENSE.md for details.
