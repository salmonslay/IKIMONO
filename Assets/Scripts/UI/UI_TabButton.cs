using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TabButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] TabGroup tabGroup;

    [HideInInspector] public Image TabBackground;

    private void Awake()
    {
        TabBackground = GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.RandomizeSound("Button"); // Philip
        tabGroup.OnTabPressedDown(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

}
