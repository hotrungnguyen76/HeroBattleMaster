using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnightEnemyController : MonoBehaviour
{
    public float walkSpeed = 3f;
    Rigidbody2D rb2d;
    TouchingDirections touchingDirections;
    public DetectionZone attackZone;
    Animator animator;
    public enum WalkableDirection { Right, Left}

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.left;
    
    public WalkableDirection WalkDirection
    {
        get => _walkDirection;
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
            }

            if (value == WalkableDirection.Right) {
                walkDirectionVector = Vector2.left;
            }
            else if (value == WalkableDirection.Left)
            {
                walkDirectionVector = Vector2.right;
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
        rb2d.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb2d.velocity.y);
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
    }

   
}
