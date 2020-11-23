using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : MonoBehaviour
{
    [SerializeField] float maxX;
    [SerializeField] float maxY;
    [SerializeField] float speed;
    [SerializeField] float bankScale;

    Vector3 startPos;

    float xAngle = 0, xSpeed = 0, yAngle = 0, ySpeed = 0;
    float bank = 0;
    float lastX = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        float x = (Mathf.PerlinNoise(Time.realtimeSinceStartup * speed, 0) - 0.5f) * maxX;
        float y = (Mathf.PerlinNoise(Time.realtimeSinceStartup * speed, 100) - 0.5f) * maxY;
        transform.position = startPos + new Vector3(x, y, 0);

        bank = Mathf.Lerp(bank, x, 0.1f);
        transform.rotation = Quaternion.Euler(0, bank * bankScale, 0);
    }
}
