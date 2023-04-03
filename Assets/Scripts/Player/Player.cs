using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    [Header("Movement:")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    private PlayerMovement PlayerMovement;

    private void Awake()
    {
        RetrieveComponents();
    }

    private void FixedUpdate()
    {
        PlayerMovement.CheckMovementInput(movementSpeed, rotationSpeed);
    }

    // Retrieves required component references
    private void RetrieveComponents()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
    }
}
