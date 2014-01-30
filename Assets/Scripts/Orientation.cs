using System.Diagnostics;
using UnityEngine;
using System.Collections;

public enum Axis
{
    Top,
    Back,
}

public class Orientation
{
    public Axis OrientationAxis{get; private set;}

#region Singleton;
    private static readonly object _Rooth = new object();
    private static Orientation _Instance = null;

    public bool InTransition { get; private set; }

    public static Orientation Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new Orientation();
            }
            return _Instance;
        }
    }


    private Orientation()
    {
        OrientationAxis = Axis.Top;
    }

#endregion;

    public bool Up()
    {
        if (OrientationAxis == Axis.Back)
        {
            OrientationAxis = Axis.Top;
            return true;
        }
        return false;
    }

    public bool Down()
    {
        if (OrientationAxis == Axis.Top)
        {
            OrientationAxis = Axis.Back;
            return true;
        }
        return false;
    }

    public void Lock()
    {
        InTransition = true;
    }

    public void Free()
    {
        InTransition = false;
    }

}
