using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode{
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
        // 下拉式選單
    }

    [SerializeField] private Mode mode;

    private void LateUpdate(){
        switch(mode){
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                // 面對玩家視角
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                // 面對玩家視角並顛倒
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                // 看相機
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                // 看相機並顛倒
                break;
        }
        
    }

    
}
