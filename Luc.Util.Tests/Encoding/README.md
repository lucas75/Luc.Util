# Encoding Tests

This directory contains unit tests for the Base?? encoding classes. Tests use `SharedTestTypes` to validate encoding/decoding for arbitrary struct types and are independent of `UUID`.

Strategy:
- Use `SharedTestTypes.RandomS8/S16/S24` to generate deterministic sample structs for tests.
- Avoid tieing encoding tests to `UUID` test data to keep them focused and maintainable.
- Ensure exactly three encoders and three decoders are covered per encoding class.

Note: See top-level `README.md` for guidance on actual encoding classes.
