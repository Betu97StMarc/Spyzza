using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pinPrefab;
    public GameObject pinSpawned;
    public Transform canvas;
    public Transform spawnPin;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnPin();
        }


    }

    public void SpawnPin()
    {
        pinSpawned = Instantiate(pinPrefab, new Vector3(0,0,0), transform.rotation);
        pinSpawned.transform.SetParent(canvas, false);
    }

}
