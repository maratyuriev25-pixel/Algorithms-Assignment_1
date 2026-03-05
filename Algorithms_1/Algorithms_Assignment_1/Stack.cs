namespace Algorithm_Assignment_1;

using System.Collections.Generic;
using System;

public class Stack
{
    private const int Capacity = 50;

    private char[] _array = new char[Capacity];

    private int _pointer;

    public void Push(char value)
    {
        if (_pointer == _array.Length)
        {
            // this code is raising an exception about reaching stack limit
            throw new Exception("Stack overflowed");
        }

        _array[_pointer] = value;
        _pointer++;
    }

    public char Pop()
    {
        if (_pointer == 0)
            throw new Exception("Stack is Empty");

        _pointer--;
        return _array[_pointer];
    }
    public int Count()
    {
        return _pointer;
    }
}