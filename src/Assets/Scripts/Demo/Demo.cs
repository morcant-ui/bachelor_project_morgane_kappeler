using Photon.Pun;
using UnityEngine;

public abstract class Demo : MonoBehaviour
{
    [SerializeField]
    protected GameObject menu;

    protected bool demoOn = false;

    // checks if all children (demo spheres) are disabled = were popped
    protected bool allChildrenOff()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    [PunRPC]
    public abstract void startDemo();
}
