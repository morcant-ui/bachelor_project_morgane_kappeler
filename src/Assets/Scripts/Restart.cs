using holoutils;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{

    [SerializeField]
    GameObject SAButton;

    [SerializeField]
    GameObject gameArea;

    public void Start()
    {
        // restart button has to be enabled at beggining for all players to get a valid PhotonViewId
        // it is then deactivated
        this.gameObject.SetActive(false);
    }

    public void RestartGame()
    {   // writes "Game restarted" to the data variable of CreateSpheres such that it will be written in the CSV
        List<string> separator = new List<string>
        {
            "Game restarted"
        };
        GameObject.Find("Timer").GetComponent<CSVLogger>().AddRow(separator);

        // reinitilizes still active spheres
        SpawnSphereInterface spheresScript = gameArea.GetComponent<SpawnSphereInterface>();
        foreach (GameObject sphere in spheresScript.activeSpheres)
        {
            gameArea.GetComponent<PhotonView>().RPC("deActivateSphere", RpcTarget.All, sphere.GetComponent<Pop>().id);
        }
        spheresScript.activeSpheres = new List<GameObject>();

        // resets the area, and de/activate some objects
        spheresScript.resetArea();
        this.GetComponent<PhotonView>().RPC("RestartAll", RpcTarget.All);

        SAButton.SetActive(true);
        this.gameObject.SetActive(false);

    }

    // reinitilizes some elements on all clients
    [PunRPC]
    public void RestartAll()
    {
        gameArea.SetActive(true);
        gameArea.GetComponent<SpawnSphereInterface>().popCounter = 0;

        GameObject.Find("PopText").GetComponent<TMPro.TextMeshProUGUI>().text = "Counter";
        GameObject.Find("TimeText").GetComponent<TMPro.TextMeshProUGUI>().text = "Time";

        GameObject.Find("EndPannel").SetActive(false);

        Timer t = GameObject.Find("Timer").GetComponent<Timer>();
        t.timeRemaining = t.totalTime;

    }

}
