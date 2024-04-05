using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.Input;
using UnityEngine;

public class Pointer : MonoBehaviour
{

    [SerializeField]
    private GazeInteractor gazeInteractor;

    [SerializeField]
    private GameObject hitPointDisplayPrefab;

    private GameObject hitPointDisplayer;

 
    private GameObject objectOfInterest;

    private bool isWatching = false;

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
                        hitPointDisplayer.transform.position = hit.point;
                    }
                    else
                    {
                        // Instantiate the hit point displayer if it hasn't been instantiated yet
                        hitPointDisplayer = Instantiate(hitPointDisplayPrefab, hit.point, Quaternion.identity);
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
    }

    public void Decrement()
    {
        isWatching = false;
    }
}
