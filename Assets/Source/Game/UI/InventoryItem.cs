using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : UIItemHandler
{
    public Transform ContainerTransform { get; protected set; } // Parent Transform
    public int ContainerDisplayOrder { get; protected set; }

    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
        ContainerTransform = transform.parent.transform.parent;
        AreaTransform = transform.parent.transform.parent.transform.parent;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ContainerDisplayOrder = ContainerTransform.GetSiblingIndex();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        GUI.Instance.SelectItem(MyItem);
        base.OnBeginDrag(eventData);
        ContainerTransform.SetAsFirstSibling();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        UIItemHandler uiItemHandler = null;

        if(!RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition))
        {
            foreach (UIItemHandler uiHandler in GUI.Instance.CraftingHandlers)
                if (RectTransformUtility.RectangleContainsScreenPoint(uiHandler.transform as RectTransform, Input.mousePosition))
                    uiItemHandler = uiHandler;

            foreach (UIItemHandler uiHandler in GUI.Instance.ToolbarHandlers)
                if (RectTransformUtility.RectangleContainsScreenPoint(uiHandler.transform as RectTransform, Input.mousePosition))
                    uiItemHandler = uiHandler;

            if(uiItemHandler == null)
            {
                LootHandler loot = Instantiate(Game.Instance.MyPrefabs["loot_box"], transform.position, Quaternion.identity).GetComponent<LootHandler>();
                loot.SetLoot(MyItem, 1);

                Game.Instance.MyPlayer.DropItem(MyItem);
                GUI.Instance.SelectItem(null);
            }
            else
            {                
                uiItemHandler.UpdateItem(MyItem);
                if(uiItemHandler.name.Contains("Crafting"))
                {
                    UpdateItem(null);
                }
            }
        }
        
        transform.SetSiblingIndex(MyDisplayOrder);
        MenuTransform.SetSiblingIndex(MenuDisplayOrder);
        AreaTransform.SetSiblingIndex(AreaDisplayOrder);
        ContainerTransform.SetSiblingIndex(ContainerDisplayOrder);

        MyImage.transform.localPosition = new Vector3(50, -50, 0);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }
}
