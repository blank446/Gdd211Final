using Unity.Mathematics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement variables")]
    [Tooltip("Pretty obvious but just in case, controls how fast the player moves")]
    [SerializeField] private float playerSpeed = 5f;
    [Tooltip("How high the player jumps")]
    [SerializeField] private float playerJump = 5f;
    [Header("Firing conditions")]
    [SerializeField] private GameObject Projectile;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private int direction;
    [Tooltip("How long the projectile lasts")]
    [SerializeField]private float lifetime = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.Normalize();
        //projectile shooting
        if (Input.GetKeyDown(KeyCode.F))
        {
            projectileFire();
        }
        //player jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * playerJump, ForceMode2D.Impulse);
            isGrounded = false;
        }
        //really scuffed, but it works, changes the direction of the projectile based on the direction the player is facing
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = 1;
        }
    }
    void FixedUpdate()
    {
        //player movement left and right
        rb.linearVelocity = new Vector2(movement.x * playerSpeed, rb.linearVelocity.y);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    void projectileFire()
    {
        Debug.Log("Shoot");
        // Instantiate the projectile at the player's position with no rotation
        GameObject Pewpew = Instantiate(Projectile, transform.position, quaternion.identity);
        if (direction > 0)
        {
            // Set the projectile's velocity to move right
            Pewpew.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(10f, 0f);
        } else
        {
            // Set the projectile's velocity to move left
            Pewpew.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-10f, 0f);
        }
        // Destroy the projectile after lifetime seconds
            Destroy(Pewpew, lifetime);
    }
}
