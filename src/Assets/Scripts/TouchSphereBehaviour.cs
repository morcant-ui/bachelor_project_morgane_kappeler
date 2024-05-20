using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSphereBehaviour : MonoBehaviour
{
    private bool isTouched = false;
    //private GameObject sphere;
    private Color sphereOriginalColor;
    private Material sphereMaterial;
    private Color transparentColor;

    // Start is called before the first frame update
    void Start()
    {
        sphereMaterial = GetComponent<Renderer>().materials[0];
        sphereOriginalColor = sphereMaterial.color;
        transparentColor = new Color(sphereOriginalColor.r, sphereOriginalColor.g, sphereOriginalColor.b, 0.0f); // Adjust alpha to make it slightly transparent
        
        Debug.Log("Color orignal : " + sphereOriginalColor);
        Debug.Log("And transparant color:" + transparentColor);
        sphereMaterial.color = transparentColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouched)
        {
            sphereMaterial.color = sphereOriginalColor;
        }
        else
        {
            sphereMaterial.color = transparentColor;
        }
    }

    public void Increment()
    {
        isTouched = true;
    }

    public void Decrement()
    {
        isTouched = false;
    }
}
