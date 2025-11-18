# Test Samples

This folder contains supporting sample types and generated test samples for the unit tests.

- `SharedTestTypes.cs`: small struct types and helpers used by encoding tests (S8, S16, S24) — moved here to centralize test sample helpers.
- `Generated/`: generated sample files (UUID test samples, etc.)

Guidelines:
- Keep sample types and generated data separate from test logic.
- Keep `SharedTestTypes` namespace as `Luc.Util.Tests` so tests can reference it without extra using directives.
