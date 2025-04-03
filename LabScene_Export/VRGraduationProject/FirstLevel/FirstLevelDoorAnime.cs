using UnityEngine;

public class FirstLevelDoorAnime : MonoBehaviour
{
    public static FirstLevelDoorAnime Instance{get ; private set;}

    private Animator _animator;

    private const string OPEN = "Open";
    private const string CLOSE = "Close";

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        Instance = this;
    }

    public void Handle_DestroyDoor()
    {
        Destroy(gameObject);
    }

    private void OpenDoor()
    {
        _animator.SetTrigger(OPEN);
    }

    private void CloseDoor()
    {
        _animator.SetTrigger(CLOSE);
    }
}
