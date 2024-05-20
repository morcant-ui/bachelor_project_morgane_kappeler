using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///////////////////////////////
// TODO
// 1. pas pouvoir spawn au même endroit qu'avant
// 2. ajouter coroutine to buildsphere pr allow que sphere spawn pas en même temps/ avant que despawn
// 3. récupérer données
// 4. timer
// 5. counter
// 
///////////////////////////////

public class SpawnSphere : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform gameArea;
    [SerializeField]
    private static int nbrObjectToSpawn = 3;

    private GameObject[] spawnObject = new GameObject[nbrObjectToSpawn];
    private Material spawnMaterial;
    private Color[] colors;

    ///////////////////////////////
    // From Sophie's code
    ///////////////////////////////
    public List<GameObject> activeSpheres = new List<GameObject>();
    private Vector3[] sizes = new Vector3[3];


    // Start is called before the first frame update
    void Start()
    {
        //Initialization

        // Calculate sizes based on fractions of the cube's dimensions
        Debug.Log("edge of cube" + gameArea.localScale.x);
        float cubeEdge = Mathf.Min(gameArea.localScale.x, gameArea.localScale.y, gameArea.localScale.z); //Since cube is uniform we don't need to put the Min but better safe than sorry
        float oneThird = cubeEdge / 3;
        float oneSixth = cubeEdge / 6;
        float oneNinth = cubeEdge / 9;
        Debug.Log("cubeEdge is:" + cubeEdge);
        Debug.Log("oneThird:"+ oneThird);
        sizes[0] = new Vector3(oneThird, oneThird, oneThird);
        sizes[1] = new Vector3(oneSixth, oneSixth, oneSixth);
        sizes[2] = new Vector3(oneNinth, oneNinth, oneNinth);
        Debug.Log("sizes" + sizes[0] + sizes[1] + sizes[2]);
        // size from Sophie's code
        //sizes = new Vector3[3] {}; // the biggest is like 1/3 of one arrête du cube et après diviser par 3 !!!
        //sizeMap = new Dictionary<float, string>() { { sizes[0].x, "small" }, { sizes[1].x, "medium" }, { sizes[2].x, "large" } };

        BuildSphere(nbrObjectToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void BuildSphere(int nbrSphereToBuild)
    {

        

        //float radius = prefab.GetComponent<SphereCollider>().radius;

        for (int i = 0; i < nbrSphereToBuild; i++)
        {
            // Choose a random size from the sizes array
            Vector3 selectedSize = sizes[Random.Range(0, sizes.Length)];

            float radius = selectedSize.x / 2;

            //Dimension of a cube: https://discussions.unity.com/t/dimensions-of-a-cube/112166
            Vector3 deltaRandom = new Vector3(
            Random.Range(-gameArea.localScale.x / 2 + radius, gameArea.localScale.x / 2 - radius),
            Random.Range(-gameArea.localScale.y / 2 + radius, gameArea.localScale.y / 2 - radius),
            Random.Range(-gameArea.localScale.z / 2 + radius, gameArea.localScale.z / 2 - radius)
            );

            // Ensure the random position is clamped within the cube
            Vector3 spawnPosition = gameArea.position + deltaRandom;

            bool positionIsValid = true;

            // Check against existing spawn positions
            for (int j = 0; j < i; j++)
            {

                Vector3 otherSize = spawnObject[j].transform.localScale;
                float combinedRadius = (selectedSize.x / 2) + (otherSize.x / 2); // Calculate combined radius
                if (Vector3.Distance(spawnPosition, spawnObject[j].transform.position) < combinedRadius)
                {
                    // The new position is too close to an existing object
                    positionIsValid = false;
                    break;
                }
            }

            if (positionIsValid)
            {
                

                // Spawn the object with the selected size
                spawnObject[i] = Instantiate(prefab, spawnPosition, gameArea.rotation);
                spawnObject[i].transform.localScale = selectedSize; // Apply the selected size
                // Store the material of the spawned object
                spawnMaterial = spawnObject[i].GetComponent<Renderer>().material;
                // Apply random color to the spawned object
                RandomColor(spawnMaterial);

                // adds it to activeSpheres (Sophie)
                activeSpheres.Add(spawnObject[i]);
                spawnObject[i].transform.parent = gameArea; ///THIS
            }
            else
            {
                // Retry spawning at a new position if the position is not valid
                i--;
            }
        }

    }

    void RandomColor(Material spawnMat)
    {
    colors = new[] {
    Color.magenta,
    Color.cyan,
    Color.yellow};

    int rand = Random.Range(0, colors.Length);
    spawnMat.color = colors[rand];
        
    }

    private IEnumerator DestroySphere(GameObject sphere)
    {

        //yield return new WaitForSeconds(0.07f); ORIGINAL
        yield return new WaitForSeconds(0.09f);

        Destroy(sphere);
    }

    ///////////////////////////////
    // From Sophie's code
    ///////////////////////////////

    // converts the global scales for the spheres to local scales
    //NOT USED
    public Vector3 ConvertToLocalScale(float globalScale)
    {
        return new Vector3(globalScale / transform.lossyScale.x, globalScale / transform.lossyScale.y, globalScale / transform.lossyScale.z);
    }


    // called when sphere is popped (from TaskPop script)
    // MODIFIED
    public void spherePoped(GameObject sphere)
    {

        // remove popped sphere from activeSpheres list
        activeSpheres.Remove(sphere);
        // destroy the popped sphere
        StartCoroutine(DestroySphere(sphere)); //mine
        // creates a random new sphere
        if (activeSpheres.Count < nbrObjectToSpawn)
        {
            BuildSphere(1);
        }
    }
}
