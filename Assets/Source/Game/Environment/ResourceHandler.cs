    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    public bool IsMinable { get; private set; }
    public bool IsHackable { get; private set; }
    public bool IsHuntable { get; private set; }
    public bool IsHarvestable { get; private set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if(name.Contains("Coal") || name.Contains("Iron") || name.Contains("Stone"))
        {
            IsMinable = true;
            IsHackable = false;
            IsHuntable = false;
            IsHarvestable = false;
        }

        if(name.Contains("Tree"))
        {
            IsMinable = false;
            IsHackable = true;
            IsHuntable = false;
            IsHarvestable = false;
        }

        if(name.Contains("Wheat") || name.Contains("Herbs") || name.Contains("Mushroom"))
        {
            IsMinable = false;
            IsHackable = false;
            IsHuntable = false;
            IsHarvestable = true;
        }

        if(name.Contains("Deer"))
        {
            IsMinable = false;
            IsHackable = false;
            IsHuntable = true;
            IsHarvestable = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item Harvest()
    {
        if(IsMinable)
        {
            LootHandler loot = Instantiate(Game.Instance.MyPrefabs["loot_box"], transform.position, Quaternion.identity).GetComponent<LootHandler>();

            if (name.Contains("Coal"))
                loot.SetLoot(Resource.COAL, 1);

            if (name.Contains("Iron"))
                loot.SetLoot(Resource.IRON, 1);

            if (name.Contains("Stone"))
                loot.SetLoot(Resource.STONE, 1);
        }

        if(IsHackable)
        {
            LootHandler loot = Instantiate(Game.Instance.MyPrefabs["loot_box"], transform.position, Quaternion.identity).GetComponent<LootHandler>();

            if (name.Contains("Tree"))
                loot.SetLoot(Resource.WOOD, 1);

            transform.position = Vector3.zero;
        }

        if(IsHuntable)
        {
            LootHandler loot1 = Instantiate(Game.Instance.MyPrefabs["loot_box"], transform.position, Quaternion.identity).GetComponent<LootHandler>();
            loot1.SetLoot(Resource.MEAT, 1);

            LootHandler loot2 = Instantiate(Game.Instance.MyPrefabs["loot_box"], transform.position, Quaternion.identity).GetComponent<LootHandler>();
            loot2.SetLoot(Resource.LEATHER, 1);

            transform.position = Vector3.zero;
        }

        if(IsHarvestable)
        {
            transform.position = Vector3.zero;

            if (name.Contains("Wheat"))
                return ItemsDatabase.CreateNewItem("wheat");

            if (name.Contains("Herbs"))
                return ItemsDatabase.CreateNewItem("herbs");

            if (name.Contains("Mushroom"))
                return ItemsDatabase.CreateNewItem("mushrooms");
        }

        return null;
    }
}
