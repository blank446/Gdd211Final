using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] private Animator ani;
    [Header("Movement variables")]
    [Tooltip("Pretty obvious but just in case, controls how fast the player moves")]
    [SerializeField] private float Playerspeed = 5f;
    [Tooltip("How high the player jumps")]
    [SerializeField] private float Playerjump = 5f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    [Tooltip("The player's health")]
    [SerializeField] public float PlayerHealth = 10f;
    [Tooltip("Projectile prefab")]
    [SerializeField] private GameObject projectilePrefab;
    private bool facingRight = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            facingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            facingRight = true;
        }
        movement.Normalize();
        if (PlayerHealth <= 0)
        {
          // Handle player death
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //instantiate a projectile when the player presses f in the direction they're facing
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            Collider2D projectileCol = projectile.GetComponent<Collider2D>();
            Collider2D playerCol = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(projectileCol, playerCol);
            if (facingRight)
            {
                projectileRb.linearVelocity = new Vector2(10f, 0f);
            }
            else if (facingRight == false)
            {
                projectileRb.linearVelocity = new Vector2(-10f, 0f);
            }
        }
    }
    void FixedUpdate()
    {
        //player movement left and right
        rb.linearVelocity = new Vector2(movement.x * Playerspeed, rb.linearVelocity.y);
        //player jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * Playerjump, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
