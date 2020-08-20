using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemsDatabase
{
    public static List<Item> Items { get; private set; }

    static ItemsDatabase()
    {
        Items = new List<Item>();

        Items.Add(new Component("Wood", "Crafting material used in constructions.", 1, Game.Instance.MySprites["wood"], new List<string>()));
        Items.Add(new Component("Coal", "Crafting material used in cooking food.", 1, Game.Instance.MySprites["coal"], new List<string>()));
        Items.Add(new Component("Iron", "Crafting material used in crafting weaponry.", 1, Game.Instance.MySprites["coal"], new List<string>()));
        Items.Add(new Component("Stone", "Crafting material used in constructions.", 1, Game.Instance.MySprites["stone"], new List<string>()));
        Items.Add(new Component("Flour", "Crafting material used in cooking food.", 1, Game.Instance.MySprites["flour"], new List<string>() { "Wheat" }));
        Items.Add(new Component("Wheat", "Crafting material used in cooking food and constructions.", 1, Game.Instance.MySprites["wheat"], new List<string>()));
        Items.Add(new Component("Planks", "Crafting material used in constructions.", 1, Game.Instance.MySprites["wood"], new List<string>() { "Wood", "Wood" }));
        Items.Add(new Component("Sticks", "Crafting material used in constructions.", 1, Game.Instance.MySprites["wood"], new List<string>() { "Wood" }));
        Items.Add(new Component("Timber", "Crafting material used in constructions.", 1, Game.Instance.MySprites["wood"], new List<string>() { "Wood", "Wood", "Wood" }));
        Items.Add(new Component("Thatch", "Crafting material used in constructions.", 1, Game.Instance.MySprites["wheat"], new List<string>() { "Wheat", "Wheat" }));
        Items.Add(new Component("Flask", "Crafting material used in preparing potions.", 1, Game.Instance.MySprites["potion"], new List<string>() { "Leather" }));
        Items.Add(new Component("Arrows", "Arrows are used by the bow to hunt.", 1, Game.Instance.MySprites["quiver"], new List<string>() { "Iron", "Sticks" }));
        Items.Add(new Component("Leather", "Crafting material used in preparing potions and constructions.", 1, Game.Instance.MySprites["leather"], new List<string>()));

        Items.Add(new Consumable("Potion", "Regenerates the player's health and stamina.", 1, Game.Instance.MySprites["potion"],
            new List<string>() { "Flask", "Herbs", "Mushrooms" },
            new Dictionary<string, float>() { { "HealthRegen", 35 }, { "StaminaRegen", 35 } }));
        Items.Add(new Consumable("Cooked Meat", "Prepared meat that greatly reduces hunger.", 1, Game.Instance.MySprites["meat"],
            new List<string>() { "Meat", "Coal", "Campfire" },
            new Dictionary<string, float>() { { "HungerDrain", 50 } }));
        Items.Add(new Consumable("Stew", "Prepared meat that greatly reduces hunger and regenerates some stamina.", 1, Game.Instance.MySprites["meat"],
            new List<string>() { "Meat", "Herbs", "Mushrooms", "Campfire" },
            new Dictionary<string, float>() { { "HungerDrain", 50 }, { "StaminaRegen", 20 } }));
        Items.Add(new Consumable("Meat", "Crafting material used in cooking food. It can be eaten to satisfy your hunger.", 1, Game.Instance.MySprites["meat"],
            new List<string>(),
            new Dictionary<string, float>() { { "HungerDrain", 5 } }));
        Items.Add(new Consumable("Herbs", "Crafting material used in cooking food and preparing potions. They can be eaten to slightly heal yourself.", 1, Game.Instance.MySprites["herbs"],
            new List<string>(),
            new Dictionary<string, float>() { { "HealthRegen", 10 } }));
        Items.Add(new Consumable("Mushrooms", "Crafting material used in cooking food and preparing potions. They can be eaten to generate some stamina.", 1, Game.Instance.MySprites["mushrooms"],
            new List<string>(),
            new Dictionary<string, float>() { { "StaminaRegen", 10 } }));

        Items.Add(new Structure("Hut", "Simple building that can offer protection against the elements.", 1, Game.Instance.MySprites["hut"],
            new List<string>() { "Timber", "Timber", "Timber", "Thatch", "Thatch" }));
        Items.Add(new Structure("Cabin", "Medium sized building that offers protections against the elements and some crafting facilities.", 1, Game.Instance.MySprites["cabin"], new List<string>() { "Timber", "Timber", "Planks", "Planks", "Thatch", "Thatch" }));
        Items.Add(new Structure("House", "Huge building that offers all facilities required.", 1, Game.Instance.MySprites["cabin"],
            new List<string>() { "Stone", "Coal", "Timber", "Planks", "Planks", "Sticks" }));
        Items.Add(new Structure("Campfire", "Simple structure used in cooking.", 1, Game.Instance.MySprites["campfire"],
            new List<string>() { "Stone", "Coal" }));

        Items.Add(new Gear("Axe", "My Axe made for cutting", 1, Game.Instance.MySprites["axe1"], new List<string>()));
        Items.Add(new Gear("Pickaxe", "My Pickaxe made for mining", 1, Game.Instance.MySprites["pickaxe"], new List<string>()));
        Items.Add(new Gear("Bow", "My Bow made for hunting", 1, Game.Instance.MySprites["bow"], new List<string>()));
    }

    public static List<string> GetCraftingMaterialsList(string itemName)
    {
        foreach (Item item in Items)
            if (item.Name == itemName)
                return item.CraftingMaterials;

        return null;
    }
    public static Item CreateNewItem(string itemName)
    {
        foreach(Item item in Items)
        {
            if(item.Name.ToLowerInvariant() == itemName.ToLowerInvariant())
            {
                if(item.MyType == ItemType.COMPONENT)
                {
                    return new Component(item.Name, item.Description, item.MaxStackSize, item.MySprite, item.CraftingMaterials);
                }

                if(item.MyType == ItemType.CONSUMABLE)
                {
                    Consumable consumableItem = item as Consumable;
                    return new Consumable(consumableItem.Name, consumableItem.Description, consumableItem.MaxStackSize, consumableItem.MySprite, consumableItem.CraftingMaterials, consumableItem.Effects);
                }

                if(item.MyType == ItemType.STRUCTURE)
                {
                    return new Structure(item.Name, item.Description, item.MaxStackSize, item.MySprite, item.CraftingMaterials);
                }

                if(item.MyType == ItemType.GEAR)
                {
                    return new Gear(item.Name, item.Description, item.MaxStackSize, item.MySprite, item.CraftingMaterials);
                }
            }
        }

        return null;
    }
}
