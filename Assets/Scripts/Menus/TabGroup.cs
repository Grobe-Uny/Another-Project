using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    [SerializeField]public List<GameObject> objectsToSwap;
    
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabSelected;

    [SerializeField]private TabButton selectedTab;

    public void Subscribe(TabButton tabButton)
    {
        
         if(tabButtons == null)
         {
            tabButtons = new List<TabButton>();
         }
         tabButtons.Add(tabButton);
    }

    public void OnTabEnter(TabButton tabButton)
    {
        ResetTabs();

        if (selectedTab == null || tabButton != selectedTab)
        {
            tabButton.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton tabButton)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton tabButton)
    {
        selectedTab = tabButton;
        ResetTabs();
        tabButton.background.sprite = tabSelected;
        int index = tabButton.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton tabButton in tabButtons)
        {
            if(selectedTab != null && tabButton == selectedTab)
                continue;
            
            tabButton.background.sprite = tabIdle;
        }
    }
    
}
