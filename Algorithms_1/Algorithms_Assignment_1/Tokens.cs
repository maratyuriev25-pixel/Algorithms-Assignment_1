using System;
using System.ComponentModel;
using System.Collections.Generic;
namespace Algorithm_Assignment_1;

public class Tokens
{
    private Queue postFix = new Queue { }; //for numbers
    private Stack opStack = new Stack { }; //for operations

    public bool IsOperator(char i)
    {
        if ((i == '+') || (i == '-') || (i == '*') || (i == '/') || (i == ':'))
            return true;
        else
            return false;
            
    }

    public void processToken(char token)
    {
        if(char.IsNumber(token) == true)
        {
            postFix.Enqueue(token);
        }
        else if(IsOperator(token) == true)
        {
            opStack.Push(token);
        }

    }
    public Queue ToPostFix(string infix)
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
        
}
