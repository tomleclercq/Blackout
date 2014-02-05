using UnityEngine;
using System.Collections;

public enum MenuTypes
{
    Main = 0,
    Instructions,
    Credits,
    Any,
};

public class Menu : MonoBehaviour
{
    static public Menu Main = null ;
    static public Menu[] List = null;
    static public MenuTypes TypeCurrent = MenuTypes.Any;

    public MenuTypes Type;
    public bool bEnable;
    public Button[] Buttons;
    public Menu[] SubMenus;
    public Transform tDisplay;

    static private bool bStaticInit = false;
    private int iCurrentEnable;
    private float fTimer;
    private bool bInit = false;

    void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (!this.bInit)
        {
            //T.Log("Init Begin for menu " + this.Type.ToString()+">");
            if ( !Menu.bStaticInit && this.Type == MenuTypes.Main )
            {
                //T.Log(">" + "Init Menu.bStaticInit");

                Menu.TypeCurrent = this.Type;
                if (Menu.TypeCurrent != MenuTypes.Main)
                {
                    Menu.Main = T.LoadT("MainMenu").GetComponent(typeof(Menu)) as Menu;
                }
                else
                {
                    Menu.Main = this;
                }
                Menu.List = new Menu[this.SubMenus.Length + 1];
                Menu.List[0] = Menu.Main;
                for (int iIndex = 0 + 1; iIndex < this.SubMenus.Length + 1; iIndex++)
                {
                    Menu.List[iIndex] = this.SubMenus[iIndex - 1];
                    //T.Log(">"+iIndex+"# subMenu type is "+Menu.List[iIndex].Type.ToString());
                }
                Menu.bStaticInit = true;
            }

            if (this.bEnable)
            {
                Menu.TypeCurrent = this.Type;
            }
            this.UpdateDisplaysStates();

            this.bInit = true;
            T.Log("> menu "+this.Type.ToString()+ " Initialized");
        }
    }

    static public void ChangeTo(MenuTypes _type)
    {
        T.Log("Change to"+_type.ToString()+">");
        Menu next = Menu.FindOfType( _type );
        next.SetStatus( true );
        T.Log(">"+_type.ToString() + " Enable");
    }

    static public Menu FindOfType(MenuTypes _wantedType)
    {
        Menu menu = Menu.List[0];

        foreach (Menu m in Menu.List)
        {
            if (m.Type == _wantedType)
            {
                menu = m;
                break;
            }
        }
        return menu;
    }

    public void SetStatus(bool _status)
    {
        if (!this.bInit) this.Init();
        this.bEnable = _status;
        this.UpdateDisplaysStates();
        DisplaysVisibility(_status);
        if (!_status) this.fTimer = 0f;
    }

    void Update()
    {
        if (this.bEnable)
        {
            this.fTimer += Time.deltaTime;

            if( this.UpdateInputs() )
            this.UpdateDisplaysStates();
        }
        if( this.tDisplay.renderer.enabled != this.bEnable )
            T.SetHierarchyVisibility(this.tDisplay, this.bEnable );
        if (Menu.TypeCurrent != this.Type )
        {
            Menu.TypeCurrent = this.Type;
        }
    }

    private void UpdateDisplaysStates()
    {
        for (int i = 0; i < this.Buttons.Length; i++)
        {
            if( i == this.iCurrentEnable ){
                this.Buttons[i].SetSelected(true);
            }else{
                this.Buttons[i].SetSelected(false);
            }
        }
    }

    private void DisplaysVisibility(bool _visible)
    {
        for (int i = 0; i < this.Buttons.Length; i++)
        {
            this.Buttons[i].SetVisible(_visible);
        }
    }

    private void HideMenus()
    {
        if (!this.bInit) this.Init();
        T.Log("Hide SubMenus >");
        for (int iIndex = 0; iIndex < Menu.List.Length; iIndex++)
        {
            if (Menu.List[iIndex] != null)
            {
                Menu.List[iIndex].SetStatus(false);
            }
        }
    }

    private bool UpdateInputs()
    {
        bool bUserHasEnterInput = false;

        if (Inputs.Press(InputKb.Down) || Inputs.Press(InputKb.Left))
        {
            this.iCurrentEnable = this.iCurrentEnable < this.Buttons.Length - 1 ? this.iCurrentEnable + 1 : 0;
            bUserHasEnterInput = true;
        }
        if (Inputs.Press(InputKb.Up) || Inputs.Press(InputKb.Right))
        {
            this.iCurrentEnable = this.iCurrentEnable > 0 ? this.iCurrentEnable - 1 : this.Buttons.Length - 1;
            bUserHasEnterInput = true;
        }
        if( Inputs.Press(InputKb.Backspace) )
        {
            if (Menu.TypeCurrent != MenuTypes.Main)
            {
                Back();
            }
        }
        if ( (Inputs.Press(InputKb.Enter) || Inputs.Press(InputKb.Space)) && fTimer >= 0.85f )
        {
            switch (this.Buttons[this.iCurrentEnable].Type)
            {
                case ButtonTypes.Play:
                    Application.LoadLevel(1);
                    break;
                case ButtonTypes.Instructions:
                    this.SetStatus(false);
                    Menu.ChangeTo(MenuTypes.Instructions);
                    break;
                case ButtonTypes.Credits:
                    this.SetStatus(false);
                    Menu.ChangeTo(MenuTypes.Credits);
                    break;
                case ButtonTypes.Back:
                    Back();
                    break;
            }
            bUserHasEnterInput = true;
        }
        return bUserHasEnterInput;
    }
    private void Back()
    {
        this.SetStatus(false);
        Menu.ChangeTo(MenuTypes.Main);
    }
}
