using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Miotsukushi.Model
{
    class ExList<T> : IEnumerable<T>
    {
        List<T> list;

        public T this[int index]
        {
            get { return list[index]; }
            set
            {
                list[index] = value;
                OnExListChanged(new ExListChangedEventArgs(ExListChangedEventArgs.ChangeTypeEnum.Replaced, index));
            }
        }

        public int Count { get { return list.Count; } }

        public ExList()
        {
            list = new List<T>();
        }

        public void Add(T item)
        {
            list.Add(item);
            OnExListChanged(new ExListChangedEventArgs(ExListChangedEventArgs.ChangeTypeEnum.Added, list.Count - 1));
        }

        public bool Remove(T item)
        {
            int index = list.IndexOf(item);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            else
                return false;
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
            OnExListChanged(new ExListChangedEventArgs(ExListChangedEventArgs.ChangeTypeEnum.Removed, index));
        }

        public int RemoveAll(Predicate<T> match)
        {
            int ret = list.RemoveAll(match);
            OnExListChanged(new ExListChangedEventArgs(ExListChangedEventArgs.ChangeTypeEnum.Reset, -1));
            return ret;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in list)
                yield return item;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        public event ExListChangedEventHandler ExListChanged;
        public delegate void ExListChangedEventHandler(object sender, ExListChangedEventArgs e);
        protected virtual void OnExListChanged(ExListChangedEventArgs e) { if (ExListChanged != null) { ExListChanged(this, e); } }

    }

    class ExListChangedEventArgs : EventArgs
    {
        public enum ChangeTypeEnum
        {
            Added, Removed, Replaced, Cleared, Reset
        };

        public int ChangedIndex;
        public ChangeTypeEnum ChangeType;

        public ExListChangedEventArgs(ChangeTypeEnum type, int index)
        {
            ChangeType = type;
            ChangedIndex = index;
        }
    }
}
