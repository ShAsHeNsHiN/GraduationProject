using System;
using UnityEngine;

public class PartOfScale : MonoBehaviour
{
    public float Mass {get ; protected set;}

    /// <summary>
    /// 質量增加
    /// </summary>
    /// <param name="collider"></param>
    /// <remarks>*collider 這個參數專為 VRLeftPart 寫的</remarks>
    /// <exception cref="NotImplementedException"></exception>
    public virtual void MassIncrease(Collider collider)
    {
        throw new NotImplementedException();
    }

    public virtual void MassDecrease()
    {
        throw new NotImplementedException();
    }
}
