using System.Collections;
using UnityEngine;

public class PopSphere : MonoBehaviour
{
    private bool isTouched = false;
    private GameObject sphere;
    private Material outlineMaterial;
    private Color originalColor;


    // Start is called before the first frame update
    void Start()
    {
        sphere = gameObject;
        outlineMaterial = GetComponent<Renderer>().materials[1];
        originalColor = outlineMaterial.color;
        //Debug.Log("Color orignal : " + originalColor);
        outlineMaterial.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (isTouched)
        {            
            outlineMaterial.color = originalColor;
            //call method spherePoped from SpawnSphere
            this.transform.parent.GetComponent<SpawnSphere>().spherePoped(sphere); 

            //StartCoroutine(DestroySphere());
        }

    }

    public void Increment()
    {
        isTouched = true;
    }

    //Not used anymore
    private IEnumerator DestroySphere()
    {

        yield return new WaitForSeconds(1.5f);

        Destroy(sphere);
    }


    public void Decrement()
    {
        isTouched = false;
    }
}

