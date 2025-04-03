using System;
using System.Linq;
using UnityEngine;

public class TriggerToReleaseDropper : MonoBehaviour , IHasProgress
{
    public static TriggerToReleaseDropper Instance{get; private set;}

    [SerializeField] private Transform _liquidImage;
    [SerializeField] private TheSolutionChangedColorGraduallySO _theSolutionChangedColorGraduallySO;

    [Header("變色相關")]
    private const float DESIREDDURATION = 3f;
    private float _elapsedTime;

    private const string PURPLE_CABBAGE_LIQUID = "PurpleCabbageLiquid";

    private MeshRenderer _meshRenderer;
    
    private bool _purpleCabbageLiquidTouched;

    public event EventHandler<IHasProgress.OnPrgressChangedEventArgs> OnProgressChanged;

    private string _detectionSolutionName;

    private void Awake()
    {
        Instance = this;

        _purpleCabbageLiquidTouched = false;

        _meshRenderer = _liquidImage.GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        _meshRenderer.sharedMaterial.color = _theSolutionChangedColorGraduallySO.previousMaterial.color;

        _detectionSolutionName = transform.parent.name;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.CompareTag(PURPLE_CABBAGE_LIQUID))
        {
            _purpleCabbageLiquidTouched = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.transform.CompareTag(PURPLE_CABBAGE_LIQUID))
        {
            Destroy(collider.gameObject);
        }
    }

    private void Update()
    {
        if(_purpleCabbageLiquidTouched)
        {
            _elapsedTime += Time.deltaTime;

            float percentageComplete = _elapsedTime / DESIREDDURATION;

            if(percentageComplete <= 1)
            {
                // when value == 1 , the material color is same to the purpleCabbage In Water For A Long Time color

                _meshRenderer.sharedMaterial.color = Color.Lerp(_theSolutionChangedColorGraduallySO.previousMaterial.color , _theSolutionChangedColorGraduallySO.changedMaterial.color , percentageComplete);

                OnProgressChanged?.Invoke(this , new IHasProgress.OnPrgressChangedEventArgs
                {
                    progressNormalized = percentageComplete
                });
            }

            else
            {
                // handle the elapsedTime maybe not equal to one!
                OnProgressChanged?.Invoke(this , new IHasProgress.OnPrgressChangedEventArgs
                {
                    progressNormalized = 1f
                });

                var pokeButtonReleaseList = ControlSixthDice.Instance.PokeButtonReleaseList;

                var targetPokeButton = pokeButtonReleaseList.SingleOrDefault(item => item.name.Contains(_detectionSolutionName));

                targetPokeButton.StartColorTransition();

                // 停止執行 Update()
                enabled = false;
            }
            
        }

        if(_meshRenderer.sharedMaterial.color == _theSolutionChangedColorGraduallySO.changedMaterial.color)
        {
            // the cause is besides color , their material aren't the same.
            enabled = false;
        }
    }

    private void OnDestroy()
    {
        OnProgressChanged = null;
    }
}
