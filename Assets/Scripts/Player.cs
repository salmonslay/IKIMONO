using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    public Item food1;
    public Item food2;

    private void Awake()
    {
        inventory = new Inventory();
        inventory.AddItem(food1);
        inventory.AddItem(food2);
        uiInventory.SetInventory(inventory);
    }
}
