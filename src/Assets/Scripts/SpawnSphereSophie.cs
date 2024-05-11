using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnSphereSophie : MonoBehaviour
{
    #region Private Serializable Fields
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform gameArea;
    [SerializeField]
    private static int nbrObjectToSpawn = 3;
    [SerializeField]
    private static int nbrSpawnPoint = 27;
    #endregion

    #region Private Fields
    //private GameObject[] spawn= new GameObject[nbrObjectToSpawn]; //DO I NEED THIS???
    private Material spawnMaterial;
    private Color[] colors;
    // Array to store positions
    private Vector3[] spherePositions;

    /// <summary>
    /// From Sophie
    /// </summary>
    private Vector3[] sizes = new Vector3[3];
    private GameObject[] spheresArray = new GameObject[nbrSpawnPoint];
    private GameObject newSphere;
    #endregion

    #region Publib fields

    /// <summary>
    /// From Sophie
    /// </summary>
    public int popCounter;
    public List<GameObject> activeSpheres = new List<GameObject>();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Initialisation 

        popCounter = 0;

        // Calculate sizes based on fractions of the cube's dimensions
        sizes = new Vector3[3] { ConvertToLocalScale(0.03f), ConvertToLocalScale(0.063f), ConvertToLocalScale(0.099f) };
        Debug.Log("sizes" + sizes[0] + sizes[1] + sizes[2]);

        //populate the array of sphere (27 spheres)
        GenerateSpherePositions();

        

        //build initial sphere
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Private Methods

    void StartGame()//maybe this one should be public?
    {
        // create sphereNumber spheres (here 3)
        for (int i = 0; i < nbrObjectToSpawn; i++)
        {
            MakeSphere(-1);
        }

    }

    void MakeSphere(int prevPos)//int prevId
    {
        Debug.Log("yooooooooooooooooooooooooooooooooooooooooooooo");

        //find random position in 27 position possibles
        int pos = Random.Range(0, nbrSpawnPoint);
        while (activeSpheres.Exists(sphere => sphere.transform.position == spherePositions[pos] || prevPos == pos))
        {
            pos = Random.Range(0, nbrSpawnPoint);
        }

        // Instantiate the sphere prefab at the random position
        GameObject newSphere = Instantiate(prefab, spherePositions[pos], Quaternion.identity);
        newSphere.transform.parent = gameArea; // Set the parent to the game area


        // Choose a random size from the sizes array
        Vector3 selectedSize = sizes[Random.Range(0, sizes.Length)];

        // Store the material of the spawned object
        spawnMaterial = newSphere.GetComponent<Renderer>().material;
        // Apply random color to the spawned object
        RandomColor(spawnMaterial);
        newSphere.transform.localScale = selectedSize; // Apply the selected size
        //keep the position in memory to check future spawning 
        newSphere.GetComponent<PopSphere>().setIndex(pos);

        // Add the new sphere to activeSpheres
        activeSpheres.Add(newSphere);


        Debug.Log(selectedSize);
        //Debug.Log("Previous Position: " + prevPos + "\nNew Position: " + pos);

    }

    //generate 27 positions available to spawn spheres inside the cube and fill spherePosition[] with it
    void GenerateSpherePositions()
    {
        spherePositions = new Vector3[nbrSpawnPoint];

        // Get the center and size of the game area
        Vector3 center = gameArea.position;
        Vector3 size = gameArea.localScale;

        // Calculate half size to adjust positions within the game area
        Vector3 halfSize = size / 3f;

        // Generate positions within the game area
        int index = 0;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    float xPos = center.x + ((x - 1) * halfSize.x);
                    float yPos = center.y + ((y - 1) * halfSize.y);
                    float zPos = center.z + ((z - 1) * halfSize.z);

                    spherePositions[index] = new Vector3(xPos, yPos, zPos);
                    index++;
                }
            }
        }
    }

    void RandomColor(Material spawnMat)
    {
        colors = new[] {
        Color.magenta,
        Color.cyan,
        Color.gray,
        Color.yellow};
        

        int rand = Random.Range(0, colors.Length);
        spawnMat.color = colors[rand];

    }

    private IEnumerator DestroySphere(GameObject sphere)
    {

        yield return new WaitForSeconds(0.07f);

        Destroy(sphere);
    }

    #endregion

    #region Public methods

    public Material getMaterial(GameObject sphere)
    {
        foreach (GameObject activeSphere in activeSpheres)
        {
            if (activeSphere == sphere)
            {
                // If the current activeSphere is equal to the given sphere, return its material
                Renderer renderer = activeSphere.GetComponent<Renderer>();
                if (renderer != null)
                {
                    return renderer.material;
                }
            }
        }
        return null;
    }

    ///////////////////////////////
    // From Sophie's code
    //////////////////////////////


    // called when sphere is popped (from TaskPop script)
    // MODIFIED
    public void spherePoped(GameObject sphere, int id)
    {


        // remove popped sphere from activeSpheres list
        activeSpheres.Remove(sphere);
        // destroy the popped sphere
        StartCoroutine(DestroySphere(sphere)); //mine
        // creates a random new sphere
        if (activeSpheres.Count < nbrObjectToSpawn)
        {
            MakeSphere(id);
        }
    }

    // converts the global scales for the spheres to local scales
    public Vector3 ConvertToLocalScale(float globalScale)
    {
        return new Vector3(globalScale / transform.lossyScale.x, globalScale / transform.lossyScale.y, globalScale / transform.lossyScale.z);
    }

    #endregion
}
//TROP PETIT
//RESPAWN PAS DS LE CUBE MAIS L� OU CUBE A ETE INSTANCIE... ---> this will be fixed with intro of PUN (me thinks...)