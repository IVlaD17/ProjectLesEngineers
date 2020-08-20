using UnityEngine;
using System.Collections;

public class LootHandler : MonoBehaviour
{
    public Item MyItem { get; private set; }
    public int Quantity { get; private set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        transform.position = Game.Instance.MyPlayer.transform.position + Vector3.forward * 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLoot(Item item, int quantity)
    {
        MyItem = item;
        Quantity = quantity;
    }

    public void SetLoot(Resource resource, int quantity)
    {
        switch (resource)
        {
            case Resource.COAL:
                MyItem = ItemsDatabase.CreateNewItem("coal");
                break;
            case Resource.IRON:
                MyItem = ItemsDatabase.CreateNewItem("iron");
                break;
            case Resource.STONE:
                MyItem = ItemsDatabase.CreateNewItem("stone");
                break;
            case Resource.WOOD:
                MyItem = ItemsDatabase.CreateNewItem("wood");
                break;
            case Resource.WHEAT:
                MyItem = ItemsDatabase.CreateNewItem("wheat");
                break;
            case Resource.HERBS:
                MyItem = ItemsDatabase.CreateNewItem("herbs");
                break;
            case Resource.MUSHROOMS:
                MyItem = ItemsDatabase.CreateNewItem("mushrooms");
                break;
            case Resource.MEAT:
                MyItem = ItemsDatabase.CreateNewItem("meat");
                break;
            case Resource.LEATHER:
                MyItem = ItemsDatabase.CreateNewItem("leather");
                break;
            default:
                MyItem = null;
                break;
        }
        Quantity = quantity;
    }

    public Item CollectLoot()
    {
        Destroy(gameObject);
        return MyItem;
    }
}
