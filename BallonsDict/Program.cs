using System;
using System.Collections.Generic;
using System.Linq;

namespace BalloonString
{
    class Program
    {
        private static string userWord;
        private static int fullWordCount;
        private static int token;
        private static int proccessedCorrectChars;
        private static bool shouldContinue;
        private static Dictionary<string, string> processedstrings;

        static void Main(string[] args)
        {
            Console.WriteLine("Choose a word for which to search for: ");
            userWord = UserInput();
            Console.WriteLine("Choose a BIG word in which to search from:  ");
            string inputBigWord = UserInput();
            ProcessString(inputBigWord);
            Console.WriteLine($"There's that much '{userWord}' in '{inputBigWord}' - {fullWordCount} times");

        }

        private static string UserInput()
        {
            string userinput = Console.ReadLine();
            return userinput;
        }

        static void ProcessString(string input)
        {
            shouldContinue = true;
            var charInput = Diktantas(input);
            var charDiktantas = Diktantas(userWord);

            while (shouldContinue)
            {
                for (int i = 0; i < charDiktantas.Count; i++)
                {
                    var targetValue = charDiktantas[i];

                    var contains = charInput
                        .Where(x => x.Value == targetValue)
                        .Select(e => (KeyValuePair<int, char>?)e)
                        .FirstOrDefault();

                    if (contains != null)
                    {
                        charInput.Remove(contains.Value.Key);
                        proccessedCorrectChars++;
                        if (proccessedCorrectChars == charDiktantas.Count)
                        {
                            fullWordCount++;
                            proccessedCorrectChars = 0;
                            break;
                        }
                    }

                    if (proccessedCorrectChars == 0)
                        shouldContinue = false;
                }
            }
        }

        static Dictionary<int, char> Diktantas(string input)
        {
            token = 0;
            var inputChars = input.ToCharArray();
            Dictionary<int, char> dict = new Dictionary<int, char>();
            inputChars.ToList().ForEach(x =>
            {
                dict.Add(token, x);
                token++;
            }
            );
            return dict;
        }
    }
}
