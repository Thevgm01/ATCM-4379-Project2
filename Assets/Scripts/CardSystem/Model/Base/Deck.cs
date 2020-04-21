using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Deck <T> where T : Card
{
    public event Action Emptied = delegate { };
    public event Action<T> CardAdded = delegate { };
    public event Action<T> CardRemoved = delegate { };
    // technically a Stack would be 'proper'
    // using a List instead for more control, like drawing from bottom/middle
    List<T> _cards = new List<T>();

    #region Properties    
    public int Count => _cards.Count;
    public int LastIndex
    {
        get
        {
            if(_cards.Count == 0)
            {
                return 0;
            }
            else
            {
                return _cards.Count - 1;
            }
        }
    }
    public T TopItem => _cards[_cards.Count - 1];
    public T BottomItem => _cards[0];
    public bool IsEmpty => _cards.Count == 0;
    #endregion
    
    public void Add(T card, DeckPosition position = DeckPosition.Bottom)
    {
        // bodyguard
        if(card == null) { return; }

        int targetIndex = GetIndexFromPosition(position);
        // to add it to 'Top' we actually want to add it at the end,
        // by default Insert() moves the current index upwards
        if (targetIndex == LastIndex)
        {
            _cards.Add(card);
        }
        else
        {
            _cards.Insert(targetIndex, card);
        }

        CardAdded?.Invoke(card);
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
            Debug.LogWarning("Deck: Cannot draw new item - deck is empty!");
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
        if (IsCardAtIndex(index))
        {
            return _cards[index];
        }
        else
        {
            return default;
        }
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

        CardRemoved?.Invoke(removedItem);

        if (_cards.Count == 0)
        {
            Emptied?.Invoke();
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
        // start at the top, randomly swapping cards as we move our way down
        for (int currentIndex = Count - 1; currentIndex > 0; --currentIndex)
        {
            // choose a random card
            int randomIndex = UnityEngine.Random.Range(0, currentIndex + 1);
            T randomCard = _cards[randomIndex];
            // random card swaps places with our current index
            _cards[randomIndex] = _cards[currentIndex];
            _cards[currentIndex] = randomCard;
            // move downwards to next card index
        }
    }

    public void TransferDeckCards(Deck<T> transferIntoDeck)
    {
        int numCardsToTransfer = Count;
        // transfor discard cards back into main
        for (int i = 0; i < numCardsToTransfer; i++)
        {
            T card = Draw();
            transferIntoDeck.Add(card);
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
        // if our deck is empty, index should always be 0
        if(_cards.Count == 0)
        {
            newPositionIndex = 0;
        }
        // get end of index if it's on 'from the top'
        if (position == DeckPosition.Top)
        {
            // index is 1 higher than last index, to add to end
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
