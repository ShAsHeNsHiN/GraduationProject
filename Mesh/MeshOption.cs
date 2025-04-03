using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshOption : MonoBehaviour
{
    public float size = .25f;
    [SerializeField] private UnityEngine.GameObject [] options;
    [SerializeField] private int currentOption = 1;
    [SerializeField] private UnityEngine.GameObject instance;

    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position , size);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position , size * 1.1f);
    }

    public void NextOption(){
        if(instance != null){
            DestroyImmediate(instance);
            instance = null;
        }

        currentOption++;

        if(currentOption >= options.Length){
            currentOption = -1;
        }
        else{
            instance = Instantiate(options[currentOption] , transform);
            instance.transform.position = transform.position;
            // instance.transform.SetParent(transform , false);
            // instance.transform.position = transform.position;
        }
    }
}
