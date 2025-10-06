using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public Inventory Container;
    public void addItem(Item _item, int amount)
    {
        
        bool hasItem = false;
        for (int i = 0; i < Container.Items.Length; i++) {
            if (Container.Items[i].Item.ID == _item.ID)
            {
                Container.Items[i].addAmount(amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            SetEmptySlot(_item, amount);
        }
    }
    public bool UseItem(int index)
    {
        if (Container.Items[index].amount <= 1)
        {
            RemoveItem(Container.Items[index].Item);
            return true;
        }
        else if (Container.Items[index].Item.type != itemType.Weapon)
        {
            Container.Items[index].amount--;
            return false;
        }
        return false;
    }
    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.Item, item2.amount,item2.id);
        item2.UpdateSlot(item1.Item,item1.amount, item1.id);
        item1.UpdateSlot(temp.Item, temp.amount, temp.id);
    }
    public void RemoveItem(Item _item)
    {
        for(int i = 0;i < Container.Items.Length; i++)
        {
            if (Container.Items[i].Item == _item)
            {
                Container.Items[i].UpdateSlot(null, 0,-1);
            }
        }
    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for(int i = 0;i < Container.Items.Length; i++)
        {
            if (Container.Items[i].id <= -1)
            {
                Container.Items[i].UpdateSlot(_item, _amount, _item.ID);
                return Container.Items[i];
            }
        }
        //what happens when inventory is full
        return null;
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[20];

}
[System.Serializable]
public class InventorySlot
{
    public UserInterface parent;
    public int id = -1;
    public Item Item;
    public int amount;
    public InventorySlot(Item item, int amount, int id)
    {
        Item = item;
        this.amount = amount;
        this.id = id;
    }
    public InventorySlot()
    {
        id = -1;
        Item = null;
        this.amount = 0;
    }
    public void addAmount(int amnt)
    {
        amount += amnt;
    }
    public void UpdateSlot(Item item, int amount, int id)
    {
        Item = item;
        this.amount = amount;
        this.id = id;
    }
}
