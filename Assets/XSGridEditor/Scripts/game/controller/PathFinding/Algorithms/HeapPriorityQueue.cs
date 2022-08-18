using System.Collections.Generic;

namespace XSSLG
{
    class PriorityQueueItem<T>
    {
        public T Item { get; private set; }

        /// <summary> 优先级 </summary>
        public int Priority { get; private set; }

        /// <summary> 构造函数 </summary>
        public PriorityQueueItem(T item, int priority)
        {
            (this.Item, this.Priority) = (item, priority);
        }
    }

    /// <summary> 优先级队列 </summary>
    class PriorityQueue<T>
    {
        private List<PriorityQueueItem<T>> list = new List<PriorityQueueItem<T>>();

        public int Count { get => this.list.Count; }

        public void Enqueue(T item, int priority)
        {
            this.list.Add(new PriorityQueueItem<T>(item, priority));
            this.list.Sort((x, y) => y.Priority - x.Priority);
        }

        public T Dequeue()
        {
            if (this.list.Count == 0)
                return default(T);

            var ret = this.list[0];
            this.list.RemoveAt(0);
            return ret.Item;
        }
    }
}