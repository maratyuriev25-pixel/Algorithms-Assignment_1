using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace Algorithm_Assignment_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Tokens t = new Tokens();
            Console.WriteLine("Welcome! What do you want to calculate?");
            while (true)
            {
                string userInput = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(userInput)) continue;

                try
                {
                    if (userInput.Contains('='))    
                    {
                        t.SetVariable(userInput);
                    }
                    else
                    {
                        Queue postfix = t.ToPostFix(userInput);
                        float result = t.PostFixCalculation(postfix);
                        Console.WriteLine("Postfix form: " + string.Join("", postfix.ToArray()));
                        Console.WriteLine($"Result is: {t.PostFixCalculation(postfix)}");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("error: " + exception.Message);
                }
            }
        }
    }
}
