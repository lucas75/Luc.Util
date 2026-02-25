namespace Luc.Util.Uuid;

public static class UUIDExtensions
{
  /// <summary>
  /// Converts a <see cref="Guid"/> to a <see cref="UUID"/> using RFC 4122 byte order.
  /// </summary>
  public static UUID AsUUID(this Guid guid)
  {
    Span<byte> rfcBytes = stackalloc byte[16];
    guid.TryWriteBytes(rfcBytes, bigEndian: true, out _);
    return new UUID(rfcBytes);
  }
}

