using System;
using System.Collections.Generic;

class Heap<T>
{
    private List<T> items_;
    private Dictionary<T, int> itemIndices_; // added to keep track of the index of each item in the heap
    private Comparison<T> comparison_;

    public Heap(IEnumerable<T> _collection, Comparison<T> _comparison)
    {
        this.items_ = new List<T>(_collection);
        this.itemIndices_ = new Dictionary<T, int>();
        this.comparison_ = _comparison;

        for (int i = Parent(items_.Count - 1); i >= 0; i--)
        {
            BubbleDown(i);
        }
    }

    public int Count { get { return items_.Count; } }

    public T Peek()
    {
        if (items_.Count == 0) throw new InvalidOperationException("Heap is empty.");
        return items_[0];
    }

    public T Pop()
    {
        if (items_.Count == 0) throw new InvalidOperationException("Heap is empty.");

        T result = items_[0];
        items_[0] = items_[items_.Count - 1];
        items_.RemoveAt(items_.Count - 1);
        itemIndices_.Remove(result); // remove the item from the dictionary when it's removed from the heap
        BubbleDown(0);
        return result;
    }

    public void Push(T _item)
    {
        items_.Add(_item);
        itemIndices_[_item] = items_.Count - 1; // add the item to the dictionary with its index in the heap
        BubbleUp(items_.Count - 1);
    }

    public void ChangePriority(T _item)
    {
        if (itemIndices_.TryGetValue(_item, out var index)) // check if the item is in the heap
        {
            BubbleUp(index);
            BubbleDown(index);
        }
        else
        {
            throw new ArgumentException("Item not found in heap.");
        }
    }

    private void BubbleUp(int _i)
    {
        while (_i > 0 && comparison_(items_[_i], items_[Parent(_i)]) < 0)
        {
            Swap(_i, Parent(_i));
            _i = Parent(_i);
        }
    }

    private void BubbleDown(int _i)
    {
        while (LeftChild(_i) < items_.Count)
        {
            int j = LeftChild(_i);
            if (RightChild(_i) < items_.Count && comparison_(items_[RightChild(_i)], items_[j]) < 0)
            {
                j = RightChild(_i);
            }
            if (comparison_(items_[j], items_[_i]) >= 0)
            {
                break;
            }
            Swap(_i, j);
            _i = j;
        }
    }

    private void Swap(int _i, int _j)
    {
        (items_[_i], items_[_j]) = (items_[_j], items_[_i]);
        itemIndices_[items_[_i]] = _i; // update the dictionary with the new indices of the swapped items
        itemIndices_[items_[_j]] = _j;
    }

    private static int Parent(int _i)
    {
        return (_i - 1) / 2;
    }

    private static int LeftChild(int _i)
    {
        return 2 * _i + 1;
    }

    private static int RightChild(int _i)
    {
        return 2 * _i + 2;
    }
}
