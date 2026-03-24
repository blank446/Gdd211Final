using UnityEngine;

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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.Normalize();
    }
    void FixedUpdate()
    {
        //player movement left and right
        rb.linearVelocity = new Vector2(movement.x * Playerspeed, rb.linearVelocity.y);
        //player jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jump");
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
