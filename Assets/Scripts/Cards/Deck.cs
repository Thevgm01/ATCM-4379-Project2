using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Deck <T> where T : Card
{
    public enum Position
    {
        Top,
        Middle,
        Bottom
    }

    List<T> _cards = new List<T>();

    public event Action Emptied = delegate { };
    public event Action<T> CardAdded = delegate { };
    public event Action<T> CardRemoved = delegate { };

    public int Count => _cards.Count;
    public T TopItem => _cards[_cards.Count - 1];
    public T BottomItem => _cards[0];
    public bool IsEmpty => _cards.Count == 0;
    public int LastIndex
    {
        get
        {
            if (_cards.Count == 0) return 0;
            else return _cards.Count - 1;
        }
    }

    // Merge two decks together
    // Default behavior is to take from the new deck bottom-up, and add to the
    // top of the current deck (basically just dropping the new deck on top)
    public void MergeDeck(Deck<T> otherDeck, 
        Position thisDeckPosition = Position.Top, 
        Position otherDeckPosition = Position.Bottom)
    {
        while(!otherDeck.IsEmpty)
        {
            Add(otherDeck.Draw(otherDeckPosition), thisDeckPosition);
        }
    }

    public void AddAll(List<T> newCards, Position deckPosition = Position.Top)
    {
        foreach (T card in newCards)
        {
            Add(card, deckPosition);
        }
    }

    public void Add(T card, Position deckPosition = Position.Top)
    {
        if (card == null) return;

        if (deckPosition == Position.Top) _cards.Add(card);
        else _cards.Insert(IndexFromDeckPosition(deckPosition), card);

        CardAdded?.Invoke(card);
    }

    public T Peek(Position deckPosition = Position.Top)
    {
        if (IsEmpty) return default(T);

        return _cards[IndexFromDeckPosition(deckPosition)];
    }

    public T Draw(Position deckPosition = Position.Top)
    {
        return RemoveAt(IndexFromDeckPosition(deckPosition));
    }

    public bool Contains(T card)
    {
        return _cards.Contains(card);
    }

    public T Remove(T card)
    {
        if(!_cards.Contains(card))
        {
            Debug.LogWarning("Deck: Does not contain card " + card.Name + ".");
            return default(T);
        }

        _cards.Remove(card);
        CardRemoved?.Invoke(card);
        if (IsEmpty) Emptied?.Invoke();

        return card;
    }

    public T RemoveAt(int index)
    {
        if (IsEmpty || index < 0 || index >= Count)
        {
            Debug.LogWarning("Deck: Index " + index + " is out of range for deck size " + Count + ".");
            return default(T);
        }

        T card = _cards[index];
        _cards.RemoveAt(index);

        CardRemoved?.Invoke(card);
        if (IsEmpty) Emptied?.Invoke();

        return card;
    }

    public void Shuffle()
    {
        // if (IsEmpty) return;

        for(int i = LastIndex; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            T temp = _cards[randomIndex];
            _cards[randomIndex] = _cards[i];
            _cards[i] = temp;
        }
    }

    private int IndexFromDeckPosition(Position position)
    {
        if (IsEmpty) return 0;

        switch (position)
        {
            case Position.Top: return LastIndex;
            case Position.Middle: return UnityEngine.Random.Range(0, LastIndex);
            case Position.Bottom: return 0;
        }

        return -1;
    }

    public IEnumerator GetEnumerator()
    {
        return _cards.GetEnumerator();
    }
}
