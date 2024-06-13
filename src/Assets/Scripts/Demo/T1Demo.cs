using Microsoft.MixedReality.GraphicsTools;
using Photon.Pun;
using UnityEngine;

public class T1Demo : Demo
{
    [SerializeField]
    GameObject crosshair;
    void Update()
    {   // if the demo was on and all children are disabled the demo ends
        if (allChildrenOff() && demoOn)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                menu.SetActive(true);
            }
            demoOn = false;

            foreach (Transform sphere in this.gameObject.transform)
            {
                if (SceneConfig.useVisualizations)
                {
                    Debug.Log("Yo, me should remove the visus :)");
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
        foreach (Transform sphere in this.gameObject.transform)
        {
            sphere.gameObject.GetComponent<MeshOutline>().OutlineWidth = 0.0f;

            sphere.gameObject.SetActive(true);
            sphere.GetComponent<Pop>().myFlag = false;
            sphere.GetComponent<Pop>().otherFlag = false;
            sphere.GetComponent<Pop>().readyCount = 0;
            if (SceneConfig.useVisualizations)
            {
                Debug.Log("Yo, me should add the visus :)");
                this.GetComponent<PhotonView>().RPC("addVisus", RpcTarget.All);
            }

        }
    }

    [PunRPC]
    public void addVisus()
    {
        foreach (Transform sphere in this.gameObject.transform)
        {
            sphere.gameObject.GetComponent<Pointer>().enabled = true;
            sphere.gameObject.GetComponent<GazeVisualizer>().enabled = true;
            crosshair.SetActive(true);

        }
    }

    [PunRPC]
    public void removeVisus()
    {
        foreach (Transform sphere in this.gameObject.transform)
        {
            //sphere.gameObject.GetComponent<Pointer>().enabled = false;
            sphere.gameObject.GetComponent<GazeVisualizer>().enabled = false;
            crosshair.SetActive(false);

        }
    }

}