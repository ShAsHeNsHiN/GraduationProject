using UnityEngine;
using UnityEngine.InputSystem;

public class StartGameKey : MonoBehaviour
{
    [SerializeField] private InputActionReference _startGameKeyReference;

    private void Awake()
    {
        _startGameKeyReference.action.performed += StartGame;
    }

    private void StartGame(InputAction.CallbackContext obj)
    {
        Loader.Load(Loader.Scene.LabScene);
    }

    private void OnDestroy()
    {
        _startGameKeyReference.action.performed -= StartGame;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            StartGame(default);
        }
    }
}
