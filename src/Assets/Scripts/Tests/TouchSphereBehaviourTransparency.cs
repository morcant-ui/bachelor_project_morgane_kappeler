using UnityEngine;


public class TouchSphereBehaviour : MonoBehaviour
{
    private bool isTouched = false;
    private Color sphereOriginalColor;
    private Material sphereMaterial;
    private Color transparentColor;
    

    // Start is called before the first frame update
    void Start()
    {
        sphereMaterial = GetComponent<Renderer>().material;
        sphereOriginalColor = sphereMaterial.color;
        transparentColor = new Color(sphereOriginalColor.r, sphereOriginalColor.g, sphereOriginalColor.b, 0.3f); // Adjust alpha to make it slightly transparent
        //print("transparentColor origin" + transparentColor);

        //Debug.Log("Color orignal : " + sphereOriginalColor);
        //Debug.Log("And transparant color:" + transparentColor);
        sphereMaterial.color = transparentColor;
    }

    // Update is called once per frame
    void Update()
    {
        float sliderValue = SliderTransparency.oldSliderValue;
        transparentColor = new Color(sphereOriginalColor.r, sphereOriginalColor.g, sphereOriginalColor.b, sliderValue);
        //print("transparent color updated" + transparentColor);
        if (isTouched)
        {
            //Debug.Log("original color up!");
            sphereMaterial.color = sphereOriginalColor;
        }
        else
        {
            
            //Debug.Log("transparent color is stayingggggggg!");
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
