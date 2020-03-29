using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField]
    private ChunkSettings settings;
    private ListOptim<Cube> cubes;

    private void Start()
    {
        cubes = new ListOptim<Cube>(1000);
        MeshRenderer[] cubesToAdd = GetComponentsInChildren<MeshRenderer>();
        for(int i =0; i < cubesToAdd.Length; ++i)
        {
            cubes.Add(new Cube(cubesToAdd[i].gameObject));
        }
        ActiveAll(false);
        RandomizeCubeActive();
        LifeGeneration();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ActiveAll(false);
            RandomizeCubeActive();
            LifeGeneration();
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            LifeGeneration();
        }
    }

    private void ActiveAll(bool isActive)
    {
        for(int i =0; i < cubes.Count; ++i)
        {
            if (cubes[i].posY==0)
            {
                cubes[i].cube.SetActive(true);
            }
            else
            {
                cubes[i].cube.SetActive(isActive);
            }
        }
    }

    private void RandomizeCubeActive()
    {
        for(int i =0; i < cubes.Count; ++i)
        {
            int rand = Random.Range(1, 101);
            if(rand>=50)
            {
                cubes[i].cube.SetActive(true);
            }
        }
    }

    private void LifeGeneration()
    {
        for (int j = 0; j < settings.loopCount; ++j)
        {
            for (int i = 0; i < cubes.Count; ++i)
            {
                int neighbor = NeighborCount(cubes[i].posX, cubes[i].posY, cubes[i].posZ);
                if (neighbor == settings.neighborCountStayAlive)
                {
                    cubes[i].cube.SetActive(true);
                }
                else if (neighbor == settings.neighborCountNothingChange)
                {
                    continue;
                }
                else
                {
                    cubes[i].cube.SetActive(false);
                }
            }
        }
    }

    private int NeighborCount(int posX, int posY, int posZ)
    {
        int count = 0;
        for(int x = -1; x < 2; ++x)
        {
            for(int y = -1; y < 2; ++y)
            {
                for(int z = -1; z < 2; ++z)
                {
                    if ((x==0&&y==0&&z==0))
                    {
                        //continue;
                    }
                    else
                    {
                        GameObject cube = GetCubeAtPosition(posX + x, posY + y, posZ + z);
                        count += cube != null ? ( cube.gameObject.activeInHierarchy ? 1 : 0 ) : 0;
                    }
                }
            }
        }
        return count;
    }

    private GameObject GetCubeAtPosition(int posX, int posY, int posZ)
    {
        GameObject result = null;
        for (int i = 0; i < cubes.Count; ++i)
        {
            if(cubes[i].IsCurrentCube(posX,posY,posZ))
            {
                result = cubes[i].cube;
            }
        }
        return result;
    }

    public struct Cube
    {
        public GameObject cube;
        public int posX;
        public int posY;
        public int posZ;

        public Cube(GameObject cube)
        {
            this.cube = cube;
            posX = (int)cube.transform.localPosition.x;
            posY = (int)cube.transform.localPosition.y;
            posZ = (int)cube.transform.localPosition.z;
        }

        public bool IsCurrentCube(int x, int y, int z)
        {
            return posX == x && posY == y && posZ == z;
        }
    }

}
