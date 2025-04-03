using UnityEngine;

public class VRLeftPart : PartOfScale
{
    private static VRLeftPart instance;
    public static VRLeftPart Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<VRLeftPart>();
            }

            return instance;
        }
    }

    [Header("Gold Information")]
    [SerializeField] private TheScaleItemSO _14KGoldSO;
    [SerializeField] private TheScaleItemSO _18KGoldSO;

    private const string _14KGold_TAG = "_14KGold";
    private const string _18KGold_TAG = "_18KGold";

    private void Awake()
    {
        VRChooseGoldUI.Instance.OnPreviousGoldInLeftPartOfScaleRemoved += Handle_PreviousGoldRemoved;
    }

    private void Start()
    {
        VRTriggerTheGold.Instance.OnTouchedGameObjectStop += MassIncrease;
    }

    public override void MassDecrease()
    {
        Mass = 0;
    }

    public override void MassIncrease(Collider collider)
    {
        if(collider.transform.CompareTag(_14KGold_TAG))
        {
            Mass = _14KGoldSO.Weight;
        }

        else
        {
            Mass = _18KGoldSO.Weight;
        }

        collider.transform.SetParent(transform);
    }

    private void Handle_PreviousGoldRemoved()
    {
        foreach (Transform child in transform)
        {
            // When spawnGold Trigger the scale
            // 確保不會誤刪到其他 Transform 
            if(child.transform.CompareTag(_14KGold_TAG) || child.transform.CompareTag(_18KGold_TAG))
            {
                Destroy(child.gameObject);

                MassDecrease();
            }

            else
            {
                continue;
            }
        }
    }
}
