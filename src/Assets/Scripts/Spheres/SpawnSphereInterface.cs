using holoutils;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.SpatialManipulation;

public abstract class SpawnSphereInterface : MonoBehaviour
{
    # region Private Fields
    // parent object (gameArea)
    [SerializeField]
    protected GameObject gameArea;
    #endregion

    #region Public Fields
    // data for storage
    public List<GameObject> activeSpheres = new List<GameObject>();

    // counts the number of spheres popped and display
    [System.NonSerialized]
    public int popCounter;

    public List<string> data;
    #endregion

    #region Protected Fields
    // sizes used and the mapping to their names in the CSV file
    protected static Dictionary<float, string> sizeMap;

    protected float time;
    protected float gazeTime;
    protected float[] gazeTimes = new float[3];
    // Liste pour stocker toutes les interactions de regard
    protected List<GazeInteraction> gazeInteractions = new List<GazeInteraction>();

    protected TMPro.TextMeshProUGUI popText;

    // to store data to the CSV file
    protected CSVLogger logger;

    // colors used
    protected Material[] colors;
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    public void setArea()
    {
        gameArea.GetComponent<PhotonView>().RPC("fixArea", RpcTarget.All);
    }

    public void resetArea()
    {
        gameArea.GetComponent<PhotonView>().RPC("unfixArea", RpcTarget.All);
    }

    // called when the button Start Game (StartButton) is pressed
    // it activates 3 spheres (sphereNumber) and starts a CSV, the time since stratup is stored for the later data collection
    public abstract void startGame();

    // called when sphere is popped (for Pop script)
    public abstract void spherePoped(int id, float time1, float time2, float time3);
    #endregion

    #region PUN Related Methods
    // prevents players from moving the game area
    [PunRPC]
    public abstract void fixArea();

    // allows players to move to game area
    [PunRPC]
    public void unfixArea()
    {
        gameArea.layer = LayerMask.NameToLayer("Default");
        gameArea.GetComponent<Collider>().enabled = true;
        gameArea.GetComponent<ObjectManipulator>().enabled = true;
        //gameArea.GetComponent<NearInteractionGrabbable>().enabled = true;
        gameArea.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5724139f, 0f, 1f, 0.5019608f));
        //PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOn);
    }

    [PunRPC]
    public void startCount()
    {
        popText.text = 0.ToString();
    }

    [PunRPC]
    public void updateValues()
    {
        popCounter++;
        popText.text = popCounter.ToString();
    }

    [PunRPC]
    public abstract void deActivateSphere(int id);

    [PunRPC]
    public abstract void newSphere(int sPos, int sColor, int sSize);
    #endregion
}
