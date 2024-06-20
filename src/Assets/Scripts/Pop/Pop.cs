using Photon.Pun;
using UnityEngine;
using MixedReality.Toolkit.Examples; //for script IdlePointer

public abstract class Pop : MonoBehaviour
{
    #region Private Fields
    private float amountToDecrease = 0.5f;
    private bool isTouched = false;

    //DEMO
    private GameObject sphere;
    private Material sphereMaterial;
    private Color sphereColor;
    #endregion

    #region Public Fields
    public int id;

    public bool myFlag = false;
    public bool otherFlag = false;
  
    // checks that both players agree that the sphere is ready to pop
    public int readyCount = 0;
    #endregion

    #region Protected Fields
    protected float touchTime;
    protected float[] touchTimes = new float[3];
    #endregion

    public void setIndex(int position) { id = position; }

    // Start is called before the first frame update
    void Start()
    {
        //not used I think

    }
       
    // Update is called once per frame
    void Update()
    {
        if (isTouched)
        {
            //will be the animated outline
        }
    }

    public void Increment()
    {
        isTouched = true;
        touchTime = GameObject.Find("Timer").GetComponent<Timer>().timeRemaining;
        this.transform.parent.GetComponent<SpawnSphere>().GetComponent<PhotonView>().RPC("clientTouched", PhotonNetwork.LocalPlayer, id, amountToDecrease);
        this.GetComponent<PhotonView>().RPC("dualTouch", RpcTarget.All, touchTime);
    }

    public void Decrement()
    {
        isTouched = false;
        this.transform.parent.GetComponent<SpawnSphere>().GetComponent<PhotonView>().RPC("clientUntouched", PhotonNetwork.LocalPlayer, id);
        this.GetComponent<PhotonView>().RPC("unTouch", RpcTarget.All);
    }

    //DEMOOOOO
    public void IncrementDemo()
    {
        sphere = gameObject;
        //Debug.Log("Here is my name in incrementing: " + sphere.name);
        sphereMaterial = sphere.GetComponent<Renderer>().material;
        sphereColor = sphereMaterial.color;
        sphereMaterial.color = new Color(sphereColor.r - amountToDecrease, sphereColor.g - amountToDecrease, sphereColor.b - amountToDecrease, 255f);
        this.GetComponent<PhotonView>().RPC("dualTouch", RpcTarget.All, touchTime);
    }

    public void DecrementDemo()
    {
        sphere = gameObject;
        sphereMaterial = sphere.GetComponent<Renderer>().material;
        sphereColor = sphereMaterial.color;
        sphereMaterial.color = new Color(sphereColor.r + amountToDecrease, sphereColor.g + amountToDecrease, sphereColor.b + amountToDecrease, 255f);
        this.GetComponent<PhotonView>().RPC("unTouch", RpcTarget.All);
    }

    #region PUN Related Methods
    [PunRPC]
    public abstract void dualTouch(float time, PhotonMessageInfo info);

    // used to pop a sphere when two players confirmed touching a spheres at the same time
    [PunRPC]
    public abstract void readyToPop(float time1, float time2, float time3);

    // handles the flags when a sphere is untouched
    [PunRPC]
    public void unTouch(PhotonMessageInfo info)
    {
        if (info.Sender.IsLocal)
        {
            myFlag = false;
        }
        else
        {
            otherFlag = false;
        }
    }

    [PunRPC]
    public void addIdleCursor() {       
        //Debug.Log("Activate Idle Cursor");
        GameObject idleCursorPrefab = this.transform.parent.GetComponent<SpawnSphere>().IdleCursorPrefab;
        GameObject idleCrosshair = Instantiate(idleCursorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        idleCrosshair.AddComponent<IdlePointer>();
        idleCrosshair.GetComponent<IdlePointer>().defaultDistanceInMeters = this.transform.parent.GetComponent<SpawnSphere>().DefaultDistanceInMeters;
        idleCrosshair.GetComponent<IdlePointer>().idleStateColor = this.transform.parent.GetComponent<SpawnSphere>().IdleStateColor;
        idleCrosshair.GetComponent<IdlePointer>().hightlightStateColor = this.transform.parent.GetComponent<SpawnSphere>().HightlightStateColor;
        idleCrosshair.GetComponent<IdlePointer>().gazeController = this.transform.parent.GetComponent<SpawnSphere>().ActionBasedController;
        idleCrosshair.GetComponent<IdlePointer>()._gazeTranslationAction = this.transform.parent.GetComponent<SpawnSphere>().InputActionProperty;

        idleCrosshair.tag = "IdleCrosshair"; // Assign a unique tag
    }   

    [PunRPC]
    public void destroyIdleCursors()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            GameObject[] idleCrosshairs = GameObject.FindGameObjectsWithTag("IdleCrosshair");
            if (idleCrosshairs.Length > 0)
            {
                //Debug.Log("Destroying all Idle Cursors");
                foreach (GameObject idleCrosshair in idleCrosshairs)
                {
                    Destroy(idleCrosshair);
                }
            }
            else
            {
                Debug.LogWarning("No idle cursors found to destroy.");
            }
        }
        
    }
    #endregion
}
