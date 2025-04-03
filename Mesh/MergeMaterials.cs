using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MergeMaterials : MonoBehaviour
{
    [SerializeField] private Material red;
    [SerializeField] private Material flask;

    public void AdvancedMerge()
    {
        for(int a = 0 ; a < transform.childCount ; a++){
            transform.GetChild(a).gameObject.SetActive(true);
        }

        // All our children (and us)
        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>(false);
        foreach(MeshFilter mF in filters){
            Debug.Log(mF);
        }

        // All the meshes in our children (just a big list)
        List<Material> mat = new List<Material>();
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(false); // <-- you can optimize this

        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer.transform == transform)
                continue;

            Material[] localMats = renderer.sharedMaterials;
            foreach (Material localMat in localMats){
                Debug.Log(localMat);
                if (!mat.Contains(localMat)){
                    mat.Add (localMat);
                }              
            }
        }

        // List<mat> have all of the material we used in the gameobject

        // Each material will have a mesh for it.
        List<Mesh> submeshes = new List<Mesh>();

        foreach (Material material in mat)
        {
            // Make a combiner for each (sub)mesh that is mapped to the right material.
            List<CombineInstance> combiners = new List<CombineInstance>();
            foreach (MeshFilter filter in filters)
            {
                if (filter.transform == transform) continue;
                // The filter doesn't know what materials are involved, get the renderer.
                MeshRenderer renderer = filter.GetComponent<MeshRenderer>();  // <-- (Easy optimization is possible here, give it a try!)
                if (renderer == null)
                {
                    Debug.LogError (filter.name + " has no MeshRenderer");
                    continue;
                }

                // Let's see if their materials are the one we want right now.
                Material [] localMaterials = renderer.sharedMaterials;
                for (int materialIndex = 0; materialIndex < localMaterials.Length; materialIndex++)
                {
                    if (localMaterials [materialIndex] != material)
                    continue;
                    // This submesh is the material we're looking for right now.
                    CombineInstance ci = new CombineInstance
                    {
                        mesh = filter.sharedMesh,
                        subMeshIndex = materialIndex,
                        transform = Matrix4x4.identity
                    };
                    combiners.Add(ci);
                }
            }

            // Flatten into a single mesh.
            Mesh mesh = new Mesh ();
            mesh.CombineMeshes (combiners.ToArray(), true);
            submeshes.Add (mesh);
        }

        // The final mesh: combine all the material-specific meshes as independent submeshes.
        List<CombineInstance>finalCombiners = new List<CombineInstance>();

        foreach (Mesh mesh in submeshes)
        {
            CombineInstance ci = new CombineInstance
            {
                mesh = mesh,
                subMeshIndex = 0,
                transform = Matrix4x4.identity
            };
            finalCombiners.Add (ci);
        }

        Mesh finalMesh = new Mesh();

        finalMesh.CombineMeshes (finalCombiners.ToArray(), false);

        if(transform.GetComponent<MeshFilter>()){
            MeshFilter myMeshFilter = transform.GetComponent<MeshFilter>();
            myMeshFilter.sharedMesh = finalMesh;

            for(int a = 0 ; a < transform.childCount ; a++){
                transform.GetChild(a).gameObject.SetActive(false);
            }
        }
        else{
            transform.AddComponent<MeshFilter>().sharedMesh = finalMesh;
        }

        if(transform.GetComponent<MeshRenderer>()){
            MeshRenderer myMeshRenderer = transform.GetComponent<MeshRenderer>();
            // myMeshRenderer.sharedMaterials = new Material [mat.Count];
            foreach(Material submeshesMat in mat){
                Debug.Log(submeshesMat);
            }
        }
        else{
            transform.AddComponent<MeshRenderer>();
        }

        Debug.Log ("Final mesh has " + submeshes.Count + " materials.");
    }
}
