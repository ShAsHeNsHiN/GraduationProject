using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VRChooseGoldUI : MonoBehaviour
{
    private static VRChooseGoldUI _instance;
    public static VRChooseGoldUI Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<VRChooseGoldUI>();
            }

            return _instance;
        }
    }

    [SerializeField] private TMP_Dropdown _chooseGoldDropdown;
    [SerializeField] private Button _submitButton;

    [SerializeField] private ItemSO _14KGoldItemSO;
    [SerializeField] private ItemSO _18KGoldItemSO;

    private Transform _currentGoldTransform;

    public event Action OnPreviousGoldInLeftPartOfScaleRemoved;

    public event Action OnPreviousGoldInSpawnGoldPositionRemoved;

    public event Action OnPreviousGoldInGraduationCylinderRemoved;

    public event Action<Transform> OnGoldInstantiate;

    private void Awake()
    {
        // *Dropdown default value is gold 1
        _currentGoldTransform = _14KGoldItemSO.itemTransform;
    }

    private void Start()
    {
        _chooseGoldDropdown.onValueChanged.AddListener(Handle_ChooseGold);

        _submitButton.onClick.AddListener(Handle_SubmitButtonClick);
    }

    private void Handle_ChooseGold(int chooseGold)
    {
        _currentGoldTransform = chooseGold switch
        {
            (int)EKindOfGold._14K金 => _14KGoldItemSO.itemTransform ,
            (int)EKindOfGold._18K金 => _18KGoldItemSO.itemTransform ,
            _ => throw new NotImplementedException()
        };
    }

    private void Handle_SubmitButtonClick()
    {
        // a scene just need one gold , so when player every time click the submit button , the previous gold need to be removed

        OnPreviousGoldInSpawnGoldPositionRemoved?.Invoke();

        OnPreviousGoldInLeftPartOfScaleRemoved?.Invoke();

        OnPreviousGoldInGraduationCylinderRemoved?.Invoke();

        OnGoldInstantiate?.Invoke(_currentGoldTransform);
    }

    private void OnDestroy()
    {
        OnPreviousGoldInGraduationCylinderRemoved = null;

        OnPreviousGoldInLeftPartOfScaleRemoved = null;

        OnPreviousGoldInSpawnGoldPositionRemoved = null;

        OnGoldInstantiate = null;
    }
}
