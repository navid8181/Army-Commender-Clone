using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelType { MainMenue, PauseMenue }
public class UIPanelManager : MonoBehaviour
{


    public BasePanel[] allPanels;

    private Dictionary<PanelType, BasePanel> orderPanels;

    private Stack<BasePanel> activePanel;


    private void Awake()
    {
        orderPanels = new Dictionary<PanelType, BasePanel>();
        activePanel = new Stack<BasePanel>();  
        
        InitializePanels();

    }

    public void InitializePanels()
    {
        for (int i = 0; i < allPanels.Length; i++)
        {
            orderPanels.Add(allPanels[i].panelName, allPanels[i]);
        }
    }


    public void AddPanel(BasePanel panel)
    {
        BasePanel currentAvtivePanel = null;
        if (activePanel.Count > 0)
            currentAvtivePanel = activePanel.Peek();

        if(currentAvtivePanel == null)
        {
            panel.OnEnter();
            activePanel.Push(panel);
        }
        else
        {
            currentAvtivePanel.OnExit();
            panel.OnEnter();

            activePanel.Push(panel);
        }
           

    }

    public void RemovePanel()
    {
        BasePanel previousPanel = activePanel.Pop();

        previousPanel.OnExit();

        if (activePanel.Count <= 0) return;

        BasePanel currentPanle = activePanel.Peek();

        currentPanle.OnEnter();
    }
}
