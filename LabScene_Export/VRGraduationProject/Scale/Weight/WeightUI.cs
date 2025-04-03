using UnityEngine;
using UnityEngine.UI;

public class WeightUI : MonoBehaviour
{
    [Header("Weight UI")]
    [SerializeField] private Button _minusButton;
    [SerializeField] private Button _plusButton;

    private PartOfScale _rightPart;

    private void Awake()
    {
        _rightPart = VRRightPart.Instance;
    }

    private void Start()
    {
        _minusButton.onClick.AddListener(Handle_MinusButtonClick);

        _plusButton.onClick.AddListener(Handle_PlusButtonClick);
    }

    private void Handle_MinusButtonClick()
    {
        _rightPart.MassDecrease();

        VRRightPart.Instance.DestroyWeight();
    }

    private void Handle_PlusButtonClick()
    {
        _rightPart.MassIncrease(null);

        VRRightPart.Instance.InstantiateWeight();
    }
}
