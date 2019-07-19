using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TestP6WebServices.P6Api
{
    /// <summary>
    /// Class ExtensionMethods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Count the number of words in a string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static int WordCount(this string input)
        {
            return input.Split(new[] { ' ', '.', '?' },
                               StringSplitOptions.RemoveEmptyEntries).Length;
        }

        /// <summary>
        /// Almosts the equal floats.
        /// </summary>
        /// <param name="dVal1">The d val1.</param>
        /// <param name="dVal2">The d val2.</param>
        /// <param name="dTmp">The d TMP.</param>
        /// <returns>Boolean.</returns>
        public static Boolean AlmostEqualFloats(this double dVal1, double dVal2, double dTmp)
        {
            var bResult = (((dVal2 - dTmp) < dVal1) && (dVal1 < (dVal2 + dTmp)));
            return bResult;
        }

        /// <summary>
        /// Formats the with.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentNullException">format</exception>
        public static string FormatWith(this string format, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            return string.Format(format, args);
        }

        /// <summary>
        /// Formats the with.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="args">The args.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentNullException">format</exception>
        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            return string.Format(provider, format, args);
        }

        /// <summary>
        /// Determines whether [contains] [the specified source].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="toCheck">To check.</param>
        /// <param name="comp">The comp.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified source]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            if (string.IsNullOrEmpty(toCheck) || string.IsNullOrEmpty(source))
                return false;

            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// Converts a String to an Int32?
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static Int32? ToInt(this string input)
        {
            int val;
            if (int.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Converts a string to a DateTime?
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static DateTime? ToDate(this string input)
        {
            DateTime val;
            if (DateTime.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Converts a string to a Decimal?.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static Decimal? ToDecimal(this string input)
        {
            decimal val;
            if (decimal.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Converts a string to a Double?.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static Double? ToDouble(this string input)
        {
            Double val;
            if (Double.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Determines whether the compareTo string [is equal to] [the specified input string].
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="compareTo">The string to compare to.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>
        ///   <c>true</c> if [is equal to] [the specified string]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEqualTo(this String input, String compareTo, Boolean ignoreCase)
        {
            return string.Compare(input, compareTo, ignoreCase) == 0;
        }

        /// <summary>
        /// Determines whether the specified input string is empty.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the specified input is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEmpty(this String input)
        {
            return (input != null && input.Length == 0);
        }

        /// <summary>
        /// Determines whether the specified input is null.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>
        ///   <c>true</c> if the specified input string is null; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNull(this String input)
        {
            return (input == null);
        }

        /// <summary>
        /// Determines whether the input string [is null or empty].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified input]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNullOrEmpty(this String input)
        {
            return String.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Determines whether the input string [is not null or empty].
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>
        ///   <c>true</c> if [is not null or empty] [the specified input string]; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNotNullOrEmpty(this String input)
        {
            return !String.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Gets a CLS complaint string based on the input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static String GetClsString(this String input)
        {
            return Regex.Replace(input.Trim(), @"[\W]", @"_");
        }

        /// <summary>
        /// Gets the name of the month.
        /// </summary>
        /// <param name="monthNumber">The month number.</param>
        /// <returns></returns>
        public static String GetMonthName(Int32 monthNumber)
        {
            return GetMonthName(monthNumber, false);
        }

        /// <summary>
        /// Gets the name of the month.
        /// </summary>
        /// <param name="monthNumber">The month number.</param>
        /// <param name="abbreviateMonth">if set to <c>true</c> [abbreviate month name].</param>
        /// <returns></returns>
        public static String GetMonthName(Int32 monthNumber, Boolean abbreviateMonth)
        {
            if (monthNumber < 1 || monthNumber > 12)
                throw new ArgumentOutOfRangeException("monthNumber");
            var date = new DateTime(1, monthNumber, 1);
            return abbreviateMonth ? date.ToString("MMM") : date.ToString("MMMM");
        }

        /// <summary>
        /// Get the Rows in a list for a the table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public static List<DataRow> RowList(this DataTable table)
        {
            return table.Rows.Cast<DataRow>().ToList();
        }

        /// <summary>
        /// Gets the field item frp a row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public static object GetItem(this DataRow row, String field)
        {
            return !row.Table.Columns.Contains(field) ? null : row[field];
        }

        /// <summary>
        /// Filters the rows to a list.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public static List<DataRow> FilterRowsToList(this DataTable table, List<Int32> ids, String fieldName)
        {
            Func<DataRow, bool> filter = row => ids.Contains((Int32)row.GetItem(fieldName));
            return table.RowList().Where(filter).ToList();
        }

        /// <summary>
        /// Filters the rows to a list.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public static List<DataRow> FilterRowsToList(this DataTable table, List<String> ids, String fieldName)
        {
            Func<DataRow, bool> filter = row => ids.Contains((String)row.GetItem(fieldName));
            return table.RowList().Where(filter).ToList();
        }

        /// <summary>
        /// Filters the rows to a data table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public static DataTable FilterRowsToDataTable(this DataTable table, List<Int32> ids, String fieldName)
        {
            DataTable filteredTable = table.Clone();
            List<DataRow> matchingRows = FilterRowsToList(table, ids, fieldName);

            foreach (DataRow filteredRow in matchingRows)
            {
                filteredTable.ImportRow(filteredRow);
            }
            return filteredTable;
        }

        /// <summary>
        /// Filters the rows to a data table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public static DataTable FilterRowsToDataTable(this DataTable table, List<String> ids, String fieldName)
        {
            DataTable filteredTable = table.Clone();
            List<DataRow> matchingRows = FilterRowsToList(table, ids, fieldName);

            foreach (DataRow filteredRow in matchingRows)
            {
                filteredTable.ImportRow(filteredRow);
            }
            return filteredTable;
        }

        /// <summary>
        /// Selects the into table the rows base on the filter.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static DataTable SelectIntoTable(this DataTable table, String filter)
        {
            DataTable selectResults = table.Clone();
            DataRow[] rows = table.Select(filter);
            foreach (DataRow row in rows)
            {
                selectResults.ImportRow(row);
            }
            return selectResults;
        }

        /// <summary>
        /// Deletes rows from the table base on the filter.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static DataTable Delete(this DataTable table, string filter)
        {
            table.Select(filter).Delete();
            return table;
        }

        /// <summary>
        /// Deletes the specified rows.
        /// </summary>
        /// <param name="rows">The rows.</param>
        public static void Delete(this IEnumerable<DataRow> rows)
        {
            foreach (DataRow row in rows)
                row.Delete();
        }

        /// <summary>
        /// Gets the or add value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dict">The dict.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static TValue GetOrAddValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key)
                where TValue : new()
        {
            TValue value;
            if (dict.TryGetValue(key, out value))
                return value;
            value = new TValue();
            dict.Add(key, value);
            return value;
        }

        /// <summary>
        /// Gets the or add value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dict">The dict.</param>
        /// <param name="key">The key.</param>
        /// <param name="generator">The generator.</param>
        /// <returns></returns>
        public static TValue GetOrAddValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> generator)
        {
            TValue value;
            if (dict.TryGetValue(key, out value))
                return value;
            value = generator();
            dict.Add(key, value);
            return value;
        }

        /// <summary>
        /// Determines whether the specified enumerable has items.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>
        ///   <c>true</c> if the specified enumerable has items; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasItems(this IEnumerable enumerable)
        {
            if (enumerable == null) return false;
            try
            {
                var enumerator = enumerable.GetEnumerator();
                if (enumerator.MoveNext()) return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified enumerable is empty.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>
        ///   <c>true</c> if the specified enumerable is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEmpty(this IEnumerable enumerable)
        {
            return !enumerable.HasItems();
        }

        /// <summary>
        /// Removes the invalid chars.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static String RemoveInvalidChars(this String fileName)
        {
            var regex = String.Format("[{0}]", Regex.Escape(new String(Path.GetInvalidFileNameChars())));
            var removeInvalidChars = new Regex(regex,
                                               RegexOptions.Singleline | RegexOptions.Compiled |
                                               RegexOptions.CultureInvariant);
            return removeInvalidChars.Replace(fileName, "");
        }
    }
}