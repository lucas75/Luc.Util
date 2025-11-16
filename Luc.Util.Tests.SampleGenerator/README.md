Luc.Util.Tests.SampleGenerator
================================

This small console project generates deterministic test samples for the `Luc.Util.Tests` project.
It creates the file `Luc.Util.Tests/Generated/Uuid7TestSamples.cs` containing an array of UUIDv7
records (with multiple encodings and timestamps) that the unit tests use.

How it works
------------
- It uses the library's `Uuid7` implementation to generate the samples.
- Runs `Uuid7.NewUuid7()` 100 times.
- Captures two timestamps around each generation (`TS1` and `TS2`).
- Stores the raw UUID and its `Id25`, `Id26`, and `Id22` string representations.
- Emits strongly-typed records into `Uuid7TestSamples.cs` under the
	`Luc.Util.Tests.Generated` namespace.

Running the generator
---------------------
You must run this project from the **solution root** (`Luc.Util` folder), because the
generator expects to find the `Luc.Util.Tests/Luc.Util.Tests.csproj` file relative to
the working directory.

From the solution root:

```bash
cd /home/lucas/Dev/Luc.Util   # or your local clone root
dotnet run --project Luc.Util.Tests.SampleGenerator/Luc.Util.Tests.SampleGenerator.csproj
```

If the project cannot find `Luc.Util.Tests/Luc.Util.Tests.csproj` it will throw an
exception; be sure to run it from the solution root.

When to run it
--------------
- After making changes to UUIDv7 generation or its string encodings.
- When you want to refresh the UUIDv7 sample data used by tests.

The generator will overwrite `Luc.Util.Tests/Generated/Uuid7TestSamples.cs` each time
you run it.

