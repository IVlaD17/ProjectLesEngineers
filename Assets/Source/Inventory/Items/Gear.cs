using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : Item, IEquipable
{
    public Gear(string name, string description, int maxStackSize, Sprite sprite, List<string> craftingMaterials) : base(name, description, maxStackSize, sprite, craftingMaterials)
    {
        MyType = ItemType.GEAR;

        IsConsumable = false;
        IsEquippable = true;
        IsPlaceable = false;
    }
}
