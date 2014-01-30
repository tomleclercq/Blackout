using UnityEngine;
using System.Collections;

public enum MenuItems
{
    Play=0,
    Instructions,
    Credits
};


public class Menu : MonoBehaviour
{
    public Button[] buttons;
    public bool[] bActive;
    public bool[] bSubMenu;
    public Menu[] SubMenu;
    static protected bool bInSubMenu;
    static public Menu MainMenu;

    private int iCurrentEnable;
    private bool bInit = false;
    void Awake()
    {
        Init();
        MainMenu = T.LoadT("Items").GetComponent(typeof(Menu)) as Menu;
    }

    private void Init()
    {
        if (!bInit)
        {
            iCurrentEnable = 0; 
            Inputs.Init();
            bInSubMenu = false;
            T.Log("Init");
            bInit = true;
        }
        HideSubMenus();
        SetButtonActive();
        UpdateButtonStates();
    }

    void Update()
    {
        if (!bInSubMenu && iCurrentEnable >= 0 )
        {
            UpdateButtonStates();
            if( UpdateInputs() )
            {
                SetButtonActive();
            }
        }
    }

    public void SetActive(bool _active)
    {
        if (!bInit) Init();
        T.SetHierarchyVisibility(this.transform, _active);
        if (_active)
            iCurrentEnable = 0;
        else
            iCurrentEnable = -1;
        SetButtonActive();
    }

    private void HideSubMenus()
    {
        if (!bInit) Init();
        T.Log("Hide SubMenus");
        int iIndex = 0;
        foreach (bool b in bSubMenu)
        {
            if (b)
            {
                SubMenu[iIndex].SetActive(false);
            }
            iIndex++;
        }
    }

    private void SetButtonActive()
    {
        if (!bInit) Init();
        T.Log("Set button Active");
        for (int i = 0; i < bActive.Length; ++i)
        {
            if (i == iCurrentEnable)
                this.bActive[i] = true;
            else
                this.bActive[i] = false;
        }
    }

    private void UpdateButtonStates()
    {
        int iIndex = 0;
        foreach (Button b in buttons)
        {
            b.SetSelected(bActive[iIndex]);
            iIndex++;
        }
    }

    private bool UpdateInputs()
    {
        bool bResult = false;

        if(Inputs.IsTrigger(InputKb.Up))
        {
            bResult = true;
            iCurrentEnable++;
        }
        if(Inputs.IsTrigger(InputKb.Left))
        {
            bResult = true;
            iCurrentEnable++;
        }
        if(Inputs.IsTrigger(InputKb.Down))
        {
            bResult = true;
            iCurrentEnable--;
        }
        if(Inputs.IsTrigger(InputKb.Right))
        {
            bResult = true;
            iCurrentEnable--;
        }

        if(iCurrentEnable < 0)
        {
            iCurrentEnable = buttons.Length - 1;
        }
        if(iCurrentEnable >= buttons.Length )
        {
            iCurrentEnable = 0;
        }

        if (Inputs.IsTrigger(InputKb.Space) || Inputs.IsTrigger(InputKb.Enter))
        {
            if(buttons[iCurrentEnable].sName == "Play" )
                Application.LoadLevel(1);
            if(buttons[iCurrentEnable].sName == "Instructions")
            {
                bInSubMenu = true;
                T.Log("Instructions");
                T.SetHierarchyVisibility(this.transform, false);
            }
            if(buttons[iCurrentEnable].sName == "Credits")
            {
                bInSubMenu = true;
                T.Log("Credits");
                SetActive(false);
                if (bSubMenu[iCurrentEnable] && SubMenu[iCurrentEnable] != null)
                {
                    SubMenu[iCurrentEnable].SetActive(true);
                }
            }
            if( buttons[iCurrentEnable].sName == "Back")
            {
                bInSubMenu = false;
                T.Log("Back");
                SetActive(false);
                if (MainMenu != null)
                {
                    MainMenu.SetActive(true);
                }
            }
        }
        return bResult;
    }
}
