using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVROrNot : MonoBehaviour
{
    [SerializeField] private GameObject XRRig;
    [SerializeField] private GameObject WebGLRig;

    private bool isOnXRDevice;

    private void Awake(){
        if(Application.platform is RuntimePlatform.Android){
            isOnXRDevice = true;
        }

        if(Application.platform is RuntimePlatform.WebGLPlayer){
            isOnXRDevice = false;
        }
    }

    private void Start(){
        XRRig.SetActive(isOnXRDevice);
        WebGLRig.SetActive(!isOnXRDevice);
    }
}
