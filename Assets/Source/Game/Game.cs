using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public Player MyPlayer { get; private set; }

    public Dictionary<string, Sprite> MySprites { get; private set; }
    public Dictionary<string, GameObject> MyPrefabs { get; private set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        Instance = this;

        MySprites = new Dictionary<string, Sprite>();

        // Components UI Sprites
        MySprites.Add("coal", Resources.Load<Sprite>("Images/Components/coal"));
        MySprites.Add("herbs", Resources.Load<Sprite>("Images/Components/herbs"));
        MySprites.Add("leather", Resources.Load<Sprite>("Images/Components/leather"));
        MySprites.Add("meat", Resources.Load<Sprite>("Images/Components/meat"));
        MySprites.Add("mushrooms", Resources.Load<Sprite>("Images/Components/mushrooms"));
        MySprites.Add("stone", Resources.Load<Sprite>("Images/Components/stone"));
        MySprites.Add("wheat", Resources.Load<Sprite>("Images/Components/wheat"));
        MySprites.Add("wood", Resources.Load<Sprite>("Images/Components/wood"));

        // Consumables UI Sprites
        MySprites.Add("potion", Resources.Load<Sprite>("Images/Consumables/potion"));
        MySprites.Add("quiver", Resources.Load<Sprite>("Images/Consumables/quiver"));
        MySprites.Add("flour", Resources.Load<Sprite>("Images/Consumables/flour"));

        // Gear UI Sprites
        MySprites.Add("axe1", Resources.Load<Sprite>("Images/Gear/axe1"));
        MySprites.Add("bow", Resources.Load<Sprite>("Images/Gear/bow"));
        MySprites.Add("pickaxe", Resources.Load<Sprite>("Images/Gear/pickaxe"));

        // Structures UI Sprites
        MySprites.Add("hut", Resources.Load<Sprite>("Images/Structures/hut"));
        MySprites.Add("cabin", Resources.Load<Sprite>("Images/Structures/cabin"));
        MySprites.Add("campfire", Resources.Load<Sprite>("Images/Structures/campfire"));

        // Generic UI Sprites
        MySprites.Add("slot", Resources.Load<Sprite>("Images/UI/slot"));

        MyPrefabs = new Dictionary<string, GameObject>();

        // Items Prefabs
        MyPrefabs.Add("loot_box", Resources.Load<GameObject>("Models/Items/LootBox"));

        // Mines Prefabs
        MyPrefabs.Add("coal_mine", Resources.Load<GameObject>("Models/Mines/CoalMine"));
        MyPrefabs.Add("iron_mine", Resources.Load<GameObject>("Models/Mines/IronMine"));
        MyPrefabs.Add("stone_mine", Resources.Load<GameObject>("Models/Mines/StoneMine"));

        // NPCs Prefabs
        MyPrefabs.Add("deer", Resources.Load<GameObject>("Models/NPCs/Deer"));

        // Plants Prefabs
        MyPrefabs.Add("herbs", Resources.Load<GameObject>("Models/Plants/Herbs"));
        MyPrefabs.Add("mushroom", Resources.Load<GameObject>("Models/Plants/Mushroom"));
        MyPrefabs.Add("wheat", Resources.Load<GameObject>("Models/Plants/Wheat"));

        // Trees Prefabs
        MyPrefabs.Add("tree1", Resources.Load<GameObject>("Models/Trees/Tree1"));

        MyPlayer = GameObject.Find("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
