using TMPro;
using UnityEngine;

public class GraduationCylinderText : MonoBehaviour
{
    public static GraduationCylinderText Instance{get ; private set;}

    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        Instance = this;

        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetGraduationCylinderText(default);
    }

    public void SetGraduationCylinderText(float mLText)
    {
        textMeshProUGUI.SetText($"刻度 : {mLText}");
    }
}
