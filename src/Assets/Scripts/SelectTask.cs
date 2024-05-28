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
            this.transform.GetChild(0).gameObject.SetActive(false); // the buttons to select
            SceneConfig.useVisualizations = false; // taks 1 without visualizations
            StartCoroutine(LoadScene());
        }
    }

    public void T2Start()
    {
       
        if (PhotonNetwork.IsMasterClient)
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            SceneConfig.useVisualizations = true; // task 2 with visualizations
            StartCoroutine(LoadScene());
        }
    }

    // Avoid pokePointerUp exception
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.LoadLevel("Migration");
    }
}
