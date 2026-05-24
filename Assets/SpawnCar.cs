using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnCar : MonoBehaviour
{
    //Instantance car
    //deklarasi dan inisialisasi konstruktor
    public Car Avanza = new Car("Avanza", 120f, "B 1234 ABC", "Red");
    public Car Xenia = new Car("Xenia", 110f, "B 5678 DEF", "Blue");  

    void Start()
    {
        Debug.Log(Avanza.LicenseNumber);
    }
}
