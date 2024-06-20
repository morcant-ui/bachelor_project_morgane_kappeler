using Photon.Pun;
using System.Collections;
using UnityEngine;

public class BackToStart : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject gameArea;
    [SerializeField]
    GameObject SAButton;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject endPannel;
    [SerializeField]
    GameObject restartButton;
    [SerializeField]
    GameObject menu;
    [SerializeField]
    GameObject backToStartButton;

    // the backToStart button is enabled for the master client only
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // when backToStart button is pressed, the level 0 is loaded for all players
    public void backToStart()
    {
        this.GetComponent<PhotonView>().RPC("DeleteAll", RpcTarget.All);
        //disable idlecursor at the end 
        if (SceneConfig.useVisualizations)
        {
            for (int i = 0; i < gameArea.transform.childCount; i++)
            {
                Transform child = gameArea.transform.GetChild(i);
                if (child.name == "demoSpheres" || child.name == "CrosshairDemo")
                {
                    Debug.Log("Doing nothing");
                }
                else
                {
                    child.GetComponent<Pop>().GetComponent<PhotonView>().RPC("destroyIdleCursors", RpcTarget.All);

                }
            }
        }
        //GameObject.Find("CanvasLoading").transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(wait());
    }

    // avoid pokePointerUp exception
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        PhotonNetwork.LoadLevel(0);
    }

    [PunRPC]
    // to avoid players touching an object just before scene reload 
    public void DeleteAll()
    {
        gameArea.SetActive(false);
        SAButton.SetActive(false);
        canvas.SetActive(false);
        endPannel.SetActive(false);
        restartButton.SetActive(false);
        menu.SetActive(false);
        backToStartButton.SetActive(false);

    }
}
