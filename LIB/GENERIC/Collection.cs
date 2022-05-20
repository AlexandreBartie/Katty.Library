using System;
using System.Collections.Generic;
using System.Text;

namespace Katty
{
    public class myCollection<T> : List<T>
    {
        public bool IsFull => !IsEmpty;
        public bool IsEmpty => (Count == 0);

    }

}
