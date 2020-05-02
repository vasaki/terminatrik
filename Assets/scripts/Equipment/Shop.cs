using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    Rect canvasMeasure = new Rect();
    Rect itemMeasure = new Rect();
    Vector2 itemCenter = new Vector2();
    Text goldText;
    float gold;
    int maxItemsPerRow;
    Equipment equipment;

    void MeasureSizes()
    {
        GameObject testItem =
            (GameObject)Instantiate(Resources.Load("Equipment/ShopItem"), transform);
        Transform topLeft = 
            testItem.transform.Find("TopLeftCorner");
        Transform bottomRight = 
            testItem.transform.Find("BottomRightCorner");
        itemMeasure.Set(0, 0, bottomRight.position.x - topLeft.position.x,
            topLeft.position.y - bottomRight.position.y);
        itemCenter.x = testItem.transform.position.x - topLeft.position.x;
        itemCenter.y = testItem.transform.position.y - topLeft.position.y;
        Destroy(testItem);
        topLeft = transform.Find("TopLeftCorner");
        bottomRight = transform.Find("BottomRightCorner");
        canvasMeasure.Set(topLeft.position.x, topLeft.position.y,
            bottomRight.position.x - topLeft.position.x,
            topLeft.position.y - bottomRight.position.y);
        maxItemsPerRow = Mathf.FloorToInt(canvasMeasure.width / itemMeasure.width);
    }
    private void Awake()
    {
        goldText = transform.Find("GoldText").gameObject.GetComponent<Text>();
        EventManager.AddListener(EventNames.GoldChange, OnGoldChange);
        EventManager.AddListener(EventNames.NoGoldMessage, OnNoGoldMessage);
    }
    void Start()
    {
        Time.timeScale = 0;
        gold = GameManager.Gold;
        GoldTextChange();
    }
    public void OnDoneButton()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void OnDestroy()
    {
        Time.timeScale = 1;
    }
    void GoldTextChange()
    {
        goldText.text = GameConstants.ScoreTextTitle + gold;
    }
    public void DisplayItems(Equipment equipment)
    {
        Text descriptionText = 
            transform.Find("DescriptionText").gameObject.GetComponent<Text>();
        this.equipment = equipment;
        descriptionText.enabled = false;
        int displayedNo = 0;
        int placeInRow = 0;
        MeasureSizes();
        int rowsNo = Mathf.FloorToInt(equipment.Count / maxItemsPerRow);
        if (equipment.Count % maxItemsPerRow > 0) rowsNo++;
        Vector3 itemPosition = new Vector3();
        itemPosition.y = canvasMeasure.y + itemCenter.y -
            ((canvasMeasure.height - (rowsNo * itemMeasure.height)) / 2) + itemMeasure.height;
        foreach(KeyValuePair<EquipmentType, EquipmentData> piece in equipment)
        {
            EquipmentData item = piece.Value;
            if (placeInRow == 0)
            {
                int itemsInThisRow = Mathf.Min(equipment.Count - displayedNo, maxItemsPerRow);
                itemPosition.y -= itemMeasure.height;
                itemPosition.x = canvasMeasure.x + itemCenter.x + 
                    (canvasMeasure.width - (itemsInThisRow * itemMeasure.width)) / 2;
            }
            //set shop item parameters
            GameObject shopItemObj = 
                (GameObject)Instantiate(Resources.Load("Equipment/ShopItem"), transform);
            ShopItem shopItem = shopItemObj.GetComponent<ShopItem>();
            shopItemObj.transform.position = itemPosition;
            shopItem.SetEquipmentCard(equipment, item, descriptionText, gold);
            //prepare for next item
            displayedNo++;
            placeInRow++;
            if (placeInRow == maxItemsPerRow) placeInRow = 0;
            else itemPosition.x += itemMeasure.width;
        }
    }
    private void OnGoldChange(EventParameter param)
    {
        gold = ((FloatParam)param).Float;
        GoldTextChange();
    }
    private void OnNoGoldMessage(EventParameter param)
    {
        Instantiate(Resources.Load("Menus/NoGoldText"), transform);
    }

}
