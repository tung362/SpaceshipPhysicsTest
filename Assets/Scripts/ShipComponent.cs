using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipComponent : MonoBehaviour
{
	void Start ()
    {
        ApplyToNewParent();
    }
	
	void Update ()
    {
		
	}

    public void ApplyToNewParent()
    {
        transform.root.GetComponent<TestMovement>().ShipComponents.Add(gameObject);
    }
}
