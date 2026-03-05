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
        if(char.IsDigit(token) == true)
        {
            postFix.Enqueue(token);
        }
        else if(IsOperator(token) == true)
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
        foreach(char token in infix)
        {
            processToken(token);
        }
        while (opStack.Count() > 0)
        {
            char op = opStack.Pop();
            postFix.Enqueue(op);
        }
        return postFix;
    }
    public float PostFixCalculation(CharQueue postFix)
    {
        FloatStack valueStack = new FloatStack();

        foreach(char i in postFix.ToArray())
        {
            float a = 0, b = 0;
            if (char.IsDigit(i))
            {
                valueStack.Push(i - '0');
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
