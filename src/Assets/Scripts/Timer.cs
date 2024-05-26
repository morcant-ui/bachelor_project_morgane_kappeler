using UnityEngine;
using Photon.Pun;
using holoutils;
using System;
using System.Collections;

public class Timer : MonoBehaviour
{

    [SerializeField]
    GameObject gameArea;

    [SerializeField]
    GameObject endPannel;

    [SerializeField]
    public float timeRemaining;

    [SerializeField]
    public GameObject restart;

    private bool timerIsRunning = false;

    [NonSerialized]
    // for the end display
    public float totalTime;

    // for the time display
    private TMPro.TextMeshProUGUI timeText;

    // for saving the data in a CSV file
    private CSVLogger logger;


    private void Start()
    {
        // time for the game
        totalTime = timeRemaining;

        // gets the text object for displaying the time left and displays the time
        timeText = GameObject.Find("TimeText").GetComponent<TMPro.TextMeshProUGUI>();
        this.DisplayTime(timeRemaining);
        Debug.Log("Hello ME is timer, I started the time");

        // stores the logger component in a variable for later usage
        logger = this.GetComponent<CSVLogger>();

    }


    // called on all clients when start game button is pressed
    [PunRPC]
    public void startTimer()
    {
        timerIsRunning = true;
    }


    void Update()
    {
        if (timerIsRunning)
        {   // displays the time minus the delay since the last update
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining < 0) { timeRemaining = 0; }
                this.DisplayTime(timeRemaining);
                //Debug.Log("Hello, me is timer again, you still have " + timeRemaining);
            }
            // when the time is over -> fix remaining time to 0 and display it, set the timeIsRunning to false to stop the updates
            else
            {
                this.DisplayTime(timeRemaining);

                timerIsRunning = false;

                // diables the area, gets the number of popped spheres
                gameArea.SetActive(false);

                // sets the position of the end display to the previous position of the area
                endPannel.transform.position = gameArea.transform.position;

                // to wait for last bubble to be popped properly
                StartCoroutine(waiter());
            }
        }
    }

    // displays the time on the display in minutes, seconds and hundredth
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float centiSeconds = (timeToDisplay % 1) * 100;
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiSeconds);
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);

        // diplays end text and set the endDisplay active
        int popCounter = gameArea.GetComponent<SpawnSphereInterface>().popCounter;
        endPannel.transform.GetChild(1).transform.Find("Text").GetComponent<TMPro.TextMeshProUGUI>().text = "Congratulations!\n\nGiven time: " + totalTime / 60 + " minutes\n\nScore: " + popCounter + " spheres popped\n";
        endPannel.SetActive(true);
        this.GetComponent<AudioSource>().Play();

        // the master also stores the data in a CSV file on the headset (under Documents, CSVLogger)
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master makes a csv");
            // saves data
            logger.FlushData();
            // clears data variable used to store data in the Create Spheres script
            gameArea.GetComponent<SpawnSphereInterface>().data.Clear();
            // if we want to restart game
            restart.SetActive(true);
        }
    }

}

