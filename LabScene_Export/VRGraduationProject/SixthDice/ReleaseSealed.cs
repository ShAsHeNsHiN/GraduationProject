using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSealed : MonoBehaviour
{
    [Header("TopButton")]
    [SerializeField] private Transform _topButton;
    [SerializeField] private Material _topButtonMaterial;

    [Header("ConnectButton")]
    [SerializeField] private Transform _connectButton;
    [SerializeField] private Material _connectButtonMaterial;

    [Space]
    [SerializeField] private Material _sealedMaterial;

    private MeshRenderer _topButtonMeshRenderer;

    private MeshRenderer _connectButtonMeshRenderer;

    private const float DESIRED_DURATION = 1f;

    private float _elapsedTime;

    private bool _isPurpleCabbageLiquidInDetectionSolution;

    public bool SuccessfulActive {get ; private set;}

    private void Awake()
    {
        _topButtonMeshRenderer = _topButton.GetComponent<MeshRenderer>();

        _connectButtonMeshRenderer = _connectButton.GetComponent<MeshRenderer>();

        _isPurpleCabbageLiquidInDetectionSolution = default;

        SuccessfulActive = default;
    }

    private void Start()
    {
        _topButtonMeshRenderer.sharedMaterial.color = _sealedMaterial.color;

        _connectButtonMeshRenderer.sharedMaterial.color = _sealedMaterial.color;
    }

    private void Update()
    {
        if(_isPurpleCabbageLiquidInDetectionSolution)
        {
            _elapsedTime += Time.deltaTime;

            float percentageComplete = _elapsedTime / DESIRED_DURATION;

            if(percentageComplete <= 1)
            {
                _topButtonMeshRenderer.sharedMaterial.color = Color.Lerp(_sealedMaterial.color , _topButtonMaterial.color , percentageComplete);

                _connectButtonMeshRenderer.sharedMaterial.color = Color.Lerp(_sealedMaterial.color , _connectButtonMaterial.color , percentageComplete);
            }

            else
            {
                // After color changed
                _isPurpleCabbageLiquidInDetectionSolution = false;

                SuccessfulActive = true;

                // 停止 Update()
                enabled = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            // test to change poke button color
            _isPurpleCabbageLiquidInDetectionSolution = true;
        }
    }

    /// <summary>
    /// 觸發顏色變化，開始計時讓顏色逐步改變
    /// </summary>
    public void StartColorTransition()
    {
        _isPurpleCabbageLiquidInDetectionSolution = true;
    }
}
