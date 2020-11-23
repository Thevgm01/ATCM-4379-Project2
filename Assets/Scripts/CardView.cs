using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] GameObject sensetiveInformation;
    [SerializeField] TMPro.TextMeshPro title;
    [SerializeField] TMPro.TextMeshPro description;
    [SerializeField] SpriteRenderer image;

    private Vector3 desiredPosition;
    private bool moving;

    private float curAngle = 0;
    private float flipAngle = 0;
    private bool Visible => sensetiveInformation.activeSelf;
    private bool rotating;

    public void LoadCardData(CardData card)
    {
        title.text = card.Name;
        description.text = card.Description;
        image.sprite = card.Graphic;
    }

    public void SetPosition(Vector3 position)
    {
        desiredPosition = position;
        moving = true;
    }

    public void SetVisible(bool visible)
    {
        if (visible)
        {
            flipAngle = 0;
            sensetiveInformation.SetActive(true);
        }
        else
        {
            flipAngle = -180;
        }
        rotating = true;
    }

    public void Move(float lerpAmount)
    {
        if (moving)
        {
            Vector3 posDelta = desiredPosition - transform.localPosition;
            if(posDelta.sqrMagnitude > 0.001f)
            {
                transform.localPosition += posDelta * lerpAmount;
            }
            else
            {
                transform.localPosition = desiredPosition;
                moving = false;
            }
        }

        if(rotating)
        {
            float rotDelta = curAngle - flipAngle;
            if (rotDelta < 0.001f)
            {
                curAngle -= rotDelta * lerpAmount;

                //if (curAngle >= 90 && Visible) sensetiveInformation.SetActive(false);
                //else if (curAngle < 90 && !Visible) sensetiveInformation.SetActive(true);
            }
            else
            {
                curAngle = flipAngle;
                if(curAngle == 180) sensetiveInformation.SetActive(true);
                rotating = false;
            }
            transform.rotation = Quaternion.Euler(0, curAngle, 0);
        }
    }
}
