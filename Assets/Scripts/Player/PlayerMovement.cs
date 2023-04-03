using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private DynamicJoystick joystick;

    private Rigidbody playerRB;
    private Animator playerAnim;

    private void Awake()
    {
        RetrieveComponents();
    }

    // Check movement input
    public void CheckMovementInput(float movementSpeed, float rotationSpeed)
    {
        #if UNITY_ANDROID
        // Android input
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        #else
        // PC input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        #endif

        if (horizontalInput != 0 || verticalInput != 0)
        {
            playerAnim.SetBool("isWalking", true);
            Move(horizontalInput, verticalInput, movementSpeed, rotationSpeed);
        }
        else
        {
            playerAnim.SetBool("isWalking", false);
        }
    }

    // Perform movement
    private void Move(float horizontal, float vertical, float speed, float rotationSpeed)
    {
        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection.Normalize();

        Quaternion rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        playerRB.velocity = movementDirection * speed;
    }

    // Retrieve required components
    private void RetrieveComponents()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
}
