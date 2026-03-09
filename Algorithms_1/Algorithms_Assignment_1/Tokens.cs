using System;
using System.ComponentModel;
using System.Collections.Generic;
namespace Algorithm_Assignment_1;

public class Tokens
{
    private CharQueue postFix = new CharQueue { }; //for numbers
    private OperatorStack opStack = new OperatorStack { }; //for operations

    public bool IsOperator(char i)
    {
        if ((i == '+') || (i == '-') || (i == '*') || (i == '/') || (i == ':') || (i == '^'))
            return true;
        else
            return false;
            
    }

    private int Precedence(char op)
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
            while (opStack.Count() > 0 && Precedence(opStack.Peek()) >= Precedence(token))
            {
                postFix.Enqueue(opStack.Pop());
            }
            opStack.Push(token);
        }

    }
    public CharQueue ToPostFix(string infix)
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
    public float PostFixCalculation(CharQueue postFix)
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
