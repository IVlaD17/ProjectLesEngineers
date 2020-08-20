using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public Player MyOwner { get; private set; }
    public int MaxInventorySize { get; private set; }
    public List<Item> MyItems { get; private set; }
    public List<int> MyStacks { get; private set; }

    public Inventory(Player myOwner, int maxSize)
    {
        MyOwner = myOwner;
        MaxInventorySize = maxSize;
        MyItems = new List<Item>();

        MyItems.Add(ItemsDatabase.CreateNewItem("axe"));
        MyItems.Add(ItemsDatabase.CreateNewItem("bow"));
        MyItems.Add(ItemsDatabase.CreateNewItem("pickaxe"));

        MyItems.Add(ItemsDatabase.CreateNewItem("arrows"));
        MyItems.Add(ItemsDatabase.CreateNewItem("arrows"));
        MyItems.Add(ItemsDatabase.CreateNewItem("arrows"));
        MyItems.Add(ItemsDatabase.CreateNewItem("arrows"));
        MyItems.Add(ItemsDatabase.CreateNewItem("arrows"));
    }

    public void AddItem(Item newItem)
    {
        MyItems.Add(newItem);
    }

    public void RemoveItem(Item item)
    {
        MyItems.Remove(item);
    }
}
