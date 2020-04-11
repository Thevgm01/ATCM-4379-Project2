using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCardView : MonoBehaviour
{
    [SerializeField] Text _nameTextUI = null;
    [SerializeField] Text _costTextUI = null;
    [SerializeField] Text _descriptionTextUI = null;
    [SerializeField] Image _graphicUI = null;

    public void Display(AbilityCard card)
    {
        _nameTextUI.text = card.Name;
        _costTextUI.text = card.Cost.ToString();
        _descriptionTextUI.text = card.Description;
        _graphicUI.sprite = card.Graphic;
    }
}
