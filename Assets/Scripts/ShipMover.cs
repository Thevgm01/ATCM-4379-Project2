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

    float bank = 0;

    float time;
    float randXOffset, randYOffset;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.localPosition;
        if (transform.localRotation.eulerAngles.z > 90) bankScale = -bankScale;

        randXOffset = Random.Range(0, 1000);
        randYOffset = Random.Range(0, 1000);
    }

    // Update is called once per frame
    void Update()
    {
        time += speed * Time.deltaTime;
        float x = (Mathf.PerlinNoise(time * speed, randXOffset) - 0.5f) * maxX;
        float y = (Mathf.PerlinNoise(time * speed, randYOffset) - 0.5f) * maxY;
        transform.localPosition = startPos + new Vector3(x, y, 0);

        bank = Mathf.Lerp(bank, x, 0.1f);
        transform.localRotation = Quaternion.Euler(0, bank * bankScale, 0);
    }
}
