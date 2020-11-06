using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, ITargetable
{
    public PlayerView _playerView;

    public void Target()
    {
        Debug.Log("TARGET platform.");
    }

    void OnMouseDown()
    {
        _playerView.SetTarget(this);
    }
}
