using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private bool isWatching = false;

    [SerializeField]
    private Color activatedColor = Color.cyan;

    private Material material;

    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWatching)
        {
            material.color = activatedColor;
        } else
        {
            material.color = originalColor;
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
