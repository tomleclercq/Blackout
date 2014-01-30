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
    static public Menu MainMenu;

    public bool bMenuEnable;
    public Button[] Buttons;
    public Menu[] SubMenu;

    private int iCurrentEnable;
    private bool bInit = false;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (!bInit)
        {
            if (Menu.MainMenu == null) MainMenu = T.LoadT("Items").GetComponent(typeof(Menu)) as Menu;
            this.bInit = true;
            T.Log("Init");
        }
        UpdateButtonStates();
        if( SubMenu.Length > 0 ) HideSubMenus();
    }

    public void SetActive(bool _active)
    {
        if (!bInit) Init();

        this.bMenuEnable = _active;

        T.SetHierarchyVisibility( this.transform, this.bMenuEnable );
        
        this.UpdateButtonStates();
    }

    void Update()
    {
        if ( this.bMenuEnable )
        {
            UpdateButtonStates();
            UpdateInputs();
        }
    }

    private void UpdateButtonStates()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            if( i == this.iCurrentEnable )
                Buttons[i].SetSelected(true);
            else
                Buttons[i].SetSelected(false);
        }
    }

    private void ButtonVisibility( bool _visible)
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetVisible(_visible);
        }
    }

    private void HideSubMenus()
    {
        if (!bInit) Init();
        T.Log("Hide SubMenus");
        for (int iIndex = 0; iIndex < Buttons.Length; iIndex++)
        {
            if (SubMenu[iIndex] != null)
            {
                SubMenu[iIndex].SetActive(false);
            }
        }
    }

    private void UpdateInputs()
    {

        if(Inputs.Press(InputKb.Down) || Inputs.Press(InputKb.Left) )
        {
            iCurrentEnable = iCurrentEnable < this.Buttons.Length - 1? iCurrentEnable+1 : 0;
        }
        if(Inputs.Press(InputKb.Up) || Inputs.Press(InputKb.Right) )
        {
            iCurrentEnable = iCurrentEnable > 0 ? iCurrentEnable-1 : this.Buttons.Length - 1;
        }
             /*
        if( Inputs.Press(InputKb.Enter) )
        {
            switch (this.Buttons[this.iCurrentEnable].sName)
            {
                case "Play":
                    Application.LoadLevel(1);
                    break;
                case "Instructions":
                    bInSubMenu = true;
                    T.Log("Instructions");
                    SetActive( false );
                    SubMenu[iCurrentEnable].SetActive(true);
                    T.SetHierarchyVisibility(this.transform, false);
                    break;
                case "Credits":
                      bInSubMenu = true;
                    T.Log("Credits");
                    SetActive(false);
                    //SubMenu[iCurrentEnable].SetActive(true);
                    break;
                case "Back":
                    bInSubMenu = false;
                    T.Log("Back");
                    SetActive(false);
                    if (MainMenu != null)
                    {
                        MainMenu.SetActive(true);
                    }
                    break;
            }
        }   */
    }
}
