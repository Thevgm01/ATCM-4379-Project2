using System.Collections;
using System.Collections.Generic;

public static class CardUtility<T>
{
    public static List<T> Shuffle(List<T> cards)
    {
        for (int i = cards.Count - 1; i > 0; --i)
        {
            // choose a random card
            int j = UnityEngine.Random.Range(0, i + 1);
            T randomItems = cards[j];
            // random card swaps places with our current index
            cards[j] = cards[i];
            cards[i] = randomItems;
            // move upwards to next card index
        }

        return cards;
    }
}
