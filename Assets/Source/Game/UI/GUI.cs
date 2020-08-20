using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    public static GUI Instance { get; private set; }

    public Item SelectedItem { get; private set; }

    [Header("GUI")]
    public GameObject MyControlPanelScreen;
    public GameObject MyMenuScreen;
    public Text MyCrosshair;

    [Header("Game Stats Display")]
    public Slider MyHealthSlider;
    public Slider MyStaminaSlider;
    public Slider MyHungerSlider;

    [Header("Utility Display")]
    public Text MyEquippedText;
    public Text MyResourceText;

    [Header("Toolbar Menu")]
    public GameObject Toolbar;
    public GameObject ToolbarItemPrefab;
    public ToolbarItem[] ToolbarHandlers;

    [Header("Inventory Menu")]
    public GameObject InventoryMenu;
    public GameObject InventoryItemPrefab;
    public InventoryItem[] InventoryHandlers;
    public Text CapacityText;
    public Text ItemNameText;
    public Text ItemDetailsText;

    [Header("Crafting Menu")]
    public Dropdown CraftableList;
    public Text CraftingMaterials;
    public GameObject CraftingMenu;
    public GameObject CraftingItemPrefab;
    public CraftingItem[] CraftingHandlers;
    public Button CraftItemButton;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        Instance = this;

        // Toolbar Initialisation
        ToolbarHandlers = new ToolbarItem[Constants.TOOLBAR_SIZE];
        for(int itemIndex = 1; itemIndex <= Constants.TOOLBAR_SIZE; itemIndex++)
        {
            ToolbarHandlers[itemIndex - 1] = Instantiate(ToolbarItemPrefab, Toolbar.transform).GetComponent<ToolbarItem>();
            ToolbarHandlers[itemIndex - 1].gameObject.name = "ToolbarItem" + itemIndex;
        }

        // Inventory Initialisation
        InventoryHandlers = new InventoryItem[Constants.INVENTORY_SIZE];
        for (int itemIndex = 1; itemIndex <= Constants.INVENTORY_SIZE; itemIndex++)
        {
            InventoryHandlers[itemIndex - 1] = Instantiate(InventoryItemPrefab, InventoryMenu.transform).GetComponent<InventoryItem>();
            InventoryHandlers[itemIndex - 1].gameObject.name = "InventoryItem" + itemIndex;
        }

        // Crafting Initialisation
        CraftingHandlers = new CraftingItem[Constants.CRAFTING_SIZE];
        for (int itemIndex = 1; itemIndex <= Constants.CRAFTING_SIZE; itemIndex++)
        {
            CraftingHandlers[itemIndex - 1] = Instantiate(CraftingItemPrefab, CraftingMenu.transform).GetComponent<CraftingItem>();
            CraftingHandlers[itemIndex - 1].gameObject.name = "CraftingItem" + itemIndex;
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        // Toolbar Configuration
        for(int itemIndex = 0; itemIndex < ToolbarHandlers.Length; itemIndex++)
        {
            Vector3 position = ToolbarHandlers[itemIndex].transform.localPosition;

            if (itemIndex < ToolbarHandlers.Length / 2)
                position.x -= 120 * (ToolbarHandlers.Length / 2 - itemIndex);

            if (itemIndex >= ToolbarHandlers.Length / 2)
                position.x += 120 * (itemIndex % (ToolbarHandlers.Length / 2));

            ToolbarHandlers[itemIndex].transform.localPosition = position;

            ToolbarHandlers[itemIndex].UpdateItem(null);
        }
        // Inventory Configuration
        for (int itemIndex = 0; itemIndex < InventoryHandlers.Length; itemIndex++)
        {
            Vector3 position = InventoryHandlers[itemIndex].transform.localPosition;
            position.x += 110 * (itemIndex % 10);
            position.y -= 110 * (itemIndex / 10);
            InventoryHandlers[itemIndex].transform.localPosition = position;

            if (itemIndex < Game.Instance.MyPlayer.MyInventory.MyItems.Count)
                InventoryHandlers[itemIndex].UpdateItem(Game.Instance.MyPlayer.MyInventory.MyItems[itemIndex]);
            else
                InventoryHandlers[itemIndex].UpdateItem(null);
        }

        // Crafting Configuration
        for(int itemIndex = 0; itemIndex < CraftingHandlers.Length; itemIndex++)
        {
            Vector3 position = CraftingHandlers[itemIndex].transform.localPosition;
            position.x += 100 * itemIndex + 5;
            CraftingHandlers[itemIndex].transform.localPosition = position;

            CraftingHandlers[itemIndex].UpdateItem(null);
        }

        CraftItemButton.interactable = false;
        ItemNameText.text = "";
        ItemDetailsText.text = "";

        ToggleControlPanel();
        OnCraftingListItemChanged();
    }

    // Update is called once per frame
    void Update()
    {
        // Toolbar

        // Inventory
        CapacityText.text = Game.Instance.MyPlayer.GetInventoryCapacity();

        if (SelectedItem != null)
        {
            ItemNameText.text = SelectedItem.Name;
            ItemDetailsText.text = SelectedItem.Description;
        }
        else
        {
            ItemNameText.text = "No item selected";
            ItemDetailsText.text = "No description";
        }

        // Crafting
        if (Game.Instance.MyPlayer.MyActivityState.Name == "InventoryState")
        {
            if (Game.Instance.MyPlayer.HasSpaceInInventory() && CheckCraftingConditions())
                CraftItemButton.interactable = true;
            else
                CraftItemButton.interactable = false;
        }

        // Player Stats
        MyHealthSlider.value = Game.Instance.MyPlayer.Health;
        MyStaminaSlider.value = Game.Instance.MyPlayer.Stamina;
        MyHungerSlider.value = Game.Instance.MyPlayer.Hunger;

        // Player Equipment
        MyEquippedText.text = Game.Instance.MyPlayer.GetEquipped();
        MyResourceText.text = Game.Instance.MyPlayer.GetResource();
    }

    List<string> GetCraftingMaterials()
    {
        List<string> craftingMaterials = new List<string>();
        foreach(CraftingItem uiItemHandler in CraftingHandlers)
        {
            if(uiItemHandler.MyItem != null)
            {
                craftingMaterials.Add(uiItemHandler.MyItem.Name);
            }
        }

        return craftingMaterials;
    }

    public void UpdateInventoryItemHandlers()
    {
        int itemIndex;
        for(itemIndex = 0; itemIndex < Game.Instance.MyPlayer.MyInventory.MyItems.Count; itemIndex++)
            InventoryHandlers[itemIndex].UpdateItem(Game.Instance.MyPlayer.MyInventory.MyItems[itemIndex]);

        if(itemIndex < InventoryHandlers.Length)
        {
            for (int uiItemIndex = itemIndex; uiItemIndex < InventoryHandlers.Length; uiItemIndex++)
                InventoryHandlers[uiItemIndex].UpdateItem(null);
        }
    }

    public void OnCraftItemButtonClicked()
    {
        List<Item> materials = new List<Item>();

        foreach (UIItemHandler uIItemHandler in CraftingHandlers)
            if (uIItemHandler.MyItem != null)
                materials.Add(uIItemHandler.MyItem);

        Game.Instance.MyPlayer.CraftItem(CraftableList.options[CraftableList.value].text, materials);

        foreach (UIItemHandler uIItemHandler in CraftingHandlers)
            uIItemHandler.UpdateItem(null);
    }
    public void OnCraftingListItemChanged()
    {
        string itemName = CraftableList.options[CraftableList.value].text;
        List<string> craftingMaterials = ItemsDatabase.GetCraftingMaterialsList(itemName);

        CraftingMaterials.text = "";
        foreach (string mat in craftingMaterials)
            CraftingMaterials.text += mat + "\n";
    }
    public bool CheckCraftingConditions()
    {
        string itemName = CraftableList.options[CraftableList.value].text;
        List<string> craftingMaterialsRequired = ItemsDatabase.GetCraftingMaterialsList(itemName);
        List<string> craftingMaterialsAdded = GetCraftingMaterials();

        bool allConditionsMet = true;

        if (craftingMaterialsAdded.Count != craftingMaterialsRequired.Count)
        {
            allConditionsMet = false;
        }
        else
        {
            bool[] craftingConditionsMet = new bool[craftingMaterialsRequired.Count];

            for (int reqIndex = 0; reqIndex < craftingMaterialsRequired.Count; reqIndex++)
            {
                for (int addIndex = 0; addIndex < craftingMaterialsAdded.Count; addIndex++)
                {
                    if (craftingMaterialsRequired[reqIndex] == craftingMaterialsAdded[addIndex])
                    {
                        craftingConditionsMet[reqIndex] = true;
                        craftingMaterialsAdded.Remove(craftingMaterialsAdded[addIndex]);
                        addIndex = craftingMaterialsAdded.Count;
                    }
                }
            }

            for (int condIndex = 0; condIndex < craftingConditionsMet.Length; condIndex++)
            {
                if(craftingConditionsMet[condIndex] == false)
                {
                    allConditionsMet = false;
                }
            }
        }

        if (allConditionsMet == true)
            return true;
        else
            return false;
    }

    public void SelectItem(Item item)
    {
        SelectedItem = item;
    }
    public void DeselectItem()
    {
        SelectedItem = null;
    }
    public void ToggleControlPanel()
    {
        if (MyControlPanelScreen.activeSelf)
        {
            MyCrosshair.gameObject.SetActive(true);
            MyControlPanelScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            MyCrosshair.gameObject.SetActive(false);
            MyControlPanelScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
