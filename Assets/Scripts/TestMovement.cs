using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    //Tracking
    public List<GameObject> ShipComponents = new List<GameObject>();
    public List<GameObject> ShipThrusters = new List<GameObject>();

    //Data
    public float MaxLeanSideValue = 45;
    public float MaxLeanForwardValue = 20;
    public float LeanSmoothness = 0.03f;

    //Debug
    public GameObject CenterOfMassObj;

    //Needed compoents
    private Rigidbody TheRigidbody;

    void Start()
    {
        TheRigidbody = GetComponent<Rigidbody>();
    }
	
	void FixedUpdate()
    {
        Movement();
        Lean();
    }

    void Movement()
    {
        //Debug.Log(TheRigidbody.centerOfMass);
        CenterOfMassObj.transform.localPosition = TheRigidbody.centerOfMass;

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        /*Determine which thrusters activate when executing a movement*/
        for (int i = 0; i < ShipThrusters.Count; ++i)
        {
            Thruster aTruster = ShipThrusters[i].GetComponent<Thruster>();
            aTruster.Activated = false;

            //Left turn
            if (Input.GetKey(KeyCode.A))
            {
                if (ShipThrusters[i].transform.localPosition.y > TheRigidbody.centerOfMass.y && aTruster.IsLeft ||
                        ShipThrusters[i].transform.localPosition.y < TheRigidbody.centerOfMass.y && aTruster.IsRight ||
                        ShipThrusters[i].transform.localPosition.y > TheRigidbody.centerOfMass.y && ShipThrusters[i].transform.localPosition.x < TheRigidbody.centerOfMass.x && aTruster.IsDown ||
                        ShipThrusters[i].transform.localPosition.y < TheRigidbody.centerOfMass.y && ShipThrusters[i].transform.localPosition.x > TheRigidbody.centerOfMass.x && aTruster.IsUp)
                {
                    aTruster.Activated = true;
                    TheRigidbody.AddForceAtPosition(ShipThrusters[i].transform.right * aTruster.TrusterSpeed, ShipThrusters[i].transform.position);
                }
            }

            //Right turn
            if (Input.GetKey(KeyCode.D))
            {
                if (ShipThrusters[i].transform.localPosition.y > TheRigidbody.centerOfMass.y && aTruster.IsRight ||
                        ShipThrusters[i].transform.localPosition.y < TheRigidbody.centerOfMass.y && aTruster.IsLeft ||
                        ShipThrusters[i].transform.localPosition.y > TheRigidbody.centerOfMass.y && ShipThrusters[i].transform.localPosition.x > TheRigidbody.centerOfMass.x && aTruster.IsDown ||
                        ShipThrusters[i].transform.localPosition.y < TheRigidbody.centerOfMass.y && ShipThrusters[i].transform.localPosition.x < TheRigidbody.centerOfMass.x && aTruster.IsUp)
                {
                    aTruster.Activated = true;
                    TheRigidbody.AddForceAtPosition(ShipThrusters[i].transform.right * aTruster.TrusterSpeed, ShipThrusters[i].transform.position);
                }
            }

            if (Input.GetKey(KeyCode.W))
            {
                if (aTruster.IsUp)
                {
                    aTruster.Activated = true;
                    TheRigidbody.AddForceAtPosition(ShipThrusters[i].transform.right * aTruster.TrusterSpeed, ShipThrusters[i].transform.position);
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (aTruster.IsDown)
                {
                    aTruster.Activated = true;
                    TheRigidbody.AddForceAtPosition(ShipThrusters[i].transform.right * aTruster.TrusterSpeed, ShipThrusters[i].transform.position);
                }
            }
        }
    }

    void Lean()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Quaternion targetLean = Quaternion.Euler(transform.rotation.eulerAngles.x, MaxLeanSideValue, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetLean, LeanSmoothness);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Quaternion targetLean = Quaternion.Euler(transform.rotation.eulerAngles.x, -MaxLeanSideValue, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetLean, LeanSmoothness);
        }
        else
        {
            Quaternion targetLean = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetLean, LeanSmoothness);
        }
    }
}
