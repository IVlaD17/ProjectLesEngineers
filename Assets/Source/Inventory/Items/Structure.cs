using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : Item, IPlaceable
{
    public Structure(string name, string description, int maxStackSize, Sprite sprite, List<string> craftingMaterials) : base(name, description, maxStackSize, sprite, craftingMaterials)
    {
        MyType = ItemType.STRUCTURE;

        IsConsumable = false;
        IsEquippable = false;
        IsPlaceable = true;
    }
}
