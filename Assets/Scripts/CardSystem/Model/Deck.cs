using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DeckPosition
{
    Top,
    Middle,
    Bottom
}

public class Deck <T>
{
    public event Action Emptied = delegate { };
    public event Action<T> Added = delegate { };
    public event Action<T> Removed = delegate { };
    // technically a Stack would be 'proper'
    // using a List instead for more control, like drawing from bottom/middle
    List<T> _items = new List<T>();

    #region Properties    
    public int NumberOfItems => _items.Count;
    public int LastIndex => _items.Count - 1;
    public T TopItem => _items[0];
    public T BottomItem => _items[_items.Count - 1];
    public bool IsEmpty => _items.Count == 0;
    #endregion
    
    // Add card at specified index. Default to bottom
    public void Add(T item, DeckPosition position = DeckPosition.Bottom)
    {
        int targetIndex = GetIndexFromPosition(position);

        _items.Insert(targetIndex, item);
        Added?.Invoke(item);
    }

    public void Add(List<T> items, DeckPosition position = DeckPosition.Bottom)
    {
        int itemCount = items.Count;
        for (int i = 0; i < itemCount; i++)
        {
            Add(items[i], position);  
        }
    }

    // Draws next item (top of deck). default to top
    public T Draw(DeckPosition position = DeckPosition.Top)
    {
        if (IsEmpty)
        {
            Debug.LogError("Deck: Cannot draw new item - deck is empty!");
            return default;
        }

        int targetIndex = GetIndexFromPosition(position);

        T itemToRemove = _items[targetIndex];
        Remove(targetIndex);
        

        return itemToRemove;
    }

    public List<T> Draw(int numOfItems, DeckPosition position = DeckPosition.Top)
    {
        List<T> drawnItems = new List<T>();
        T drawnItem;    // for readability
        for (int i = 0; i < numOfItems; i++)
        {
            if (!IsEmpty)
            {
                drawnItem = Draw(position);
                drawnItems.Add(drawnItem);
            }
        }
        return drawnItems;
    }

    // use this to return the card at the index, but don't alter position
    public T View(int index)
    {
        return _items[index];
    }

    // technically this is the same as Draw without returning an item
    public void Remove(int index)
    {
        if (IsEmpty)
        {
            Debug.LogWarning("Deck: Nothing to remove; deck is already empty");
            return;
        }
        else if(!IsIndexWithinListRange(index))
        {
            Debug.LogWarning("Deck: Nothing to remove; index out of range");
            return;
        }

        T removedItem = _items[index];
        _items.RemoveAt(index);

        Removed?.Invoke(removedItem);

        if (_items.Count == 0)
        {
            Emptied.Invoke();
        }
    }

    public void RemoveAll()
    {
        _items.Clear();

        Emptied?.Invoke();
    }

    /// <summary>
    /// Randomly shuffles cards, from the bottom up
    /// </summary>
    public void Shuffle()
    {
        // start at the bottom, randomly swapping cards as we move our way up
        for (int i = NumberOfItems - 1; i > 0; --i)
        {
            // choose a random card
            int j = UnityEngine.Random.Range(0, i + 1);
            T randomItems = _items[j];
            // random card swaps places with our current index
            _items[j] = _items[i];
            _items[i] = randomItems;
            // move upwards to next card index
        }
    }

    private bool IsItemAtIndex(int index)
    {
        // is the hand empty
        if (IsEmpty)
        {
            Debug.LogWarning("Deck: Nothing to view; hand is already empty");

            return default;
        }
        // is index within bounds of list
        else if (!IsIndexWithinListRange(index))
        {
            Debug.LogWarning("Deck: Cannot view; index out of range");
            return default;
        }
        // is the item present actually an item
        else if (_items[index] == null)
        {
            Debug.LogWarning("Deck: Nothing contained in requested index");
            return default;
        }

        // otherwise, we're valid!
        return true;
    }

    bool IsIndexWithinListRange(int index)
    {
        // if index is within the range of contents
        if (index >= 0 && index <= _items.Count - 1)
        {
            return true;
        }

        Debug.LogWarning("Deck: index outside of range;" +
            " index: " + index);
        return false;
    }

    private int GetIndexFromPosition(DeckPosition position)
    {
        int newPositionIndex = 0;
        // get end of index if it's on 'from the top'
        if (position == DeckPosition.Top)
        {
            newPositionIndex = LastIndex;
        }
        // randomize if drawing from the middle
        else if (position == DeckPosition.Middle)
        {
            newPositionIndex = UnityEngine.Random.Range(0, LastIndex);
        }
        // get 0 index if it's 'from the bottom'
        else if (position == DeckPosition.Bottom)
        {
            newPositionIndex = 0;
        }

        return newPositionIndex;
    }
}
