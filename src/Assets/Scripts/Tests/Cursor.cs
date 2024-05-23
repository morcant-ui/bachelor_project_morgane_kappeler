using MixedReality.Toolkit;
using MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.GraphicsBuffer;

public class Cursor : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private GazeInteractor gazeInteractorPointer;

    [SerializeField]
    private ActionBasedController gazeController;

    [SerializeField]
    private GameObject hitPointDisplayPrefab;

    [SerializeField]
    private float defaultDistanceInMeters = 2f;

    [SerializeField]
    private Color idleStateColor;

    [SerializeField]
    private InputActionProperty _gazeTranslationAction;
    #endregion

    #region Private Fields
    private GameObject hitPointDisplayer;

    private GameObject objectOfInterest;

    private bool isWatching = false;

    private Material material;

    private IGazeInteractor gazeInteractor;
    #endregion

    private void Awake()
    {
        material = GetComponent<Renderer>().material;

        gazeInteractor = gazeController.GetComponentInChildren<IGazeInteractor>();

    }

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
            var ray = new Ray(gazeInteractorPointer.rayOriginTransform.position, gazeInteractorPointer.rayOriginTransform.forward * 3);
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
