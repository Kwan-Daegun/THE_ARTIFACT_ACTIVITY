using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 moveVector;

    public Vector2 LastDirection { get; private set; }
    public bool IsMoving { get; private set; }

    private bool isHarvesting;
    private float harvestTimer;

    private const string X = "Horizontal";
    private const string Y = "Vertical";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        LastDirection = Vector2.down; // default facing
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
            rb.velocity = Vector2.zero;
            IsMoving = false;
            return;
        }

        moveVector = new Vector2(Input.GetAxisRaw(X), Input.GetAxisRaw(Y));

        if (moveVector.sqrMagnitude > 1)
            moveVector.Normalize();

        rb.velocity = moveVector * movementSpeed;

        IsMoving = moveVector != Vector2.zero;

        if (IsMoving)
            LastDirection = moveVector;
    }

    public void HarvestStopMovement(float time)
    {
        isHarvesting = true;
        harvestTimer = Time.time + time;
    }
}
