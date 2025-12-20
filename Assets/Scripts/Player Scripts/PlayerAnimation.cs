using UnityEngine;

public enum Direction4
{
    Down,
    Left,
    Up,
    Right
}

public class PlayerAnimation : MonoBehaviour
{
    [Header("Idle Animations")]
    public Sprite[] idleDown;
    public Sprite[] idleLeft;
    public Sprite[] idleUp;
    public Sprite[] idleRight;

    [Header("Walk Animations")]
    public Sprite[] walkDown;
    public Sprite[] walkLeft;
    public Sprite[] walkUp;
    public Sprite[] walkRight;

    public float frameTime = 0.15f;

    private float timer;
    private int frame;

    private SpriteRenderer sr;
    private PlayerMovement movement;

    private Direction4 currentDirection = Direction4.Down;
    private Direction4 lastDirection;
    private bool lastMoving; // FIX: track state change

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
        lastDirection = currentDirection;
        lastMoving = false;
    }

    private void Update()
    {
        if (movement.IsMoving)
            currentDirection = GetDirection(movement.LastDirection);

        // FIX: reset animation when direction OR state changes
        if (currentDirection != lastDirection || movement.IsMoving != lastMoving)
        {
            frame = 0;
            timer = 0f;
            lastDirection = currentDirection;
            lastMoving = movement.IsMoving;
        }

        Sprite[] anim = GetAnimationSet(movement.IsMoving, currentDirection);
        if (anim == null || anim.Length == 0)
            return;

        // FIX: clamp frame to prevent IndexOutOfRange
        if (frame >= anim.Length)
            frame = 0;

        if (Time.time >= timer)
        {
            sr.sprite = anim[frame];
            frame++;
            timer = Time.time + frameTime;
        }
    }

    Sprite[] GetAnimationSet(bool moving, Direction4 dir)
    {
        if (!moving)
        {
            return dir switch
            {
                Direction4.Down => idleDown,
                Direction4.Left => idleLeft,
                Direction4.Up => idleUp,
                Direction4.Right => idleRight,
                _ => idleDown
            };
        }

        return dir switch
        {
            Direction4.Down => walkDown,
            Direction4.Left => walkLeft,
            Direction4.Up => walkUp,
            Direction4.Right => walkRight,
            _ => walkDown
        };
    }

    Direction4 GetDirection(Vector2 dir)
    {
        if (dir == Vector2.zero)
            return currentDirection;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            return dir.x > 0 ? Direction4.Right : Direction4.Left;
        else
            return dir.y > 0 ? Direction4.Up : Direction4.Down;
    }
}
