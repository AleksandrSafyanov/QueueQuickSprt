using System;
using System.Diagnostics;
using QueueNode;

namespace Queue
{
  public class QueueSort
  {
    Node _head;
    Node _tail;

    //count - кол-во элементов в очереди. Свойства: get - для чтения, private set - для записи только в этом классе
    public int Count { get; private set; } = 0;

    //добавление элемента в очередь, data - его значение 
    public void Enqueue(int data)
    {
      //создаем новый элемент
      Node _newNode = new Node(data);
      //если очередь пуста, элемент добавляется в начало очереди
      if (IsEmpty())
      {
        _head = _newNode;
        _tail = _head;
      }
      //иначе - в конец
      else
      {
        _tail.Next = _newNode;
        _tail = _tail.Next;
      }
      //увеличиваем значение размера очереди
      Count++;
    }

    //возвращение элемента из очереди
    public int Dequeue()
    {
      if (IsEmpty())
      {
        Console.WriteLine("Queue is Empty!");
        return 0;
      }
      Count--;

      //"голова" переходит на следующий элемент после очереди
      int _result = _head.Data;
      _head = _head.Next;
      return _result;
    }

    //чтение первого элемента очереди
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

    //проверка на пустоту очереди
    public bool IsEmpty()
    {
      return _head == null;
    }

    //получение значения элемента по его индексу в очереди
    public int GetNumber(int Position)
    {
      if (IsEmpty() == false)
      {
        int ValueGet = 0;

        //переборка всех значений очереди
        for (int i = 1; i < Count + 1; i++)
        {
          //если цикл дошел до нужной позиции => в ValueGet записывается значение
          if (i == Position)
            ValueGet = Peek();

          //если позиция не равна единице =>  тогда 1ый элемент отправляем в конец  очереди
          if (Position != 1)
            Enqueue(Dequeue());
          //иначе цикл прекращается
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

    //установка значения по индексу
    public void SetNumber(int Position, int ValueSet)
    {
      if (IsEmpty() == false)
      {
        //если кол-во элементов в очереди больше переданной позиции
        if (Position <= Count)
        {
          //переборка всех значений очереди
          for (int i = 1; i < Count + 1; i++)
          {
            //если цикл дошел до нужной позиции => в "голову" записывается значение передаваемое параметром
            if (i == Position)
              _head.Data = ValueSet;

            //если позиция не равна единице =>  тогда 1ый элемент отправляем в конец  очереди
            if (Position != 1)
              Enqueue(Dequeue());
            //иначе цикл прекращается
            else
              break;
          }
        }
        //если кол-во элементов в очереди меньше переданной позиции 
        else
        {
          //если переданная позиция равна кол-ву элементов в очереди + 1 => добавляется значение в начало
          if (Position == Count + 1)
            Enqueue(ValueSet);
          //иначе все последующие элементы до переданной позиции заполнятся нулями
          else
          {
            for (int i = Count + 1; i < Position; i++)
              Enqueue(0);
            //а на место переданной позиции установится переданной значение
            Enqueue(ValueSet);
          }
        }
      }
      else
        Console.WriteLine("Queue is Empty!");
    }

    //быстрая сортировка
    public void QuickSort(int leftBorder, int rightBorder)
    {
      //если левая граница больше правой, то программа прекращает работу
      if (leftBorder >= rightBorder)
        return;

      int left = leftBorder;
      int right = rightBorder;

      while (left != right)
      {
        while (left != right)
        {
          //элемент в очереди левой границы(left) <= элемента в очереди правой границы(right)
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

    //метод замены элементов местами
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
      Console.WriteLine("Quick queue sorting!");
      //кол-во элементов очереди
      const int _digitsCount = 1000;

      var time = new Stopwatch();

      //хранилище ключей
      int[] key = new int[_digitsCount];
      QueueSort queue = new QueueSort();
      Random rnd = new Random();

      //заполняем очередь и хранилище ключей рандомными целыми числами
      for (int i = 0; i < _digitsCount; i++)
      {
        key[i] = rnd.Next(1000);
        queue.Enqueue(key[i]);
      }

      Console.WriteLine("_________________________________________________________");
      Console.WriteLine($"Count of items in the queue: {queue.Count}");
      Console.WriteLine("_________________________________________________________");

      Console.WriteLine("Unsorted queue:");
      for (int i = 0; i < _digitsCount; i++)
        Console.Write("{0,6}", key[i]);

      //начало отсчета времени
      time.Start();

      //вызов сортировки
      queue.QuickSort(1, _digitsCount);

      //конец отсета времени
      time.Stop();

      Console.WriteLine("\nSorted queue:");
      for (int i = 0; i < _digitsCount; i++)
        Console.Write("{0,6}", queue.Dequeue());

      Console.WriteLine("\n_________________________________________________________");
      Console.WriteLine($"Time spent: {time.ElapsedMilliseconds}");

      TimeSpan interval = TimeSpan.FromMilliseconds((double)time.ElapsedMilliseconds);
      Console.WriteLine("h.m.s.ms\n{0}:{1}:{2}:{3}", interval.Hours, interval.Minutes, interval.Seconds, interval.Milliseconds);

      Console.ReadLine();
    }
  }
}