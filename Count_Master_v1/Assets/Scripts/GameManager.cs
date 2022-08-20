using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject mainMenuScreen;
    public GameObject winScreen;
    public GameObject lostScreen;

    public static GameManager instance;
    public GameState state;
    public static event Action<GameState> OnStateChange;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                HandleMenu();
                break;
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lost:
                HandleLost();
                break;
        }

        OnStateChange?.Invoke(newState);
    }

    private void HandleMenu()
    {
        mainMenuScreen.SetActive(true);
    }
    private void HandlePlaying()
    {
        mainMenuScreen.SetActive(false);
    }
    private void HandleWin()
    {
        winScreen.SetActive(true);
    }
    private void HandleLost()
    {
        lostScreen.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public enum GameState
{
    MainMenu,
    Playing,
    Win,
    Lost
};
}