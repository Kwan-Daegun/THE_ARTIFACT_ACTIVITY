using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private GameObject player;

    private Vector3 tempPos;

    [SerializeField]
    private float min_X, max_X, min_Y, max_Y;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {

        tempPos = transform.position;

        tempPos.x = player.transform.position.x;
        tempPos.y = player.transform.position.y;

        if (tempPos.x > max_X)
            tempPos.x = max_X;

        if (tempPos.x < min_X)
            tempPos.x = min_X;

        if (tempPos.y > max_Y)
            tempPos.y = max_Y;

        if (tempPos.y < min_Y)
            tempPos.y = min_Y;

        transform.position = tempPos;

    }


} // class




























