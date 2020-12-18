using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Shape
{
    Sphere,Cube,Cylinder
}
[System.Serializable]
public class Shapes : MonoBehaviour
{
    public Shape shape;
    public Rigidbody rb;
    public Renderer rd;
    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rd = gameObject.GetComponent<Renderer>();
    }

   
   
    


}
[System.Serializable]
public class SerializeTransform
{

    public float[] _position = new float[3];
    public float[] _rotation = new float[3];
    public Shape shape;
    public float[] _velocity = new float[3];
}
