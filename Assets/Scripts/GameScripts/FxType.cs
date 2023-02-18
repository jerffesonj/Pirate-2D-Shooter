using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxType : MonoBehaviour
{
    public enum FxName
    {
        Explosion,
        Fire
    }

    public FxName fxName;

    public FxName GetFxName()
    {
        return fxName;
    }
}
