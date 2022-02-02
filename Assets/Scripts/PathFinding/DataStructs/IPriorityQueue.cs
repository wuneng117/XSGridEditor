using System;

namespace XSSLG
{
    /// <summary> 优先级队列 </summary>
    public interface IPriorityQueue<T>
    {
        /// <summary> 数量 </summary>
        int Count { get; }
        /// <summary> 压入一个item </summary>
        void Enqueue(T item, float priority);
        /// <summary> 弹出优先级最低的 </summary>
        T Dequeue();
    }

    /// <summary> 队列item </summary>
    class PriorityQueueNode<T> : IComparable
    {
        public T Item { get; private set; }
        /// <summary> 优先级 </summary>
        public float Priority { get; private set; }

        /// <summary> 构造函数 </summary>
        public PriorityQueueNode(T item, float priority)
        {
            Item = item;
            Priority = priority;
        }

        /// <summary> 比对优先级 </summary>
        public int CompareTo(object obj) => Priority.CompareTo((obj as PriorityQueueNode<T>).Priority);
    }
}