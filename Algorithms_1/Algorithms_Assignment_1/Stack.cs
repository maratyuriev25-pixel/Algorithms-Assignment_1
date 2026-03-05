namespace Algorithm_Assignment_1;

using System.Collections.Generic;
using System;

public class OperatorStack
{
    private const int Capacity = 50;

    private char[] _array = new char[Capacity];

    private int _pointer;

    public void Push(char value)
    {
        if (_pointer == _array.Length)
            throw new Exception("Stack overflowed");

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
    
    public char Peek()
    {
        if (_pointer == 0)
            throw new Exception("Stack is Empty");
        return _array[_pointer - 1];
    }
}

public class FloatStack
{
    private const int Capacity = 50;
    private float[] _array = new float[Capacity];
    private int _pointer = 0;

    public void Push(float value)
    {
        if (_pointer == Capacity)
            throw new Exception("Stack overflowed");

        _array[_pointer++] = value;
    }

    public float Pop()
    {
        if (_pointer == 0)
            throw new Exception("Stack is Empty");

        _pointer--;
        return _array[_pointer];
    }
}