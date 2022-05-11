using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<UI_TabButton> tabButtons;
    [SerializeField] private List<GameObject> tabWindows;

    [SerializeField] private Color tabIdleColor;
    [SerializeField] private Color tabActiveColor;
    [SerializeField] private Color tabSelectedColor;

    private UI_TabButton selectedTab;

    private void Start()
    {
        OnTabSelected(tabButtons[0]);
    }

    public void OnTabPressedDown(UI_TabButton tab)
    {
        ResetTabs();
        tab.TabBackground.color = tabActiveColor;
    }
    public void OnTabSelected(UI_TabButton tab)
    {
        selectedTab = tab;
        ResetTabs();
        tab.TabBackground.color = tabSelectedColor;

        int windowsIndex = tab.transform.GetSiblingIndex();
        for (int i = 0; i < tabWindows.Count; i++)
        {
            if (i == windowsIndex)
            {
                tabWindows[i].SetActive(true);
            }
            else
            {
                tabWindows[i].SetActive(false);
            }

        }
    }

    public void ResetTabs()
    {
        foreach (UI_TabButton tab in tabButtons)
        {
            if (selectedTab != null && tab == selectedTab) { continue; }
            tab.TabBackground.color = tabIdleColor;
        }
    }

}
