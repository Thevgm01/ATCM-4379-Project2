using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTarget : MonoBehaviour, ITargetable, IDamageable
{
    public void Kill()
    {
        Debug.Log("Test target has died!");
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Test target has taken " + damage + " damage!");
    }

    public void Target()
    {
        Debug.Log("TARGET test target.");
    }
}
