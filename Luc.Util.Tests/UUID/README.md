# UUID Tests

This folder contains unit tests focused on `UUID` behaviors and integration with Base encodings. Use this directory for tests that need real UUID samples like `UuidTestSamples` or `Uuid7TestSamples`.

Guidance:
- Keep encoding-focused tests that rely on UUIDs here rather than in the Base encoding test folder.
- Keep unit tests deterministic where possible (use fixed RNG seeds).
