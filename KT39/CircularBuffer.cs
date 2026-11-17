using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KT39
{
    class CircularBuffer<T> : IEnumerable<T>
    {
        private T[] buffer;
        private int capacity;
        private int count;
        private int head;

        public int Count
        {
            get { return count; }
        }

        public int Capacity
        {
            get { return capacity; }
        }

        public CircularBuffer(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Ёмкость должна быть больше нуля.");

            this.capacity = capacity;
            buffer = new T[capacity];
            count = 0;
            head = 0;
        }

        public void Add(T item)
        {
            switch (count < capacity)
            {
                case true:
                    {
                        int index = (head + count) % capacity;
                        buffer[index] = item;
                        count++;
                        break;
                    }

                case false:
                    {
                        buffer[head] = item;
                        head = (head + 1) % capacity;
                        break;
                    }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
            {
                int index = (head + i) % capacity;
                yield return buffer[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> Reverse()
        {
            for (int i = count - 1; i >= 0; i--)
            {
                int index = (head + i) % capacity;
                yield return buffer[index];
            }
        }
    }
}
