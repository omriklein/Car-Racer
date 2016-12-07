using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour
{
    private WheelCollider wc;

    void Awake()
    {
        wc = this.GetComponent<WheelCollider>();
    }

    public void move(float value)
    {
        wc.motorTorque = value;
    }

    public void turn(float value)
    {
        wc.steerAngle = value;
        this.transform.localEulerAngles = new Vector3(0f, wc.steerAngle, 90f);
    }
}
