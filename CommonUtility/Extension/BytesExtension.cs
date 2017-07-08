﻿using System;
using System.Text;

namespace CommonUtility.Extension
{
    public static class BytesExtension
    {
        /// <summary>
        /// 将bytes转换为string
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="removeHyphen">是否移除连字符</param>
        /// <returns></returns>
        public static string ToString(this byte[] bytes, bool removeHyphen)
        {
            var result = BitConverter.ToString(bytes);
            return removeHyphen ? result.Replace("-", string.Empty) : result;
        }

        public static string ToString(this byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        public static string ToBase64String(this byte[] bytes)
        {
            return System.Convert.ToBase64String(bytes);
        }

        public static string ToBase64UrlString(this byte[] bytes)
        {
            var value = bytes.ToBase64String();
            return value.TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

    }
}
