using UnityEngine;

public class VRGraduationTextUI : MonoBehaviour
{
    private static VRGraduationTextUI _instance;
    public static VRGraduationTextUI Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<VRGraduationTextUI>();
            }

            return _instance;
        }
    }

    [Header("Smaller Information")]
    [SerializeField] private LiquidSO _smallerMLSO;
    [SerializeField] private Transform _smallerMLPosition;
    
    [Header("Bigger Information")]
    [SerializeField] private LiquidSO _biggerMLSO;
    [SerializeField] private Transform _biggerMLPosition;

    [SerializeField] private GameObject _graduationTextContainer;

    private void Awake()
    {
        VRTriggerLiquid.Instance.OnRemoveGold += Handle_GraduationCylinderTextForSmallerML;

        ButtonForGraCyXRSimpleInteractable.Instance.OnAddWater += Handle_GraduationCylinderTextForSmallerML;

        VRTriggerLiquid.Instance.OnGoldTriggerLiquid += Handle_GraduationCylinderTextForBiggerML;
    }

    private void Start()
    {
        Hide();
    }

    private void Handle_GraduationCylinderTextForSmallerML()
    {
        GraduationCylinderText.Instance.SetGraduationCylinderText(_smallerMLSO.mL);

        transform.position = _smallerMLPosition.position;
    }

    // *這邊的參數是為了 OnGoldTriggerLiquid 而加的
    private void Handle_GraduationCylinderTextForBiggerML(Collider collider)
    {
        GraduationCylinderText.Instance.SetGraduationCylinderText(_biggerMLSO.mL);

        transform.position = _biggerMLPosition.position;
    }

    public void Hide()
    {
        _graduationTextContainer.SetActive(false);
    }

    public void Show()
    {
        _graduationTextContainer.SetActive(true);
    }
}
