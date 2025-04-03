using UnityEngine;

public class Dropper : MonoBehaviour
{
    public static Dropper Instance{get; private set;}

    [SerializeField] private GameObject _liquidPivot;
    [SerializeField] private MeshRenderer _liquidMeshRenderer;

    [SerializeField] private Transform _purpleCabbageLiquidTransform;
    [SerializeField] private Transform _addPurpleCabbageLiquidTransform;

    private const float SQUEEZEANIMETIMER = 1f;

    private const string SQUEEZE_PURPLE_CABBAGE_LIQUID = "squeezePurpleCabbageLiquid";

    private const string RELEASE_PURPLE_CABBAGE_LIQUID = "releasePurpleCabbageLiquid";

    private bool _isReleasing;

    private float _releaseTimer;

    private Animator _animator;

    /// <summary>
    /// 若液體的 Y 軸縮放值大於 0，表示目前仍有液體存在
    /// </summary>
    public bool HavePurpleCabbageLiquid => _liquidPivot.transform.localScale.y > 0;

    private void Awake()
    {
        Instance = this;

        _animator = GetComponent<Animator>();

        _isReleasing = false;

        _releaseTimer = SQUEEZEANIMETIMER;
    }

    private void Update()
    {
        _liquidPivot.SetActive(HavePurpleCabbageLiquid);

        if(_isReleasing)
        {
            if(_releaseTimer > 0)
            {
                _releaseTimer -= Time.deltaTime;

                Transform spawnLiquidTransform = Instantiate(_purpleCabbageLiquidTransform ,_addPurpleCabbageLiquidTransform);

                spawnLiquidTransform.GetComponent<MeshRenderer>().sharedMaterial = _liquidMeshRenderer.sharedMaterial;
            }

            else
            {
                // release finish
                _isReleasing = false;

                _releaseTimer = SQUEEZEANIMETIMER;

                if(_addPurpleCabbageLiquidTransform.childCount != 0)
                {
                    // 某些碰撞體的影響可能導致銷毀失敗，因此這裡使用迴圈確保所有液體被清除
                    foreach(Transform purpleCabbaeLiquid in _addPurpleCabbageLiquidTransform)
                    {
                        Destroy(purpleCabbaeLiquid.gameObject);
                    }
                }
            }
        }
    }

    public void SqueezeAnimation()
    {
        _animator.SetTrigger(SQUEEZE_PURPLE_CABBAGE_LIQUID);
    }

    public void ReleaseAnimation()
    {
        _animator.SetTrigger(RELEASE_PURPLE_CABBAGE_LIQUID);
    }

    public void StartReleasing()
    {
        _isReleasing = true;
    }

    public void ChangeColor(Color color)
    {
        _liquidMeshRenderer.sharedMaterial.color = color;
    }
}
