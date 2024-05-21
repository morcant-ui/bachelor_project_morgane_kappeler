using Microsoft.MixedReality.GraphicsTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigmoidVisualization : MonoBehaviour
{
    #region SerializeFields
    [SerializeField]
    private Color originalColor;
    #endregion

    #region Private Fields
    private bool isWatched = false;
    private GameObject sphere;
    private Material outlineMaterial;

    private float fixationStartTime;
    private float fixationEndTime;
    private float currentIntensity = 0f;
    #endregion

    #region Public Fields
    // Parameters to control speed of intensity change
    public float increaseSpeed = 1.0f;
    public float decreaseSpeed = 0.5f;
    public float maxIntensity = 1.0f;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Renderer renderer = GetComponent<Renderer>();
        //outlineMaterial = renderer.material;
        outlineMaterial = GetComponent<MeshOutline>().OutlineMaterial;
        outlineMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (isWatched)
        {
            //outlineMaterial.color = originalColor;

            float fixationDuration = Time.time - fixationStartTime;
            float targetIntensity = CalculateSigmoidIntensity(fixationDuration);
            currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * increaseSpeed);
            Debug.Log("intensity should be rising:" + currentIntensity);
        }
        else
        {
            //outlineMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);
            float fixationDuration = Time.time - fixationEndTime;
            currentIntensity = Mathf.Lerp(currentIntensity, 0, Time.deltaTime * decreaseSpeed);
            Debug.Log("intensity should be decreasing:" + currentIntensity);
        }
        //outlineMaterial.SetFloat("_OutlineIntensity", currentIntensity);
        outlineMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, currentIntensity);
    }

    #region Private Methods
    private float CalculateSigmoidIntensity(float time)
    {
        // Sigmoid function: intensity = maxIntensity / (1 + exp(-k * (time - t0)))
        float k = 1.0f; // Adjust this value to change the curve steepness
        float t0 = 1.0f; // Adjust this value to shift the curve along the x-axis
        return maxIntensity / (1.0f + Mathf.Exp(-k * (time - t0)));
    }
    #endregion

    #region Public Methods
    public void Increment()
    {
        isWatched = true;
    }

    public void Decrement()
    {
        isWatched = false;
    }
    #endregion
}
