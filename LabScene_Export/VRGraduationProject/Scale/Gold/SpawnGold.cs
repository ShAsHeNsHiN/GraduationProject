using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGold : MonoBehaviour
{
    public static SpawnGold Instance {get ; private set;}

    private void Awake()
    {
        Instance = this;

        VRChooseGoldUI.Instance.OnPreviousGoldInSpawnGoldPositionRemoved += Handle_PreviousGoldRemoved;

        VRChooseGoldUI.Instance.OnGoldInstantiate += Handle_GoldInstantiate;
    }

    private void Handle_PreviousGoldRemoved()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void Handle_GoldInstantiate(Transform goldTransform)
    {
        Instantiate(goldTransform , transform);
    }
}
