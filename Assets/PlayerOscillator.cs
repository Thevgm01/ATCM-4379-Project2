using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOscillator : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float minAngle;
    [SerializeField] float maxAngle;
    float angle;

    // Update is called once per frame
    void Update()
    {
        angle += speed * Time.deltaTime;
        if (angle > Mathf.PI * 2) angle -= Mathf.PI * 2;

        float angleDiff = maxAngle - minAngle;
        transform.localRotation = Quaternion.Euler((Mathf.Cos(angle) * 0.5f + 0.5f) * (maxAngle - minAngle) + minAngle, 0, 0);
    }
}
