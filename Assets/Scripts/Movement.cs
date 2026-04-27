using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Movement : MonoBehaviour
{
    [Header("Movement variables")]
    [Tooltip("Pretty obvious but just in case, controls how fast the player moves")]
    [SerializeField] private float Playerspeed = 5f;
    [Tooltip("How high the player jumps")]
    [SerializeField] private float Playerjump = 5f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    [Tooltip("The player's health")]
    public float PlayerHealth = 10f;
    [Tooltip("Projectile prefab")]
    [SerializeField] private GameObject projectilePrefab;
    private bool facingRight = true;
    [Header("UI variables")]
    [Tooltip("UI element to display the player's health")]
    [SerializeField] private TMP_Text health_text;
    [SerializeField] private float projectileLife = 5f;
    private float projectileLifetime;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (health_text != null)
        {
            health_text.text = ("Health: ") + PlayerHealth.ToString();
        }
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
            projectileLifetime = projectileLife;
            Collider2D projectileCol = projectile.GetComponent<Collider2D>();
            Collider2D playerCol = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(projectileCol, playerCol);

            if (facingRight)
            {
                projectileRb.linearVelocity = new Vector2(10f, 0f);
            }
            else if (!facingRight)
            {
                projectileRb.linearVelocity = new Vector2(-10f, 0f);
            }
            if (projectileLifetime <= 0f)
            {
                Destroy(projectile);
            }
        }
        projectileLifetime -= Time.deltaTime;
        if (health_text != null)
        {
            health_text.text = PlayerHealth.ToString();
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
        if (collision.gameObject.CompareTag("Goal"))
        {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
