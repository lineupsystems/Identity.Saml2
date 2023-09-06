using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace ITfoxtec.Identity.Saml2.Http.Compression;

public static class SamlCompressionHelper
{
	public static string CompressRequest(string value)
	{
		using var compressedStream = new MemoryStream();
		using var deflateStream = new DeflateStream(compressedStream, CompressionMode.Compress);
		using (var originalStream = new StreamWriter(deflateStream))
		{
			originalStream.Write(value);
		}

		return Convert.ToBase64String(compressedStream.ToArray());
	}
	
	public static string DecompressResponse(string value)
	{
		using var originalStream = new MemoryStream(Convert.FromBase64String(value));
		using var decompressedStream = new MemoryStream();
		using (var deflateStream = new DeflateStream(originalStream, CompressionMode.Decompress))
		{
			deflateStream.CopyTo(decompressedStream);
		}
		return Encoding.UTF8.GetString(decompressedStream.ToArray());
	}
}