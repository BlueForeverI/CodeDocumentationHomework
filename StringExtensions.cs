namespace Telerik.ILS.Common
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Contains extensin methods for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Calculates MD5 hash for a given string
        /// </summary>
        /// <param name="input">The string to generate hash for</param>
        /// <returns>Generated MD5 hash</returns>
        /// <example>
        /// This sample shows call to call the <see cref="ToMd5Hash"/> method
        /// <code>
        /// string hash = "Hello World!.ToMd5Hash();
        /// </code>
        /// </example>
        public static string ToMd5Hash(this string input)
        {
            var md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new StringBuilder to collect the bytes
            // and create a string.
            var builder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return builder.ToString();
        }

        /// <summary>
        /// Converts a string value to a boolean value
        /// </summary>
        /// <param name="input">String to convert</param>
        /// <returns>Boolean representation of the string</returns>
        /// <example>
        /// This sample shows call to call the <see cref="ToBoolean"/> method
        /// <code>
        /// bool isFemale = "true".ToBoolean();
        /// </code>
        /// </example>
        public static bool ToBoolean(this string input)
        {
            var stringTrueValues = new[] { "true", "ok", "yes", "1", "да" };

            return stringTrueValues.Contains(input.ToLower());
        }

        /// <summary>
        /// Parses a short value from a string
        /// </summary>
        /// <param name="input">The string to read from</param>
        /// <returns>Extracted 'short' value</returns>
        /// <example>
        /// This sample shows call to call the <see cref="ToShort"/> method
        /// <code>
        /// short days = "365".ToShort();
        /// </code>
        /// </example>
        public static short ToShort(this string input)
        {
            short shortValue;
            short.TryParse(input, out shortValue);

            return shortValue;
        }

        /// <summary>
        /// Parses a integer value from a string
        /// </summary>
        /// <param name="input">The string to read from</param>
        /// <returns>Extracted 'int' value</returns>
        /// <example>
        /// This sample shows call to call the <see cref="ToInteger"/> method
        /// <code>
        /// int peopleCount = "1234567".ToInteger();
        /// </code>
        /// </example>
        public static int ToInteger(this string input)
        {
            int integerValue;
            int.TryParse(input, out integerValue);

            return integerValue;
        }

        /// <summary>
        /// Parse long value from a string
        /// </summary>
        /// <param name="input">The string to read from</param>
        /// <returns>Extracted 'long' value</returns>
        /// <example>
        /// This sample shows call to call the <see cref="ToLong"/> method
        /// <code>
        /// long totalWeightInGrams = "3498725178".ToLong();
        /// </code>
        /// </example>
        public static long ToLong(this string input)
        {
            long longValue;
            long.TryParse(input, out longValue);

            return longValue;
        }

        /// <summary>
        /// Convert a string to a DateTime 
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <returns>DateTime representation of the string</returns>
        /// <example>
        /// This sample shows call to call the <see cref="ToDateTime"/> method
        /// <code>
        /// DateTime birthDay = "12.04.2013 г.".ToDateTime();
        /// </code>
        /// </example>
        public static DateTime ToDateTime(this string input)
        {
            DateTime dateTimeValue;
            DateTime.TryParse(input, out dateTimeValue);

            return dateTimeValue;
        }

        /// <summary>
        /// Makes the first letter of a strign in CAPITAL_CASE
        /// </summary>
        /// <param name="input">The string to capitalize</param>
        /// <returns>The same string, but with capitalized first letter</returns>
        /// <example>
        /// This sample shows call to call the <see cref="CapitalizeFirstLetter"/> method
        /// <code>
        /// string name = "pesho".CapitalizeFirstLetter();
        /// </code>
        /// </example>
        public static string CapitalizeFirstLetter(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // return the string with capitalized first letter
            return input.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + 
                input.Substring(1, input.Length - 1);
        }

        /// <summary>
        /// Return a strign that is placed between two other strings
        /// </summary>
        /// <param name="input">The whole string</param>
        /// <param name="startString">The string to start extracting from</param>
        /// <param name="endString">The string to end extracting</param>
        /// <param name="startFrom">Index to start searching from (default is 0)</param>
        /// <returns>
        /// The string that is between the start and end strings, 
        /// and is after the index
        /// </returns>
        /// <example>
        /// This sample shows call to call the <see cref="GetStringBetween"/> method
        /// <code>
        /// string anchorContent = "<a>Hello</a>".GetStringBetween("<a>", "</a>");
        /// </code>
        /// </example>
        public static string GetStringBetween(this string input, string startString, 
            string endString, int startFrom = 0)
        {
            // search only in the remaining string after the 'startFrom' index
            input = input.Substring(startFrom);
            startFrom = 0;

            if (!input.Contains(startString) || !input.Contains(endString))
            {
                return string.Empty;
            }

            // calculate teh startPosition by finding the index of the startString and
            // adding the length of the startString
            var startPosition = 
                input.IndexOf(startString, startFrom, StringComparison.Ordinal) + 
                startString.Length;

            if (startPosition == -1)
            {
                return string.Empty;
            }

            // calculate teh endPosition by finding the index of the endString and
            // adding the length of the endString
            var endPosition = input.IndexOf(endString, startPosition, StringComparison.Ordinal);

            if (endPosition == -1)
            {
                return string.Empty;
            }

            return input.Substring(startPosition, endPosition - startPosition);
        }

        /// <summary>
        /// Converts all cyrilic characters in a string to latin characters
        /// </summary>
        /// <param name="input">String to convert</param>
        /// <returns>A string with all cyrilic characters replaced with latin letters</returns>
        /// <example>
        /// This sample shows call to call the <see cref="ConvertCyrilicToLatinLetters"/> method
        /// <code>
        /// string latin = "Иванчо и Марийка отишли на училище".ConvertCyrilicToLatinLetters();
        /// </code>
        /// </example>
        public static string ConvertCyrillicToLatinLetters(this string input)
        {
            var bulgarianLetters = new[]
                                       {
                                           "а", "б", "в", "г", "д", "е", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п",
                                           "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ь", "ю", "я"
                                       };

            var latinRepresentationsOfBulgarianLetters = new[]
                                                             {
                                                                 "a", "b", "v", "g", "d", "e", "j", "z", "i", "y", "k",
                                                                 "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h",
                                                                 "c", "ch", "sh", "sht", "u", "i", "yu", "ya"
                                                             };

            // replace every letter from the bulgarian aplhabet with a latin symbol that
            // has the same index
            for (var i = 0; i < bulgarianLetters.Length; i++)
            {
                input = input.Replace(bulgarianLetters[i], 
                    latinRepresentationsOfBulgarianLetters[i]);

                input = input.Replace(bulgarianLetters[i].ToUpper(), 
                    latinRepresentationsOfBulgarianLetters[i].CapitalizeFirstLetter());
            }

            return input;
        }

        /// <summary>
        /// Converts all latin characters in a string to cyrilic characters
        /// </summary>
        /// <param name="input">String to convert</param>
        /// <returns>A string with all latin characters replaced with cyrilic letters</returns>
        /// <example>
        /// This sample shows call to call the <see cref="ConvertLatinToCyrilicKeyboard"/> method
        /// <code>
        /// string cyrilic = "Baj Pesho".ConvertLatinTOCyrilicKeyboard();
        /// </code>
        /// </example>
        public static string ConvertLatinToCyrillicKeyboard(this string input)
        {
            var latinLetters = new[]
                                   {
                                       "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
                                       "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
                                   };

            var bulgarianRepresentationOfLatinKeyboard = new[]
                                                             {
                                                                 "а", "б", "ц", "д", "е", "ф", "г", "х", "и", "й", "к",
                                                                 "л", "м", "н", "о", "п", "я", "р", "с", "т", "у", "ж",
                                                                 "в", "ь", "ъ", "з"
                                                             };

            // replace every letter from the latin aplhabet with a cyrilic symbol that
            // has the same index
            for (int i = 0; i < latinLetters.Length; i++)
            {
                input = input.Replace(latinLetters[i], 
                    bulgarianRepresentationOfLatinKeyboard[i]);

                input = input.Replace(latinLetters[i].ToUpper(), 
                    bulgarianRepresentationOfLatinKeyboard[i].ToUpper());
            }

            return input;
        }

        /// <summary>
        /// Converts a string to a valid username
        /// </summary>
        /// <param name="input">String to convert</param>
        /// <returns>A string that is a valid username</returns>
        /// <example>
        /// This sample shows call to call the <see cref="ToValidUsername"/> method
        /// <code>
        /// string username = "bai pesho-95".ToValidUsername();
        /// </code>
        /// </example>
        public static string ToValidUsername(this string input)
        {
            input = input.ConvertCyrillicToLatinLetters();

            // remove invalid characters and return converted string
            return Regex.Replace(input, @"[^a-zA-z0-9_\.]+", string.Empty);
        }

        /// <summary>
        /// Convers a string to a valid file name
        /// </summary>
        /// <param name="input">String to convert</param>
        /// <returns>A string that is a valid file name</returns>
        /// <example>
        /// This sample shows call to call the <see cref="ToValidLatinFilename"/> method
        /// <code>
        /// string fileName = "What is this?_?.html".ToValidLatinFileName();
        /// </code>
        /// </example>
        public static string ToValidLatinFileName(this string input)
        {
            input = input.Replace(" ", "-").ConvertCyrillicToLatinLetters();

            // remove invalid characters and return converted string
            return Regex.Replace(input, @"[^a-zA-z0-9_\.\-]+", string.Empty);
        }

        /// <summary>
        /// Gets the first cahrsCount characters from a string
        /// </summary>
        /// <param name="input">The string to get characters from</param>
        /// <param name="charsCount">Number of characters to get</param>
        /// <returns>The first charsCount characters as a string</returns>
        /// <example>
        /// This sample shows call to call the <see cref="GetFirstCharacters"/> method
        /// <code>
        /// string beginning = "Hello World!".GetFirstCharacters(5);
        /// </code>
        /// </example>
        public static string GetFirstCharacters(this string input, int charsCount)
        {
            return input.Substring(0, Math.Min(input.Length, charsCount));
        }

        /// <summary>
        /// Extracts the extension from a file name
        /// </summary>
        /// <param name="fileName">File name to extract from</param>
        /// <returns>The extension of the file</returns>
        /// <example>
        /// This sample shows call to call the <see cref="GetFileExtension"/> method
        /// <code>
        /// string extension = "file.html".GetFileExtension();
        /// </code>
        /// </example>
        public static string GetFileExtension(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return string.Empty;
            }

            string[] fileParts = fileName.Split(new[] { "." }, StringSplitOptions.None);

            // file name has no extension
            if (fileParts.Count() == 1 || string.IsNullOrEmpty(fileParts.Last()))
            {
                return string.Empty;
            }

            return fileParts.Last().Trim().ToLower();
        }

        /// <summary>
        /// Return the content type of a file
        /// based on the extension
        /// </summary>
        /// <param name="fileExtension">Exxtension of the file</param>
        /// <returns>The content type of the file</returns>
        /// <example>
        /// This sample shows call to call the <see cref="ToContentType"/> method
        /// <code>
        /// string contentType = "avatar.png".ToContentType();
        /// </code>
        /// </example>
        public static string ToContentType(this string fileExtension)
        {
            var fileExtensionToContentType = new Dictionary<string, string>
                                                 {
                                                     { "jpg", "image/jpeg" },
                                                     { "jpeg", "image/jpeg" },
                                                     { "png", "image/x-png" },
                                                     {
                                                         "docx",
                                                         "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                                                     },
                                                     { "doc", "application/msword" },
                                                     { "pdf", "application/pdf" },
                                                     { "txt", "text/plain" },
                                                     { "rtf", "application/rtf" }
                                                 };

            // extension is in the dictionary - return corresponding content type 
            if (fileExtensionToContentType.ContainsKey(fileExtension.Trim()))
            {
                return fileExtensionToContentType[fileExtension.Trim()];
            }

            // extension is not in the dictionary - return default content type
            return "application/octet-stream";
        }

        /// <summary>
        /// Convert a string to a byte array
        /// </summary>
        /// <param name="input">String to convert</param>
        /// <returns>The byte representation of the string</returns>
        /// <example>
        /// This sample shows call to call the <see cref="ToByteArray"/> method
        /// <code>
        /// byte[] buffer = "The quick brown fox".ToByteArray();
        /// </code>
        /// </example>
        public static byte[] ToByteArray(this string input)
        {
            var bytesArray = new byte[input.Length * sizeof(char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, bytesArray, 0, bytesArray.Length);

            return bytesArray;
        }
    }
}
