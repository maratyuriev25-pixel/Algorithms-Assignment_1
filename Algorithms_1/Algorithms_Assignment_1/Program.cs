using System;
using System.Collections.Generic;
namespace Algorithm_Assignment_1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome! What do you want to calculate?");
                string userInput = Console.ReadLine();

                Tokens t = new Tokens();

                Queue postFix = t.ToPostFixCalc(userInput);
                var result = t.ToPostFixView(userInput);

                Console.WriteLine("Postfix form: " + string.Join(" ", result));
                Console.WriteLine($"Result is: {t.PostFixCalculation(postFix)}");
            }
        }
    }
}
