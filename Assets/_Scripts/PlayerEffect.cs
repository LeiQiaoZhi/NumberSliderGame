using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public SpriteRenderer mainSprite;
    public SpriteRenderer borderSprite;
    [Header("Break Effect")] public ParticleSystem breakEffect;
    public float directionVelocityMultiplier;
    public float pieceSizeMultiplier = 1;
    [Header("Health Reduce Effect")]
    public float blackenTime = 0.4f;
    public float restoreTime = 0.2f;
    [SerializeField] private Color blackenColor;
    

    private InfiniteGridSystem gridSystem_;

    void Start()
    {
        gridSystem_ = FindObjectOfType<InfiniteGridSystem>();
        transform.localScale = new Vector3(gridSystem_.GetCellDimension().x, gridSystem_.GetCellDimension().y, 1);
    }

    private void OnEnable()
    {
        PlayerMovement.OnPlayerMove += OnPlayerMove;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerMove -= OnPlayerMove;
    }

    private void OnPlayerMove(PlayerMovement.MergeResult _mergeResult)
    {
        if (numberText != null)
            numberText.TweenNumber(_mergeResult.result, 0.14f, 5).Play();
        if (_mergeResult.type is PlayerMovement.MergeType.Divide or PlayerMovement.MergeType.Minus)
        {
            if (breakEffect == null) return;
            ParticleSystem effect = Instantiate(breakEffect);
            ParticleSystem.MainModule main = effect.main;
            main.startColor = _mergeResult.targetTransform.GetComponent<NumberCell>().GetColor();
            effect.transform.position = _mergeResult.targetTransform.position;
            effect.transform.localScale = _mergeResult.targetTransform.localScale.x * Vector3.one * pieceSizeMultiplier;
            effect.transform.rotation = Quaternion.identity;
            // change effect's direction based on the direction of the move
            ParticleSystem.VelocityOverLifetimeModule velocity = effect.velocityOverLifetime;
            velocity.x = _mergeResult.moveDirection.x * directionVelocityMultiplier;
            velocity.y = _mergeResult.moveDirection.y * directionVelocityMultiplier;
            velocity.z = 0;
            // TODO: change effect's spawn number based on the original number of the move
            Destroy(effect.gameObject, 1.0f);
        }

        if (_mergeResult.type == PlayerMovement.MergeType.Fail)
        {
            if (mainSprite == null) return;
            StartCoroutine(FailEffect());
        }
    }

    private IEnumerator FailEffect()
    {
        GameManager.Instance.PauseGame();
        Color mainSpriteColor = mainSprite.color;
        Color borderSpriteColor = borderSprite.color;
        mainSprite.TweenColor(blackenColor,blackenTime).Play();
        borderSprite.TweenColor(blackenColor ,blackenTime).Play();
        yield return new WaitForSeconds(blackenTime);
        mainSprite.TweenColor(mainSpriteColor,restoreTime).Play();
        borderSprite.TweenColor(borderSpriteColor,restoreTime).Play();
        yield return new WaitForSeconds(restoreTime);
        mainSprite.color = mainSpriteColor;
        borderSprite.color = borderSpriteColor;
        GameManager.Instance.ResumeGame();
    }
}