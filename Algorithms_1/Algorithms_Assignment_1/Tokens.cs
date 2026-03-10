using System;
using System.ComponentModel;
using System.Collections.Generic;
namespace Algorithm_Assignment_1;

public class Tokens
{
    private Queue postFix = new Queue { };
    private OperatorStack opStack = new OperatorStack { };

    public bool IsOperator(char i)
    {
        if ((i == '+') || (i == '-') || (i == '*') || (i == '/') || (i == ':') || (i == '^'))
            return true;
        else
            return false;
            
    }

    private int Candidate(char op)
    {
        return op switch
        {
            '+' or '-' => 1,
            '*' or '/' => 2,
            '^' => 3,
            _ => 0
        };
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

            opStack.Pop();
        }
        else if (IsOperator(token) == true)
        {
            while (opStack.Count() > 0 && Candidate(opStack.Peek()) >= Candidate(token))
            {
                postFix.Enqueue(opStack.Pop());
            }
            opStack.Push(token);
        }

    }
    public Queue ToPostFixCalc(string infix)
    {
        string number = "";

        foreach(char token in infix)
        {
            if(char.IsDigit(token) == true)
                    number += token;
            else
            {
                if (number != "")
                {
                    foreach(char c in number)
                        postFix.Enqueue(c);

                    postFix.Enqueue(' ');
                    number = "";
                }

                processToken(token);    
            }
        }
        if(number != "")
        {
            foreach (char c in number)
                postFix.Enqueue(c);

            postFix.Enqueue(' ');
        }
        while (opStack.Count() > 0)
        {
            postFix.Enqueue(opStack.Pop());
        }

        return postFix;
    }



    public List<object> ToPostFixView(string infix)
    {
        List<string> numbers = new List<string>();
        List<char> operators = new List<char>();
        List<object> result = new List<object>();

        string number = "";

        foreach (char token in infix)
        {
            if (char.IsDigit(token) == true)
                number += token;

            else
            {
                if (number != "")
                {
                    numbers.Add(number);
                    number = "";
                }
                if (IsOperator(token) == true)
                    operators.Add(token);
            }
        }

        if( number != "")
        numbers.Add(number);

        operators.Sort((a,b) => Candidate(b).CompareTo(Candidate(a)));

        foreach(string token in numbers)
            result.Add(token);

        foreach(char token in operators)
            result.Add(token.ToString());

        return result;
    }


    public float PostFixCalculation(Queue postFix)
    {
        FloatStack valueStack = new FloatStack();
        string number = "";

        foreach(char i in postFix.ToArray())
        {
            float a = 0, b = 0;
            if (char.IsDigit(i))
            {
                number += i;
            }
            else if(i == ' ')
            {
                if (number != " ")
                {
                    valueStack.Push(float.Parse(number));
                    number = "";
                }
            }
            else
            {
                b = valueStack.Pop();
                a = valueStack.Pop();
            }
            switch (i)
            {
                case '+': valueStack.Push(a + b); break;
                case '-': valueStack.Push(a - b); break;
                case '/': valueStack.Push(a / b); break;
                case ':': valueStack.Push(a / b); break;
                case '*': valueStack.Push(a * b); break;
                case '^': valueStack.Push((float)Math.Pow(a,b)); break;
            }
        }
        return valueStack.Pop();
    }
}
