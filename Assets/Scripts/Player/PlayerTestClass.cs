using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestClass : MonoBehaviour
{
    //Placeholder Playerscript för testning
    public static PlayerTestClass Instance { get; private set; }

    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    public ItemScriptableObject food1;
    public ItemScriptableObject food2;

    private void Awake()
    {
        Instance = this;

        inventory = new Inventory();
        //inventory.AddItem(food1);
        //inventory.AddItem(food2);
        //inventory.AddItem(food2);
        //inventory.AddItem(food2);
        uiInventory.SetInventory(inventory);
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
