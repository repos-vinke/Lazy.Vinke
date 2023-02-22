// LazyConvert.cs
//
// This file is integrated part of Lazy project
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2020, December 10

using System;
using System.Xml;
using System.Data;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;

namespace Lazy.Vinke
{
    public static class LazyConvert
    {
        /// <summary>
        /// Try convert an Object value to Int16 value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <returns>The converted Int16 value</returns>
        public static Int16 ToInt16(Object value)
        {
            return Convert.ToInt16(value);
        }

        /// <summary>
        /// Try convert an Object value to Int16 value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <param name="failValue">The Int16 value to return if fail to convert</param>
        /// <param name="nullAsZero">Indicate if null value will be considered as zero</param>
        /// <returns>The converted Int16 value or the fail Int16 value</returns>
        public static Int16 ToInt16(Object value, Int16 failValue, Boolean nullAsZero = false)
        {
            if (value == null)
                return nullAsZero == true ? (Int16)0 : failValue;

            try { return Convert.ToInt16(value); }
            catch { return failValue; }
        }

        /// <summary>
        /// Try convert an Object value to Int32 value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <returns>The converted Int32 value</returns>
        public static Int32 ToInt32(Object value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Try convert an Object value to Int32 value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <param name="failValue">The Int32 value to return if fail to convert</param>
        /// <param name="nullAsZero">Indicate if null value will be considered as zero</param>
        /// <returns>The converted Int32 value or the fail Int32 value</returns>
        public static Int32 ToInt32(Object value, Int32 failValue, Boolean nullAsZero = false)
        {
            if (value == null)
                return nullAsZero == true ? 0 : failValue;

            try { return Convert.ToInt32(value); }
            catch { return failValue; }
        }

        /// <summary>
        /// Try convert an Object value to Int64 value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <returns>The converted Int64 value</returns>
        public static Int64 ToInt64(Object value)
        {
            return Convert.ToInt64(value);
        }

        /// <summary>
        /// Try convert an Object value to Int64 value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <param name="failValue">The Int64 value to return if fail to convert</param>
        /// <param name="nullAsZero">Indicate if null value will be considered as zero</param>
        /// <returns>The converted Int64 value or the fail Int64 value</returns>
        public static Int64 ToInt64(Object value, Int64 failValue, Boolean nullAsZero = false)
        {
            if (value == null)
                return nullAsZero == true ? 0 : failValue;

            try { return Convert.ToInt64(value); }
            catch { return failValue; }
        }

        /// <summary>
        /// Try convert an Object value to Float value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <returns>The converted Float value</returns>
        public static float ToFloat(Object value)
        {
            return (float)Convert.ToDouble(value);
        }

        /// <summary>
        /// Try convert an Object value to Float value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <param name="failValue">The Float value to return if fail to convert</param>
        /// <param name="nullAsZero">Indicate if null value will be considered as zero</param>
        /// <returns>The converted Float value or the fail Float value</returns>
        public static float ToFloat(Object value, float failValue, Boolean nullAsZero = false)
        {
            if (value == null)
                return nullAsZero == true ? 0.0f : failValue;

            try { return (float)Convert.ToDouble(value); }
            catch { return failValue; }
        }

        /// <summary>
        /// Try convert an Object value to Double value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <returns>The converted Double value</returns>
        public static Double ToDouble(Object value)
        {
            return Convert.ToDouble(value);
        }

        /// <summary>
        /// Try convert an Object value to Double value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <param name="failValue">The Double value to return if fail to convert</param>
        /// <param name="nullAsZero">Indicate if null value will be considered as zero</param>
        /// <returns>The converted Double value or the fail Double value</returns>
        public static Double ToDouble(Object value, Double failValue, Boolean nullAsZero = false)
        {
            if (value == null)
                return nullAsZero == true ? 0.0d : failValue;

            try { return Convert.ToDouble(value); }
            catch { return failValue; }
        }

        /// <summary>
        /// Try convert an Object value to Decimal value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <returns>The converted Decimal value</returns>
        public static Decimal ToDecimal(Object value)
        {
            return Convert.ToDecimal(value);
        }

        /// <summary>
        /// Try convert an Object value to Decimal value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <param name="failValue">The Decimal value to return if fail to convert</param>
        /// <param name="nullAsZero">Indicate if null value will be considered as zero</param>
        /// <returns>The converted Decimal value or the fail Decimal value</returns>
        public static Decimal ToDecimal(Object value, Decimal failValue, Boolean nullAsZero = false)
        {
            if (value == null)
                return nullAsZero == true ? 0.0m : failValue;

            try { return Convert.ToDecimal(value); }
            catch { return failValue; }
        }

        /// <summary>
        /// Try convert an Object value to Char value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <returns>The converted Char value</returns>
        public static Char ToChar(Object value)
        {
            return Convert.ToChar(value);
        }

        /// <summary>
        /// Try convert an Object value to Char value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <param name="failValue">The Char value to return if fail to convert</param>
        /// <returns>The converted Char value or the fail Char value</returns>
        public static Char ToChar(Object value, Char failValue)
        {
            try { return Convert.ToChar(value); }
            catch { return failValue; }
        }

        /// <summary>
        /// Try convert an Object value to String value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <returns>The converted String value</returns>
        public static String ToString(Object value)
        {
            return Convert.ToString(value);
        }

        /// <summary>
        /// Try convert an Object value to String value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <param name="failValue">The String value to return if fail to convert</param>
        /// <returns>The converted String value or the fail String value</returns>
        public static String ToString(Object value, String failValue)
        {
            try { return Convert.ToString(value); }
            catch { return failValue; }
        }

        /// <summary>
        /// Try convert an Object value to DateTime value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <returns>The converted DateTime value</returns>
        public static DateTime ToDateTime(Object value)
        {
            return Convert.ToDateTime(value);
        }

        /// <summary>
        /// Try convert an Object value to DateTime value
        /// </summary>
        /// <param name="value">The Object value to convert</param>
        /// <param name="failValue">The DateTime value to return if fail to convert</param>
        /// <returns>The converted DateTime value or the fail DateTime value</returns>
        public static DateTime ToDateTime(Object value, DateTime failValue)
        {
            try { return Convert.ToDateTime(value); }
            catch { return failValue; }
        }

        /// <summary>
        /// Try convert an String value to Base64String value
        /// </summary>
        /// <param name="value">The String value to convert</param>
        /// <returns>The converted Base64String value</returns>
        public static String ToBase64String(String value)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(value));
        }

