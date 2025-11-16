#pragma warning disable IDE1006 

using System;

namespace Luc.Util;

public interface UUID
{
    ReadOnlySpan<byte> Bytes { get; }
    string ToString();
    bool Equals(object? obj);
    int GetHashCode();
    string ToBase36();
    string ToBase25();
}

