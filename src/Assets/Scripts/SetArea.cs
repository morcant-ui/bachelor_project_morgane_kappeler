using Photon.Pun;
using UnityEngine;

public class SetArea : MonoBehaviour
{

    [SerializeField]
    private GameObject gameArea;

    [SerializeField]
    private GameObject menu;

    public void Start()
    {
        
        if (!PhotonNetwork.IsMasterClient)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("CanvasLoading").SetActive(false);//NEW
        }
    }

    //makes the game area unmovable for all clients
    public void fixAreaPosition()
    {
        gameArea.GetComponent<SpawnSphereInterface>().setArea();
        //menu.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, this.transform.position.z); not needed?
        menu.SetActive(true);
        this.gameObject.SetActive(false);
    }

}