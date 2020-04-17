using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOnClick : MonoBehaviour, ITargetable
{
    void Start()
    {

    }

    private void OnMouseOver()
    {
        // if we're hovering over and detect mouse click...
        if (Input.GetMouseButtonDown(0))
        {
            Target();
        }
    }

    public void Target()
    {
        
    }
}
