using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Luc.Util.Encoding;

/// <summary>
/// Provides URL-safe Base64 encoding and decoding for arbitrary byte structures.
/// </summary>
public static class Base64
{
  /// <summary>
  /// Encodes in base64 URL-safe format.
  /// </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string Encode(ReadOnlySpan<byte> bytes)
  {
    return Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');
  }

  /// <summary>
  /// Encodes a structure to URL-safe Base64.
  /// </summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string Encode<T>(T value) where T : IEncodingOutput<T>, IEncodingInput
  {
    var (bytesMem, _) = value.EncodeToBytes();
    return Encode(bytesMem.Span);
  }

  /// <summary>
  /// Decodes a URL-safe Base64 string to a byte array.
  /// </summary>
  [MethodImpl()]
  private static byte[] DecodeToBytes(string base64String)
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
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Decode<T>(string base64String) where T : IEncodingOutput<T>, IEncodingInput
  {
    var bytes = DecodeToBytes(base64String);
    return T.DecodeFromBytes(new ReadOnlyMemory<byte>(bytes));
  }

  /// <summary>
  /// Decodes a URL-safe Base64 string with a specified expected length.
  /// </summary>
  /// <typeparam name="T">The type to decode to.</typeparam>
  /// <param name="base64String">The Base64 string to decode.</param>
  /// <param name="expectedLength">The expected length of the Base64 string.</param>
  /// <returns>The decoded structure.</returns>
  /// <exception cref="ArgumentException">Thrown if the string length doesn't match expected size.</exception>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T Decode<T>(string base64String, int expectedLength) where T : IEncodingOutput<T>, IEncodingInput
  {
    if (base64String.Length != expectedLength)
      throw new ArgumentException($"Base64 string must be exactly {expectedLength} characters long.", nameof(base64String));
    return Decode<T>(base64String);
  }
}

