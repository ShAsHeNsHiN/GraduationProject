using UnityEngine;

public class VRWeightUI : MonoBehaviour
{
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
