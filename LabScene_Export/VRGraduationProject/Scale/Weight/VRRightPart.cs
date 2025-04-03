using UnityEngine;

public class VRRightPart : PartOfScale
{
    private static VRRightPart instance;
    public static VRRightPart Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<VRRightPart>();
            }

            return instance;
        }
    }

    [Header("Weight")]
    [SerializeField] private TheScaleItemSO _weightSO;
    [SerializeField] private ItemSO _weightItemSO;

    private const float LEFT_POSITION_MINIMUM = -.22f;
    private const float LEFT_POSITION_MAXIMUM = .25f;

    private const float RIGHT_POSITION_MINIMUM = -.29f;
    private const float RIGHT_POSITION_MAXIMUM = .21f;

    public override void MassIncrease(Collider collider)
    {
        Mass += _weightSO.Weight;
    }

    public override void MassDecrease()
    {
        if(Mass > 0)
        {
            Mass -= _weightSO.Weight;
        }
    }

    public void InstantiateWeight()
    {
        Transform weightTransform = Instantiate(_weightItemSO.itemTransform , transform);

        Vector3 weightSpawnPosition = new(Random.Range(LEFT_POSITION_MINIMUM , LEFT_POSITION_MAXIMUM) , -.5f , Random.Range(RIGHT_POSITION_MINIMUM , RIGHT_POSITION_MAXIMUM));

        weightTransform.localPosition = weightSpawnPosition;
    }

    public void DestroyWeight()
    {
        if(transform.childCount != 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
