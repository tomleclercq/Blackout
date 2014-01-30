using UnityEngine;
using System.Collections;

public enum InputKb
{
    Up = 0,
    Left,
    Down,
    Right,
    Space,
    Enter,
    CtrlL,
    CtrlR,
};

public class Inputs : MonoBehaviour
{

    static public KeyCode kcUp = KeyCode.UpArrow;
    static public KeyCode kcLeft = KeyCode.LeftArrow;
    static public KeyCode kcDown = KeyCode.DownArrow;
    static public KeyCode kcRight = KeyCode.UpArrow;
    static public KeyCode kcSpace = KeyCode.Space;
    static public KeyCode kcEnter = KeyCode.Return;
    static public KeyCode kcCtrlR = KeyCode.RightControl;
    static public KeyCode kcCtrlL = KeyCode.LeftControl;

    public KeyCode Up = KeyCode.UpArrow;
    public KeyCode Left = KeyCode.LeftArrow;
    public KeyCode Down = KeyCode.DownArrow;
    public KeyCode Right = KeyCode.UpArrow;
    public KeyCode Space = KeyCode.Space;
    public KeyCode Enter = KeyCode.Return;
    public KeyCode CtrlR = KeyCode.RightControl;
    public KeyCode CtrlL = KeyCode.LeftControl;

    static private ArrayList lList = new ArrayList();
    static public KeyCode[] KeysGame;

    static public KeyCode[] keyCodesHelper;
    static public KeyCode[] keyCodesGame;

    void Awake()
    {
        kcUp = Up;
        kcLeft = Left;
        kcDown = Down;
        kcRight = Right;
        kcSpace = Space;
        kcEnter = Enter;
        kcCtrlR = CtrlR;
        kcCtrlL = CtrlL;
    }


    static public bool IsTrigger(InputKb _Input)
    {
        bool bResult = Input.GetKeyDown(keyCodesGame[(int)_Input]);
        if (bResult) T.DbgLog("-" + _Input.ToString() + "- Hold", TColors.White);
        return bResult;
    }

    static public bool IsHold(InputKb _Input)
    {
        bool bResult = Input.GetKey(keyCodesGame[(int)_Input]);
        if (bResult) T.DbgLog("-" + _Input.ToString() + "- Hold", TColors.White);
        return bResult;
    }

    static public bool IsTrigger(KeysHelp _Input)
    {
        bool bResult = Input.GetKeyDown(keyCodesHelper[(int)_Input]);
        if (bResult) T.DbgLog("-" + _Input.ToString() + "- Press", TColors.White);
        return bResult;
    }

    static public bool IsHold(KeysHelp _Input)
    {
        bool bResult = Input.GetKey(keyCodesHelper[(int)_Input]);
        if (bResult) T.DbgLog("-" + _Input.ToString() + "- Hold", TColors.White);
        return bResult;
    }

    static public void Init()
    {
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
        ArrayList Inputs = new ArrayList();
        for (int i = 0; i < KeysGame.Length; ++i)
        {
            Inputs.Add(KeysGame[i]);
        }
        InitInputsGame(Inputs);
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
