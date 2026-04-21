using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBaseClass : MonoBehaviour
{
    [Header("Stuff from the base class")]
    [Tooltip("How much damage the enemy deals on contact")]
    [SerializeField]protected float damage;
    protected Boolean possessed = false;
    [Tooltip("How long the enemy is possessed")]
    [SerializeField] protected float controlTime = 5f;
    protected float controlTimer;
    
    protected void Start()
    {
        controlTimer = controlTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            possessed = true;
            TakeOver();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            //Damage the player
            Debug.Log("Hit");
            collision.gameObject.GetComponent<Movement>().PlayerHealth -= damage;
        }
    }
    public virtual void TakeOver()
    {
        Debug.Log("Put new code here");
    }
}
