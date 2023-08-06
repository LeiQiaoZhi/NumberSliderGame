using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// singleton, don't destroy on load
/// </summary>
public class GameManager : MonoBehaviour
{
    public Progression progression;
    public ColorPreset colorPreset;
    public GameStates gameStates;

    private PlayerMovement playerMovement_;
    private NumberGridGenerator numberGridGenerator_;

    private static GameManager Instance { get; set; }

    /// this will only call once
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        gameStates.Init();
        DontDestroyOnLoad(gameObject);
    }


    /// this will call every time a scene is loaded
    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        StartGame();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void StartGame()
    {
        playerMovement_ = FindObjectOfType<PlayerMovement>();
        numberGridGenerator_ = FindObjectOfType<NumberGridGenerator>();

        numberGridGenerator_.Init(colorPreset, progression.GetWorld(), gameStates);
        playerMovement_.OnGameStart(numberGridGenerator_, gameStates);
        gameStates.state = GameStates.GameState.Playing;
    }

    public void EnterPortal()
    {
        progression = progression.nextProgression;
        SceneLoader.Instance.ReloadScene();
    }
}