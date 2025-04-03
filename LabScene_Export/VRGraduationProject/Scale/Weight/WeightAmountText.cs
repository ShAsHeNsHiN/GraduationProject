using TMPro;
using UnityEngine;

public class WeightAmountText : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void SetWeightAmountText(int amount)
    {
        textMeshProUGUI.SetText($"砝碼 : {amount}");
    }

    private void Update()
    {
        SetWeightAmountText((int)VRRightPart.Instance.Mass);
    }
}
