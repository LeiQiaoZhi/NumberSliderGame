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
        // happens when going to the tutorial scene
        if (playerMovement_ == null || numberGridGenerator_ == null)
        {
            XLogger.LogWarning(Category.GameManager, "PlayerMovement or NumberGridGenerator not found");
            Destroy(gameObject);
            return;
        }

        World world = progression_.GetWorld();
        numberGridGenerator_.Init(world.colorPreset, world);
        playerMovement_.OnGenerationStart(numberGridGenerator_, gameStates, world.GetPlayerStartPosition());
    }

    // after scene opening animation
    public void StartGame()
    {
        gameStates.state = GameStates.GameState.Playing;
        OnGameStart?.Invoke();
    }

    public void EnterPortal()
    {
        StartCoroutine(LoadLevelCoroutine(progression_.nextProgression, 3.0f));
    }

    private IEnumerator LoadLevelCoroutine(Progression _progression, float _animationTime)
    {
        yield return new WaitForSeconds(_animationTime);
        progression_ = _progression;
        SceneLoader.Instance.ReloadScene();
    }

    public void LoadLevel(Progression _progression, float _animationTime = 3.0f)
    {
        StartCoroutine(LoadLevelCoroutine(_progression, _animationTime));
    }

    public void RestartGame()
    {
        progression_ = progression_.GetRestartProgression();
        gameStates.state = GameStates.GameState.Over;
        OnGameRestart?.Invoke();
        SceneLoader.Instance.ReloadScene();
    }

    public void ToProgression(Progression _progression)
    {
        progression_ = _progression;
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