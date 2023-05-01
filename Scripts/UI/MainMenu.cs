using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public enum MainMenuState
{
    main, howToPlay, credits, quit
}

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance {get; private set;}

    [Header("Game")]
    [SerializeField] private string NameOfMainGameScene;

    [Header("Menus")]
    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private GameObject howToPlayMenuObject, creditsMenuObject, quitMenuObject;

    [Header("Buttons Selected")]
    [SerializeField] private GameObject howToPlayExitButton;
    [SerializeField] private GameObject creditsExitButton, quitExitButton, howToPlayEnterButton, creditsEnterButton, quitEnterButton;

    [Header("Other")]
    [SerializeField] private TextMeshProUGUI LegalAndBuildInfo;

    private MainMenuState currentState = MainMenuState.main;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple MainMenu scripts found in this scene. There should only be one.");
        }
        instance = this;

        LegalAndBuildInfo.text = Application.version + "\nBuilt by: Joseph and Everette for Ludum Dare 53 \nAll Rights are owned by their original owners";
    }

    public void Back(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Back();
        }
    }

    public void Back()
    {
        switch (currentState)
        {
            case MainMenuState.main:
                OnQuitClicked();
                break;
            case MainMenuState.howToPlay:
                OnHowToPlayExited();
                break;
            case MainMenuState.credits:
                OnCreditsExit();
                break;
            case MainMenuState.quit:
                OnQuitExit();
                break;
            default:
                break;
        }
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadSceneAsync(NameOfMainGameScene);
    }

    public void OnHowToPlayClicked()
    {
        currentState = MainMenuState.howToPlay;

        mainMenuObject.SetActive(false);
        howToPlayMenuObject.SetActive(true);

        StartCoroutine(setSelected(howToPlayEnterButton));
    }

    public void OnHowToPlayExited()
    {
        currentState = MainMenuState.main;

        mainMenuObject.SetActive(true);
        howToPlayMenuObject.SetActive(false);

        StartCoroutine(setSelected(howToPlayExitButton));
    }

    public void OnCreditsClicked()
    {
        currentState = MainMenuState.credits;

        mainMenuObject.SetActive(false);
        creditsMenuObject.SetActive(true);

        StartCoroutine(setSelected(creditsEnterButton));
    }

    public void OnCreditsExit()
    {
        currentState = MainMenuState.main;

        mainMenuObject.SetActive(true);
        creditsMenuObject.SetActive(false);

        StartCoroutine(setSelected(creditsExitButton));
    }

    public void OnQuitClicked()
    {
        currentState = MainMenuState.quit;

        mainMenuObject.SetActive(false);
        quitMenuObject.SetActive(true);

        StartCoroutine(setSelected(quitEnterButton));
    }

    public void OnQuitExit()
    {
        currentState = MainMenuState.main;
        
        mainMenuObject.SetActive(true);
        quitMenuObject.SetActive(false);

        StartCoroutine(setSelected(quitExitButton));
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private IEnumerator setSelected(GameObject gameObject)
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
