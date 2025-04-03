using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private InputActionReference shootRef;

    [Header("Pistol")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float bulletSpeed;

    private int startChildAmount;

    private XRGrabInteractable xRGrabInteractable;

    private void Awake(){
        xRGrabInteractable = GetComponent<XRGrabInteractable>();

        xRGrabInteractable.activated.AddListener(Shoot);
    }

    private void Start(){

    }

    private void Shoot(ActivateEventArgs args){
        GameObject spawnBullet = Instantiate(bullet);
        spawnBullet.transform.position = spawnPos.position;
        spawnBullet.GetComponent<Rigidbody>().velocity = spawnPos.forward * bulletSpeed;

        Destroy(spawnBullet , .5f);
    }
}
