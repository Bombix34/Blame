using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    private ListOptim<GameObject> cubes;

    private void Start()
    {
        cubes = new ListOptim<GameObject>(1000);
        MeshRenderer[] cubesToAdd = GetComponentsInChildren<MeshRenderer>();
        print(cubesToAdd.Length);
        for(int i =0; i < cubesToAdd.Length; ++i)
        {
            cubes.Add(cubesToAdd[i].gameObject);
        }
        ActiveAll(false);
        RandomizeCubeActive();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ActiveAll(false);
            RandomizeCubeActive();
        }
    }

    private void ActiveAll(bool isActive)
    {
        for(int i =0; i < cubes.Count; ++i)
        {
            cubes[i].SetActive(isActive);
        }
    }

    private void RandomizeCubeActive()
    {
        for(int i =0; i < cubes.Count; ++i)
        {
            int rand = Random.Range(1, 101);
            if(rand>=50)
            {
                cubes[i].SetActive(true);
            }
        }
    }
}
