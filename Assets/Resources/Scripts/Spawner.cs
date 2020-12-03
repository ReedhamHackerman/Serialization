using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject cube;
    public GameObject cylinder;
    public GameObject sphere;
    public int NumberOfUnit;
    public List<GameObject> allObjects;
    public 
    // Start is called before the first frame update
    void Start()
    {
        cube = Resources.Load<GameObject>("Prefabs/Cube");
        cylinder = Resources.Load<GameObject>("Prefabs/Cylinder");
        sphere = Resources.Load<GameObject>("Prefabs/Sphere");
        
        SpawnObjects();
       

    }

    public void SpawnObjects()
    {
        for (int i = 0; i < NumberOfUnit; i++)
        {
            GameObject duplicatCube = Instantiate(cube, new Vector3(Random.Range(-100, 100), 12, Random.Range(-100, 100)), Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            duplicatCube.GetComponent<Renderer>().material.SetColor("_Color", Random.ColorHSV());
            GameObject duplicateCylinder = Instantiate(cylinder, new Vector3(Random.Range(-100, 100), 12, Random.Range(-100, 100)), Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            duplicateCylinder.GetComponent<Renderer>().material.SetColor("_Color", Random.ColorHSV());
            GameObject duplicatSphere = Instantiate(sphere, new Vector3(Random.Range(-100, 100), 12, Random.Range(-100, 100)), Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            duplicatSphere.GetComponent<Renderer>().material.SetColor("_Color", Random.ColorHSV());
            allObjects.Add(duplicatCube);
            allObjects.Add(duplicateCylinder);
            allObjects.Add(duplicatSphere);
        }
    }
    public void DestoyAllGameObjects()
    {
        //foreach (GameObject duplicate in allObjects)
        //{
        //    //GameObject fake = duplicate;
        //    allObjects.Remove(duplicate);
        //   // Destroy(fake);
        //}
        for (int i = allObjects.Count-1; i>=0; i--)
        {
            GameObject fake = allObjects[i];
            allObjects.Remove(allObjects[i]);
            Destroy(fake.gameObject);
        }
        SpawnObjects();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum ObjectType
{
    Cube,Cylinder,Sphere
}