using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CutPurpleCabbage : MonoBehaviour
{
    [SerializeField] private Transform _startPositionTransform;
    [SerializeField] private Transform _endPositionTransform;
    [SerializeField] private LayerMask _purpleCabbageLayerMask;

    [SerializeField] private Transform _purpleCabbageSlicedTransform;
    [SerializeField] private Transform _purpleCabbageSlicedSpawnPosTransform;

    private void FixedUpdate()
    {
        bool hasCut = Physics.Linecast(_startPositionTransform.position , _endPositionTransform.position , out RaycastHit hit , _purpleCabbageLayerMask);

        if(hasCut && TriggerPurpleCabbageToCut.Instance.PurpleCabbageOnChoppingBoard)
        {
            GameObject purpleCabbage = hit.transform.gameObject;

            Destroy(purpleCabbage);

            Transform purpleCabbageSliceSpawnTransform = Instantiate(_purpleCabbageSlicedTransform , _purpleCabbageSlicedSpawnPosTransform);
            
            PurpleCabbageSlicedXRGrabInteractable purpleCabbageSlicedXRGrabInteractable = purpleCabbageSliceSpawnTransform.AddComponent<PurpleCabbageSlicedXRGrabInteractable>();

            PurpleCabbageXRSetProperty(purpleCabbageSlicedXRGrabInteractable);

            enabled = false;
        }
    }

    /// <summary>
    /// 幫生成的紫甘藍設定 XR 的參數
    /// </summary>
    /// <param name="purpleCabbageSlicedXRGrabInteractable"></param>
    private void PurpleCabbageXRSetProperty(PurpleCabbageSlicedXRGrabInteractable purpleCabbageSlicedXRGrabInteractable)
    {
        int bitGrabLayer = 1 << 1;

        purpleCabbageSlicedXRGrabInteractable.interactionLayers = bitGrabLayer;

        purpleCabbageSlicedXRGrabInteractable.distanceCalculationMode = XRBaseInteractable.DistanceCalculationMode.TransformPosition;

        purpleCabbageSlicedXRGrabInteractable.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        
        purpleCabbageSlicedXRGrabInteractable.useDynamicAttach = true;
    }
}
