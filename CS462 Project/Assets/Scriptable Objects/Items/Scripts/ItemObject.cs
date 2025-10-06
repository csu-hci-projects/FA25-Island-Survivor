using JetBrains.Annotations;
using UnityEngine;

public enum itemType
{
    Food,
    Weapon,
    Equipment,
    Healing,
    Key
}
public abstract class ItemObject : ScriptableObject
{
    public int ID;
    public Sprite uiDisplay;
    public GameObject playerHoldingObject;
    public itemType type;
    [TextArea(15,20)]
    public string description;

    public abstract bool Use();
    
    
}

[System.Serializable]
public class Item
{
    public string Name;
    public int ID = 0;
    public Sprite uiDisplay;
    public GameObject playerHoldingObject;
    public itemType type;
    public ItemObject itemObject;
    public Item(ItemObject item)
    {
        Name = item.name;
        ID = item.ID;
        uiDisplay = item.uiDisplay;
        playerHoldingObject = item.playerHoldingObject;
        type = item.type;
        itemObject = item;
    }
    public Item()
    {
        Name = "";
        ID = -1;
        uiDisplay = null;
        playerHoldingObject = null;
        itemObject = null;
    }
    public bool UseItem()
    {
        return itemObject.Use();
    }
}