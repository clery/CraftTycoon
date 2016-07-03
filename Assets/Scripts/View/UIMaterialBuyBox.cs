using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UIMaterialBuyBox : MonoBehaviour, IPointerClickHandler {

    private CraftMaterial _material;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Text _name;
    [SerializeField]
    private Text _description;
    [SerializeField]
    private Text _price;
    [SerializeField]
    private Text _multiplierText;
    [SerializeField]
    private GameObject _hiddenPart;

    public void Load(CraftMaterial material) {
        _icon.sprite = SpriteDatabase.Instance.Sprites[material.IconId];
        _material = material;
        _name.text = material.Name;
        _description.text = material.Description;
        _price.text = material.BasePrice.ToString();
        _name.color = CraftMaterial.RarityColor[material.Rarity];
    }

    public void OnPointerClick(PointerEventData e) {
        _hiddenPart.SetActive(!_hiddenPart.activeSelf);
    }

    public void ChangeMultiplier() {
        MarketManager.Instance.ChangeMultiplier(_material.Id);
        _multiplierText.text = "x" + MarketManager.Instance.Multipliers[_material.Id].ToString();
        _price.text = (_material.BasePrice * MarketManager.Instance.Multipliers[_material.Id]).ToString();
    }

    public void Buy() {
        MarketManager.Instance.Buy(_material.Id);
    }
}
