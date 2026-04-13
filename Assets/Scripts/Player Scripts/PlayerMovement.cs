using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 3f;

    private Rigidbody2D rb;

    private Vector2 moveVector;
    private float moveX;

    public Vector2 LastDirection { get; private set; }
    public bool IsMoving { get; private set; }

    private bool isHarvesting;
    private float harvestTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        LastDirection = Vector2.down;
    }

    private void Update()
    {
        if (Time.time > harvestTimer)
            isHarvesting = false;
    }

    private void FixedUpdate()
    {
        if (isHarvesting)
        {
            rb.linearVelocity = Vector2.zero;
            IsMoving = false;
            return;
        }



        rb.linearVelocity = moveVector * movementSpeed;

        IsMoving = moveVector != Vector2.zero;

        if (IsMoving)
            LastDirection = moveVector;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveVector = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moveVector = Vector2.zero;
        }
    }

    public void HarvestStopMovement(float time)
    {
        isHarvesting = true;
        harvestTimer = Time.time + time;
    }
}