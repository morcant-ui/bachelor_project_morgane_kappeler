using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSphere : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private Transform gameArea;
    [SerializeField]
    private int nbrObjectToSpawn = 3;

    private GameObject spawnObject;
    private Material spawnMaterial;
    private Color[] colors;


    // Start is called before the first frame update
    void Start()
    {
        float radius = prefab.GetComponent<SphereCollider>().radius;

        for (int i = 0; i < nbrObjectToSpawn; i++) { 
        //Dimension of a cube: https://discussions.unity.com/t/dimensions-of-a-cube/112166
        Vector3 deltaRandom = new Vector3(
        Random.Range(-gameArea.localScale.x / 2 + radius, gameArea.localScale.x / 2 - radius),
        Random.Range(-gameArea.localScale.y / 2 + radius, gameArea.localScale.y / 2 - radius),
        Random.Range(-gameArea.localScale.z / 2 + radius, gameArea.localScale.z / 2 - radius)
        );  

        spawnObject = Instantiate(prefab, gameArea.position + deltaRandom, gameArea.rotation);
        spawnMaterial = spawnObject.GetComponent<Renderer>().material;
        RandomColor();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomColor()
    {
    colors = new[] {
    Color.magenta,
    Color.cyan,
    Color.yellow};

        int rand = Random.Range(0, colors.Length);
        spawnMaterial.color = colors[rand];
        
    }
}
