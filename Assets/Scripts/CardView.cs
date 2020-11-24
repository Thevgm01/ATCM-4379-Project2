using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardView : MonoBehaviour
{
    public Action<CardView> MouseOver = delegate { };

    [SerializeField] GameObject sensetiveInformation;
    [SerializeField] TMPro.TextMeshPro title;
    [SerializeField] TMPro.TextMeshPro description;
    [SerializeField] SpriteRenderer image;

    public Vector3 desiredPosition { get; private set; }
    private bool moving;
    private bool world;

    private float curAngle = 0;
    private float flipAngle = 0;
    private bool Visible => sensetiveInformation.activeSelf;
    private bool rotating;

    public void LoadCardData(CardData card)
    {
        title.text = card.Name;
        description.text = card.Description;
        image.sprite = card.Graphic;

        if (card is WeaponCardData)
        {
            WeaponCardData weapon = (WeaponCardData)card;
            description.text +=
                "\nDamage: " + weapon.Damage +
                "\nShots: " + weapon.NumberOfShots +
                "\nComponent slots: " + weapon.SlotsRequired +
                "\nEnergy to fire: " + weapon.CostToFire;
        }
    }

    public void SetPosition(Vector3 position, bool worldRelative = false)
    {
        desiredPosition = position;
        moving = true;
        world = worldRelative;
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
            if (world)
            {
                Vector3 posDelta = desiredPosition - transform.position;
                if (posDelta.sqrMagnitude > 0.001f)
                {
                    transform.position += posDelta * lerpAmount;
                }
                else
                {
                    transform.position = desiredPosition;
                    moving = false;
                }
            }
            else
            {
                Vector3 posDelta = desiredPosition - transform.localPosition;
                if (posDelta.sqrMagnitude > 0.001f)
                {
                    transform.localPosition += posDelta * lerpAmount;
                }
                else
                {
                    transform.localPosition = desiredPosition;
                    moving = false;
                }
            }
        }

        if(rotating)
        {
            float rotDelta = curAngle - flipAngle;
            if (rotDelta < 0.001f)
            {
                curAngle -= rotDelta * lerpAmount;
            }
            else
            {
                curAngle = flipAngle;
                if(curAngle == -180) sensetiveInformation.SetActive(false);
                rotating = false;
            }
            transform.localRotation = Quaternion.Euler(0, curAngle, 0);
        }
    }

    void OnMouseOver()
    {
        if(Visible) MouseOver?.Invoke(this);
    }
}
