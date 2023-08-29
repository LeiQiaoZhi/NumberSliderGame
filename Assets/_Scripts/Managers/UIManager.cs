using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject pauseButton;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] private GameObject scoreEffectPrefab;
    public Transform scoreEffectHolder;
    [Header("Game State")] public GameStates gameStates;

    private void Awake()
    {
        SetEnableGameOverScreen(false);
        pauseScreen.SetActive(false);
    }

    private void OnEnable()
    {
        ScoreManger.OnScoreChange += UpdateScoreText;
    }

    private void OnDisable()
    {
        ScoreManger.OnScoreChange -= UpdateScoreText;
    }

    private void UpdateScoreText(int _score, int _change)
    {
        if (_change == 0)
            return;
        scoreText.TweenNumber(_score, 0.4f).Play();
        GameObject effect = Instantiate(scoreEffectPrefab, scoreEffectHolder);
        effect.transform.localPosition = Vector3.zero;
        effect.GetComponentInChildren<TextMeshProUGUI>().text = $"+{_change}";
        Destroy(effect,2.0f);
    }

    public void SetEnableGameOverScreen(bool _enable)
    {
        gameOverScreen.SetActive(_enable);
    }

    public void Pause()
    {
        if (pauseScreen.activeSelf)
            return;
        pauseScreen.SetActive(true);
        pauseButton.SetActive(false);
        GameManager.Instance.PauseGame();
    }

    public void Resume()
    {
        if (gameStates.state == GameStates.GameState.Over || !pauseScreen.activeSelf)
            return;
        pauseScreen.SetActive(false);
        pauseButton.SetActive(true);
        GameManager.Instance.ResumeGame();
    }
}