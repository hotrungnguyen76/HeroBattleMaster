using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class KnightEnemyController : MonoBehaviour
{
    public float walkSpeed = 3f;
    Rigidbody2D rb2d;
    TouchingDirections touchingDirections;
    public DetectionZone attackZone;
    Animator animator;
    Damageable damageable;

    public enum WalkableDirection { Right, Left}

    private WalkableDirection _walkDirection = WalkableDirection.Right;
    private Vector2 walkDirectionVector = Vector2.right;

    public bool CanMove
    {
        get => animator.GetBool(AnimationStrings.canMove);
    }
    
    public WalkableDirection WalkDirection
    {
        get => _walkDirection;
        set
        {
            if (_walkDirection != value)
            {
                Vector3 newScale = gameObject.transform.localScale;
                newScale.x *= -1;
                gameObject.transform.localScale = newScale;

                //gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
            }

            if (value == WalkableDirection.Right) {
                walkDirectionVector = Vector2.right;
            }
            else if (value == WalkableDirection.Left)
            {
                walkDirectionVector = Vector2.left;
            }
            _walkDirection = value;
        }
    }

    private bool _hasTarget = false;
    public bool HasTarget
    {
        get => _hasTarget;
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        HasTarget = attackZone.detectedCollider.Count > 0;
    }

    void FixedUpdate()
    {
        if (touchingDirections.IsOnWall && touchingDirections.IsGrounded)
        {
            FlipDirection();
        }

        if (!damageable.IsHit)
        {
            if (CanMove)
            {
                rb2d.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
        
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right )
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left )
        {
            WalkDirection = WalkableDirection.Right;
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            rb2d.velocity = new Vector2(-1 * knockback.x, rb2d.velocity.y + knockback.y);
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            rb2d.velocity = new Vector2(knockback.x, rb2d.velocity.y + knockback.y);
        }
    }

   
}
