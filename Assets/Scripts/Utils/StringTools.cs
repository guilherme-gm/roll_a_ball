using UnityEngine;
using System.Text;
using System.Collections;

// Source: http://www.dotnetperls.com/truncate

/// <summary>
/// Custom string utility methods.
/// </summary>
public static class StringTool
{
	/// <summary>
	/// Get a substring of the first N characters.
	/// </summary>
	public static string Truncate(string source, int length)
	{
		if (source.Length > length)
		{
			source = source.Substring(0, length);
		}
		return source;
	}

	public static string GetString(byte[] bytes)
	{
		byte[] str = new byte[bytes.Length];
		int i = 0;

		for (i = 0; i < bytes.Length; i++)
		{
			if (bytes[i] != 0x00)
				str[i] = bytes[i];
			else
				break;
		}

		if (i <= 0) return "";
		return Encoding.ASCII.GetString (str);
	}
}
