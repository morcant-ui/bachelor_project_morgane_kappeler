using Microsoft.MixedReality.GraphicsTools;
using Photon.Pun;
using UnityEngine;

public class T1Demo : Demo
{
    [SerializeField]
    GameObject crosshair;

    private int counter;

    private void Start()
    {
        counter = 0;
    }
    void Update()
    {   // if the demo was on and all children are disabled the demo ends
        //if (allChildrenOff() && demoOn)
        if(counter >= 6 && demoOn)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                menu.SetActive(true);
            }
            demoOn = false;

            foreach (Transform sphere in this.gameObject.transform)
            {
                if (SceneConfig.useVisualizations && PhotonNetwork.IsMasterClient)
                {
                    this.GetComponent<PhotonView>().RPC("removeVisus", RpcTarget.All);
                }

            }
        }

    }

    // starts the demo for all players
    [PunRPC]
    public override void startDemo()
    {
        demoOn = true;
        int i = 0;
        foreach (Transform sphere in this.gameObject.transform)
        {
            sphere.gameObject.GetComponent<MeshOutline>().OutlineWidth = 0.0f;
            if(i<3)
            {
                sphere.gameObject.SetActive(true);
            }
            i++;
            sphere.GetComponent<Pop>().myFlag = false;
            sphere.GetComponent<Pop>().otherFlag = false;
            sphere.GetComponent<Pop>().readyCount = 0;
            if (SceneConfig.useVisualizations && PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Yo, me should add the visus :)");
                this.GetComponent<PhotonView>().RPC("addVisus", RpcTarget.All);
            }

        }

    }

    [PunRPC]
    public void nextSphere()
    {
        if (demoOn && counter <= 2)
        {
            GameObject parentObject = this.gameObject;
            Transform child = parentObject.transform.GetChild(counter+3);
            child.gameObject.SetActive(true);
            
        }
        counter++;
        Debug.Log("me is counter and me is : " + counter);
    }

    [PunRPC]
    public void addVisus()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            foreach (Transform sphere in this.gameObject.transform)
            {
                sphere.gameObject.GetComponent<PointerDemo>().enabled = true;
                sphere.gameObject.GetComponent<GazeVisualizer>().enabled = true;
                crosshair.SetActive(true);

            }
        }
        
    }

    [PunRPC]
    public void removeVisus()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            foreach (Transform sphere in this.gameObject.transform)
            {
                //sphere.gameObject.GetComponent<Pointer>().enabled = false;
                sphere.gameObject.GetComponent<GazeVisualizer>().enabled = false;
                crosshair.SetActive(false);

            }
        }
        
    }

}