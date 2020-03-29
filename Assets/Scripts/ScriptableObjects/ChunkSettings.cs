using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Blame/SpawnerSettings")]
public class ChunkSettings : ScriptableObject
{

    [Header("Algo life settings")]
    public int loopCount = 2;
    public int neighborCountStayAlive = 3;
    public int neighborCountNothingChange = 2;
    //public Vector2 neighborCountToDie;
}
