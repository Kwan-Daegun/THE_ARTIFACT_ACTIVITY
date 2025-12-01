using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private Text timerText;

    public float timeToWin = 300f;

    private bool gameOver;

    private GameObject artifact;

    private StringBuilder stringBuilder;

    // Start is called before the first frame update
    void Awake()
    {
        artifact = GameObject.FindWithTag("Artifact");

        stringBuilder = new StringBuilder();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameOver || !artifact)
            return;

        timeToWin -= Time.deltaTime;

        if (timeToWin <= 0f)
        {

            timeToWin = 0f;
            gameOver = true;

            Destroy(artifact);

            // show win panel
            GameOverUIController.instance.GameOver("You WIN!");

        }

        //timerText.text = "Time Remaining: " + (int)timeToWin;

        DisplayTime((int)timeToWin);

    } // update

    void DisplayTime(int time) {

        //reset string builder
        stringBuilder.Length = 0;

        stringBuilder.Append("Time Remaining: ");
        stringBuilder.Append(time);

        timerText.text = stringBuilder.ToString();

    }


} // class



























