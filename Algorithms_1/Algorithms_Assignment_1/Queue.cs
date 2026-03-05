namespace Algorithm_Assignment_1;

using System;
using System.ComponentModel;
using System.Collections.Generic;

public class CharQueue
{
	private const int Capacity = 50;
	private char[] _array = new char[50];
	private int _index = 0;
	private int _tail = 0;
	private int _count = 0;
	public void Enqueue(char element)
	{
			if (_count == Capacity) throw new Exception("Queue overflow");
			_array[_tail] = element;
			_tail = (_tail + 1) % Capacity;
			_count++;
	}
    public char Dequeue()
    {
        if (_count == 0)
            throw new Exception("Queue empty");

		char value = _array[_index];
        _index = (_index + 1) % Capacity;
        _count--;
		return value;
    }
    public int Count()
    {
        return _count;
    }
	public char[] ToArray()
	{
		char[] result = new char[_count];
		for (int i = 0; i < _count; i++) 
			result[i] = _array[(_index + i) % Capacity];
		return result;

	}
}