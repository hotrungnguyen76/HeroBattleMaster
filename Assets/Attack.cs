using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        Damageable damageable = collision.GetComponent<Damageable>();   
        if( damageable != null)
        {
            if (damageable.IsAlive)
            {
                damageable.Hit(attackDamage, knockback);
            }
            
            
        }
    }
}
