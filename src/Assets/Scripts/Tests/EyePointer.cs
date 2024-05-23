using MixedReality.Toolkit.Input;
using UnityEngine;

namespace EyePointerDemo
{
    public class EyePointer : MonoBehaviour
    {
        [SerializeField]
        private GazeInteractor gazeInteractor;

        [SerializeField]
        private GameObject hitPointDisplayPrefab;

        private GameObject hitPointDisplayer;

        [SerializeField]
        private GameObject objectOfInterest;


   

        private void Start()
        {
            hitPointDisplayer = Instantiate(hitPointDisplayPrefab);
        }

        private void Update()
        {
            var ray = new Ray(gazeInteractor.rayOriginTransform.position,
                gazeInteractor.rayOriginTransform.forward * 3);
            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject == objectOfInterest)
                {
                    hitPointDisplayer.transform.position = hit.point;
                }
            }
        }

    }
}