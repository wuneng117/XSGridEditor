using System.Collections.Generic;

namespace XSSLG
{
    class XSPriorityQueueItem<T>
    {
        public T Item { get; private set; }

        public int Priority { get; private set; }

        /// <summary> Constructor </summary>
        public XSPriorityQueueItem(T item, int priority)
        {
            (this.Item, this.Priority) = (item, priority);
        }
    }

    class PriorityQueue<T>
    {
        protected List<XSPriorityQueueItem<T>> list = new List<XSPriorityQueueItem<T>>();

        public int Count { get => this.list.Count; }

        public virtual void Enqueue(T item, int priority)
        {
            this.list.Add(new XSPriorityQueueItem<T>(item, priority));
            this.list.Sort((x, y) => y.Priority - x.Priority);
        }

        public virtual T Dequeue()
        {
            if (this.list.Count == 0)
            {
                return default(T);
            }

            var ret = this.list[0];
            this.list.RemoveAt(0);
            return ret.Item;
        }
    }
}