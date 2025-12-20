using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore player
        if (other.CompareTag("Player"))
            return;

        // Apply damage to Wolf
        WolfHealth wolf = other.GetComponent<WolfHealth>();
        if (wolf != null)
        {
            wolf.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
