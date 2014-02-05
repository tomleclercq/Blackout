using UnityEngine;
using System.Collections;

public enum InputKb
{
    Up = 0,
    Left,
    Down,
    Right,
    Space,
    Backspace,
    Enter,
    CtrlL,
    CtrlR,
};

public class Inputs : MonoBehaviour
{

    static public KeyCode kcUp = KeyCode.UpArrow;
    static public KeyCode kcLeft = KeyCode.LeftArrow;
    static public KeyCode kcDown = KeyCode.DownArrow;
    static public KeyCode kcRight = KeyCode.RightArrow;
    static public KeyCode kcSpace = KeyCode.Space;
    static public KeyCode kcBackspace = KeyCode.Backspace;
    static public KeyCode kcEnter = KeyCode.Return;
    static public KeyCode kcCtrlR = KeyCode.RightControl;
    static public KeyCode kcCtrlL = KeyCode.LeftControl;

    public KeyCode Up = KeyCode.UpArrow;
    public KeyCode Left = KeyCode.LeftArrow;
    public KeyCode Down = KeyCode.DownArrow;
    public KeyCode Right = KeyCode.RightArrow;
    public KeyCode Space = KeyCode.Space;
    public KeyCode Backspace= KeyCode.Backspace;
    public KeyCode Enter = KeyCode.Return;
    public KeyCode CtrlR = KeyCode.RightControl;
    public KeyCode CtrlL = KeyCode.LeftControl;

    static public KeyCode[] KeysGame;

    static public KeyCode[] keyCodesHelper;
    static public KeyCode[] keyCodesGame;

    static public bool bInit = false;
    static public bool bDebug = false;

    void Awake()
    {
        this.InitUserData();
        if (!Inputs.bInit) Inputs.Init();
    }

    void Start()
    {
        this.InitUserData();
        if (!Inputs.bInit) Inputs.Init();
    }

    private void InitUserData()
    {
        Inputs.kcUp = Up;
        Inputs.kcLeft = Left;
        Inputs.kcDown = Down;
        Inputs.kcRight = Right;
        Inputs.kcSpace = Space;
        Inputs.kcBackspace = Backspace;
        Inputs.kcEnter = Enter;
        Inputs.kcCtrlR = CtrlR;
        Inputs.kcCtrlL = CtrlL;
    }

    static public bool Press(InputKb _Input)
    {
        bool bResult = Input.GetKeyDown(keyCodesGame[(int)_Input]);
        if (bResult && bDebug) T.Log("-" + _Input.ToString() + "- Press", TColors.White);
        return bResult;
    }

    static public bool Release(InputKb _Input)
    {
        bool bResult = Input.GetKeyUp(keyCodesGame[(int)_Input]);
        if (bResult && bDebug) T.Log("-" + _Input.ToString() + "- Press", TColors.White);
        return bResult;
    }

    static public bool Hold(InputKb _Input)
    {
        bool bResult = Input.GetKey(keyCodesGame[(int)_Input]);
        if (bResult && bDebug) T.Log("-" + _Input.ToString() + "- Hold", TColors.White);
        return bResult;
    }

    static public bool Press(KeysHelp _Input)
    {
        bool bResult = Input.GetKeyDown(keyCodesHelper[(int)_Input]);
        if (bResult && bDebug) T.Log("-" + _Input.ToString() + "- Press", TColors.White);
        return bResult;
    }

    static public bool Hold(KeysHelp _Input)
    {
        bool bResult = Input.GetKey(keyCodesHelper[(int)_Input]);
        if (bResult && bDebug) T.DbgLog("-" + _Input.ToString() + "- Hold", TColors.White);
        return bResult;
    }

    static public void Init()
    {
        if (!Inputs.bInit){
            GameObject goUserInputs = T.LoadGO("Inputs");
            Inputs inputsUser = goUserInputs.GetComponent(typeof(Inputs)) as Inputs;
            if (inputsUser != null && !Inputs.bInit)
                inputsUser.InitUserData();

            KeysGame = new KeyCode[]{
              kcUp,
              kcLeft,
              kcDown,
              kcRight,
              kcSpace,
              kcEnter,
              kcCtrlL,
              kcCtrlR,
            };

            Helper.Init();
            ArrayList arrayInputs = new ArrayList();
            for (int i = 0; i < KeysGame.Length; ++i)
            {
                arrayInputs.Add(KeysGame[i]);
            }
            InitInputsGame(arrayInputs);
            Inputs.bInit = true;
            T.Log("Inputs Initialized");
        }
    }

    static public void InitInputsGame(ArrayList _InputsKeyCodes)
    {
        T.DbgLog("InitInputsApp", TColors.White);
        object[] objArray;
        objArray = _InputsKeyCodes.ToArray();
        keyCodesGame = new KeyCode[objArray.Length];

        int index = 0;
        foreach (object o in objArray)
        {
            keyCodesGame[index] = (KeyCode)o;
            index++;
        }
        CheckInputs();
    }

    static public void InitInputsHelper(ArrayList _InputsKeyCodes)
    {
        T.DbgLog("InitInputsHelper", TColors.White);
        object[] objArray;
        objArray = _InputsKeyCodes.ToArray();
        keyCodesHelper = new KeyCode[objArray.Length];

        int index = 0;
        foreach (object o in objArray)
        {
            keyCodesHelper[index] = (KeyCode)o;
            index++;
        }
    }

    static public bool CheckInputs()
    {
        bool doubled = false;
        for (int i = 0; i < Helper.staticInputs.Length; i++)
        {
            for (int j = 0; j < keyCodesGame.Length; j++)
            {
                if (Helper.staticInputs[i] == keyCodesGame[j])
                {
                    T.LogW("Input <color=red>>" + Helper.staticInputs[i].ToString() + "<</color> is define two times", TColors.White);
                    doubled = true;
                }
            }
        }
        return !doubled;
    }

}
