using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeVisualizer : MonoBehaviour
{
    private bool isWatched = false;
    private GameObject sphere;
    private Material outlineMaterial;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        sphere = gameObject;
        outlineMaterial = GetComponent<Renderer>().materials[1];
        originalColor = outlineMaterial.color;
        //Debug.Log("Color orignal : " + originalColor);
        outlineMaterial.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWatched)
        {
            outlineMaterial.color = originalColor;
        }
        else
        {
            outlineMaterial.color = new Color(0, 0, 0, 0);
        }
    }

    public void Increment()
    {
        isWatched = true;
    }

    public void Decrement()
    {
        isWatched = false;
    }
}
