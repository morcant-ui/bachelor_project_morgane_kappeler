using MixedReality.Toolkit.Input;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pointer : MonoBehaviour
{

    //[SerializeField]
    public GazeInteractor gazeInteractor;

    [SerializeField]
    private GameObject hitPointDisplayPrefab;

    public GameObject hitPointDisplayer;

 
    private GameObject objectOfInterest;

    private bool isWatching = false;


    private const float hitPointOffset = 0.01f; //NEW

    private Stack<float> gazeTimeINStack = new Stack<float>();
    private float totalGazeTime = 0f;

    private int id;
    public void setIndex(int position) { id = position; }


    // Start is called before the first frame update
    void Start()
    {
        objectOfInterest = gameObject;
        hitPointDisplayer = Instantiate(hitPointDisplayPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWatching)
        {
            var ray = new Ray(gazeInteractor.rayOriginTransform.position, gazeInteractor.rayOriginTransform.forward * 3);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject == objectOfInterest)
                {
                    if (hitPointDisplayer != null)
                    {
                        hitPointDisplayer.SetActive(true); // Activate the pointer if it's not already active

                        // Adjust the position slightly in front of the hit point to avoid penetration
                        Vector3 offsetPosition = hit.point + hit.normal * hitPointOffset; //NEW
                        hitPointDisplayer.transform.position = offsetPosition; //NEW

                        // Make the crosshair face the camera
                        hitPointDisplayer.transform.LookAt(Camera.main.transform); //NEW
                        hitPointDisplayer.transform.Rotate(0, 180, 0); // Inverse to face the camera properly NEW

                        //hitPointDisplayer.transform.position = hit.point; OLD THING WAS NOT COMMENTED!
                    }
                    else
                    {

                        // Adjust the position slightly in front of the hit point to avoid penetration
                        Vector3 offsetPosition = hit.point + hit.normal * hitPointOffset; //NEW

                        // Instantiate the hit point displayer if it hasn't been instantiated yet
                        hitPointDisplayer = Instantiate(hitPointDisplayPrefab, hit.point, Quaternion.identity);

                        hitPointDisplayer.transform.position = offsetPosition; //NEW
                    }
                }
                else
                {
                    // If the object of interest is not hit, deactivate the pointer
                    if (hitPointDisplayer != null)
                    {
                        hitPointDisplayer.SetActive(false);
                    }
                }
            }
        }
        else
        {
            // If not watching, deactivate the pointer
            if (hitPointDisplayer != null)
            {
                hitPointDisplayer.SetActive(false);
            }
        }
    }

    public void Increment()
    {
        isWatching = true;
        gazeTimeINStack.Push(GameObject.Find("Timer").GetComponent<Timer>().timeRemaining);

    }

    public void Decrement()
    {
        if (isWatching && gazeTimeINStack.Count > 0)
        {
            float gazeTimeIN = gazeTimeINStack.Pop();
            float gazeTimeOUT = GameObject.Find("Timer").GetComponent<Timer>().timeRemaining; 
            totalGazeTime = Math.Abs(gazeTimeIN - gazeTimeOUT);
            this.transform.parent.GetComponent<SpawnSphere>().GetComponent<PhotonView>().RPC("gazeTimeUpdate", RpcTarget.All,totalGazeTime, gazeTimeIN, id);
        }
        isWatching = false;
    }
}
