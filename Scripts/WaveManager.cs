using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class WaveManager : MonoBehaviour
{
    public static WaveManager instance {get; private set;}

    public bool StartOnAwake = true;
    public List<Wave> normalWaves;
    public Wave lastWave;
    [SerializeField] private int index;

    [Header("Start/Finish Events")]
    public UnityEvent<float> OnAnyWaveStart;
    public UnityEvent OnAnyWaveFinish, OnAllNormalWavesDone, OnLastWaveStart, OnLastWaveFinish;

    private bool isRunning = false;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple WaveManagers found in this scene.  There should only be one.");
        }
        instance = this;

        if(StartOnAwake)
        {
            RunNormalWaves();
        }
    }

    public void RunNormalWaves()
    {
        if(isRunning) return;

        OnAnyWaveFinish.AddListener(StartNextNormalWave);

        StartNextNormalWave();
    }

    public void StartLastWave()
    {
        StopAllCoroutines();

        OnAnyWaveStart.Invoke(lastWave.timeBeforeWaveStarts);
        OnLastWaveStart.Invoke();
        OnAnyWaveFinish.AddListener(OnLastWaveFinish.Invoke);
        StartCoroutine(timerBeforeWave(lastWave));
    }

    private void StartNextNormalWave()
    {
        if(index >= normalWaves.Count)
        {
            WavesDone();
            return;
        }
        else
        {
            OnAnyWaveStart.Invoke(normalWaves[index].timeBeforeWaveStarts);
            StartCoroutine(timerBeforeWave(normalWaves[index]));
        }
    }

    private IEnumerator timerBeforeWave(Wave wave)
    {
        yield return new WaitForSeconds(wave.timeBeforeWaveStarts);
        StartCoroutine(RunWave(wave));
    }

    private IEnumerator RunWave(Wave wave)
    {
        int enemyIndex = 0;

        while(enemyIndex < wave.enemies.Count)
        {
            Instantiate(wave.enemies[enemyIndex], this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(wave.spawnRate);
            enemyIndex++;
        }

        index++;
        OnAnyWaveFinish.Invoke();
    }

    public void WavesDone()
    {
        OnAllNormalWavesDone.Invoke();
        isRunning = false;
    }


}
