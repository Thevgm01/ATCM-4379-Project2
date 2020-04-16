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

public class Deck <T> where T : Card
{
    public event Action Emptied = delegate { };
    public event Action<T> Added = delegate { };
    public event Action<T> Removed = delegate { };
    // technically a Stack would be 'proper'
    // using a List instead for more control, like drawing from bottom/middle
    List<T> _cards = new List<T>();

    #region Properties    
    public int Count => _cards.Count;
    public int LastIndex => _cards.Count - 1;
    public T TopItem => _cards[0];
    public T BottomItem => _cards[_cards.Count - 1];
    public bool IsEmpty => _cards.Count == 0;
    #endregion
    
    // Add card at specified index. Default to bottom
    public void Add(T card, DeckPosition position = DeckPosition.Bottom)
    {
        int targetIndex = GetIndexFromPosition(position);

        _cards.Insert(targetIndex, card);
        Added?.Invoke(card);
    }

    public void Add(List<T> cards, DeckPosition position = DeckPosition.Bottom)
    {
        int itemCount = cards.Count;
        for (int i = 0; i < itemCount; i++)
        {
            Add(cards[i], position);  
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

        T cardToRemove = _cards[targetIndex];
        Remove(targetIndex);
        

        return cardToRemove;
    }

    public List<T> Draw(int numberOfCards, DeckPosition position = DeckPosition.Top)
    {
        List<T> drawnCards = new List<T>();
        T drawnCard;    // for readability
        for (int i = 0; i < numberOfCards; i++)
        {
            if (!IsEmpty)
            {
                drawnCard = Draw(position);
                drawnCards.Add(drawnCard);
            }
        }
        return drawnCards;
    }

    // use this to return the card at the index, but don't alter position
    public T GetCard(int index)
    {
        return _cards[index];
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

        T removedItem = _cards[index];
        _cards.RemoveAt(index);

        Removed?.Invoke(removedItem);

        if (_cards.Count == 0)
        {
            Emptied.Invoke();
        }
    }

    public void RemoveAll()
    {
        _cards.Clear();

        Emptied?.Invoke();
    }

    /// <summary>
    /// Randomly shuffles cards, from the bottom up
    /// </summary>
    public void Shuffle()
    {
        // start at the bottom, randomly swapping cards as we move our way up
        for (int i = Count - 1; i > 0; --i)
        {
            // choose a random card
            int j = UnityEngine.Random.Range(0, i + 1);
            T randomCard = _cards[j];
            // random card swaps places with our current index
            _cards[j] = _cards[i];
            _cards[i] = randomCard;
            // move upwards to next card index
        }
    }

    private bool IsCardAtIndex(int targetIndex)
    {
        // is the hand empty
        if (IsEmpty)
        {
            Debug.LogWarning("Deck: Nothing to view; hand is already empty");

            return default;
        }
        // is index within bounds of list
        else if (!IsIndexWithinListRange(targetIndex))
        {
            Debug.LogWarning("Deck: Cannot view; index out of range");
            return default;
        }
        // is the item present actually an item
        else if (_cards[targetIndex] == null)
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
        if (index >= 0 && index <= _cards.Count - 1)
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

    public void TransferDeckCards(Deck<T> targetDeck)
    {
        // transfor discard cards back into main
        for (int i = 0; i < Count; i++)
        {
            T card = Draw();
            targetDeck.Add(card);
        }
    }
}
