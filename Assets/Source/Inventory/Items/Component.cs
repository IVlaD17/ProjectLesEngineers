using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component : Item
{
    public Component(string name, string description, int maxStackSize, Sprite sprite, List<string> craftingMaterials) : base(name, description, maxStackSize, sprite, craftingMaterials)
    {
        MyType = ItemType.COMPONENT;

        IsConsumable = false;
        IsEquippable = false;
        IsPlaceable = false;
    }
}
