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
    public Rigidbody Rigidbody { get { return Rigidbody; } }

   
   
    


}
[System.Serializable]
public class SerializeTransform
{

    public float[] _position = new float[3];
    public float[] _rotation = new float[3];
    public Shape shape;

}
