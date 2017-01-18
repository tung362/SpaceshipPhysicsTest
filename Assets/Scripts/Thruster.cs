using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles particles
public class Thruster : MonoBehaviour
{
    //Have 1 checked only
    public bool IsLeft = false;
    public bool IsRight = false;
    public bool IsUp = false;
    public bool IsDown = false;

    public bool Activated = false;

    public float TrusterSpeed = 2;

    void Start()
    {
        ApplyToNewParent();
    }

    void Update()
    {
        if (Activated) GetComponent<Renderer>().material.color = Color.blue;
        else GetComponent<Renderer>().material.color = Color.white;
    }

    public void ApplyToNewParent()
    {
        transform.root.GetComponent<TestMovement>().ShipThrusters.Add(gameObject);
    }
}
