using System;
using System.Linq;
using System.Reflection;
using Luc.Util.Encoding;

namespace Luc.Util.Tests.Encoding;

public static class BaseEncodingUtils
{
    public static string InvokeEncode(Type encodingClassType, IEncodingInput instance)
    {
        var encodeMethod = encodingClassType.GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.Name == "Encode" && m.IsGenericMethod && m.GetParameters().Length == 1)
            .Single()!
            .MakeGenericMethod(instance.GetType());

        var result = encodeMethod.Invoke(null, [instance]) ?? throw new InvalidOperationException("Encode returned null");
        return (string)result;
    }

    public static object InvokeDecodeWithLength(Type encodingClassType, IEncodingInput instance, string encoded, int bitLength)
    {
        var type = instance.GetType();
        var decodeCandidates = encodingClassType.GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.Name == "Decode" && m.IsGenericMethod).ToArray();

        // Prefer the (string, int) overload but fall back to a single-string overload if missing.
        var twoParam = decodeCandidates.FirstOrDefault(m => m.GetParameters().Length == 2 && m.GetParameters()[0].ParameterType == typeof(string));
        if (twoParam is not null)
        {
            var decodeMethod = twoParam.MakeGenericMethod(type);
            return decodeMethod.Invoke(null, [encoded, bitLength])!;
        }

        var oneParam = decodeCandidates.FirstOrDefault(m => m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(string));
        if (oneParam is not null)
        {
            var decodeMethod = oneParam.MakeGenericMethod(type);
            return decodeMethod.Invoke(null, [encoded])!;
        }

        throw new InvalidOperationException("No suitable Decode method found for encoding class.");
    }

    public static object InvokeDecode(Type encodingClassType, IEncodingInput instance, string encoded)
    {
        var type = instance.GetType();
        var decodeCandidates = encodingClassType.GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.Name == "Decode" && m.IsGenericMethod).ToArray();

        // Prefer the single-string overload
        var oneParam = decodeCandidates.FirstOrDefault(m => m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(string));
        if (oneParam is not null)
        {
            var method = oneParam.MakeGenericMethod(type);
            return method.Invoke(null, [encoded])!;
        }

        // Fallback to a two-parameter overload where we compute the bit-length from the instance type
        var twoParam = decodeCandidates.FirstOrDefault(m => m.GetParameters().Length == 2 && m.GetParameters()[0].ParameterType == typeof(string));
        if (twoParam is not null)
        {
            int bitLength = System.Runtime.InteropServices.Marshal.SizeOf(instance.GetType()) * 8;
            var method = twoParam.MakeGenericMethod(type);
            return method.Invoke(null, [encoded, bitLength])!;
        }

        throw new InvalidOperationException("No suitable Decode method found for encoding class.");
    }
}
