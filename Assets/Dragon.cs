using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public DetectionZone detectionZone;

    Animator animator;
    Rigidbody2D rb2d;

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
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }



    // Update is called once per frame
    void Update()
    {
        HasTarget = detectionZone.detectedCollider.Count > 0;
    }
}
