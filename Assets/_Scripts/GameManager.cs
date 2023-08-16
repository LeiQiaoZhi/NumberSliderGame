using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// singleton, don't destroy on load
/// </summary>
public class GameManager : MonoBehaviour
{
    public Progression startingProgression;
    private Progression progression_;
    public GameStates gameStates;
    
    public delegate void GameStart();
    public static event GameStart OnGameStart;
    public delegate void GameRestart();
    public static event GameRestart OnGameRestart;

    private PlayerMovement playerMovement_;
    private NumberGridGenerator numberGridGenerator_;

    public static GameManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        // this will only call once
        progression_ = startingProgression;
        DontDestroyOnLoad(gameObject);
    }


    /// this will call every time a scene is loaded
    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        StartGeneration();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void StartGeneration()
    {
        Time.timeScale = 1.0f;
        gameStates.state = GameStates.GameState.Generation;
        playerMovement_ = FindObjectOfType<PlayerMovement>();
        numberGridGenerator_ = FindObjectOfType<NumberGridGenerator>();

        World world = progression_.GetWorld();
        numberGridGenerator_.Init(world.colorPreset, world, gameStates);
        playerMovement_.OnGenerationStart(numberGridGenerator_, gameStates);
    }

    // after scene opening animation
    public void StartGame()
    {
        gameStates.state = GameStates.GameState.Playing;
        OnGameStart?.Invoke();
    }


    public void EnterPortal()
    {
        StartCoroutine(EnterPortalCoroutine());
    }

    private IEnumerator EnterPortalCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        progression_ = progression_.nextProgression;
        SceneLoader.Instance.ReloadScene();
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        progression_ = startingProgression;
        gameStates.state = GameStates.GameState.Over;
        OnGameRestart?.Invoke();
        SceneLoader.Instance.ReloadScene();
    }

    public void PauseGame()
    {
        gameStates.state = GameStates.GameState.Pause;
    }
    public void ResumeGame()
    {
        gameStates.state = GameStates.GameState.Playing;
    }
}