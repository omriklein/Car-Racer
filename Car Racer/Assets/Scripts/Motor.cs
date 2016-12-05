using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Motor : MonoBehaviour
{

    public Wheel[] wheels;
    public float motorPower = 500f;
    public float turnPower = 500f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wheels.Length >= 2)
        {
            float torque = Input.GetAxis("Vertical") * motorPower;
            float turnSpeed = Input.GetAxis("Horizontal") * turnPower;

            //Back Wheels Drive
            wheels[wheels.Length - 1].move(torque);
            wheels[wheels.Length - 2].move(torque);

            //Front Wheels Steer
            wheels[0].turn(turnSpeed);
            wheels[1].turn(turnSpeed);

        }
    }
}
