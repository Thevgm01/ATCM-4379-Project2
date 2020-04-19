using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameView : MonoBehaviour
{
    [SerializeField] PlayerView _playerView = null;

    CardPlayerController _playerController = null;

    public void Setup(CardGameSM gameController)
    {
        _playerController = gameController.PlayerController;

        _playerView.Setup(_playerController.Players[0], gameController.TargetController);
        //TODO simulate that player is 0 index, in this case. Fix in the future
        CardPlayer playerToSetup = _playerController.Players[0];
        if(playerToSetup.Hand.Count > 0)
        {
            SetupPlayerCards(playerToSetup);
        }
    }

    void SetupPlayerCards(CardPlayer player)
    {
        for (int i = 0; i < player.Hand.Count; i++)
        {
            AbilityCard newCard = player.Hand.GetCard(i);
            _playerView.AddNewCard(newCard);
        }
    }
}
