using System;
using UnityEngine;

public class FlyingEnemy : EnemyBaseClass
{ 
    [Tooltip("The speed the enemy flies at'")]
    [SerializeField] private float flySpeed = 3f;
    [Tooltip("The max height of the enemy")]
    [SerializeField] private float maxHeight = 5f;
    [Tooltip("The minimum height of the enemy")]
    [SerializeField] private float minHeight = 1f;
    [SerializeField] private Rigidbody2D rb;
    private Boolean isGoingUp = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //this bool detects if the enemy is below the minimum height or above the maximum height, and changes the direction of the enemy accordingly
        Vector2 enemyPosition = new Vector2();
        Vector2 minPos = new Vector2(rb.position.x, minHeight);
        Vector2 maxPos = new Vector2(rb.position.x, maxHeight);
        Vector2 currentPos = new Vector2(rb.position.x, rb.position.y);
        if (currentPos.y <= minPos.y)
        {
            isGoingUp = true;
        } else if (currentPos.y >= maxPos.y)
        {
            isGoingUp = false;
        }
        //here is where the force is applied
        float direction = isGoingUp ? 1f : -1f;
        transform.position += new Vector3(0f, direction * flySpeed * Time.deltaTime, 0f);
        //Debug.Log(enemyPosition.y);
    }
    public override void TakeOver()
    {
        
    }
}