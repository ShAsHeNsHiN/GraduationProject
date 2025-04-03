using UnityEngine;

public class PurpleCabbageSlicedJustVisual : MonoBehaviour
{
    private void Start()
    {
        TriggerPurpleCabbageSliced.Instance.OnAddPurpleCabbageSliced += Show;

        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
