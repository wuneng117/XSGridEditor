using System.Collections.Generic;

namespace XSSLG
{
    /// <summary> 优先级队列 </summary>
    class HeapPriorityQueue<T> : IPriorityQueue<T>
    {
        private List<PriorityQueueNode<T>> _queue;

        public HeapPriorityQueue()
        {
            _queue = new List<PriorityQueueNode<T>>();
        }

        public int Count
        {
            get { return _queue.Count; }
        }

        public void Enqueue(T item, float priority)
        {
            _queue.Add(new PriorityQueueNode<T>(item, priority));
            int ci = _queue.Count - 1;
            while (ci > 0)
            {
                int pi = (ci - 1) / 2;
                if (_queue[ci].CompareTo(_queue[pi]) >= 0)
                    break;
                var tmp = _queue[ci];
                _queue[ci] = _queue[pi];
                _queue[pi] = tmp;
                ci = pi;
            }
        }
        public T Dequeue()
        {
            int li = _queue.Count - 1;
            var frontItem = _queue[0];
            _queue[0] = _queue[li];
            _queue.RemoveAt(li);

            --li;
            int pi = 0;
            while (true)
            {
                int ci = pi * 2 + 1;
                if (ci > li) break;
                int rc = ci + 1;
                if (rc <= li && _queue[rc].CompareTo(_queue[ci]) < 0)
                    ci = rc;
                if (_queue[pi].CompareTo(_queue[ci]) <= 0) break;
                var tmp = _queue[pi];
                _queue[pi] = _queue[ci];
                _queue[ci] = tmp;
                pi = ci;
            }
            return frontItem.Item;
        }
    }
}