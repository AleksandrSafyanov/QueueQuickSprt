using System;
using QueueNode;

namespace Queue
{
  public class QueueSort
  {
    protected Node _head;
    protected Node _tail;

    public int Count { get; private set; } = 0;

    public void Enqueue(int data)
    {
      Node _newNode = new Node(data);
      if (IsEmpty())
      {
        _head = _newNode;
        _tail = _head;
      }
      else
      {
        _tail.Next = _newNode;
        _tail = _tail.Next;
      }
      Count++;
    }
    public int Dequeue()
    {
      if (IsEmpty())
        throw new Exception("Queue is Empty!");

      Count--;
      int _result = _head.Data;
      _head = _head.Next;
      return _result;
    }
    public bool IsEmpty()
    {
      return _head == null;
    }
    public int Peek()
    {
      if (IsEmpty() == false)
        return _head.Data;

      else
      {
        Console.WriteLine("Queue is Empty!");
        return 0;
      }
    }
    public int GetNumber(int Position)
    {
      if (IsEmpty() == false)
      {
        int ValueGet = 0;

        for (int i = 1; i < Count + 1; i++)
        {
          if (i == Position)
            ValueGet = Peek();

          if (Position != 1)
            Enqueue(Dequeue());
          else
            break;
        }
        return ValueGet;
      }
      else
      {
        Console.WriteLine("Queue is Empty!");
        return 0;
      }
    }
    public void SetNumber(int Position, int ValueSet)
    {
      if (IsEmpty() == false)
      {
        if (Position <= Count)
        {
          for (int i = 1; i < Count + 1; i++)
          {
            if (i == Position)
              _head.Data = ValueSet;

            if (Position != 1)
              Enqueue(Dequeue());
            else
              break;
          }
        }
        else
        {
          if (Position == Count + 1)
            Enqueue(ValueSet);

          else
          {
            for (int i = Count + 1; i < Position; i++)
              Enqueue(0);

            Enqueue(ValueSet);
          }
        }
      }
      else
        Console.WriteLine("Queue is Empty!");
    }
    public void QuickSort(int leftBorder, int rightBorder)
    {
      if (leftBorder >= rightBorder)
        return;

      int left = leftBorder;
      int right = rightBorder;

      while (left != right)
      {
        while (left != right)
        {
          if (GetNumber(left) <= GetNumber(right))
            --right;

          else
          {
            Swap(left, right);
            --right;
          }
        }
        while (left != right)
        {
          if (GetNumber(left) <= GetNumber(right))
            ++left;

          else
          {
            Swap(left, right);
            ++left;
          }
        }
      }
      if (left - 1 > leftBorder)
        QuickSort(leftBorder, left - 1);

      if (right + 1 < rightBorder)
        QuickSort(right + 1, rightBorder);
    }
    public void Swap(int leftElement, int rightElement)
    {
      int temp = GetNumber(leftElement);
      SetNumber(leftElement, GetNumber(rightElement));
      SetNumber(rightElement, temp);
    }
  }
  class Program
  {
    static void Main()
    {
      const int _digitsCount = 500;

      int[] KeyStore = new int[_digitsCount];

      QueueSort queue = new QueueSort();
      Random rnd = new Random();

      for (int i = 0; i < _digitsCount; i++)
      {
        KeyStore[i] = rnd.Next(1000);
        queue.Enqueue(KeyStore[i]);
      }

      queue.QuickSort(1, _digitsCount);

      Console.WriteLine(queue.Count);
      Console.WriteLine();

      for (int i = 0; i < _digitsCount; i++)
        Console.Write("{0,6}", queue.Dequeue());

      Console.ReadLine();
    }
  }
}
