using Microsoft.MixedReality.GraphicsTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopSphere : MonoBehaviour
{
    private bool isTouched = false;
    private GameObject sphere;


    // Start is called before the first frame update
    void Start()
    {
        sphere = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (isTouched)
        {
            sphere.GetComponent<MeshOutline>().OutlineWidth = 0.005f;

            StartCoroutine(DestroySphere());
        }
        else
        {
            sphere.GetComponent<MeshOutline>().OutlineWidth = 0f;
        }

    }

    public void Increment()
    {
        isTouched = true;
    }

    private IEnumerator DestroySphere()
    {

        yield return new WaitForSeconds(1f);

        Destroy(sphere);
    }


    public void Decrement()
    {
        isTouched = false;
    }
}

