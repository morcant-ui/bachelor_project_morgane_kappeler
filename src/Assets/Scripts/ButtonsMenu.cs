using Photon.Pun;
using UnityEngine;

// script associated to the Menu gameobject
// it provides the methods called by each button of the menu when pressed
public class ButtonsMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject gameArea;

    [SerializeField]
    private GameObject SAButton;

    //[SerializeField]
    //GameObject demoSpheres;

    public void unfixAreaPosition()
    {
        gameArea.GetComponent<SpawnSphereInterface>().resetArea();
        SAButton.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void beginGame()
    {
        gameArea.GetComponent<SpawnSphereInterface>().startGame();
        this.gameObject.SetActive(false);
        
    }

    //public void startDemo()
    //{
    //    demoSpheres.GetComponent<PhotonView>().RPC("startDemo", RpcTarget.All);
    //    this.gameObject.SetActive(false);
    //}

}

