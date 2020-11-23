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

    void Update()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point);

            if (grabbedCardView == null)
            {
                CardView cardViewUnderMouse = hit.transform.GetComponent<CardView>();
                if (cardViewUnderMouse != null)
                {
                    Card cardUnderMouse = cardViewer.cardsViewDictionary_inverse[cardViewUnderMouse];
                    if (hand.Contains(cardUnderMouse))
                    {
                        /*
                        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
                        {
                            cardViewer.ReorganizeHand(null);
                            cardViewUnderMouse.SetPosition(cardViewUnderMouse.desiredPosition + new Vector3(0, 2, -2));
                        }
                        */
                        if (Input.GetMouseButtonDown(0))
                        {
                            GrabCard(cardUnderMouse, cardViewUnderMouse);
                        }
                    }
                }
                else
                {
                    cardViewer.ReorganizeHand();
                }
            }
            else
            {
                grabbedCardView.SetPosition(hit.point, true);
            }
        }

        if (grabbedCard != null && Input.GetMouseButtonUp(0))
        {
            if (grabbedCard.Data != null &&
                IsValidTarget(grabbedCard.Data.Target, hit.transform))
            {
                grabbedCard.Play();
            }
            UngrabCard();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            hand.Add(draw.Draw());
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            discard.Add(hand.Draw());
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
