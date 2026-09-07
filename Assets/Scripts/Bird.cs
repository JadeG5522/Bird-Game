using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Bird : MonoBehaviour
{
    private Rigidbody rb; 
    private float movementX;
    private float movementY;
    private int points = 0;

    private Animator animator;

    public int Points => points;

    public float speed = 0; 
    public float jumpForce = 5;
    public ScoreDisplay scoreDisplay;

    public static bool gameStarted = false;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip pointSound;

    void Start()
    {
        Time.timeScale = 1f;
        animator = GetComponent<Animator>();
        rb = GetComponent <Rigidbody>(); 
        rb.useGravity = false;
        animator.SetBool("flying", true);
    }

   void FixedUpdate() 
   {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY) * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement; 

        Camera mainCamera = Camera.main;

        // Keep bird within camera viewpoint
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(newPosition);
        
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0.03f, 0.97f);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, 0.05f, 0.95f);

        transform.position = 
            mainCamera.ViewportToWorldPoint(viewportPosition);
   }

    void OnJump()
    {
        if (gameStarted != true)
        {
            gameStarted = true;
        }

        if (rb.useGravity != true)
        {
            rb.useGravity = true;
        }

        audioSource.PlayOneShot(audioSource.clip);

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    }

    void OnMove(InputValue movementValue)
    {
        if (gameStarted != true)
        {
            gameStarted = true;
        }
        if (rb.useGravity != true)
        {
            rb.useGravity = true;
        }

        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movementX = movementVector.x; 
        movementY = movementVector.y; 


    } 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PointZone"))
        {
            Debug.Log("+1 Point");
            points += 1;
            audioSource.PlayOneShot(pointSound);
        }
        if (other.CompareTag("DeathWall"))
        {
            EndGame();
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        if (!gameStarted)
        {
            return;
        }

        Debug.Log("Game Over!");
        audioSource.PlayOneShot(deathSound);
        animator.SetTrigger("die");
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        gameStarted = false;
        Time.timeScale = 0f;
        scoreDisplay.ShowGameOver();
    }
}
