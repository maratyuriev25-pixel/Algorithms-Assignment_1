using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace Algorithm_Assignment_1;

public class Tokens
{
    private Queue postFix = new Queue();
    private OperatorStack opStack = new OperatorStack();
    private Dictionary<string, float> variables = new Dictionary<string, float>();

    public bool IsOperator(char i)
    {
        if ((i == '+') || (i == '-') || (i == '*') || (i == '/') || (i == ':') || (i == '^') || (i == 'u') || (i == 'c') || (i == 's'))
            return true;
        else
            return false;
    }

    private int Candidate(char op)
    {
        return op switch
        {
            'u' or 's' or 'c' => 4,
            '^' => 3,
            '*' or '/' or ':' => 2,
            '+' or '-' => 1,
            _ => 0
        };
    }
    private bool IsUnaryMinus(char token, int index, string infix)
    {
        if (token != '-') return false;
        if (index == 0) return true;    
        char prev = infix[index - 1];

        return IsOperator(prev) || prev == '(' || prev == '|';
    }
   

    public void processToken(char token)
    {

        if (char.IsDigit(token) == true)
        {
            postFix.Enqueue(token);
        }

        else if (token == '(')
        {
            opStack.Push(token);
        }

        else if (token == ')')
        {
            while (opStack.Count() > 0 && opStack.Peek() != '(')
            {
                postFix.Enqueue(opStack.Pop());
            }
            if(opStack.Count() > 0)
                opStack.Pop();

        }
        else if (token == '|')
        {
            if (opStack.Count() > 0 && opStack.Peek() == '|')
            {
                while (opStack.Count() > 0 && opStack.Peek() != '|')
                {
                    postFix.Enqueue(opStack.Pop());
                }

                opStack.Pop();
                postFix.Enqueue('|');
            }
            else
            {
                opStack.Push(token);
            }
        }

        else if (IsOperator(token) == true)
        {
            while (
                opStack.Count() > 0 &&
                opStack.Peek() != '(' &&
                opStack.Peek() != '|' &&
                (
                    Candidate(opStack.Peek()) > Candidate(token) ||
                    (Candidate(opStack.Peek()) == Candidate(token) && token != '^' && token != 'u')
                )
            )
            {
                postFix.Enqueue(opStack.Pop());
            }
            opStack.Push(token);
        }

    }

    public void SetVariable(string input)
    {
        string[] parts = input.Split('=');
        if (parts.Length != 2)
            throw new Exception("Invalid variable assignment. Please use the format: variable = value");

        string name = parts[0].Trim();
        string expression = parts[1].Trim();

        Queue postfix = ToPostFix(expression);
        float value = PostFixCalculation(postfix);

        variables[name] = value;
        Console.WriteLine($"{name} = {value}");
    }



    public Queue ToPostFix(string infix)
    {

        postFix = new Queue();
        opStack = new OperatorStack();
        string number = "";

        infix = infix.Replace("sin", "s").Replace("cos", "c");

        for (int i = 0; i <  infix.Length; i++)
        {
            char token = infix[i];

            if(IsUnaryMinus(token, i, infix))
            {
                opStack.Push('u');
                continue;
            }

            if (char.IsLetter(token) && token != 's' && token != 'c')
            {
                string varName = token.ToString();
                if (!variables.ContainsKey(varName))
                    throw new Exception($"Undefined variable: {varName}");

                string varValueStr = variables[varName].ToString();
                foreach (char c in varValueStr)
                    postFix.Enqueue(c);
                postFix.Enqueue(' ');
                continue;
            }

            if (char.IsDigit(token) == true)
                    number += token;
            else
            {
                if (number != "")
                {
                    foreach (char c in number) postFix.Enqueue(c); 
                    postFix.Enqueue(' ');
                    number = "";
                }

                processToken(token);    
            }
        }
        if(number != "")
        {
            foreach (char c in number) postFix.Enqueue(c);
            postFix.Enqueue(' ');
        }

        while (opStack.Count() > 0)
        {
            postFix.Enqueue(opStack.Pop());
        }

        return postFix;
    }

    public float PostFixCalculation(Queue postFix)
    {
        FloatStack valueStack = new FloatStack();
        string number = "";

        foreach(char i in postFix.ToArray())
        {

            if (char.IsDigit(i))
            {
                number += i;
            }

            else if (i == ' ')
            {
                if (number != "")
                {
                    valueStack.Push(float.Parse(number));
                    number = "";
                }
            }

            else if (i == '|')
            {
                valueStack.Push((float)Math.Abs(valueStack.Pop()));
            }

            else if(i == 'u')
            {
                if (valueStack.Count() < 1)
                    throw new Exception("Unary minus: no operand");
                float a = valueStack.Pop();
                valueStack.Push(-a);
            }

            else if (i == 's')
            {
                float a = valueStack.Pop();
                valueStack.Push((float)Math.Sin(a*Math.PI/180));
            }

            else if (i == 'c')
            {
                float a = valueStack.Pop();
                valueStack.Push((float)Math.Cos(a * Math.PI / 180));
            }

            else
            {
                if (valueStack.Count() < 2)
                    throw new InvalidOperationException("Not enough operands for the operator.");
                float b = valueStack.Pop();
                float a = valueStack.Pop();

                switch (i)
                {
                    case '+': valueStack.Push(a + b); break;
                    case '-': valueStack.Push(a - b); break;
                    case '/': valueStack.Push(a / b); break;
                    case ':': valueStack.Push(a / b); break;
                    case '*': valueStack.Push(a * b); break;
                    case '^': valueStack.Push((float)Math.Pow(a, b)); break;
                }
            }
        }
        return valueStack.Pop();
    }
}
