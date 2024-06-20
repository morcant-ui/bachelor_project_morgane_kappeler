using UnityEngine;
using Photon.Pun;
using Microsoft.MixedReality.GraphicsTools;

public class DemoPop : Pop
{
    [PunRPC]
    override
    public void dualTouch(float time, PhotonMessageInfo info)
    {
       
        touchTimes[info.Sender.ActorNumber - 1] = time;

        // the info.Sender is the player who called the RPC.
        if (info.Sender.IsLocal)
        {
            myFlag = true;
        }
        else
        {
            otherFlag = true;
        }

        // call to the readyToPop method when both flags true
        if (myFlag && otherFlag)
        {
            this.GetComponent<PhotonView>().RPC("readyToPop", RpcTarget.All, touchTimes[0], touchTimes[1], touchTimes[2]);
        }
    }

    // the sound of the popped sphere is displayed by all players, the master handles the deactivation of this sphere and produces a new one
    // by calling spherePoped. The outline is set to 0 and both flags are set back to false
    [PunRPC]
    override
    public void readyToPop(float time1, float time2, float time3)
    {   // this method is called by the two players touching the spheres
        // the sphere is popped when counter >= 2 only such that there are not two spheres created for each one popped (2 players send this RPC)
        readyCount++;

        if (readyCount >= 2)
        {
            this.transform.parent.transform.parent.GetComponent<AudioSource>().Play();
            if (PhotonNetwork.IsMasterClient)
            {
                this.GetComponent<PhotonView>().RPC("deActivateSphere", RpcTarget.All);
                this.transform.parent.GetComponent<T1Demo>().GetComponent<PhotonView>().RPC("nextSphere", RpcTarget.All);
            }
            this.GetComponent<MeshOutline>().OutlineWidth = 0f;
            this.GetComponent<PointerDemo>().enabled = false;
            this.GetComponent<PointerDemo>().hitPointDisplayer.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                touchTimes[i] = 0;
            }
            myFlag = false;
            otherFlag = false;
            readyCount = 0;
        }
    }

    [PunRPC]
    public void deActivateSphere()
    {
        this.gameObject.SetActive(false);
    }
}