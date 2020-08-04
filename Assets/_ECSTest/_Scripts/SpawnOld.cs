using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOld : MonoBehaviour
{
    [Range(0, 15000)] [SerializeField] private int maxSheep;
    [SerializeField] private GameObject sheepPrefab;
    private Transform[] sheepTransforms;

    void Start()
    {
        sheepTransforms = new Transform[maxSheep];
        for (int i = 0; i < maxSheep; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            GameObject sheep = Instantiate(sheepPrefab, pos, Quaternion.identity);
            sheepTransforms[i] = sheep.transform;
        }
    }

    void Update()
    {
        for (int i = 0; i < sheepTransforms.Length; i++)
        {
            sheepTransforms[i].Translate(0, 0, Time.deltaTime);
            if (sheepTransforms[i].position.z > 50)
            {
                sheepTransforms[i].transform.position = new Vector3(sheepTransforms[i].transform.position.x, 0, -50);
            }
        }   
    }
}
