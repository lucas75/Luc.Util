namespace Luc.Util.UUID;

public static class UUIDExtensions
{
  /// <summary>
  /// Converts a <see cref="Guid"/> to a <see cref="Uuid"/> using RFC 4122 byte order.
  /// </summary>
  public static Uuid AsUUID(this Guid guid)
  {
    Span<byte> rfcBytes = stackalloc byte[16];
    guid.TryWriteBytes(rfcBytes, bigEndian: true, out _);
    return new Uuid(rfcBytes);
  }
}

