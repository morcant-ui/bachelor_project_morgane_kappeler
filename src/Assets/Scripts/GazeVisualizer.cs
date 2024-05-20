using Microsoft.MixedReality.GraphicsTools;
using UnityEngine;

public class GazeVisualizer : MonoBehaviour
{

    private bool isWatched = false;
    private GameObject sphere;
    private Material outlineMaterial;
    [SerializeField]
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        sphere = gameObject;
        outlineMaterial = GetComponent<MeshOutline>().OutlineMaterial;
        //Debug.Log("outline is"+outlineMaterial);
        //outlineMaterial= GetComponent<Renderer>().materials[1];
        //originalColor = outlineMaterial.color;
        //Debug.Log("outline COLOR is"+originalColor);
        //Debug.Log("Color orignal : " + originalColor);
        outlineMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);
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
            outlineMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);
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
