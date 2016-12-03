using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour {

    WheelCollider wc;

    void Awake()
    {
        wc = this.GetComponent<WheelCollider>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void move(float value)
    {
        wc.motorTorque = value;
    }
}
