using UnityEngine;

public class Title : MonoBehaviour
{
    public static Title Instance {get ; private set;}

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LeftKey.Instance.OnKeyPressed += Hide;

        RightKey.Instance.OnKeyPressed += Hide;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
