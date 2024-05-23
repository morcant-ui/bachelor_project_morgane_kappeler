using Photon.Pun;
using System.Collections;
using UnityEngine;

public class SelectTask : MonoBehaviour
{
    // Used to launch Task 1 or 2 for all the players (called by master client)
    public void T1Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            this.transform.GetChild(0).gameObject.SetActive(false); //the buttons to select
            //GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true); //loading mess of startscene -> No Need?
            StartCoroutine(wait1());
        }
    }
    public void T2Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            //GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true); //No need?
            StartCoroutine(wait2());
        }

    }

    // avoid pokePointerUp exception
    IEnumerator wait1()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.LoadLevel("Migration");
    }

    IEnumerator wait2()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.LoadLevel("Migration");
    }
}

