using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera MyCamera { get; private set; }

    public float Health { get; private set; }
    public float HealthGrowth { get; private set; }

    public float Hunger { get; private set; }
    public float HungerGrowth { get; private set; }

    public float Stamina { get; private set; }
    public float StaminaGrowth { get; private set; }

    public CharacterController MyCharController { get; private set; }
    public Rigidbody MyRigidBody { get; private set; }

    public Game MyGameManager { get; private set; }

    public ActivityState MyActivityState { get; private set; }
    public MovementState MyMovementState { get; private set; }
    public EquipmentState MyEquippedState { get; private set; }

    public Inventory MyInventory { get; private set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        Health = 100;
        Stamina = 100;
        Hunger = 0;

        MyRigidBody = GetComponent<Rigidbody>();
        MyCharController = GetComponent<CharacterController>();
        MyCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MyGameManager = Game.Instance;

        if (MyRigidBody != null)
            MyRigidBody.freezeRotation = true;
                
        if (MyCharController == null)
            Debug.Log("ERROR: MyCharControlerIsNull");

        MyActivityState = new IdleState(this);
        MyActivityState.Enter();

        MyMovementState = new StandingState(this, MyCharController, MyRigidBody, transform, null);
        MyMovementState.Enter();

        MyEquippedState = new UnarmedState(this);
        MyEquippedState.Enter();

        MyInventory = new Inventory(this, 60);
    }

    // Update is called once per frame
    void Update()
    {
        ActivityState activityState = (ActivityState)MyActivityState.HandleInput();
        if (activityState != null)
        {
            MyActivityState.Exit();
            MyActivityState = activityState;
            MyActivityState.Enter();
        }
        if (MyActivityState != null)
            MyActivityState.Update();

        MovementState movementState = (MovementState)MyMovementState.HandleInput();
        if (movementState != null)
        {
            MyMovementState.Exit();
            MyMovementState = movementState;
            MyMovementState.Enter();
        }
        if (MyMovementState != null)
            MyMovementState.Update();

        EquipmentState equippedState = (EquipmentState)MyEquippedState.HandleInput();
        if (equippedState != null)
        {
            MyEquippedState.Exit();
            MyEquippedState = equippedState;
            MyEquippedState.Enter();
        }
        if (MyEquippedState != null)
            MyEquippedState.Update();
    }

    public void RegenHealth(float rate)
    {
        if (Health + rate <= 100)
            Health += rate;
        else
            Health = 100;
    }
    public void DrainHealth(float rate)
    {
        if (Health - rate >= 0)
            Health -= rate;
        else
            Health = 0;
    }

    public void RegenHunger(float rate)
    {
        if (Hunger + rate <= 100)
            Hunger += rate;
        else
            Hunger = 100;
    }
    public void DrainHunger(float rate)
    {
        if (Hunger - rate >= 0)
            Hunger -= rate;
        else
            Hunger = 0;
    }

    public void RegenStamina(float rate)
    {
        if (Stamina + rate <= 100)
            Stamina += rate;
        else
            Stamina = 100;
    }
    public void DrainStamina(float rate)
    {
        if (Stamina - rate >= 0)
            Stamina -= rate;
        else
            Stamina = 0;
    }

    public void BeginCrafting()
    {
        
    }
    public void CeaseCrafting()
    {
        
    }

    public string GetEquipped()
    {
        return MyEquippedState.Name.Substring(0, MyEquippedState.Name.Length - 5);
    }

    public string GetResource()
    {
        return MyEquippedState.Name == "BowState" ? GetAmmunitionCapacity().ToString() + " Arrows" : ""; 
    }

    public string GetInventoryCapacity()
    {
        return MyInventory.MyItems.Count + "/" + MyInventory.MaxInventorySize;
    }

    public int GetAmmunitionCapacity()
    {
        int ammo = 0;
        foreach (Item item in MyInventory.MyItems)
            if (item.Name == "Arrows")
                ammo++;

        return ammo;
    }

    public bool HasSpaceInInventory()
    {
        if (MyInventory.MyItems.Count < MyInventory.MaxInventorySize)
            return true;
        else
            return false;
    }

    public void GatherItem(Item item)
    {
        MyInventory.AddItem(item);
        GUI.Instance.UpdateInventoryItemHandlers();
        
    }
    public void DropItem(Item item)
    {
        MyInventory.RemoveItem(item);
        GUI.Instance.UpdateInventoryItemHandlers();
    }
    public void CraftItem(string itemName, List<Item> materials)
    {
        foreach (Item item in materials)
            MyInventory.RemoveItem(item);

        MyInventory.AddItem(ItemsDatabase.CreateNewItem(itemName));
        GUI.Instance.UpdateInventoryItemHandlers();
    }
    public void PlaceItem(Item item)
    {
        MyInventory.RemoveItem(item);
        GUI.Instance.UpdateInventoryItemHandlers();
    }
    public void ConsumeItem(Item item)
    {
        if(item != null)
        {
            if(item.IsConsumable)
            {
                Consumable consumableItem = item as Consumable;
                consumableItem.Consume();
                MyInventory.RemoveItem(item);
                GUI.Instance.UpdateInventoryItemHandlers();
            }
        }
    }
    public void ShootArrow()
    {
        int inventoryIndex = 0;
        Item arrowToRemove = null;
        if (inventoryIndex < MyInventory.MyItems.Count)
        {
            while (MyInventory.MyItems[inventoryIndex].Name != "Arrows" && inventoryIndex < MyInventory.MyItems.Count)
            {                
                inventoryIndex++;
            }

            if (MyInventory.MyItems[inventoryIndex].Name == "Arrows")
                arrowToRemove = MyInventory.MyItems[inventoryIndex];

            if (arrowToRemove != null)
                MyInventory.RemoveItem(arrowToRemove);
        }
        GUI.Instance.UpdateInventoryItemHandlers();
    }
}
