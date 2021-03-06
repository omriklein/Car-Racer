﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Motor : MonoBehaviour
{
    /* wheels in order:
    front left
    front right
    back left
    back right
    */
    public Wheel[] wheels;
    public float motorPower = 1000f;
    public float turnPower = 25f;
    public Transform centerOfMass;

    private float torque;
    private float turnSpeed;

    /*for the tricks in game
   public static bool isGruonded = true;
   public float jumpForce = 100000f;
   public float trickRotationAngle = 10;
   */

    private Rigidbody rbody;

    /* For Android
    inisiate vars for android input
    */

    private float StartZValue;
    private const float torqueMaxValue = 5000;
    private float turnSensitivity = 2f;

    void Awake()
    {
        rbody = this.GetComponent<Rigidbody>();

        if (MenuScript.GP == GamePlatform.Android || MenuScript.GP == GamePlatform.Unknown)
        {
            StartZValue = Input.acceleration.x;
        }
        else
        {
            turnSensitivity = 1;
        }
    }

    // Use this for initialization
    void Start()
    {
        rbody.centerOfMass = centerOfMass.localPosition;

    }

    /*for the tricks in game
      void Update()
      {

          if (Input.GetKey(KeyCode.Z) && !isGruonded) // rotate left
          { this.transform.Rotate(Vector3.up, trickRotationAngle); }
          if (Input.GetKey(KeyCode.X) && !isGruonded) //rotate right
          { this.transform.Rotate(Vector3.up, -trickRotationAngle); }

          if (Input.GetKeyDown(KeyCode.Space) && isGruonded) // jump
          { this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce); }

   }
    */

    void Update()
    {
        /* Restart before I find a better solution */
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(100, 8, 20);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wheels.Length >= 2)
        {
            if (MenuScript.GP == GamePlatform.Android || MenuScript.GP == GamePlatform.Unknown) // if playing on an android device
            {
                //if finger pressed or remove stop the car
                foreach (Touch t in Input.touches)
                {
                    if (t.phase == TouchPhase.Began || t.phase == TouchPhase.Ended)
                    {
                        //this.GetComponent<Rigidbody>().drag = float.MaxValue;
                        //rbody.velocity *= 0.5f;
                        //print("Zeroised");
                    }
                }

                switch (Input.touchCount)
                {
                    case 1:
                        if (rbody.velocity.z < 0)
                            rbody.velocity = new Vector3(rbody.velocity.x, rbody.velocity.y, -1*rbody.velocity.z);
                        torque = torqueMaxValue * motorPower; break;
                    case 2:
                        if (rbody.velocity.z > 0)
                            rbody.velocity = new Vector3(rbody.velocity.x, rbody.velocity.y, -1 * rbody.velocity.z);
                        torque = -1 * torqueMaxValue * motorPower; break;
                    default:
                        torque = 0; break;
                }

                turnSpeed = Mathf.Clamp(turnSpeed - ((StartZValue) - (Input.acceleration.x)), -0.5f, 0.5f);
                StartZValue = Input.acceleration.x;
            }
            else
            {
                torque = Input.GetAxis("Vertical") * motorPower;
                turnSpeed = Input.GetAxis("Horizontal");
            }

            if (torque != 0)
            {
                rbody.drag = 0;

                //Back Wheels Drive
                wheels[wheels.Length - 1].move(torque);
                wheels[wheels.Length - 2].move(torque);
            }
            else
            {
                this.GetComponent<Rigidbody>().drag = 1;
            }

            //Front Wheels Steer
            wheels[0].turn(turnSpeed * turnPower * turnSensitivity);
            wheels[1].turn(turnSpeed * turnPower * turnSensitivity);

        }
    }



}
