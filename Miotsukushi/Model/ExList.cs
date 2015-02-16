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

        public T this[int index] { get { return list[index]; } set { list[index] = value; } }

        public int Count { get { return list.Count; } }

        public ExList()
        {
            list = new List<T>();
        }

        public void Add(T item)
        {
            list.Add(item);
            OnItemAdded(new EventArgs());
        }

        public void Remove(T item)
        {
            list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public int RemoveAll(Predicate<T> match)
        {
            return list.RemoveAll(match);
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

        public event EventHandler ItemAdded;
        protected virtual void OnItemAdded(EventArgs e) { if (ItemAdded != null) { ItemAdded(this, e); } }

    }
}
