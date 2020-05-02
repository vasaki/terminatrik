using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour
{
    Text titleLabel;
    Text descriptionText;
    Text priceLabel;
    Text buttonLabel;
    string description;
    Image title;
    Image image;
    Image priceBackground;
    Button button;
    Equipment equipment;
    EquipmentData thisEquipment;
    private void Awake()
    {
        titleLabel = transform.Find("TitleLabel").gameObject.GetComponent<Text>();
        buttonLabel = transform.Find("ButtonLabel").gameObject.GetComponent<Text>();
        image = GetComponent<Image>();
        button = transform.Find("Button").gameObject.GetComponent<Button>();
        title = transform.Find("Title").gameObject.GetComponent<Image>();
        priceBackground = transform.Find("PriceBackground").gameObject.GetComponent<Image>();
        priceLabel = transform.Find("PriceLabel").gameObject.GetComponent<Text>();
        EventManager.AddListener(EventNames.EquipmentStatusChanged, OnEquipmentStatusChange);
    }
    public void SetEquipmentCard(Equipment equipment, EquipmentData eqData, 
        Text descriptionText, float gold)
    {
        this.equipment = equipment;
        thisEquipment = eqData;
        titleLabel.text = thisEquipment.name;
        Instantiate(Resources.Load(thisEquipment.prefab), transform);
        description = thisEquipment.description;
        this.descriptionText = descriptionText;
        priceLabel.text = thisEquipment.price.ToString();
        UpdateButton();
    }
    void UpdateButton()
    {
        if (thisEquipment.status == EquipmentStatus.NotAvailable) SetInactive();
        else
        {
            if (thisEquipment.hand == EquipmentHand.Secondary) SetSecondary();
            if (thisEquipment.status == EquipmentStatus.Equipped)
            {
                buttonLabel.text = "EQUIPPED";
                button.interactable = false;
            }
            else if (thisEquipment.status == EquipmentStatus.Owned)
            {
                buttonLabel.text = "EQUIP";
                button.interactable = true;
            }
        }
    }
    void OnEquipmentStatusChange(EventParameter param)
    {
        thisEquipment = equipment[thisEquipment.type];
        UpdateButton();
    }
    public void OnButtonClick()
    {
        if (thisEquipment.status == EquipmentStatus.Available)
        {
            if (GameManager.SpendGold(thisEquipment.price))
                equipment.ChangeStatus(thisEquipment.type, EquipmentStatus.Owned);
        }
        else if (thisEquipment.status == EquipmentStatus.Owned)
            equipment.ChangeStatus(thisEquipment.type, EquipmentStatus.Equipped);
        EventSystem.current.SetSelectedGameObject(null);
    }
    void SetInactive()
    {
        image.color = Color.grey;
        button.interactable = false;
        title.color = Color.grey;
        Color pg = Color.grey;
        pg.a = 0.5f;
        priceBackground.color = pg;
    }
    void SetSecondary()
    {
        image.color = GameConstants.SecondaryEquipmentColor;
        title.color = GameConstants.SecondaryEquipmentColor;
        Color pg = GameConstants.SecondaryEquipmentColor;
        pg.a = 0.5f;
        priceBackground.color = pg;
        ColorBlock buttonColor = button.colors;
        buttonColor.normalColor = GameConstants.SecondaryEquipmentColor;
        button.colors = buttonColor;
    }
    public void OnPointerEnter()
    {
        descriptionText.text = description;
        descriptionText.enabled = true;
    }
    public void OnPointerExit()
    {
        descriptionText.enabled = false;
    }
}
