using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TheSolutionBeforeAndAfterMaterialSO : ScriptableObject
{
    public Material [] acidSolutionArray;
    public Material AddPurpleCabbageLiquidForAcid;

    [Space]
    public Material [] basicSolutionArray;
    public Material AddPurpleCabbageLiquidForBasic;

    [Space]
    public Material [] neutralSolutionArray;
    public Material AddPurpleCabbageLiquidForNeutral;

    [Space]
    public Material [] allSolutionArray;
}
