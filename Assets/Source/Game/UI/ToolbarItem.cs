using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToolbarItem : UIItemHandler
{
    public Text MyText { get; private set; }

    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
        AreaTransform = transform.parent.transform.parent;
        MyText = transform.GetChild(1).GetComponent<Text>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        MyText.gameObject.name += MyIndex.ToString();

        if (MyIndex <= 9)
            MyText.text = MyIndex.ToString();
        else if (MyIndex == 10)
            MyText.text = "0";
        else if (MyIndex == 11)
            MyText.text = "-";
        else if (MyIndex == 12)
            MyText.text = "=";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (!RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition))
        {
            foreach (UIItemHandler uiHandler in GUI.Instance.ToolbarHandlers)
                if (RectTransformUtility.RectangleContainsScreenPoint(uiHandler.transform as RectTransform, Input.mousePosition))
                    uiHandler.UpdateItem(MyItem);

            UpdateItem(null);
        }

        base.OnEndDrag(eventData);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
    }
}
