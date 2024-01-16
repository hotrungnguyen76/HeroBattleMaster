using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    Rigidbody2D rb2d;
    Animator animator;
    TouchingDirections touchingDirections;
    Damageable damageable;

    public float WalkSpeed
    {
        get {
            if (CanMove)
            {
                if (touchingDirections.IsOnWall)
                {
                    return 0;
                }
                else
                {
                    return walkSpeed;
                }
            }
            else
            {
                return 0;
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

    public bool IsAlive
    {
        get => animator.GetBool(AnimationStrings.isAlive);
    }

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

    public bool CanMove
    {
        get => animator.GetBool(AnimationStrings.canMove);
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (!damageable.IsHit) rb2d.velocity = new Vector2(moveInput.x * WalkSpeed * Time.fixedDeltaTime, rb2d.velocity.y);
        
    }

    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            isMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            isMoving = false;
        }
        
    }

    public void onJump(InputAction.CallbackContext context)
    {
        // To do: Check if alive 
        if (context.started && touchingDirections.IsGrounded && CanMove)
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

    public void OnHit(int damage, Vector2 knockback)
    {
        if (IsFacingRight)
        {
            rb2d.velocity = new Vector2(-1 *knockback.x, rb2d.velocity.y + knockback.y);
        }
        else
        {
            rb2d.velocity = new Vector2( knockback.x, rb2d.velocity.y + knockback.y);
        }
    }
}
