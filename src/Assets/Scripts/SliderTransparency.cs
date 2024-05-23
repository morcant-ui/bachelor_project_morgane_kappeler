using MixedReality.Toolkit;
using MixedReality.Toolkit.UX;

using UnityEngine;
using UnityEngine.EventSystems;

public class SliderTransparency : MonoBehaviour//, IPointerUpHandler
{
    private Slider slider;
    public static float oldSliderValue { get; private set; }

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        //Sslider = GetComponent<Slider>();
        oldSliderValue = slider.Value;
        //Adds a listener to the main slider and invokes a method when the value changes.
        slider.OnValueUpdated.AddListener(delegate { ValueChangeCheck(); });
    }

    //public void OnPointerUp(PointerEventData pointerEventData)//JVEUX PAS UN POINTEUR MAIS DES MAINS
    //{
    //    if (slider.Value != oldSliderValue)
    //    {
    //        //print("Slider value is now " + slider.Value);
    //        oldSliderValue = slider.Value;
    //    }
    //}


    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        Debug.Log(slider.Value);
        oldSliderValue = slider.Value;
    }

    //public void OnValueChanged(SliderEventData pointerEventData)//JVEUX PAS UN POINTEUR MAIS DES MAINS
    //{
    //    if (slider.Value != oldSliderValue)
    //    {
    //        print("Slider value is now " + slider.Value);
    //        oldSliderValue = slider.Value;
    //    }
    //}

}
