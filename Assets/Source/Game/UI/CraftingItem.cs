using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftingItem : UIItemHandler
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

    public override void UpdateItem(Item newItem)
    {
        base.UpdateItem(newItem);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        ContainerTransform.SetAsFirstSibling();
        base.OnBeginDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition))
        {
            foreach (UIItemHandler uiHandler in GUI.Instance.InventoryHandlers)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(uiHandler.transform as RectTransform, Input.mousePosition))
                {
                    uiHandler.UpdateItem(MyItem);
                    UpdateItem(null);
                }
            }
        }

        ContainerTransform.SetSiblingIndex(ContainerDisplayOrder);
        base.OnEndDrag(eventData);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }
}
