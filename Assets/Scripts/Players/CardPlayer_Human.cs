using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardPlayer_Human : CardPlayer
{
    [SerializeField] Camera _camera;

    Collider grabCollider;
    Card grabbedCard = null;
    CardView grabbedCardView = null;

    [SerializeField] TMPro.TextMeshProUGUI shipInfoGUI;
    [SerializeField] TMPro.TextMeshProUGUI energyText;

    void Update()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point);

            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                if (grabbedCardView == null)
                {
                    CardView cardViewUnderMouse = hit.transform.GetComponent<CardView>();
                    if (cardViewUnderMouse != null)
                    {
                        Card cardUnderMouse = cardViewer.cardsViewDictionary_inverse[cardViewUnderMouse];
                        if (hand.Contains(cardUnderMouse))
                        {
                            cardViewer.ReorganizeHand();
                            cardViewUnderMouse.SetPosition(cardViewUnderMouse.desiredPosition + new Vector3(0, 2, -2));
                            SetShipInfo(null, null);
                        }
                    }
                    else
                    {
                        Ship shipUnderMouse = hit.transform.GetComponent<Ship>();
                        SetShipInfo(shipUnderMouse, hit.transform);
                        if (shipUnderMouse != null)
                            cardViewer.ReorganizeHand();
                    }
                }
                else
                {
                    grabbedCardView.SetPosition(hit.point, true);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                CardView cardViewUnderMouse = hit.transform.GetComponent<CardView>();
                if (cardViewUnderMouse != null)
                {
                    Card cardUnderMouse = cardViewer.cardsViewDictionary_inverse[cardViewUnderMouse];
                    if (hand.Contains(cardUnderMouse))
                        GrabCard(cardUnderMouse, cardViewUnderMouse);
                }
            }
        }

        if (grabbedCard != null && Input.GetMouseButtonUp(0))
        {
            TryPlayCard(grabbedCard, hit.transform, hit.point);
            UngrabCard();
        }
        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            hand.Add(draw.Draw());
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            discard.Add(hand.Draw());
        }
        */
        energyText.text = "Energy: " + energy;
    }

    void SetShipInfo(Ship s, Transform t)
    {
        if (s == null) shipInfoGUI.gameObject.SetActive(false);
        else
        {
            shipInfoGUI.gameObject.SetActive(true);
            shipInfoGUI.transform.position = _camera.WorldToScreenPoint(t.position);
            shipInfoGUI.text = 
                (s.owner == this ? "Ally " : "Enemy ") + s.name + "\n" +
                s.hitPoints + "/" + s.maxHitPoints + " HP\n" +
                (int)(s.evasionChance * 100) + "% evasion\n" +
                s.damageReduction + " armor\n" +
                s.componentSlots + "/" + s.maxComponentSlots + " components free";
        }
    }

    void GrabCard(Card card, CardView cardView)
    {
        grabbedCard = card;
        grabbedCardView = cardView;
        cardView.transform.parent = transform;

        grabCollider = cardView.GetComponent<Collider>();
        grabCollider.enabled = false;

        cardViewer.MoveCardsOutOfTheWay();

        if (card.Data != null && (
            card.Data.Target == CardData.TargetType.AllyField ||
            card.Data.Target == CardData.TargetType.AllyShip))
        {

        }
        //GrabCard?.Invoke(cardUnderMouse);
    }

    void UngrabCard()
    {
        grabbedCardView.transform.parent = cardViewer.handPile;
        cardViewer.ReorganizeHand();
        cardViewer.MoveCardsBack();

        grabbedCard = null;
        grabbedCardView = null;
        grabCollider.enabled = true;
        grabCollider = null;
    }
}
