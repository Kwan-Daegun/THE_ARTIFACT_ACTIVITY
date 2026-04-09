using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage = 10;
    [SerializeField] private float lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;

        WolfHealth wolf = other.GetComponent<WolfHealth>();
        if (wolf != null)
        {
            wolf.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}