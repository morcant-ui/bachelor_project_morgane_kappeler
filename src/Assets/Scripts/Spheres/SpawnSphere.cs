using holoutils;
using MRTK.Tutorials.MultiUserCapabilities;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.SpatialManipulation;
using Microsoft.MixedReality.GraphicsTools;
using UnityEngine.UIElements;


public class SpawnSphere : SpawnSphereInterface
{
    #region Private Serializable Fields
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Material blue;
    [SerializeField]
    private Material pink;
    [SerializeField]
    private Material yellow;
    //[SerializeField]
    //private Material white;
    [SerializeField]
    private static int nbrObjectToSpawn = 3;
    [SerializeField]
    private static int nbrSpawnPoint = 27;
    #endregion

    #region Private Fields
    private Material spawnMaterial;
    // Array to store positions
    private Vector3[] spherePositions;
    private Vector3[] sizes = new Vector3[3];
    private GameObject[] spheresArray = new GameObject[nbrSpawnPoint];
    private Color[] originalColors = new Color[nbrSpawnPoint];
    private GameObject currentNewSphere;
    private Renderer sphereRenderer;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Initialisation 

        popCounter = 0;

        logger = GameObject.Find("Timer").GetComponent<CSVLogger>();
        data = new List<string>();

        popText = GameObject.Find("PopText").GetComponent<TMPro.TextMeshProUGUI>();

        colors = new Material[] { blue, pink, yellow };

        // Calculate sizes based on fractions of the cube's dimensions
        sizes = new Vector3[3] { ConvertToLocalScale(0.03f), ConvertToLocalScale(0.063f), ConvertToLocalScale(0.099f) };
        sizeMap = new Dictionary<float, string>() { { sizes[0].x, "small" }, { sizes[1].x, "medium" }, { sizes[2].x, "large" } };

        //populate the array of sphere positions (27 positions)
        GenerateSpherePositions();

        //populate the GameArea
        PopulateGameArea(spherePositions);

        // handling the audio when a sphere pops
        gameArea.AddComponent<AudioSource>();
        gameArea.GetComponent<AudioSource>().playOnAwake = false;
        gameArea.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("blub");
        gameArea.GetComponent<AudioSource>().volume = 0.007f;

        //build initial sphere
        //startGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Private Methods

    public override void startGame()
    {
        gameArea.GetComponent<PhotonView>().RPC("startCount", RpcTarget.All);

        // create sphereNumber spheres (here 3)
        for (int i = 0; i < nbrObjectToSpawn; i++)
        {
            makeSphere(-1);
        }

        time = Time.realtimeSinceStartup;
        logger.StartNewCSV(1);
        GameObject.Find("Timer").GetComponent<PhotonView>().RPC("startTimer", RpcTarget.All);
    }

    void makeSphere(int prevPos)
    {
        //find random position in 27 position possibles
        int pos = Random.Range(0, nbrSpawnPoint);
        while (spheresArray[pos].activeSelf || prevPos == pos)
        {
            pos = Random.Range(0, nbrSpawnPoint);
        }

        //take ref to new sphere
        currentNewSphere = spheresArray[pos];

        // Add the new sphere to activeSpheres
        activeSpheres.Add(currentNewSphere);

        // random size
        int size = Random.Range(0, sizes.Length);

        //ranfom color
        int color = Random.Range(0, colors.Length);

        //keep material into memory for master
        //currentNewSphere.GetComponent<Pop>().getMaterial(colors[color]);

        // updates the new sphere to all clients
        gameArea.GetComponent<PhotonView>().RPC("newSphere", RpcTarget.All, pos, color, size);
    }

