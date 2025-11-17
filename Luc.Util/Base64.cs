using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Luc.Util;

/// <summary>
/// Provides URL-safe Base64 encoding and decoding for arbitrary byte structures.
/// </summary>
public static class Base64
{
  /// <summary>
  /// Encodes a byte span to URL-safe Base64 (no padding, uses '-' and '_' instead of '+' and '/').
  /// </summary>
  /// <param name="bytes">The bytes to encode.</param>
  /// <returns>The Base64 URL-safe string.</returns>
  public static string Encode(ReadOnlySpan<byte> bytes)
  {
    return Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
  }

  /// <summary>
  /// Encodes a structure to URL-safe Base64.
  /// </summary>
  /// <typeparam name="T">The unmanaged type to encode.</typeparam>
  /// <param name="value">The value to encode.</param>
  /// <returns>The Base64 URL-safe string.</returns>
  public static string Encode<T>(T value) where T : unmanaged
  {
    ReadOnlySpan<byte> bytes = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref Unsafe.AsRef(in value), 1));
    return Encode(bytes);
  }

  /// <summary>
  /// Decodes a URL-safe Base64 string to a byte array.
  /// </summary>
  /// <param name="base64String">The Base64 string to decode.</param>
  /// <returns>The decoded bytes.</returns>
  /// <exception cref="FormatException">Thrown if the string contains invalid characters.</exception>
  public static byte[] DecodeToBytes(string base64String)
  {
    if (string.IsNullOrEmpty(base64String)) throw new ArgumentException("Base64 string cannot be null or empty.", nameof(base64String));

    // Convert URL-safe back to standard Base64
    string standardBase64 = base64String.Replace('-', '+').Replace('_', '/');
    
    // Add padding if needed
    int padding = (4 - (standardBase64.Length % 4)) % 4;
    if (padding > 0) standardBase64 += new string('=', padding);

    try
    {
      return Convert.FromBase64String(standardBase64);
    }
    catch (FormatException ex)
    {
      throw new FormatException($"Invalid Base64 string: {ex.Message}", ex);
    }
  }

  /// <summary>
  /// Decodes a URL-safe Base64 string to a structure.
  /// </summary>
  /// <typeparam name="T">The unmanaged type to decode to.</typeparam>
  /// <param name="base64String">The Base64 string to decode.</param>
  /// <returns>The decoded structure.</returns>
  /// <exception cref="ArgumentException">Thrown if the decoded bytes don't match the expected size.</exception>
  /// <exception cref="FormatException">Thrown if the string contains invalid characters.</exception>
  public static T Decode<T>(string base64String) where T : unmanaged
  {
    byte[] bytes = DecodeToBytes(base64String);
    int expectedSize = Unsafe.SizeOf<T>();
    
    if (bytes.Length != expectedSize)
      throw new ArgumentException($"Decoded bytes must be exactly {expectedSize} bytes, got {bytes.Length}.", nameof(base64String));

    return MemoryMarshal.Read<T>(bytes);
  }

  /// <summary>
  /// Decodes a URL-safe Base64 string with a specified expected length.
  /// </summary>
  /// <typeparam name="T">The unmanaged type to decode to.</typeparam>
  /// <param name="base64String">The Base64 string to decode.</param>
  /// <param name="expectedLength">The expected length of the Base64 string.</param>
  /// <returns>The decoded structure.</returns>
  /// <exception cref="ArgumentException">Thrown if the string length or decoded bytes don't match expected sizes.</exception>
  public static T Decode<T>(string base64String, int expectedLength) where T : unmanaged
  {
    if (base64String.Length != expectedLength)
      throw new ArgumentException($"Base64 string must be exactly {expectedLength} characters long.", nameof(base64String));

    return Decode<T>(base64String);
  }
}
