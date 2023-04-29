using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Wave
{
    public float timeBeforeWaveStarts;
    public List<GameObject> enemies;
    public float spawnRate;
}
