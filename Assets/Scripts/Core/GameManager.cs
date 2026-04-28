using System.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;
    public UnityEvent<GameState> OnStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateState(GameState.MainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
                UpdateState (GameState.Paused);
            else if (currentState == GameState.Paused)
                UpdateState (GameState.Playing);
        }
    }

    public void UpdateState (GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu: Time.timeScale = 1f; break;
            case GameState.Playing: Time.timeScale = 1f; break;
            case GameState.Paused: Time.timeScale = 0f; break;
            case GameState.GameOver: Time.timeScale = 0f; break;
        }

        OnStateChanged?.Invoke(newState);
    }

    public void PauseGame()
    {
        UpdateState(GameState.Paused);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        UpdateState (GameState.GameOver);
    }

    public void StartGame() => UpdateState(GameState.Playing);
    public void ResumeGame() => UpdateState (GameState.Playing);

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PergiKeMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() => Application.Quit();
}