    //generate 27 positions available to spawn spheres inside the cube and fill spherePosition[] with it
    void GenerateSpherePositions()
    {
        spherePositions = new Vector3[nbrSpawnPoint];

        // Get the center and size of the game area
        Vector3 center = gameArea.transform.position;
        Vector3 size = gameArea.transform.localScale;

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

    void PopulateGameArea(Vector3[] spherePositions)
    {
        for (int pos = 0; pos < spherePositions.Length; pos++)
        {
            // Instantiate the sphere prefab locally
            GameObject newSphere = Instantiate(prefab, spherePositions[pos], Quaternion.identity);
            newSphere.transform.parent = gameArea.transform; // Set the parent to the game area

            //populate SphereArray
            spheresArray[pos] = newSphere;

            //set a size
            newSphere.transform.localScale = sizes[1];

            // set the ViewID manually otherwise all viewIDs set to 0, I added +10 to not interfere with previous photonView IDs like Timer, Area, demo spheres(1 + 3), retstart and backToStart buttons
            // might have to be changed if many object with PhotonViews are added
            // Manually set the PhotonView properties
            PhotonView photonView = newSphere.GetComponent<PhotonView>();
            photonView.ViewID = 10 + pos; // Ensure this does not conflict with existing ViewIDs
            photonView.OwnershipTransfer = OwnershipOption.Fixed;
            photonView.Synchronization = ViewSynchronization.UnreliableOnChange;
            photonView.observableSearch = PhotonView.ObservableSearch.AutoFindAll;
            photonView.ObservedComponents = new List<Component>
            {
                newSphere.GetComponent<GenericNetSync>()
            };

            //make it inactive
            newSphere.SetActive(false);

            //make outline invisible 
            newSphere.GetComponent<MeshOutline>().OutlineWidth = 0.0f;

            //keep the position in memory to check future spawning 
            newSphere.GetComponent<PopSphere>().setIndex(pos);
        }
    }

    #endregion

    #region Public methods

    public Material getMaterial(int sphereID)
    {
        foreach (GameObject activeSphere in activeSpheres)
        {
            if (activeSphere == spheresArray[sphereID])
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


    // called when sphere is popped (from TaskPop script)
    // MODIFIED
    public override void spherePoped(int id, float time1, float time2, float time3)
    {
        // updates popCounter and popText for all players
        gameArea.GetComponent<PhotonView>().RPC("updateValues", RpcTarget.All);

        float popTime = Time.realtimeSinceStartup;

        // data to store in CSV file
        data.Add(popCounter.ToString());
        data.Add(id.ToString());
        foreach (GameObject s in activeSpheres)
        {
            data.Add(s.GetComponent<Pop>().id.ToString());
            data.Add(sizeMap[s.transform.localScale.x]);
            data.Add(s.GetComponent<Renderer>().material.name.Split()[0]);
        }
        data.Add((popTime - time).ToString("F2"));

        data.Add(time1.ToString("F2"));
        data.Add(time2.ToString("F2"));
        data.Add(time3.ToString("F2"));

        // remove popped sphere from activeSpheres list
        activeSpheres.Remove(spheresArray[id]);

        // deactivate the popped sphere for all clients
        gameArea.GetComponent<PhotonView>().RPC("deActivateSphere", RpcTarget.All, id);

        // creates a random new sphere
        if (activeSpheres.Count < nbrObjectToSpawn)
        {
            makeSphere(id);
        }

        // add row to CSV and upadte variables for next logging
        logger.AddRow(data);
        time = popTime;
        data.Clear();
    }

    // converts the global scales for the spheres to local scales
    public Vector3 ConvertToLocalScale(float globalScale)
    {
        return new Vector3(globalScale / transform.lossyScale.x, globalScale / transform.lossyScale.y, globalScale / transform.lossyScale.z);
    }

    #endregion

    #region PUN Related Methods

    [PunRPC]
    public override void newSphere(int sPos, int sColor, int sSize)
    {
        Debug.Log("I'm asking the clients to spawn the new sphere!");
        // updates SetActive, size, color 
        spheresArray[sPos].SetActive(true);
        spheresArray[sPos].transform.localScale = sizes[sSize];
        sphereRenderer = spheresArray[sPos].GetComponent<Renderer>();
        sphereRenderer.material = colors[sColor];
        originalColors[sPos] = colors[sColor].color;
        spheresArray[sPos].GetComponent<Pop>().readyCount = 0;
        spheresArray[sPos].GetComponent<Pop>().myFlag = false;
        spheresArray[sPos].GetComponent<Pop>().otherFlag = false;
        Debug.Log("END of asking for spawn, this should be here " + sPos + " gives us " + spheresArray[sPos]);

    }

    [PunRPC]
    public override void fixArea()
    {   // makes the area untouchable, unmovable and transparent (invisible), disables HandRay
        gameArea.layer = LayerMask.NameToLayer("Ignore Raycast");
        gameArea.GetComponent<Collider>().enabled = false;
        gameArea.GetComponent<ObjectManipulator>().enabled = false;
        //gameArea.GetComponent<NearInteractionGrabbable>().enabled = false; //not needed with MRTK3
        this.GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.0f));
        //PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOff); //not needed with MRTK3
    }


    [PunRPC]
    public override void deActivateSphere(int id)
    {
        spheresArray[id].SetActive(false);       
    }

    [PunRPC]
    public void clientTouched(int id, float amountToDecrease)
    {
        //Debug.Log("Ok, client touched");
        Material sphereMaterial = spheresArray[id].GetComponent<Renderer>().material;
        //Debug.Log("sphere material of touched client before is " + sphereMaterial + " with color " + sphereMaterial.color);
        Color originalColor = originalColors[id];
        sphereMaterial.color = new Color(originalColor.r - amountToDecrease, originalColor.g - amountToDecrease, originalColor.b - amountToDecrease, 255f);
        //Debug.Log("sphere material of touched client after is " + sphereMaterial + " with color " + sphereMaterial.color);
    }

    [PunRPC]
    public void clientUntouched(int id)
    {
        //TODO take out parameter amountToIncrease if it works
        //Debug.Log("OK, client untouched");
        Material sphereMaterial = spheresArray[id].GetComponent<Renderer>().material;
        //Debug.Log("sphere material of UNtouched client before is " + sphereMaterial + " with color " + sphereMaterial.color);
        sphereMaterial.color = originalColors[id];
        //Debug.Log("sphere material of UNtouched client after is " + sphereMaterial + " with color " + sphereMaterial.color);
    }

    #endregion
}