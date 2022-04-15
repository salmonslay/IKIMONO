using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Placeholder Playerscript f�r testning
    public static Player Instance { get; private set; }

    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    public Item food1;
    public Item food2;

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