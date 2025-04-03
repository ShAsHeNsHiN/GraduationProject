using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    public void CombineMeshes(){
        for(int a = 0 ; a < transform.childCount ; a++){
            transform.GetChild(a).gameObject.SetActive(true);
        }

        Quaternion oldRot = transform.rotation;
        Vector3 oldPos = transform.position;

        // initialize pos and rot
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        // Get all the gameobject that have the component of meshfilter
        // Count the child gameobject that is active!!
        MeshFilter [] filters = GetComponentsInChildren<MeshFilter>();

        // Count all of the child gameobject!!
        MeshFilter [] filters1 = GetComponentsInChildren<MeshFilter>(true);

        // initialize mesh
        Mesh finalMesh = new Mesh();

        CombineInstance [] combineInstances = new CombineInstance[filters.Length];

        for(int a = 0 ; a < filters.Length ; a++){
            if(filters[a].transform == transform){
                continue;
            }

            combineInstances[a].subMeshIndex = 0;
            combineInstances[a].mesh = filters[a].sharedMesh;
            combineInstances[a].transform = filters[a].transform.localToWorldMatrix;
        }
        
        finalMesh.CombineMeshes(combineInstances);

        GetComponent<MeshFilter>().sharedMesh = finalMesh;

        transform.rotation = oldRot;
        transform.position = oldPos;

        for(int a = 0 ; a < transform.childCount ; a++){
            transform.GetChild(a).gameObject.SetActive(false);
        }
    }
}
