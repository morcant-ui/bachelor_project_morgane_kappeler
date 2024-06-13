using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBehavio : MonoBehaviour
{
    private bool isTouched = false;

    private Color sphereOriginalColor;
    private Color sphereTouchedColor;
    private Material sphereMaterial;
    private float amountToDecrease = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        sphereMaterial = GetComponent<Renderer>().material;
        sphereOriginalColor = sphereMaterial.color;
        sphereTouchedColor = new Color(sphereOriginalColor.r - amountToDecrease, sphereOriginalColor.g - amountToDecrease, sphereOriginalColor.b - amountToDecrease, 255f); // Adjust colors to make them a bit more dark

    }

    // Update is called once per frame
    void Update()
    {
        //float sliderValue = SliderTransparency.oldSliderValue;
        //sphereTouchedColor = new Color(sphereOriginalColor.r - sliderValue, sphereOriginalColor.g - sliderValue, sphereOriginalColor.b - sliderValue, 255f);
        if (isTouched)
        {
            sphereMaterial.color = sphereTouchedColor;
        }
        else
        {
            sphereMaterial.color = sphereOriginalColor;
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
