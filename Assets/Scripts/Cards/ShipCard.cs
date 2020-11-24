using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCard : Card
{
    public ShipCard(ShipCardData cardData)
    {
        Data = cardData;
    }

    public Ship MakeShip(CardPlayer owner, Vector3 position, Transform field)
    {
        ShipCardData shipData = (ShipCardData)Data;

        GameObject newShip = GameObject.Instantiate(shipData.Model, field);
        newShip.transform.position = position;
        Ship ship = newShip.AddComponent<Ship>();
        ship.LoadData(owner, shipData);
        return ship;
    }
}
