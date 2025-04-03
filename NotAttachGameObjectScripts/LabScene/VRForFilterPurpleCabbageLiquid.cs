using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRForFilterPurpleCabbageLiquid : MonoBehaviour
{
    [SerializeField] private GameObject liquidPivotOfFilterBeaker;

    private const string LIQUID_PIVOT_OF_DROPPER = "LiquidPivot";

    private const string FILTER_PURPLE_CABBAGE_LIQUID = "filterPurpleCabbageLiquid";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(BeakerOfSpillPurpleCabbageLiquid.Instance.FilterPurpleCabbageLiquid())
        {
            liquidPivotOfFilterBeaker.SetActive(true);

            VRForBoiledPurpleCabbageSliced.Instance.FilterPurpleCabbageLiquidFinished();

            animator.SetTrigger(FILTER_PURPLE_CABBAGE_LIQUID);
        }
    }
}
