using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public Transform animeTransform;
    public Transform parentTranform;

    public TextMeshProUGUI tmpText;

    private enum HHH{
        GameScene ,
        loadscene , 
        IDKJJJjjjj
    }

    [Range(0 , 1)]
    public float lerpTime;

    public void AddMesh(){
        if(transform.TryGetComponent(out MeshFilter meshFilter) && transform.TryGetComponent(out MeshRenderer meshRenderer)){
            Debug.Log("Already has the component!");
        }
        else{
            transform.AddComponent<MeshFilter>();
            transform.AddComponent<MeshRenderer>();
        }
    }

    public void AddRigibody(){
        transform.AddComponent<Rigidbody>();
    }

    public void Testing(){
        print(tmpText.color.a);
    }

    private void TestDictionary(){
        // Debug.Log(testDictionary["SugarWater"]);
    }

    private void Start(){
        // Time.timeScale = 0f;
        // inputActionReference.action.performed += ChangeScene;
    }

    private void ChangeScene(InputAction.CallbackContext obj){
        SceneManager.LoadScene("LabScene");
    }
}
