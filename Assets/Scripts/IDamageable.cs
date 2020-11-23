using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable : ITargetable
{
    void TakeDamage();
}