        /// <summary>
        /// Try convert an String value to Base64String value
        /// </summary>
        /// <param name="value">The String value to convert</param>
        /// <param name="failValue">The Base64String value to return if fail to convert</param>
        /// <returns>The converted Base64String value or the fail Base64String value</returns>
        public static String ToBase64String(String value, String failValue)
        {
            try { return Convert.ToBase64String(Encoding.Default.GetBytes(value)); }
            catch { return failValue; }
        }

        /// <summary>
        /// Try convert an String value to Base64String value
        /// </summary>
        /// <param name="value">The String value to convert</param>
        /// <returns>The converted Base64String value</returns>
        public static String ToBase64String(String value, Encoding encoding)
        {
            return Convert.ToBase64String(encoding.GetBytes(value));
        }

        /// <summary>
        /// Try convert an String value to Base64String value
        /// </summary>
        /// <param name="value">The String value to convert</param>
        /// <param name="failValue">The Base64String value to return if fail to convert</param>
        /// <returns>The converted Base64String value or the fail Base64String value</returns>
        public static String ToBase64String(String value, Encoding encoding, String failValue)
        {
            try { return Convert.ToBase64String(encoding.GetBytes(value)); }
            catch { return failValue; }
        }

        /// <summary>
        /// Try convert an String value from Base64String value
        /// </summary>
        /// <param name="value">The Base64String value to convert</param>
        /// <returns>The converted String value</returns>
        public static String FromBase64String(String value)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(value));
        }

        /// <summary>
        /// Try convert an String value from Base64String value
        /// </summary>
        /// <param name="value">The Base64String value to convert</param>
        /// <param name="failValue">The String value to return if fail to convert</param>
        /// <returns>The converted String value or the fail String value</returns>
        public static String FromBase64String(String value, String failValue)
        {
            try { return Encoding.Default.GetString(Convert.FromBase64String(value)); }
            catch { return failValue; }
        }

        /// <summary>
        /// Try convert an String value from Base64String value
        /// </summary>
        /// <param name="value">The Base64String value to convert</param>
        /// <param name="encoding">The encoding used to convert the Base64String</param>
        /// <returns>The converted String value</returns>
        public static String FromBase64String(String value, Encoding encoding)
        {
            return encoding.GetString(Convert.FromBase64String(value));
        }

        /// <summary>
        /// Try convert an String value from Base64String value
        /// </summary>
        /// <param name="value">The Base64String value to convert</param>
        /// <param name="encoding">The encoding used to convert the Base64String</param>
        /// <param name="failValue">The String value to return if fail to convert</param>
        /// <returns>The converted String value or the fail String value</returns>
        public static String FromBase64String(String value, Encoding encoding, String failValue)
        {
            try { return encoding.GetString(Convert.FromBase64String(value)); }
            catch { return failValue; }
        }
    }
}