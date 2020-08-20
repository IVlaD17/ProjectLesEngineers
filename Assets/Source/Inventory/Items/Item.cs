using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IDropable
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }

    public ItemType MyType { get; protected set; }
    public int MaxStackSize { get; protected set; }

    public bool IsConsumable { get; protected set; }
    public bool IsPlaceable { get; protected set; }
    public bool IsEquippable { get; protected set; }

    public List<string> CraftingMaterials { get; protected set; }

    public Sprite MySprite { get; protected set; }

    public Item(string name, string description, int maxStackSize, Sprite sprite, List<string> craftingMaterials)
    {
        Name = name;
        Description = description;
        MaxStackSize = maxStackSize;
        MySprite = sprite;
        CraftingMaterials = craftingMaterials;
    }
}
