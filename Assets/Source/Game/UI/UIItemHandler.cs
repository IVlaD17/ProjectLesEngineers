using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItemHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{    
    public Image MyImage { get; protected set; }

    public Transform MenuTransform { get; protected set; } // Parent Transform
    public int MenuDisplayOrder { get; protected set; }

    public Transform AreaTransform { get; protected set; } // Container Transform
    public int AreaDisplayOrder { get; protected set; }

    public int MyDisplayOrder { get; protected set; }

    public int MyIndex { get; protected set; }
    public Item MyItem { get; protected set; }

    // Awake is called when the script instance is being loaded
    protected virtual void Awake()
    {
        MyImage = transform.GetChild(0).GetComponent<Image>();
        MenuTransform = transform.parent.transform;
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        MyIndex = Convert.ToInt32(gameObject.name.Substring(gameObject.name.IndexOf("Item") + 4));
        MyImage.gameObject.name += MyIndex.ToString();

        MyDisplayOrder = transform.GetSiblingIndex();
        MenuDisplayOrder = MenuTransform.GetSiblingIndex();
        AreaDisplayOrder = AreaTransform.GetSiblingIndex();
    }
    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public virtual void UpdateItem(Item newItem)
    {
        MyItem = newItem;
        if (MyItem != null)
            MyImage.sprite = MyItem.MySprite;
        else
            MyImage.sprite = Game.Instance.MySprites["slot"];
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        // Left Click
        if (Input.GetMouseButtonUp(0))
            GUI.Instance.SelectItem(MyItem);

        // Right Click
        if (Input.GetMouseButtonUp(1))
            Game.Instance.MyPlayer.ConsumeItem(MyItem);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsFirstSibling();
        MenuTransform.SetAsFirstSibling();
        AreaTransform.SetAsFirstSibling();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        MyImage.transform.position = Input.mousePosition;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.SetSiblingIndex(MyDisplayOrder);
        MenuTransform.SetSiblingIndex(MenuDisplayOrder);
        AreaTransform.SetSiblingIndex(AreaDisplayOrder);

        MyImage.transform.localPosition = new Vector3(50, -50, 0);
    }

    public virtual void OnDrop(PointerEventData eventData)
    {       
        
    }
}
