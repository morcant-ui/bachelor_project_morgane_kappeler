using Photon.Pun;
using UnityEngine;

public abstract class Pop : MonoBehaviour
{
    #region Private Fields
    private bool isTouched = false;
    private GameObject sphere;

    private Color sphereOriginalColor;
    private Color sphereTouchedColor;
    private Material sphereMaterial;
    private Color clientSphereMaterialColor;
    private float amountToDecrease = 0.5f;
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
        Debug.Log("JUST ENTERED THE POP BIOME :)");
        //sphere = gameObject;

        //if (PhotonNetwork.IsMasterClient)
        //{
        //    sphereMaterial = this.transform.parent.GetComponent<SpawnSphere>().getMaterial(id);
        //    Debug.Log("Here in POP, the matos I got is:" + sphereMaterial);
        //    sphereOriginalColor = sphereMaterial.color;
        //    sphereTouchedColor = new Color(sphereOriginalColor.r - amountToDecrease, sphereOriginalColor.g - amountToDecrease, sphereOriginalColor.b - amountToDecrease, 255f); // Adjust colors to make them a bit more dark
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (sphereOriginalColor != null)
        {

            if (isTouched)
            {
                //if (PhotonNetwork.IsMasterClient)
                //{
                //    Debug.Log("Master touched something");
                //    touchTime = GameObject.Find("Timer").GetComponent<Timer>().timeRemaining;
                //    sphereMaterial.color = sphereTouchedColor;
                //    this.GetComponent<PhotonView>().RPC("dualTouch", RpcTarget.All, touchTime);
                //}
                //else
                //{
                Debug.Log("Client touched something");
                touchTime = GameObject.Find("Timer").GetComponent<Timer>().timeRemaining;
                this.transform.parent.GetComponent<SpawnSphere>().GetComponent<PhotonView>().RPC("clientTouched", PhotonNetwork.LocalPlayer, id, amountToDecrease);
                this.GetComponent<PhotonView>().RPC("dualTouch", RpcTarget.All, touchTime);

                //}

            }
            else
            {
                //if (PhotonNetwork.IsMasterClient)
                //{
                //    sphereMaterial.color = sphereOriginalColor;
                //    this.GetComponent<PhotonView>().RPC("unTouch", RpcTarget.Others);
                //}
                //else
                //{
                this.transform.parent.GetComponent<SpawnSphere>().GetComponent<PhotonView>().RPC("clientUntouched", PhotonNetwork.LocalPlayer, id);
                this.GetComponent<PhotonView>().RPC("unTouch", RpcTarget.Others);
                //}
            }
        }
        else
        {
            Debug.Log("Sorry, me not ready because I can't get a material!!");
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
    public void getClientMaterialColor(int  id, float colorR, float colorG, float colorB)
    {
        //clientSphereMaterialColor = gameObject.
    }
    #endregion
}
