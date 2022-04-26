using IKIMONO.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindowsButton : MonoBehaviour
{
    [SerializeField] private GameObject parentWindow;

    public void CloseWindows()
    {
        parentWindow.SetActive(false);
    }
}
