using System;
using UnityEngine;

public class VRForBoiledPurpleCabbageSliced : MonoBehaviour, IHasProgress
{
    public static VRForBoiledPurpleCabbageSliced Instance{get; private set;}

    [SerializeField] private Transform _liquidPivot;
    [SerializeField] private Transform _liquidImage;

    [Header("紫甘藍液")]
    [SerializeField] private Material _purpleCabbageInWaterForAMoment;
    [SerializeField] private Material _purpleCabbageInWaterForALongTime;

    private MeshRenderer _meshRenderer;

    private const float DESIREDURATION = 50f;

    private float _elapsedTime;

    private Animator _animator;

    private const string MAKE_PURPLE_CABBAGE_LIQUID = "makePurpleCabbageLiquid";

    public event EventHandler<IHasProgress.OnPrgressChangedEventArgs> OnProgressChanged;

    private void Awake()
    {
        Instance = this;

        _animator = GetComponent<Animator>();

        _meshRenderer = _liquidImage.GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _meshRenderer.sharedMaterial.color = _purpleCabbageInWaterForAMoment.color;
    }

    private void Update()
    {
        if(TriggerPurpleCabbageSliced.Instance.HasAddedPurpleCabbageSliced && SpillWater.Instance.HasAddedWater)
        {
            PurpleCabbageLiquidFillBeakerAnimation();

            _elapsedTime += Time.deltaTime;

            float percentageComplete = _elapsedTime / DESIREDURATION;

            if(percentageComplete <= 1)
            {
                // when value == 1 , the material color is same to the purpleCabbage In Water For A Long Time color

                _meshRenderer.sharedMaterial.color = Color.Lerp(_purpleCabbageInWaterForAMoment.color , _purpleCabbageInWaterForALongTime.color , percentageComplete);

                OnProgressChanged?.Invoke(this , new IHasProgress.OnPrgressChangedEventArgs
                {
                    progressNormalized = percentageComplete
                });
            }

            else
            {
                OnProgressChanged?.Invoke(this , new IHasProgress.OnPrgressChangedEventArgs
                {
                    progressNormalized = 1f
                });

                // 停止執行 Update()
                enabled = false;
            }
        }
    }

    private void PurpleCabbageLiquidFillBeakerAnimation()
    {
        _liquidPivot.gameObject.SetActive(true);

        _animator.SetTrigger(MAKE_PURPLE_CABBAGE_LIQUID);
    }

    public void FilterPurpleCabbageLiquidFinished()
    {
        _liquidPivot.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        OnProgressChanged = null;
    }
}
