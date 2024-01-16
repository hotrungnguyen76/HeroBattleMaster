using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    Animator animator;
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive
    {
        get { return _isAlive; }
        set 
        { 
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);
        }
    }

    [SerializeField]
    private int _currentHealth = 100;

    [SerializeField]
    private bool isInvincible = false;

    public bool IsHit
    {
        get
        {
            return animator.GetBool(AnimationStrings.isHit);
        }
        private set
        {
            animator.SetBool(AnimationStrings.isHit, value);
        }
    }

    private float invincibleTimer = 0;

    [SerializeField]
    private float invincibilityTimer = 0.25f;

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            healthChanged?.Invoke(_currentHealth, MaxHealth);

            if(_currentHealth <= 0)
            {
                IsAlive = false;
            }
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            if (invincibleTimer > invincibilityTimer)
            {
                isInvincible = false;
                invincibleTimer = 0;
            }

            invincibleTimer += Time.deltaTime;
        }
       
    }

    public void Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        { 
            CurrentHealth -= damage;
            isInvincible = true;

            IsHit = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
        }
    }

    public void Heal(int health)
    {
        if (IsAlive)
        {
            if (CurrentHealth + health <= MaxHealth)
            {
                CurrentHealth += health;
            }
            else
            {
                CurrentHealth = MaxHealth;
            }
            
            CharacterEvents.characterHealed.Invoke(gameObject, health);
        }
    }
}
