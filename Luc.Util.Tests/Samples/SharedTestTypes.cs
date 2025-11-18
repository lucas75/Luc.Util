// <generated>Do not alter this class</generated>
using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Luc.Util;
using Luc.Util.Encoding;

namespace Luc.Util.Tests.Samples;

public static class SharedTestTypes
{
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S01 : IEncodingInput, IEncodingOutput<S01>
  {
    byte B0;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S01 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S01>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S01>()} byte.");
        return MemoryMarshal.Read<S01>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S02 : IEncodingInput, IEncodingOutput<S02>
  {
    byte B0,B1;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S02 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S02>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S02>()} byte.");
        return MemoryMarshal.Read<S02>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S03 : IEncodingInput, IEncodingOutput<S03>
  {
    byte B0,B1,B2;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S03 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S03>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S03>()} byte.");
        return MemoryMarshal.Read<S03>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S04 : IEncodingInput, IEncodingOutput<S04>
  {
    byte B0,B1,B2,B3;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S04 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S04>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S04>()} byte.");
        return MemoryMarshal.Read<S04>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S05 : IEncodingInput, IEncodingOutput<S05>
  {
    byte B0,B1,B2,B3,B4;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S05 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S05>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S05>()} byte.");
        return MemoryMarshal.Read<S05>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S06 : IEncodingInput, IEncodingOutput<S06>
  {
    byte B0,B1,B2,B3,B4,B5;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S06 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S06>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S06>()} byte.");
        return MemoryMarshal.Read<S06>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S07 : IEncodingInput, IEncodingOutput<S07>
  {
    byte B0,B1,B2,B3,B4,B5,B6;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S07 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S07>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S07>()} byte.");
        return MemoryMarshal.Read<S07>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S08 : IEncodingInput, IEncodingOutput<S08>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S08 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S08>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S08>()} byte.");
        return MemoryMarshal.Read<S08>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S09 : IEncodingInput, IEncodingOutput<S09>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S09 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S09>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S09>()} byte.");
        return MemoryMarshal.Read<S09>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S10 : IEncodingInput, IEncodingOutput<S10>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S10 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S10>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S10>()} byte.");
        return MemoryMarshal.Read<S10>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S11 : IEncodingInput, IEncodingOutput<S11>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S11 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S11>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S11>()} byte.");
        return MemoryMarshal.Read<S11>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S12 : IEncodingInput, IEncodingOutput<S12>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S12 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S12>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S12>()} byte.");
        return MemoryMarshal.Read<S12>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S13 : IEncodingInput, IEncodingOutput<S13>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S13 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S13>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S13>()} byte.");
        return MemoryMarshal.Read<S13>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S14 : IEncodingInput, IEncodingOutput<S14>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S14 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S14>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S14>()} byte.");
        return MemoryMarshal.Read<S14>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S15 : IEncodingInput, IEncodingOutput<S15>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S15 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S15>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S15>()} byte.");
        return MemoryMarshal.Read<S15>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S16 : IEncodingInput, IEncodingOutput<S16>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S16 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S16>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S16>()} byte.");
        return MemoryMarshal.Read<S16>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S17 : IEncodingInput, IEncodingOutput<S17>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S17 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S17>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S17>()} byte.");
        return MemoryMarshal.Read<S17>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S18 : IEncodingInput, IEncodingOutput<S18>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S18 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S18>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S18>()} byte.");
        return MemoryMarshal.Read<S18>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S19 : IEncodingInput, IEncodingOutput<S19>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S19 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S19>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S19>()} byte.");
        return MemoryMarshal.Read<S19>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S20 : IEncodingInput, IEncodingOutput<S20>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S20 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S20>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S20>()} byte.");
        return MemoryMarshal.Read<S20>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S21 : IEncodingInput, IEncodingOutput<S21>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S21 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S21>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S21>()} byte.");
        return MemoryMarshal.Read<S21>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S22 : IEncodingInput, IEncodingOutput<S22>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S22 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S22>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S22>()} byte.");
        return MemoryMarshal.Read<S22>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S23 : IEncodingInput, IEncodingOutput<S23>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S23 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S23>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S23>()} byte.");
        return MemoryMarshal.Read<S23>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S24 : IEncodingInput, IEncodingOutput<S24>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S24 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S24>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S24>()} byte.");
        return MemoryMarshal.Read<S24>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S25 : IEncodingInput, IEncodingOutput<S25>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S25 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S25>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S25>()} byte.");
        return MemoryMarshal.Read<S25>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S26 : IEncodingInput, IEncodingOutput<S26>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S26 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S26>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S26>()} byte.");
        return MemoryMarshal.Read<S26>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S27 : IEncodingInput, IEncodingOutput<S27>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S27 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S27>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S27>()} byte.");
        return MemoryMarshal.Read<S27>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S28 : IEncodingInput, IEncodingOutput<S28>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S28 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S28>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S28>()} byte.");
        return MemoryMarshal.Read<S28>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S29 : IEncodingInput, IEncodingOutput<S29>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S29 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S29>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S29>()} byte.");
        return MemoryMarshal.Read<S29>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S30 : IEncodingInput, IEncodingOutput<S30>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S30 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S30>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S30>()} byte.");
        return MemoryMarshal.Read<S30>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S31 : IEncodingInput, IEncodingOutput<S31>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S31 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S31>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S31>()} byte.");
        return MemoryMarshal.Read<S31>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S32 : IEncodingInput, IEncodingOutput<S32>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S32 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S32>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S32>()} byte.");
        return MemoryMarshal.Read<S32>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S33 : IEncodingInput, IEncodingOutput<S33>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S33 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S33>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S33>()} byte.");
        return MemoryMarshal.Read<S33>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S34 : IEncodingInput, IEncodingOutput<S34>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S34 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S34>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S34>()} byte.");
        return MemoryMarshal.Read<S34>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S35 : IEncodingInput, IEncodingOutput<S35>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S35 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S35>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S35>()} byte.");
        return MemoryMarshal.Read<S35>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S36 : IEncodingInput, IEncodingOutput<S36>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S36 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S36>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S36>()} byte.");
        return MemoryMarshal.Read<S36>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S37 : IEncodingInput, IEncodingOutput<S37>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S37 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S37>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S37>()} byte.");
        return MemoryMarshal.Read<S37>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S38 : IEncodingInput, IEncodingOutput<S38>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S38 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S38>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S38>()} byte.");
        return MemoryMarshal.Read<S38>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S39 : IEncodingInput, IEncodingOutput<S39>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S39 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S39>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S39>()} byte.");
        return MemoryMarshal.Read<S39>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S40 : IEncodingInput, IEncodingOutput<S40>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S40 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S40>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S40>()} byte.");
        return MemoryMarshal.Read<S40>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S41 : IEncodingInput, IEncodingOutput<S41>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S41 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S41>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S41>()} byte.");
        return MemoryMarshal.Read<S41>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S42 : IEncodingInput, IEncodingOutput<S42>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S42 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S42>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S42>()} byte.");
        return MemoryMarshal.Read<S42>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S43 : IEncodingInput, IEncodingOutput<S43>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S43 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S43>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S43>()} byte.");
        return MemoryMarshal.Read<S43>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S44 : IEncodingInput, IEncodingOutput<S44>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S44 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S44>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S44>()} byte.");
        return MemoryMarshal.Read<S44>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S45 : IEncodingInput, IEncodingOutput<S45>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S45 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S45>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S45>()} byte.");
        return MemoryMarshal.Read<S45>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S46 : IEncodingInput, IEncodingOutput<S46>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S46 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S46>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S46>()} byte.");
        return MemoryMarshal.Read<S46>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S47 : IEncodingInput, IEncodingOutput<S47>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S47 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S47>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S47>()} byte.");
        return MemoryMarshal.Read<S47>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S48 : IEncodingInput, IEncodingOutput<S48>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S48 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S48>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S48>()} byte.");
        return MemoryMarshal.Read<S48>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S49 : IEncodingInput, IEncodingOutput<S49>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S49 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S49>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S49>()} byte.");
        return MemoryMarshal.Read<S49>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S50 : IEncodingInput, IEncodingOutput<S50>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S50 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S50>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S50>()} byte.");
        return MemoryMarshal.Read<S50>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S51 : IEncodingInput, IEncodingOutput<S51>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S51 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S51>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S51>()} byte.");
        return MemoryMarshal.Read<S51>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S52 : IEncodingInput, IEncodingOutput<S52>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S52 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S52>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S52>()} byte.");
        return MemoryMarshal.Read<S52>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S53 : IEncodingInput, IEncodingOutput<S53>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51,B52;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S53 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S53>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S53>()} byte.");
        return MemoryMarshal.Read<S53>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S54 : IEncodingInput, IEncodingOutput<S54>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51,B52,B53;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S54 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S54>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S54>()} byte.");
        return MemoryMarshal.Read<S54>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S55 : IEncodingInput, IEncodingOutput<S55>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51,B52,B53,B54;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S55 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S55>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S55>()} byte.");
        return MemoryMarshal.Read<S55>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S56 : IEncodingInput, IEncodingOutput<S56>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51,B52,B53,B54,B55;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S56 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S56>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S56>()} byte.");
        return MemoryMarshal.Read<S56>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S57 : IEncodingInput, IEncodingOutput<S57>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51,B52,B53,B54,B55,B56;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S57 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S57>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S57>()} byte.");
        return MemoryMarshal.Read<S57>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S58 : IEncodingInput, IEncodingOutput<S58>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51,B52,B53,B54,B55,B56,B57;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S58 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S58>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S58>()} byte.");
        return MemoryMarshal.Read<S58>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S59 : IEncodingInput, IEncodingOutput<S59>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51,B52,B53,B54,B55,B56,B57,B58;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S59 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S59>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S59>()} byte.");
        return MemoryMarshal.Read<S59>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S60 : IEncodingInput, IEncodingOutput<S60>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51,B52,B53,B54,B55,B56,B57,B58,B59;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S60 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S60>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S60>()} byte.");
        return MemoryMarshal.Read<S60>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S61 : IEncodingInput, IEncodingOutput<S61>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51,B52,B53,B54,B55,B56,B57,B58,B59,B60;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S61 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S61>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S61>()} byte.");
        return MemoryMarshal.Read<S61>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S62 : IEncodingInput, IEncodingOutput<S62>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51,B52,B53,B54,B55,B56,B57,B58,B59,B60,B61;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S62 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S62>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S62>()} byte.");
        return MemoryMarshal.Read<S62>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S63 : IEncodingInput, IEncodingOutput<S63>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51,B52,B53,B54,B55,B56,B57,B58,B59,B60,B61,B62;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S63 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S63>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S63>()} byte.");
        return MemoryMarshal.Read<S63>(bytes.Span);
    }
  }
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  public struct S64 : IEncodingInput, IEncodingOutput<S64>
  {
    byte B0,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,B39,B40,B41,B42,B43,B44,B45,B46,B47,B48,B49,B50,B51,B52,B53,B54,B55,B56,B57,B58,B59,B60,B61,B62,B63;

    readonly (ReadOnlyMemory<byte> Bytes, int Length) IEncodingInput.EncodeToBytes()
    {
        var bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in this), 1 /* elements */)).ToArray();
        return (bytes, bytes.Length * 8);
    }
    public static S64 DecodeFromBytes(ReadOnlyMemory<byte> bytes)
    {
        if(bytes.Length < Unsafe.SizeOf<S64>()) throw new ArgumentException($"Decoded bytes must be exactly {Unsafe.SizeOf<S64>()} byte.");
        return MemoryMarshal.Read<S64>(bytes.Span);
    }
  }


  public static readonly IEncodingInput[] Samples = [ 
      new S01(),         
      new S02(),         
      new S03(),         
      new S04(),         
      new S05(),         
      new S06(),         
      new S07(),         
      new S08(),         
      new S09(),         
      new S10(),         
      new S11(),         
      new S12(),         
      new S13(),         
      new S14(),         
      new S15(),         
      new S16(),         
      new S17(),         
      new S18(),         
      new S19(),         
      new S20(),         
      new S21(),         
      new S22(),         
      new S23(),         
      new S24(),         
      new S25(),         
      new S26(),         
      new S27(),         
      new S28(),         
      new S29(),         
      new S30(),         
      new S31(),         
      new S32(),         
      new S33(),         
      new S34(),         
      new S35(),         
      new S36(),         
      new S37(),         
      new S38(),         
      new S39(),         
      new S40(),         
      new S41(),         
      new S42(),         
      new S43(),         
      new S44(),         
      new S45(),         
      new S46(),         
      new S47(),         
      new S48(),         
      new S49(),         
      new S50(),         
      new S51(),         
      new S52(),         
      new S53(),         
      new S54(),         
      new S55(),         
      new S56(),         
      new S57(),         
      new S58(),         
      new S59(),         
      new S60(),         
      new S61(),         
      new S62(),         
      new S63(),         
      new S64(),         

  ];

  public static readonly Type[] Types = [
      typeof(SharedTestTypes.S01),
      typeof(SharedTestTypes.S02),
      typeof(SharedTestTypes.S03),
      typeof(SharedTestTypes.S04),
      typeof(SharedTestTypes.S05),
      typeof(SharedTestTypes.S06),
      typeof(SharedTestTypes.S07),
      typeof(SharedTestTypes.S08),
      typeof(SharedTestTypes.S09),
      typeof(SharedTestTypes.S10),
      typeof(SharedTestTypes.S11),
      typeof(SharedTestTypes.S12),
      typeof(SharedTestTypes.S13),
      typeof(SharedTestTypes.S14),
      typeof(SharedTestTypes.S15),
      typeof(SharedTestTypes.S16),
      typeof(SharedTestTypes.S17),
      typeof(SharedTestTypes.S18),
      typeof(SharedTestTypes.S19),
      typeof(SharedTestTypes.S20),
      typeof(SharedTestTypes.S21),
      typeof(SharedTestTypes.S22),
      typeof(SharedTestTypes.S23),
      typeof(SharedTestTypes.S24),
      typeof(SharedTestTypes.S25),
      typeof(SharedTestTypes.S26),
      typeof(SharedTestTypes.S27),
      typeof(SharedTestTypes.S28),
      typeof(SharedTestTypes.S29),
      typeof(SharedTestTypes.S30),
      typeof(SharedTestTypes.S31),
      typeof(SharedTestTypes.S32),
      typeof(SharedTestTypes.S33),
      typeof(SharedTestTypes.S34),
      typeof(SharedTestTypes.S35),
      typeof(SharedTestTypes.S36),
      typeof(SharedTestTypes.S37),
      typeof(SharedTestTypes.S38),
      typeof(SharedTestTypes.S39),
      typeof(SharedTestTypes.S40),
      typeof(SharedTestTypes.S41),
      typeof(SharedTestTypes.S42),
      typeof(SharedTestTypes.S43),
      typeof(SharedTestTypes.S44),
      typeof(SharedTestTypes.S45),
      typeof(SharedTestTypes.S46),
      typeof(SharedTestTypes.S47),
      typeof(SharedTestTypes.S48),
      typeof(SharedTestTypes.S49),
      typeof(SharedTestTypes.S50),
      typeof(SharedTestTypes.S51),
      typeof(SharedTestTypes.S52),
      typeof(SharedTestTypes.S53),
      typeof(SharedTestTypes.S54),
      typeof(SharedTestTypes.S55),
      typeof(SharedTestTypes.S56),
      typeof(SharedTestTypes.S57),
      typeof(SharedTestTypes.S58),
      typeof(SharedTestTypes.S59),
      typeof(SharedTestTypes.S60),
      typeof(SharedTestTypes.S61),
      typeof(SharedTestTypes.S62),
      typeof(SharedTestTypes.S63),
      typeof(SharedTestTypes.S64),

  ];

  public static IEncodingInput Empty(int size)
  {
    switch(size) {
      case 1: return new S01(); 
      case 2: return new S02(); 
      case 3: return new S03(); 
      case 4: return new S04(); 
      case 5: return new S05(); 
      case 6: return new S06(); 
      case 7: return new S07(); 
      case 8: return new S08(); 
      case 9: return new S09(); 
      case 10: return new S10(); 
      case 11: return new S11(); 
      case 12: return new S12(); 
      case 13: return new S13(); 
      case 14: return new S14(); 
      case 15: return new S15(); 
      case 16: return new S16(); 
      case 17: return new S17(); 
      case 18: return new S18(); 
      case 19: return new S19(); 
      case 20: return new S20(); 
      case 21: return new S21(); 
      case 22: return new S22(); 
      case 23: return new S23(); 
      case 24: return new S24(); 
      case 25: return new S25(); 
      case 26: return new S26(); 
      case 27: return new S27(); 
      case 28: return new S28(); 
      case 29: return new S29(); 
      case 30: return new S30(); 
      case 31: return new S31(); 
      case 32: return new S32(); 
      case 33: return new S33(); 
      case 34: return new S34(); 
      case 35: return new S35(); 
      case 36: return new S36(); 
      case 37: return new S37(); 
      case 38: return new S38(); 
      case 39: return new S39(); 
      case 40: return new S40(); 
      case 41: return new S41(); 
      case 42: return new S42(); 
      case 43: return new S43(); 
      case 44: return new S44(); 
      case 45: return new S45(); 
      case 46: return new S46(); 
      case 47: return new S47(); 
      case 48: return new S48(); 
      case 49: return new S49(); 
      case 50: return new S50(); 
      case 51: return new S51(); 
      case 52: return new S52(); 
      case 53: return new S53(); 
      case 54: return new S54(); 
      case 55: return new S55(); 
      case 56: return new S56(); 
      case 57: return new S57(); 
      case 58: return new S58(); 
      case 59: return new S59(); 
      case 60: return new S60(); 
      case 61: return new S61(); 
      case 62: return new S62(); 
      case 63: return new S63(); 
      case 64: return new S64(); 

      default: throw new ArgumentException("Size not supported.");
    }  
  }

  public static IEncodingInput Random(int size, Random rng)
  {
    byte[] b;
    switch(size) {
      case 1: b = new byte[1]; rng.NextBytes(b); return S01.DecodeFromBytes(b); 
      case 2: b = new byte[2]; rng.NextBytes(b); return S02.DecodeFromBytes(b); 
      case 3: b = new byte[3]; rng.NextBytes(b); return S03.DecodeFromBytes(b); 
      case 4: b = new byte[4]; rng.NextBytes(b); return S04.DecodeFromBytes(b); 
      case 5: b = new byte[5]; rng.NextBytes(b); return S05.DecodeFromBytes(b); 
      case 6: b = new byte[6]; rng.NextBytes(b); return S06.DecodeFromBytes(b); 
      case 7: b = new byte[7]; rng.NextBytes(b); return S07.DecodeFromBytes(b); 
      case 8: b = new byte[8]; rng.NextBytes(b); return S08.DecodeFromBytes(b); 
      case 9: b = new byte[9]; rng.NextBytes(b); return S09.DecodeFromBytes(b); 
      case 10: b = new byte[10]; rng.NextBytes(b); return S10.DecodeFromBytes(b); 
      case 11: b = new byte[11]; rng.NextBytes(b); return S11.DecodeFromBytes(b); 
      case 12: b = new byte[12]; rng.NextBytes(b); return S12.DecodeFromBytes(b); 
      case 13: b = new byte[13]; rng.NextBytes(b); return S13.DecodeFromBytes(b); 
      case 14: b = new byte[14]; rng.NextBytes(b); return S14.DecodeFromBytes(b); 
      case 15: b = new byte[15]; rng.NextBytes(b); return S15.DecodeFromBytes(b); 
      case 16: b = new byte[16]; rng.NextBytes(b); return S16.DecodeFromBytes(b); 
      case 17: b = new byte[17]; rng.NextBytes(b); return S17.DecodeFromBytes(b); 
      case 18: b = new byte[18]; rng.NextBytes(b); return S18.DecodeFromBytes(b); 
      case 19: b = new byte[19]; rng.NextBytes(b); return S19.DecodeFromBytes(b); 
      case 20: b = new byte[20]; rng.NextBytes(b); return S20.DecodeFromBytes(b); 
      case 21: b = new byte[21]; rng.NextBytes(b); return S21.DecodeFromBytes(b); 
      case 22: b = new byte[22]; rng.NextBytes(b); return S22.DecodeFromBytes(b); 
      case 23: b = new byte[23]; rng.NextBytes(b); return S23.DecodeFromBytes(b); 
      case 24: b = new byte[24]; rng.NextBytes(b); return S24.DecodeFromBytes(b); 
      case 25: b = new byte[25]; rng.NextBytes(b); return S25.DecodeFromBytes(b); 
      case 26: b = new byte[26]; rng.NextBytes(b); return S26.DecodeFromBytes(b); 
      case 27: b = new byte[27]; rng.NextBytes(b); return S27.DecodeFromBytes(b); 
      case 28: b = new byte[28]; rng.NextBytes(b); return S28.DecodeFromBytes(b); 
      case 29: b = new byte[29]; rng.NextBytes(b); return S29.DecodeFromBytes(b); 
      case 30: b = new byte[30]; rng.NextBytes(b); return S30.DecodeFromBytes(b); 
      case 31: b = new byte[31]; rng.NextBytes(b); return S31.DecodeFromBytes(b); 
      case 32: b = new byte[32]; rng.NextBytes(b); return S32.DecodeFromBytes(b); 
      case 33: b = new byte[33]; rng.NextBytes(b); return S33.DecodeFromBytes(b); 
      case 34: b = new byte[34]; rng.NextBytes(b); return S34.DecodeFromBytes(b); 
      case 35: b = new byte[35]; rng.NextBytes(b); return S35.DecodeFromBytes(b); 
      case 36: b = new byte[36]; rng.NextBytes(b); return S36.DecodeFromBytes(b); 
      case 37: b = new byte[37]; rng.NextBytes(b); return S37.DecodeFromBytes(b); 
      case 38: b = new byte[38]; rng.NextBytes(b); return S38.DecodeFromBytes(b); 
      case 39: b = new byte[39]; rng.NextBytes(b); return S39.DecodeFromBytes(b); 
      case 40: b = new byte[40]; rng.NextBytes(b); return S40.DecodeFromBytes(b); 
      case 41: b = new byte[41]; rng.NextBytes(b); return S41.DecodeFromBytes(b); 
      case 42: b = new byte[42]; rng.NextBytes(b); return S42.DecodeFromBytes(b); 
      case 43: b = new byte[43]; rng.NextBytes(b); return S43.DecodeFromBytes(b); 
      case 44: b = new byte[44]; rng.NextBytes(b); return S44.DecodeFromBytes(b); 
      case 45: b = new byte[45]; rng.NextBytes(b); return S45.DecodeFromBytes(b); 
      case 46: b = new byte[46]; rng.NextBytes(b); return S46.DecodeFromBytes(b); 
      case 47: b = new byte[47]; rng.NextBytes(b); return S47.DecodeFromBytes(b); 
      case 48: b = new byte[48]; rng.NextBytes(b); return S48.DecodeFromBytes(b); 
      case 49: b = new byte[49]; rng.NextBytes(b); return S49.DecodeFromBytes(b); 
      case 50: b = new byte[50]; rng.NextBytes(b); return S50.DecodeFromBytes(b); 
      case 51: b = new byte[51]; rng.NextBytes(b); return S51.DecodeFromBytes(b); 
      case 52: b = new byte[52]; rng.NextBytes(b); return S52.DecodeFromBytes(b); 
      case 53: b = new byte[53]; rng.NextBytes(b); return S53.DecodeFromBytes(b); 
      case 54: b = new byte[54]; rng.NextBytes(b); return S54.DecodeFromBytes(b); 
      case 55: b = new byte[55]; rng.NextBytes(b); return S55.DecodeFromBytes(b); 
      case 56: b = new byte[56]; rng.NextBytes(b); return S56.DecodeFromBytes(b); 
      case 57: b = new byte[57]; rng.NextBytes(b); return S57.DecodeFromBytes(b); 
      case 58: b = new byte[58]; rng.NextBytes(b); return S58.DecodeFromBytes(b); 
      case 59: b = new byte[59]; rng.NextBytes(b); return S59.DecodeFromBytes(b); 
      case 60: b = new byte[60]; rng.NextBytes(b); return S60.DecodeFromBytes(b); 
      case 61: b = new byte[61]; rng.NextBytes(b); return S61.DecodeFromBytes(b); 
      case 62: b = new byte[62]; rng.NextBytes(b); return S62.DecodeFromBytes(b); 
      case 63: b = new byte[63]; rng.NextBytes(b); return S63.DecodeFromBytes(b); 
      case 64: b = new byte[64]; rng.NextBytes(b); return S64.DecodeFromBytes(b); 

      default: throw new ArgumentException("Size not supported.");
    }  
  } 
}