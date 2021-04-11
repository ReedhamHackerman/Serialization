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
    List<Shapes> tempShapes;
    void Start()
    {
        shapeDictionary = new Dictionary<Shapes, Shape>();
        shapeNames = new List<Shapes>();
        alreadyGeneratedObjects = new List<Shapes>();
        tempShapes = new List<Shapes>();
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
        Debug.Log("Shape Name Count: "+shapeNames.Count);
      
    }


    public void InstantiateAllShape()
    {
        for (int i = 0; i < 30; i++)
        {
            Shapes FakeShape = GenerateRandomShape();
           // FakeShape.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Random.ColorHSV());
            FakeShape.rd.sharedMaterial.SetColor("_Color", Random.ColorHSV());
            FakeShape = Instantiate(FakeShape, new Vector3(Random.Range(-10, 10), 18, Random.Range(-10, 10)), Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            alreadyGeneratedObjects.Add(FakeShape);
            tempShapes.Add(FakeShape);
        }
    }
    public Shapes GenerateRandomShape()
    {
        return shapeNames[Random.Range(0, shapeNames.Count)];
    }
    public List<SerializeTransform> ReturnSavedObject()
    {
        for (int i = 0; i < alreadyGeneratedObjects.Count; i++)
        {

            SerializeTransform fakeTranform = new SerializeTransform();
            if (fakeTranform!=null)
            {
                fakeTranform._position[0] = alreadyGeneratedObjects[i].transform.position.x;
                fakeTranform._position[1] = alreadyGeneratedObjects[i].transform.position.y;
                fakeTranform._position[2] = alreadyGeneratedObjects[i].transform.position.z;

                fakeTranform._rotation[0] = alreadyGeneratedObjects[i].transform.rotation.x;
                fakeTranform._rotation[1] = alreadyGeneratedObjects[i].transform.rotation.y;
                fakeTranform._rotation[2] = alreadyGeneratedObjects[i].transform.rotation.z;

                fakeTranform._velocity[0] = alreadyGeneratedObjects[i].rb.velocity.x;
                fakeTranform._velocity[1] = alreadyGeneratedObjects[i].rb.velocity.y;
                fakeTranform._velocity[2] = alreadyGeneratedObjects[i].rb.velocity.z;



                fakeTranform.shape = alreadyGeneratedObjects[i].shape;
                savedObjects.Add(fakeTranform);
            }
           
        }
        return savedObjects;
    }
    public void SaveDataToBinary()
    {
        saveDataToDisk("ReedhamBinary", ReturnSavedObject());
    }
    public void SaveDataToXml()
    {
        SaveDataInXml("ReedhamXml", ReturnSavedObject());
    }
    public void SaveDataToJson()
    {
        SaveDataInJson("ReedhamJson", ReturnSavedObject());
    }


    public void LoadBinaryDataFromTheDisk()
    {
        List<SerializeTransform> binaryTransforms = LoadDataFromDisk<List<SerializeTransform>>("ReedhamBinary");
        foreach (SerializeTransform data in binaryTransforms)
        {
            for (int i = 0; i < shapeNames.Count; i++)
            {
                if(shapeNames[i].shape == data.shape)
                {
                    Quaternion angle = Quaternion.Euler(new Vector3(data._rotation[0], data._rotation[1], data._rotation[2]));
                    Vector3 position = new Vector3(data._position[0], data._position[1], data._position[2]);
                    shapeNames[i].rb.velocity = new Vector3(data._velocity[0], data._velocity[1], data._velocity[2]);
                    Shapes fakeShape = Instantiate(shapeNames[i],position ,angle);
                    tempShapes.Add(fakeShape);
                }
            }
        }
    }

    public void LoadDataFromXml()
    {
        List<SerializeTransform> xmlTransforms = null ;
        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(List<SerializeTransform>));
       // System.Xml.Serialization.XmlSerializer b = new System.Xml.Serialization.XmlSerializer(xmlTransforms.GetType());
        string path = Application.streamingAssetsPath + "/" + "ReedhamXml";
        StreamReader sr = new StreamReader(path);
        xmlTransforms = (List<SerializeTransform>) x.Deserialize(sr);
        foreach (SerializeTransform data in xmlTransforms)
        {
            for (int i = 0; i < shapeNames.Count; i++)
            {
                if (shapeNames[i].shape == data.shape)
                {
                    Quaternion angle = Quaternion.Euler(new Vector3(data._rotation[0], data._rotation[1], data._rotation[2]));
                    Vector3 position = new Vector3(data._position[0], data._position[1], data._position[2]);
                    shapeNames[i].rb.velocity = new Vector3(data._velocity[0], data._velocity[1], data._velocity[2]);
                    Shapes fakeShape =  Instantiate(shapeNames[i], position, angle);
                    tempShapes.Add(fakeShape);
                }
            }
        }



    }
    public  void loadDataFromJsonFile()
    {
        string jsonString ;
        string path = Application.streamingAssetsPath + "/" + "ReedhamJson";
        jsonString = File.ReadAllText(path);
        List<SerializeTransform>  serializeTransforms = (List<SerializeTransform>) JsonHelper.FromJson<SerializeTransform>(jsonString);

        foreach (SerializeTransform data in serializeTransforms)
        {
            for (int i = 0; i < shapeNames.Count; i++)
            {
                if (shapeNames[i].shape == data.shape)
                {
                    Quaternion angle = Quaternion.Euler(new Vector3(data._rotation[0], data._rotation[1], data._rotation[2]));
                    Vector3 position = new Vector3(data._position[0], data._position[1], data._position[2]);
                    shapeNames[i].rb.velocity = new Vector3(data._velocity[0], data._velocity[1], data._velocity[2]);
                    Shapes fakeShape = Instantiate(shapeNames[i], position, angle);
                    tempShapes.Add(fakeShape);
                }
            }
        }

        // StreamReader sr = new StreamReader(path);
        // jsonString = sr;
        //List<SerializeTransform> transforms = JsonUtility.FromJson(sr, typeof(List<SerializeTransform>));
        //List<SerializeTransform> serializeTransforms = JsonHelper.FromJson()
    }
    private void saveDataToDisk(string filePath, object toSave)
    {
        Debug.Log("DataSavingBinary");
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.streamingAssetsPath + "/" + filePath;
        //string path2 = Path.Combine()
        FileStream file = File.Create(path);
        bf.Serialize(file, toSave);
        file.Close();
    }

    private void SaveDataInXml(string filePath, object toSave)
    {
        Debug.Log("DataSavingXml");
       
        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(toSave.GetType());
        
        string path = Application.streamingAssetsPath + "/" + filePath;
        FileStream file = File.Create(path);
        x.Serialize(file, toSave);
        file.Close();
    }

    private void SaveDataInJson(string filePath, List<SerializeTransform> toSave)
    {
        Debug.Log("DataSavingJson");
         
        //string jsonSave = JsonUtility.ToJson(toSave,true);
       // Debug.Log(jsonSave);
        string jsonHelper = JsonHelper.ToJson(toSave,true);
        //Debug.Log(jsonHelper);
        string path = Application.streamingAssetsPath + "/" + filePath ;
       // FileStream file = File.Create(path);
        File.WriteAllText(path, jsonHelper);
      
       
    }

   
    public void DestoyAllGameObjects()
    {
        for (int i = tempShapes.Count-1; i >= 0; i--)
        {
            Shapes fake = tempShapes[i];
            tempShapes.RemoveAt(i);
            Destroy(fake.gameObject);
        }
        InstantiateAllShape();
    }


    public T LoadDataFromDisk<T>(string filePath)
    {
        T toRet;
        string path = Application.streamingAssetsPath + "/" + filePath;
        //FileInfo info = new FileInfo(path);
        //Debug.Log(info.Extension);
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            toRet = (T)bf.Deserialize(file);
            file.Close();
        }
        else
            toRet = default(T);
        return toRet;
    }
    void Update()
    {
        
    }
}
