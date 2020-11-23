using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] GameObject sensetiveInformation;
    [SerializeField] TMPro.TextMeshPro title;
    [SerializeField] TMPro.TextMeshPro description;
    [SerializeField] SpriteRenderer image;
    [SerializeField] [Range(0f, 1f)] float lerpSpeed;

    private bool moving;
    private Quaternion rightSideUp = Quaternion.identity;
    private Quaternion upsideDown = Quaternion.Euler(0, 180, 0);

    public Vector3 desiredPosition;
    public bool isVisible;

    public void LoadCardData(CardData card)
    {
        title.text = card.Name;
        description.text = card.Description;
        image.sprite = card.Graphic;
    }

    public void SetVisible(bool visible)
    {
        sensetiveInformation.SetActive(visible);
    }

    void Update()
    {
        if(moving)
        {
            float lerpAmount = lerpSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, lerpAmount)
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredPosition, lerpAmount)
        }
    }
}
