using Microsoft.MixedReality.GraphicsTools;
using Photon.Pun;
using System.Linq;
using UnityEngine;

public class GazeVisualizer : MonoBehaviour
{
    #region SerializeFields
    [SerializeField]
    public Color color;

    [SerializeField]
    Material outlineMaterial;
    #endregion

    #region Private Fields
    private bool watching = false;
    private GameObject sphere;
    private Material material;

    private float fixationStart;
    private float fixationEnd;
    private float currIntensity = 0f;

    private Material outlineMaterialReal;

    private int id;
    #endregion

    #region Public Fields
    // Parameters to control speed of intensity change
    private float increase = 1.0f;
    private float decrease = 1.2f;
    private float intensity = 1.0f;

    
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        increase = 1.0f;
        decrease = 1.2f;
        intensity = 1.0f;   

        gameObject.GetComponent<MeshOutline>().OutlineWidth = 0.01f;
        gameObject.GetComponent<MeshOutline>().OutlineMaterial = outlineMaterial;
        outlineMaterialReal = GetComponent<MeshRenderer>().materials.Last<Material>();
        outlineMaterialReal.color = new Color(color.r, color.g, color.b, 0.0f);
        //Debug.Log("Me is sigmoid, I should start with" + outlineMaterial.name + "and color: " + outlineMaterial.color + "on " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (watching)
        {
            float fixationDuration = GameObject.Find("Timer").GetComponent<Timer>().timeRemaining - fixationStart;
            float targetIntensity = CalculateSigmoid(fixationDuration);
            currIntensity = Mathf.Lerp(currIntensity, targetIntensity, Time.deltaTime * increase);
            //Debug.Log("Im updating with intensity : " + currIntensity);
        }
        else
        {
            float fixationDuration = GameObject.Find("Timer").GetComponent<Timer>().timeRemaining - fixationEnd;
            currIntensity = Mathf.Lerp(currIntensity, 0, Time.deltaTime * decrease);
        }
        //outlineMaterial.color = new Color(color.r, color.g, color.b, currIntensity); //own's gaz
        this.GetComponent<PhotonView>().RPC("updateOutline", RpcTarget.Others, currIntensity); //partner's gaze

    }

    #region Private Methods
    private float CalculateSigmoid(float time)
    {
        // Sigmoid function: intensity = maxIntensity / (1 + exp(-k * (time - t0)))
        float k = 1.0f; // Adjust this value to change the curve steepness
        float t0 = 1.0f; // Adjust this value to shift the curve along the x-axis
        return intensity / (1.0f + Mathf.Exp(-k * (time - t0)));
    }
    #endregion

    #region Public Methods
    public void Increment()
    {
        watching = true;
        fixationStart = GameObject.Find("Timer").GetComponent<Timer>().timeRemaining;
    }

    public void Decrement()
    {
        watching = false;
        fixationEnd = GameObject.Find("Timer").GetComponent<Timer>().timeRemaining;
    }

    #endregion

    [PunRPC]
    public void updateOutline(float intensity)
    {
        outlineMaterialReal.color = new Color(outlineMaterialReal.color.r, outlineMaterialReal.color.g, outlineMaterialReal.color.b, intensity);
    }
}



//using Microsoft.MixedReality.GraphicsTools;
//using UnityEngine;

//public class GazeVisualizer : MonoBehaviour
//{

//    private bool isWatched = false;
//    private GameObject sphere;
//    private Material outlineMaterial;
//    [SerializeField]
//    private Color originalColor;

//    // Start is called before the first frame update
//    void Start()
//    {
//        sphere = gameObject;
//        outlineMaterial = GetComponent<MeshOutline>().OutlineMaterial;
//        //Debug.Log("outline is"+outlineMaterial);
//        //outlineMaterial= GetComponent<Renderer>().materials[1];
//        //originalColor = outlineMaterial.color;
//        //Debug.Log("outline COLOR is"+originalColor);
//        //Debug.Log("Color orignal : " + originalColor);
//        outlineMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (isWatched)
//        {
//            outlineMaterial.color = originalColor;
//        }
//        else
//        {
//            outlineMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);
//        }
//    }

//    public void Increment()
//    {
//        isWatched = true;
//    }

//    public void Decrement()
//    {
//        isWatched = false;
//    }
//}
