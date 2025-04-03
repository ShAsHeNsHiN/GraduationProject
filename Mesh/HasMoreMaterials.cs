using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasMoreMaterials : MonoBehaviour
{
    private void Awake(){
        HasMoreSubmeshesThanMaterials(gameObject);
    }

    public bool HasMoreSubmeshesThanMaterials(UnityEngine.GameObject gameObject)
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();

        if(meshRenderer == null && meshFilter == null) return false;
        if(meshRenderer == null && meshFilter != null)
        {
        Debug.LogWarning("GameObject has a MeshFilter but no MeshRenderer", gameObject);
        return meshFilter.sharedMesh != null;
        }
        if(meshRenderer != null && meshFilter == null)
        {
        Debug.LogWarning("GameObject has a MeshRenderer but no MeshFilter", gameObject);
        return false;
        }

        int materialsCount = meshRenderer.materials.Length;
        int subMeshesCount = meshFilter.sharedMesh.subMeshCount;

        return subMeshesCount > materialsCount;
    }
}
