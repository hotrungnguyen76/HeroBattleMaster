using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 5f;

    public float WalkSpeed
    {
        get {
            if (touchingDirections.IsOnWall) {
                return 0;
            }
            else
            {
                return walkSpeed;
            }
        }
        set
        {
            walkSpeed = value;
        }
    } 

    public float jumpImpulse = 8f;

    Vector2 moveInput;

    private bool _isMoving = false;


    public bool isMoving
    {
        get => _isMoving;
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    Rigidbody2D rb2d;
    Animator animator;
    TouchingDirections touchingDirections;

    private bool _isFacingRight = true;
    

    public bool IsFacingRight
    {
        get => _isFacingRight;
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        
        rb2d.velocity = new Vector2(moveInput.x * WalkSpeed * Time.fixedDeltaTime, rb2d.velocity.y);
        
    }

    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    public void onJump(InputAction.CallbackContext context)
    {
        // To do: Check if alive 
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpImpulse);
        }
    }

    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }
    }



    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if( moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }

    }
}
