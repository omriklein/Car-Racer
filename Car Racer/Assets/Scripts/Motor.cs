using UnityEngine;
using System.Collections;

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
    public float motorPower = 500f;
    public float turnPower = 500f;
    public Transform centerOfMass;

    /*for the tricks in game
   public static bool isGruonded = true;
   public float jumpForce = 100000f;
   public float trickRotationAngle = 10;
   */

    private Rigidbody rbody;

    void Awake()
    {
        rbody = this.GetComponent<Rigidbody>();
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
            float torque = Input.GetAxis("Vertical") * motorPower;
            float turnSpeed = Input.GetAxis("Horizontal") * turnPower;

            if (torque != 0)
            {
                this.GetComponent<Rigidbody>().drag = 0;

                //Back Wheels Drive
                wheels[wheels.Length - 1].move(torque);
                wheels[wheels.Length - 2].move(torque);
            }
            else
            {
                this.GetComponent<Rigidbody>().drag = 1;
            }

            //Front Wheels Steer
            wheels[0].turn(turnSpeed);
            wheels[1].turn(turnSpeed);

        }
    }

}
