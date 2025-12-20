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

    [Header("Shoot Animations")]
    public Sprite[] shootDown;
    public Sprite[] shootLeft;
    public Sprite[] shootUp;
    public Sprite[] shootRight;

    public float frameTime = 0.15f;

    private float timer;
    private int frame;

    private SpriteRenderer sr;
    private PlayerMovement movement;

    private Direction4 currentDirection = Direction4.Down;
    private Direction4 lastDirection;
    private bool lastMoving;

    private bool isShooting;   // 🔹 REQUIRED STATE

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        movement = GetComponent<PlayerMovement>();
        lastDirection = currentDirection;
        lastMoving = false;
    }

    private void Update()
    {
        // Movement updates direction ONLY if not shooting
        if (!isShooting && movement.IsMoving)
            currentDirection = GetDirection(movement.LastDirection);

        // Reset animation on state change (ignore movement while shooting)
        if (!isShooting && (currentDirection != lastDirection || movement.IsMoving != lastMoving))
        {
            frame = 0;
            timer = 0f;
        }

        lastDirection = currentDirection;
        lastMoving = movement.IsMoving;

        Sprite[] anim = GetAnimationSet();
        if (anim == null || anim.Length == 0)
            return;

        if (frame >= anim.Length)
            frame = 0;

        if (Time.time >= timer)
        {
            sr.sprite = anim[frame];
            frame++;
            timer = Time.time + frameTime;

            // 🔹 Shooting animation ends automatically
            if (isShooting && frame >= anim.Length)
            {
                isShooting = false;
                frame = 0;
            }
        }
    }

    // 🔹 CALLED BY PlayerBow
    public void OnShoot(Vector2 shootDir)
    {
        currentDirection = GetDirection(shootDir);
        isShooting = true;
        frame = 0;
        timer = 0f;
    }

    Sprite[] GetAnimationSet()
    {
        if (isShooting)
        {
            return currentDirection switch
            {
                Direction4.Down => shootDown,
                Direction4.Left => shootLeft,
                Direction4.Up => shootUp,
                Direction4.Right => shootRight,
                _ => shootDown
            };
        }

        if (!movement.IsMoving)
        {
            return currentDirection switch
            {
                Direction4.Down => idleDown,
                Direction4.Left => idleLeft,
                Direction4.Up => idleUp,
                Direction4.Right => idleRight,
                _ => idleDown
            };
        }

        return currentDirection switch
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
