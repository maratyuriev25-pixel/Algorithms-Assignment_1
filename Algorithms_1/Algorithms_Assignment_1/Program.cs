using System;
using System.Collections.Generic;
namespace Algorithm_Assignment_1
{
    class Program
    {
        static void Main(string[] args)
        { 
            
            Console.WriteLine("Welcome! What do you want to calculate?");
            string userInput = Console.ReadLine();

            Tokens t = new Tokens();

            Queue postFix = t.ToPostFix(userInput);
            char[] arr = postFix.ToArray();
            string postfixString = new string(arr);
            Console.WriteLine($"Postfix form: {postfixString}");
        }
    }
}
