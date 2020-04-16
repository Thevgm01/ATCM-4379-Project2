using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardPlayerController
{
    public event Action<CardPlayer> NewCurrentPlayer = delegate { };

    public int PlayerCount => _players.Count;
    public CardPlayer CurrentPlayer => _players[_currentPlayerIndex];

    List<CardPlayer> _players = new List<CardPlayer>();
    int _currentPlayerIndex = 0;

    public void MakeNextPlayerCurrent()
    {
        _currentPlayerIndex = ArrayHelper.GetNextLoopedIndex(_currentPlayerIndex, _players.Count);

        NewCurrentPlayer.Invoke(CurrentPlayer);
    }

    public void MakePreviousPlayerCurrent()
    {
        _currentPlayerIndex = ArrayHelper.GetPreviousLoopedIndex(_currentPlayerIndex, _players.Count);

        NewCurrentPlayer.Invoke(CurrentPlayer);
    }

    public void SetCurrentPlayer(int targetIndex)
    {
        if(ArrayHelper.IsValidIndex(targetIndex, _players.Count))
        {
            _currentPlayerIndex = targetIndex;
        }
    }

    public void AddPlayer(bool isBot)
    {
        CardPlayer player = new CardPlayer(isBot);
        _players.Add(player);
    }

    public void RemovePlayer(int index)
    {
        _players.RemoveAt(index);
    }
}
