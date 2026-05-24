using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    //this is for testing only
    public string Name;
    public float Speed;
    public string LicenseNumber;
    public string Color;
    

    public Car(string name, float speed, string licenseNumber, string color)
    {
        Name = name;
        Speed = speed;
        LicenseNumber = licenseNumber;
        Color = color;
    }

    public void Drive()
    {
        Debug.Log($"{Name} is driving at {Speed} km/h.");
    }
    public void Brake()
    {
        Debug.Log($"{Name} is braking.");
    }
    public void Honk()
    {
        Debug.Log($"{Name} is honking.");
    }
    public void SetColor(string newColor)
    {
        Color = newColor;
        Debug.Log($"{Name} is now {Color}.");
    }
}
