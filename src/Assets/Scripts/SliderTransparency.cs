using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderTransparency : MonoBehaviour, IPointerUpHandler
{
    Slider slider;
    public static float oldSliderValue { get; private set; }

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        oldSliderValue = slider.value;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (slider.value != oldSliderValue)
        {
            print("Slider value is now " + slider.value);
            oldSliderValue = slider.value;
        }
    }

}
