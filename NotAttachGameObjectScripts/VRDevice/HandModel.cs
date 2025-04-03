using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandModel : MonoBehaviour
{
    [Header("Original Hand Model")]
    [SerializeField] private GameObject originalHandModel;
    [SerializeField] private GameObject directInteractor;
    [SerializeField] private InputActionReference gripRef;
    [SerializeField] private InputActionReference triggerRef;

    [Header("Poke Hand Model")]
    [SerializeField] private GameObject pokeHandModel;
    [SerializeField] private GameObject pokeInteractor;
    [SerializeField] private InputActionReference pokeRef;

    private void Update()
    {
        if(pokeRef.action.IsPressed())
        {
            pokeHandModel.SetActive(true);

            originalHandModel.SetActive(false);
        }

        if(gripRef.action.IsPressed() || triggerRef.action.IsPressed() || !pokeRef.action.IsPressed()){
            originalHandModel.SetActive(true);

            pokeHandModel.SetActive(false);
        }

        directInteractor.SetActive(originalHandModel.activeSelf);
        
        pokeInteractor.SetActive(pokeHandModel.activeSelf);

        // print(pokeReference.action.IsPressed());
    }
}
