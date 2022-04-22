using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] TabGroup tabGroup;

    [HideInInspector] public Image TabBackground;

    private void Awake()
    {
        TabBackground = GetComponent<Image>();
        tabGroup.AddTabButton(this);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        tabGroup.OnTabPressedDown(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

}
