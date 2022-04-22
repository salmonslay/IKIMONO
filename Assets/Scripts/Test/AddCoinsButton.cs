using IKIMONO.Pet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCoinsButton : MonoBehaviour
{
    public void AddCoins()
    {
        Player.Instance.AddCoins(10);
    }
}
