using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{
    public class myCollection<T> : List<T>
    {
        public bool IsFull => !IsEmpty;
        public bool IsEmpty => (Count == 0);

        public T Get(int prmIndice)
        {
            return this[prmIndice - 1];
        }

    }

}
