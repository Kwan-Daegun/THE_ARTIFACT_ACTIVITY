using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushFruits : MonoBehaviour
{
    [SerializeField]
    private int[] amountPerType;

    [SerializeField]
    private float[] respawnTime;

    private BushVisual bushVisual;

    private bool hasFruits;
    private float timer;

    private void Start()
    {

        bushVisual = GetComponent<BushVisual>();

        // randomly initialize some bushes and fruits
        if (Random.Range(0, 2) == 0)
        {
            hasFruits = false;
            timer = Time.time + respawnTime[(int)bushVisual.GetBushVariant()];
        }
        else
        {
            hasFruits = true;
            bushVisual.ShowFruits();
        }

    }

    private void Update()
    {
        if (Time.time > timer) {
            hasFruits = true;
            bushVisual.ShowFruits();
        }
    }

    public int HarvestFruit() {

        if (hasFruits)
        {
            hasFruits = false;
            bushVisual.HideFruits();
            timer = Time.time + respawnTime[(int)bushVisual.GetBushVariant()];
            return amountPerType[(int)bushVisual.GetBushVariant()];
        }
        else
            return 0;

    }

    public bool HasFruits() {
        return hasFruits;
    }

    // when the enemy attacks the bush and eats it
    public void EatBushFruits() {
        enabled = false;
        bushVisual.SetToDry();
    }

} // class


























