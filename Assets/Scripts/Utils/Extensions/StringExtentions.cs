using System;
using System.Text;


	public static class StringExtentions
	{

		/// <summary>
		/// Encodes the given ASCII string into 64b ASCII string 
		/// </summary>
		public static string EncodeTo64(this string _this)
		{
			var toEncodeAsBytes = Encoding.UTF8.GetBytes(_this);
			return Convert.ToBase64String(toEncodeAsBytes);
		}

		/// <summary>
		/// Decodes the given 64b ASCII string to  ASCII string 
		/// </summary>
		public static string DecodeFrom64(this string _this)
		{
			var base64EncodedBytes = Convert.FromBase64String(_this);
			return Encoding.UTF8.GetString(base64EncodedBytes);
		}
	}
