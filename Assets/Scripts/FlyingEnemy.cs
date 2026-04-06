using UnityEngine;

public class FlyingEnemy : EnemyBaseClass
{ 
    [Tooltip("The gap between each 'flap''")]
    [SerializeField] private float flySpeed = 3f;
    [Tooltip("Force the enemy flies up with")]
    [SerializeField] private float flyForce = 1f;
    private float flyTimer;
    [SerializeField] private Rigidbody2D rb;

    private void FixedUpdate()
    {
        flyTimer -= 1;
        Debug.Log(flyTimer);
        //make the enemy fly up against gravity when flySpeed equals 0 with a burst then resets flySpeed
        if (flyTimer <= 0)
        {
            rb.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse);
            flyTimer = flySpeed;
            Debug.Log("Flap");
        }
    }
    public override void TakeOver()
    {
        
    }
}