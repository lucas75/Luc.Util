namespace Luc.Util;

public static class UUIDExtensions
{
  public static UUID AsUUID(this Guid guid)
  {
    Span<byte> rfcBytes = stackalloc byte[16];
    guid.TryWriteBytes(rfcBytes, bigEndian: true, out _);
    return new UUID(rfcBytes);
  }
}
