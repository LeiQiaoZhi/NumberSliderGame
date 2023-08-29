using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private double minSwipeDistance = 100;
    private PlayerMovement playerMovement_;
    private Vector2 startTouch_;
    private Vector2 swipeDelta_;
    private bool isSwiping_;

    private void Start()
    {
        playerMovement_ = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        MobileControls();
        KeyboardControls();
    }

    private void MobileControls()
    {
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isSwiping_ = true;
                startTouch_ = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                ResetSwipe();
            }
        }
        
        swipeDelta_ = Vector2.zero;
        if (isSwiping_)
        {
            if (Input.touches.Length != 0)
                swipeDelta_ = Input.touches[0].position - startTouch_;
        }
        
        if (swipeDelta_.magnitude > minSwipeDistance)
        {
            if (Mathf.Abs(swipeDelta_.x) > Mathf.Abs(swipeDelta_.y))
            {
                if (swipeDelta_.x > 0)
                    playerMovement_.Move(new Vector2Int(1, 0));
                else
                    playerMovement_.Move(new Vector2Int(-1, 0));
            }
            else
            {
                if (swipeDelta_.y > 0)
                    playerMovement_.Move(new Vector2Int(0, 1));
                else
                    playerMovement_.Move(new Vector2Int(0, -1));
            }
            ResetSwipe();
        }
        
    }

    private void ResetSwipe()
    {
        startTouch_ = Vector2.zero;
        swipeDelta_ = Vector2.zero;
        isSwiping_ = false;
    }

    private void KeyboardControls()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            playerMovement_.Move(new Vector2Int(0, 1));
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            playerMovement_.Move(new Vector2Int(0, -1));
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            playerMovement_.Move(new Vector2Int(-1, 0));
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            playerMovement_.Move(new Vector2Int(1, 0));
    }
}
