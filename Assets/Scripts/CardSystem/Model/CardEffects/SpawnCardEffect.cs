using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpawnEffect", menuName = "Card/Effects/Spawn")]
public class SpawnCardEffect : CardPlayEffect
{
    [SerializeField] GameObject _objectToSpawn = null;

    public override void Activate(ITargetable target)
    {
        // test to see if the target is a Unity object
        MonoBehaviour worldObject = target as MonoBehaviour;
        // if it is, apply damage
        if (worldObject != null)
        {
            Vector3 positionToSpawn = worldObject.transform.position;
            GameObject newGameObject = Instantiate
                (_objectToSpawn, positionToSpawn, Quaternion.identity);
            Debug.Log("Spawn the object");
        }
        else
        {
            Debug.Log("Card is not a world object...");
        }
    }

    public override bool IsTargetValid(ITargetable target)
    {
        MonoBehaviour worldObject = target as MonoBehaviour;

        if (worldObject != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
