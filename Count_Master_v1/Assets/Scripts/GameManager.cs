using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    public GameObject mainMenuScreen;
    public GameObject winScreen;
    public GameObject lostScreen;
    public GameObject endGameVFX;
    public int currentScore;
    public TextMeshProUGUI scoreText;

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
        foreach (Transform child in endGameVFX.transform)
        {
            child.GetComponent<ParticleSystem>().Play();
        }
        scoreText.text = "Finish Count Is: " + PlayerPrefs.GetInt("Score").ToString();
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