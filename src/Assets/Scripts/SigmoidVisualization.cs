using Microsoft.MixedReality.GraphicsTools;
using Photon.Pun;
using UnityEngine;

public class SigmoidVisualization : MonoBehaviour
{
    #region SerializeFields
    //[SerializeField]
    //public Color originalColor;
    #endregion

    #region Private Fields
    private bool isWatched = false;
    private GameObject sphere;
    private Material outlineMaterial;

    private float fixationStartTime;
    private float fixationEndTime;
    private float currentIntensity = 0f;

    private int id;
    #endregion

    #region Public Fields
    // Parameters to control speed of intensity change
    public float increaseSpeed = 1.0f;
    public float decreaseSpeed = 0.75f;
    public float maxIntensity = 1.0f;

    private Color originalColor;
    #endregion

    public Color OriginalColor { get { return originalColor; } set {  originalColor = value; } }

    public void setIndex(int position) { id = position; }
    // Start is called before the first frame update
    void Start()
    {
        outlineMaterial = GetComponent<MeshOutline>().OutlineMaterial;    
        outlineMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (isWatched)
        {
            float fixationDuration = GameObject.Find("Timer").GetComponent<Timer>().timeRemaining - fixationStartTime;
            float targetIntensity = CalculateSigmoidIntensity(fixationDuration);
            currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * increaseSpeed);
        }
        else
        {
            float fixationDuration = GameObject.Find("Timer").GetComponent<Timer>().timeRemaining - fixationEndTime;
            currentIntensity = Mathf.Lerp(currentIntensity, 0, Time.deltaTime * decreaseSpeed);
        }
        //outlineMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, currentIntensity); //own's gaze
        this.transform.parent.GetComponent<SpawnSphere>().GetComponent<PhotonView>().RPC("updateOutline", RpcTarget.Others, id, currentIntensity); //partner's gaze
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
        fixationStartTime = GameObject.Find("Timer").GetComponent<Timer>().timeRemaining;
    }

    public void Decrement()
    {
        isWatched = false;
        fixationEndTime = GameObject.Find("Timer").GetComponent<Timer>().timeRemaining;
    }

    #endregion
}
