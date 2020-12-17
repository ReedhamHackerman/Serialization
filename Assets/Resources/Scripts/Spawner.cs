using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour
{


    Dictionary<Shapes, Shape> shapeDictionary;
    List<Shapes> shapeNames;
    List<Shapes> alreadyGeneratedObjects;
    List<SerializeTransform> savedObjects;

    void Start()
    {
        shapeDictionary = new Dictionary<Shapes, Shape>();
        shapeNames = new List<Shapes>();
        alreadyGeneratedObjects = new List<Shapes>();
        savedObjects = new List<SerializeTransform>();
        InitializeAllShapes();
        InstantiateAllShape();

    }
    public void InitializeAllShapes()
    {
        Shapes[] shapes = Resources.LoadAll<Shapes>("Prefabs/");
        Debug.Log(shapes.Length);
        for (int i = 0; i < shapes.Length; i++)
        {
            shapeDictionary.Add(shapes[i], shapes[i].shape);
        }
        shapeNames = shapeDictionary.Keys.ToList();
      
    }


    public void InstantiateAllShape()
    {
        for (int i = 0; i < 30; i++)
        {
            Shapes FakeShape = GenerateRandomShape();
            FakeShape.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Random.ColorHSV());
            Instantiate(FakeShape, new Vector3(Random.Range(-100, 100), 12, Random.Range(-100, 100)), Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            alreadyGeneratedObjects.Add(FakeShape);
        }
    }
    public Shapes GenerateRandomShape()
    {
        return shapeNames[Random.Range(0, shapeNames.Count)];
    }
    public void SaveToDiskBinary()
    {
        for (int i = 0; i < alreadyGeneratedObjects.Count; i++)
        {
            SerializeTransform fakeTranform = new SerializeTransform();
            fakeTranform._position[0] = alreadyGeneratedObjects[i].transform.position.x;
            fakeTranform._position[1] = alreadyGeneratedObjects[i].transform.position.y;
            fakeTranform._position[2] = alreadyGeneratedObjects[i].transform.position.z;

            fakeTranform._rotation[0] = alreadyGeneratedObjects[i].transform.rotation.x;
            fakeTranform._rotation[1] = alreadyGeneratedObjects[i].transform.rotation.y;
            fakeTranform._rotation[2] = alreadyGeneratedObjects[i].transform.rotation.z;


            fakeTranform.shape = alreadyGeneratedObjects[i].shape;
            savedObjects.Add(fakeTranform);
        }
        saveDataToDisk("Reedham", savedObjects);
    }


    public void saveDataToDisk(string filePath, object toSave)
    {
        Debug.Log("DataSaving");
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.streamingAssetsPath + "/" + filePath;
        //string path2 = Path.Combine()
        FileStream file = File.Create(path);
        bf.Serialize(file, toSave);
        file.Close();
    }
   
    
    public void DestoyAllGameObjects()
    {
       
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
