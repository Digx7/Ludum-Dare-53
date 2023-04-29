using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum UIState
{
    gamePlay, Pause, Win, Lose
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance {get; private set;}

    public bool shouldDisplayStopWatch;
    private bool isTimerGoing;
    private float timeLeft;

    [Header("GameObject Items")]
    [SerializeField] private GameObject GamePlayObject;
    [SerializeField] private GameObject PauseObject;
    [SerializeField] private GameObject WinObject;
    [SerializeField] private GameObject LoseObject;

    [Header("Text Items")]
    [SerializeField] private TextMeshProUGUI StopWatch;

    [Header("Image Items")]
    [SerializeField] private Image image;

    private UIState state = UIState.gamePlay;

    
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple UIManagers found in this scene.  There should only be one");
        }
        instance = this;
        UpdateTimer();
    }
    
    public void OnPauseClick()
    {
        GamePlayObject.SetActive(false);
        PauseObject.SetActive(true);

        state = UIState.Pause;
    }

    // Pause Menu Buttons
    public void OnResumeClick()
    {
        GamePlayObject.SetActive(true);
        PauseObject.SetActive(false);

        state = UIState.gamePlay;
    }

    public void OnRetryClick()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void OnQuitClick()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void OnGameWin()
    {
        GamePlayObject.SetActive(false);
        PauseObject.SetActive(false);
        WinObject.SetActive(true);

        state = UIState.Win;
    }

    public void OnGameLose()
    {
        GamePlayObject.SetActive(false);
        PauseObject.SetActive(false);
        LoseObject.SetActive(true);
        
        state = UIState.Lose;
    }

    public void Back()
    {
        switch (state)
        {
            case UIState.Pause:
                OnResumeClick();
                break;
            default:
                break;
        }
    }

    // Timer Functions
    private void Update()
    {
        shouldDisplayStopWatch = player_script.instance.playerHasAWatch;
        if(isTimerGoing)
        {
            timeLeft -= Time.deltaTime;
            if(timeLeft < 0) {
                timeLeft = 0;
                isTimerGoing = false;
            }
        }

        UpdateTimer();
    }

    public void StartTimer(float time)
    {
        StopAllCoroutines();
        timeLeft = time;
        isTimerGoing = true;
    }

    private void UpdateTimer()
    {
        if(shouldDisplayStopWatch) 
        {
            if(isTimerGoing)
            {
                Debug.Log("Timer is Going");
                int minutes = Convert.ToInt32(Math.Floor(timeLeft/60));
                float seconds = timeLeft - (minutes*60);
                StopWatch.text = minutes.ToString() + ":" + seconds.ToString("0");
                image.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Timer is NOT going");
                StopWatch.text = "";
                image.gameObject.SetActive(true);
            }
        }
        else 
        {
            StopWatch.text = "";
            image.gameObject.SetActive(false);
        }
    }


}
