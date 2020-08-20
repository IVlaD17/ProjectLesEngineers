using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item, IConsumable
{
    public Dictionary<string, float> Effects { get; private set; }
    public Consumable(string name, string description, int maxStackSize, Sprite sprite, List<string> craftingMaterials, Dictionary<string, float> effects) : base(name, description, maxStackSize, sprite, craftingMaterials)
    {
        MyType = ItemType.CONSUMABLE;

        IsConsumable = true;
        IsEquippable = false;
        IsPlaceable = false;

        Effects = effects;
    }

    public void Consume()
    {
        foreach(KeyValuePair<string, float> effect in Effects)
        {
            if(effect.Key.Contains("Health"))
            {
                if(effect.Key.Contains("Regen"))
                {
                    Game.Instance.MyPlayer.RegenHealth(effect.Value);
                }

                if (effect.Key.Contains("Drain"))
                {
                    Game.Instance.MyPlayer.DrainHealth(effect.Value);
                }
            }

            if (effect.Key.Contains("Hunger"))
            {
                if (effect.Key.Contains("Regen"))
                {
                    Game.Instance.MyPlayer.RegenHunger(effect.Value);
                }

                if (effect.Key.Contains("Drain"))
                {
                    Game.Instance.MyPlayer.DrainHunger(effect.Value);
                }
            }

            if (effect.Key.Contains("Stamina"))
            {
                if (effect.Key.Contains("Regen"))
                {
                    Game.Instance.MyPlayer.RegenStamina(effect.Value);
                }

                if (effect.Key.Contains("Drain"))
                {
                    Game.Instance.MyPlayer.DrainStamina(effect.Value);
                }
            }
        }
    }
}
