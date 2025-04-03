using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EzySlice;

public class DirectedSliced : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Material targetMat;
    [SerializeField] private Transform planeDebug;

    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField] private LayerMask slicedLayer;
    [SerializeField] private VelocityEstimator velocityEstimator;

    [SerializeField] private float explosionForce = 1500;
    [SerializeField] private float explosionRadius = 1;

    private void FixedUpdate(){
        // My Version
        bool hasHit = Physics.Linecast(startTransform.position , endTransform.position , out RaycastHit hit , slicedLayer);

        if(hasHit){
            // SlicedSth(target);
            GameObject target = hit.transform.gameObject;
            SlicedSth(target);
        }

        // // Valem Version
        // bool hasHitValem = Physics.Linecast(startTransform.position , endTransform.position , out RaycastHit rayHit , slicedLayer);

        // if(hasHitValem){
        //     GameObject target = rayHit.transform.gameObject;
        //     SlicedSth(target);
        // }
    }

    private void SlicedSth(GameObject target){
        // my version
        SlicedHull slicedHull = target.Slice(endTransform.position , endTransform.up);

        if(slicedHull != null){
            // Debug.Log("it's active");
            // Transform targetTransform = Instantiate(target.transform);
            GameObject upperHull = slicedHull.CreateUpperHull(target , targetMat);

            SetupSlicedComponent(upperHull);

            GameObject lowerHull = slicedHull.CreateLowerHull(target , targetMat);

            SetupSlicedComponent(lowerHull);

            Destroy(target);
        }

        // Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        // Debug.Log(velocity);

        // Vector3 minusPos = startTransform.position - endTransform.position;
        // Vector3 planeNormal = Vector3.Cross(minusPos , velocity);
        // Debug.Log(minusPos);
        // planeNormal.Normalize();
        // Debug.Log("previous!");
        // Debug.Log(planeNormal);

        // // Valem version
        // SlicedHull slicedHullValem = target.Slice(startTransform.position , startTransform.forward);

        // if(slicedHullValem != null){
        //     Debug.Log("it's active");
        //     // Transform targetTransform = Instantiate(target.transform);
        //     GameObject upperHull = slicedHullValem.CreateUpperHull(target , targetMat);

        //     SetupSlicedComponent(upperHull);

        //     GameObject lowerHull = slicedHullValem.CreateLowerHull(target , targetMat);

        //     SetupSlicedComponent(lowerHull);

        //     Destroy(target);
        // }
    }

    private void SetupSlicedComponent(GameObject hull){
        Rigidbody rb = hull.AddComponent<Rigidbody>();
        MeshCollider meshCollider = hull.AddComponent<MeshCollider>();

        meshCollider.convex = true;

        rb.AddExplosionForce(explosionForce , hull.transform.position , explosionRadius);
    }
}
