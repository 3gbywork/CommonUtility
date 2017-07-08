﻿using CommonUtility.Security;
using System;
using System.Text;
using System.Web;

namespace CommonUtility.Extension
{
    public static class StringExtension
    {
        #region Encoding
        public static string ToBase64String(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value).ToBase64String();
        }

        public static string ToBase64UrlString(this string value, Encoding encoding)
        {
            return encoding.GetBytes(value).ToBase64UrlString();
        }

        public static string UrlEncode(this string value, Encoding encoding)
        {
            return HttpUtility.UrlEncode(value, encoding);
        }

        public static string HexEncode(this string value, Encoding encoding)
        {
            return value.HexEncode(encoding, true);
        }

        public static string HexEncode(this string value, Encoding encoding, bool removeHyphen)
        {
            return encoding.GetBytes(value).ToString(removeHyphen);
        }

        public static string ToByteArray(this string value, Encoding encoding)
        {
            return value.ToByteArray(encoding, " 0x{0:X2},").TrimStart(' ').TrimEnd(',');
        }

        public static string ToByteArray(this string value, Encoding encoding, string format)
        {
            var bytes = encoding.GetBytes(value);
            StringBuilder result = new StringBuilder();
            foreach (var item in bytes)
            {
                result.AppendFormat(format, item);
            }
            return result.ToString();
        }

        public static string HtmlEncode(this string value)
        {
            return HttpUtility.HtmlEncode(value);
        }

        public static string JavaScriptStringEncode(this string value)
        {
            return HttpUtility.JavaScriptStringEncode(value);
        }

        public static Guid ToGuid(this string value)
        {
            return value.ToGuid(Encoding.UTF8);
        }

        public static Guid ToGuid(this string value, Encoding encoding)
        {
            return new Guid(new Cryptography().SetHashAlgorithm(CryptoServiceProviderType.MD5).SetEncoding(encoding).ComputeHash(value).ToString(true));
        }
        #endregion

        #region Decoding
        public static byte[] FromBase64String(this string value)
        {
            return System.Convert.FromBase64String(value);
        }

        public static byte[] FromBase64UrlString(this string value)
        {
            value = value.Replace('-', '+').Replace('_', '/');
            switch (value.Length % 4)
            {
                case 0:
                    break;
                case 2:
                    value += "==";
                    break;
                case 3:
                    value += "=";
                    break;
                default:
                    throw new Exception("Invalid length for a base64 string.");
            }

            return value.FromBase64String();
        }

        public static string UrlDecode(this string value, Encoding encoding)
        {
            return HttpUtility.UrlDecode(value, encoding);
        }

        public static string HexDecode(this string value, Encoding encoding)
        {
            if (value.Length % 2 == 1)
            {
                throw new ArgumentException("Invalid length for a hex encode string.");
            }
            var length = value.Length / 2;
            byte[] result = new byte[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = System.Convert.ToByte(value.Substring(i * 2, 2), 16);
            }

            return result.ToString(encoding);
        }

        public static string HtmlDecode(this string value)
        {
            return HttpUtility.HtmlDecode(value);
        }
        #endregion
    }
}