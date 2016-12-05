using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour
{
    private WheelCollider wc;

    public float moveSpeed = 10f;
    public float breakSpeed = 40f;
    void Awake()
    {
        wc = this.GetComponent<WheelCollider>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void move(float value)
    {
        wc.motorTorque = value;

        wc.wheelDampingRate = value == 0 ? breakSpeed : moveSpeed;
    }

    public void turn(float value)
    {
        wc.steerAngle = value;
        this.transform.localEulerAngles = new Vector3(0f, wc.steerAngle, 90f);
    }
}
