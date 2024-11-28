using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    internal interface IMyIterator<T>
    {
        bool HasNext();
        void Remove();
        IMyCollection<T> Next();
    }
}
