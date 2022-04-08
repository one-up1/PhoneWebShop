using PhoneWebShop.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PhoneWebShop.Console
{
    public class TextFormatter
    {

        private static readonly Dictionary<int, ConsoleColor> colors = new Dictionary<int, ConsoleColor>()
        {
            {0, ConsoleColor.White},
            {1, ConsoleColor.Red},
            {2, ConsoleColor.Green},
            {3, ConsoleColor.Blue},
            {4, ConsoleColor.Cyan},
            {5, ConsoleColor.DarkBlue},
            {6, ConsoleColor.DarkCyan},
            {7, ConsoleColor.DarkGray},
            {8, ConsoleColor.DarkGreen}
        };

        /// <summary>
        /// Takes a text with color codes and prints it with color.
        /// </summary>
        /// <param name="text">The text it will print, including color codes</param>
        /// <param name="endOfLine">if true: will add a linebreak at the end of text, if false: it will not.</param>
        public static void PrintColoredText(string text, bool endOfLine = true)
        {
            string[] sentenceParts = text.Split("$");
            foreach (var sentencePart in sentenceParts)
            {
                try
                {
                    if (sentencePart.Length == 0)
                        continue;
                    // Print word in color
                    int colorId = Convert.ToInt32(sentencePart[0].ToString());
                    System.Console.ForegroundColor = colors[colorId];
                    System.Console.Write(sentencePart.Substring(1));
                }
                catch (FormatException)
                {
                    System.Console.Write(sentencePart);
                }
            }

            if (endOfLine)
                System.Console.WriteLine();
            // Reset color after printing
            System.Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Takes a text as input and adds linebreaks every N characters
        /// </summary>
        /// <param name="input">The text to add linebreaks to</param>
        /// <param name="lineWidth">The amount of characters per line</param>
        /// <returns>The text with linebreaks in it</returns>
        public static string AddLineBreaks(string input, int lineWidth)
        {
            string[] words = input.Split(' ');
            var lengthCounter = 0;
            string output = "";
            foreach (var word in words)
            {
                // + 1 because of space
                lengthCounter += word.Length + 1;
                if (lengthCounter > lineWidth)
                {
                    lengthCounter = word.Length + 1;
                    output += $"\n{word} ";
                }
                else
                {
                    output += $"{word} ";
                }
            }
            return output;
        }

        /// <summary>
        /// Print a list of all phones, formatted according to a format string.
        /// </summary>
        /// <param name="phones">The list of phones to display.</param>
        /// <param name="format">The format to use when displaying. Formatted: {0} = index, {1} = brand, {2} = type</param>
        /// <param name="indexIdPairs">A reference to a dictionary in which the indices are stored together with phone IDs.
        /// This will clear the dictionary before filling it.</param>
        public static void PrintListOfPhones(List<Phone> phones, string format, Dictionary<int, int> indexIdPairs)
        {
            int indexCounter = 1;
            indexIdPairs.Clear();
            foreach (var phone in phones)
            {
                indexIdPairs.Add(indexCounter, phone.Id);
                PrintColoredText(string.Format(format, indexCounter, phone.Brand.Name, phone.Type));
                indexCounter++;
            }
        }
    }
}
