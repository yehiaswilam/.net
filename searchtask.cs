using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp12
{
    internal class searchtask
    {
   

class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Enter numbers separated by spaces: ");

                string input = Console.ReadLine();

                string[] numbers = input.Split(' ');

                List<int> numbersList = new List<int>();

                foreach (string number in numbers)
                {
                    int num = int.Parse(number);

                    if (numbersList.Contains(num))
                    {
                        throw new Exception("Duplicate number found: " + num);
                    }

                    numbersList.Add(num);
                }

                Console.WriteLine("No duplicate numbers found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }


            try
            {
                Console.Write("\nEnter a string: ");

                string text = Console.ReadLine();

                CheckVowels(text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        static void CheckVowels(string text)
        {
            bool hasVowel = false;

            foreach (char c in text.ToLower())
            {
                if (c == 'a' ||
                    c == 'e' ||
                    c == 'i' ||
                    c == 'o' ||
                    c == 'u')
                {
                    hasVowel = true;
                    break;
                }
            }

            if (!hasVowel)
            {
                throw new Exception("The string does not contain any vowels.");
            }

            Console.WriteLine("The string contains vowels.");
        }
    }
}
}
