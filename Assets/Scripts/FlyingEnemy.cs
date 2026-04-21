using System;
using UnityEngine;

public class FlyingEnemy : EnemyBaseClass
{
    [Header("Flying Enemy Stuff")]
    [Tooltip("The speed the enemy flies at'")]
    [SerializeField] private float flySpeed = 3f;
    [Tooltip("The max height of the enemy")]
    [SerializeField] private float maxHeight = 5f;
    [Tooltip("The minimum height of the enemy")]
    [SerializeField] private float minHeight = 1f;
    [Tooltip("Make sure to put a rigidBody on this object and assign it here")]
    [SerializeField] private Rigidbody2D rb;
    private Boolean isGoingUp = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //this bool detects if the enemy is below the minimum height or above the maximum height, and changes the direction of the enemy accordingly
        Vector2 minPos = new Vector2(rb.position.x, minHeight);
        Vector2 maxPos = new Vector2(rb.position.x, maxHeight);
        Vector2 currentPos = new Vector2(rb.position.x, rb.position.y);
        if (currentPos.y <= minPos.y && possessed == false)
        {
            isGoingUp = true;
        }
        else if (currentPos.y >= maxPos.y && possessed == false)
        {
            isGoingUp = false;
        }
        //here is where the force is applied
        if (!possessed)
        {
            float direction = isGoingUp ? 1f : -1f;
            rb.linearVelocity = new Vector2(0f, direction * flySpeed);
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, 0f);
            TakeOver();
            controlTimer -= Time.deltaTime;
            if (controlTimer <= 0f)
            {
                possessed = false;
                controlTimer = controlTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(controlTimer + " " + possessed);
        }
    }
    public override void TakeOver()
    {
        //give the player control of the enemy y movement
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.W))
        {
            rb.linearVelocity = new Vector2(0f, flySpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = new Vector2(0f, -flySpeed);
        }
    }
}