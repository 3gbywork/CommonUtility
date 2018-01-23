﻿using System;
using System.Linq;
using System.Reflection;

namespace CommonUtility.Convert
{

    public class Converter
    {
        #region Supported types
        // <see cref="bool"/>
        // <see cref="char"/>
        // <see cref="sbyte"/>
        // <see cref="byte"/>
        // <see cref="short"/>
        // <see cref="ushort"/>
        // <see cref="int"/>
        // <see cref="uint"/>
        // <see cref="long"/>
        // <see cref="ulong"/>
        // <see cref="float"/>
        // <see cref="double"/>
        // <see cref="decimal"/>
        // <see cref="DateTime"/>
        #endregion

        /// <summary>
        /// Converts a string to a value of the specified type,
        /// supporting only basic type conversions
        /// </summary>
        /// <typeparam name="T">Supported types</typeparam>
        /// <param name="value">Value that needs to be converted</param>
        /// <returns>Converted or default values</returns>
        public static T TryParse<T>(string value)
        {
            return TryParse(value, default(T));
        }

        /// <summary>
        /// Converts a string to a value of the specified type,
        /// supporting only basic type conversions
        /// </summary>
        /// <typeparam name="T">Supported types</typeparam>
        /// <param name="value">Value that needs to be converted</param>
        /// <param name="defaultValue">Default value</param>
        /// <returns>Converted or default values</returns>
        public static T TryParse<T>(string value, T defaultValue)
        {
            if (value == null)
            {
                return defaultValue;
            }
            Type type = typeof(T);
            // IsGenericType?
            if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }
            // For string or object types,just return the value
            var noTryParseTypes = new string[] { "string", "object" };
            if (noTryParseTypes.Any(typeName => type.Name.ToLower().Equals(typeName)))
            {
                if (string.IsNullOrEmpty(value))
                {
                    return defaultValue;
                }
                return (T)(object)value;
            }

            // Call TryParse convert the value to the specified type
            var TryParse = type.GetMethod("TryParse", BindingFlags.Static | BindingFlags.Public,
                Type.DefaultBinder, new Type[] { typeof(string), type.MakeByRefType() },
                new ParameterModifier[] { new ParameterModifier(2) });
            var parameters = new object[] { value, Activator.CreateInstance(type) };
            if ((bool)TryParse.Invoke(null, parameters))
            {
                return (T)parameters[1];
            }

            return defaultValue;
        }
    }
}
