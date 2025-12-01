using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnim : MonoBehaviour
{

    [SerializeField]
    private Sprite[] slashSprites;

    [SerializeField]
    private float timeTreshold = 0.06f;

    [SerializeField]
    private int damage = 35;

    private float timer;
    private int state = 0;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (Time.time > timer) {

            if (state == slashSprites.Length)
            {
                Destroy(gameObject);
                return;
            }
            else {
                sr.sprite = slashSprites[state];
                state++;

                timer = Time.time + timeTreshold;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Wolf")) {
            collision.GetComponent<WolfHealth>().TakeDamage(damage);
        }

    }

} // class


































