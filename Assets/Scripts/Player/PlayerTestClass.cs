using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestClass : MonoBehaviour
{
    //Placeholder Playerscript för testning
    public static PlayerTestClass Instance { get; private set; }

    [SerializeField] private UI_Inventory _uiInventory;
    [SerializeField] private Inventory _inventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _inventory = new Inventory();
        _uiInventory.SetInventory(_inventory);
    }

    public Inventory GetInventory()
    {
        return _inventory;
    }

    //bjdhgfkbfhfgvjkdfsgbhfdsjkgb fdhgjk

    //bhfjkbfhidsgfsdgdf
}
