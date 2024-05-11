using System.Collections;
using UnityEngine;

public class PopSphere : MonoBehaviour
{
    #region Private Fields
    private bool isTouched = false;
    private GameObject sphere;
    //private Material outlineMaterial;
    //private Color originalColor;
    private Color sphereOriginalColor;
    private Material sphereMaterial;
    private Color transparentColor;
    #endregion

    #region Public Fields
    public int id;   
    #endregion
    public void setIndex(int position) { id = position; }


    // Start is called before the first frame update
    void Start()
    {
        sphere = gameObject;
        //outlineMaterial = GetComponent<Renderer>().materials[1];
        //originalColor = outlineMaterial.color;
        //Debug.Log("Color orignal : " + originalColor);
        //outlineMaterial.color = new Color(0, 0, 0, 0);

        sphereMaterial = this.transform.parent.GetComponent<SpawnSphereSophie>().getMaterial(sphere);
        sphereOriginalColor = sphereMaterial.color;
        transparentColor = new Color(sphereOriginalColor.r, sphereOriginalColor.g, sphereOriginalColor.b, 0.3f); // Adjust alpha to make it slightly transparent
        sphereMaterial.color = transparentColor;
        Debug.Log("Color orignal : " + sphereOriginalColor);
        Debug.Log("And transparant color:" + transparentColor);
    }

    // Update is called once per frame
    void Update()
    {

        if (isTouched)
        {
            sphereMaterial.color = sphereOriginalColor;

            //outlineMaterial.color = originalColor;

            //call method spherePoped from SpawnSphere
            //this.transform.parent.GetComponent<SpawnSphere>().spherePoped(sphere); 
            //call method spherePoped from SpawnSphereSophie
            this.transform.parent.GetComponent<SpawnSphereSophie>().spherePoped(sphere, id); 

            
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

