using System;
using System.Collections.Generic;

class Heap<T>
{
    private List<T> items;
    private Dictionary<T, int> itemIndices; // added to keep track of the index of each item in the heap
    private Comparison<T> comparison;

    public Heap(IEnumerable<T> collection, Comparison<T> comparison)
    {
        this.items = new List<T>(collection);
        this.itemIndices = new Dictionary<T, int>();
        this.comparison = comparison;

        for (int i = Parent(items.Count - 1); i >= 0; i--)
        {
            BubbleDown(i);
        }
    }

    public int Count { get { return items.Count; } }

    public T Peek()
    {
        if (items.Count == 0) throw new InvalidOperationException("Heap is empty.");
        return items[0];
    }

    public T Pop()
    {
        if (items.Count == 0) throw new InvalidOperationException("Heap is empty.");

        T result = items[0];
        items[0] = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        itemIndices.Remove(result); // remove the item from the dictionary when it's removed from the heap
        BubbleDown(0);
        return result;
    }

    public void Push(T item)
    {
        items.Add(item);
        itemIndices[item] = items.Count - 1; // add the item to the dictionary with its index in the heap
        BubbleUp(items.Count - 1);
    }

    public void ChangePriority(T item)
    {
        if (itemIndices.TryGetValue(item, out var index)) // check if the item is in the heap
        {
            BubbleUp(index);
            BubbleDown(index);
        }
        else
        {
            throw new ArgumentException("Item not found in heap.");
        }
    }

    private void BubbleUp(int i)
    {
        while (i > 0 && comparison(items[i], items[Parent(i)]) < 0)
        {
            Swap(i, Parent(i));
            i = Parent(i);
        }
    }

    private void BubbleDown(int i)
    {
        while (LeftChild(i) < items.Count)
        {
            int j = LeftChild(i);
            if (RightChild(i) < items.Count && comparison(items[RightChild(i)], items[j]) < 0)
            {
                j = RightChild(i);
            }
            if (comparison(items[j], items[i]) >= 0)
            {
                break;
            }
            Swap(i, j);
            i = j;
        }
    }

    private void Swap(int i, int j)
    {
        (items[i], items[j]) = (items[j], items[i]);
        itemIndices[items[i]] = i; // update the dictionary with the new indices of the swapped items
        itemIndices[items[j]] = j;
    }

    private static int Parent(int i)
    {
        return (i - 1) / 2;
    }

    private static int LeftChild(int i)
    {
        return 2 * i + 1;
    }

    private static int RightChild(int i)
    {
        return 2 * i + 2;
    }
}
