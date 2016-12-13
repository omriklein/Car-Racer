using UnityEngine;
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
    private float turnSensitivity = 3f;

    //DEBUGING
    public Text gyroText;
    public Text accText;

    void Awake()
    {
        rbody = this.GetComponent<Rigidbody>();

        if (MenuScript.GP == GamePlatform.Android || MenuScript.GP == GamePlatform.Unknown)
        {
            StartZValue = Input.acceleration.x;
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
            if (MenuScript.GP == GamePlatform.Android)
            {
                switch (Input.touchCount)
                {
                    case 1:
                        torque = torqueMaxValue * motorPower; break;
                    case 2:
                        torque = -1 * torqueMaxValue * motorPower; break;
                    default:
                        torque = 0; break;
                }

                accText.text = turnSpeed + ", ";
                gyroText.text = turnSpeed * turnPower * turnSensitivity + ", ";

                turnSpeed = Mathf.Clamp(turnSpeed - ((StartZValue) - (Input.acceleration.x)), -0.5f, 0.5f);
                StartZValue = Input.acceleration.x;
            }
            else
            {
                torque = Input.GetAxis("Vertical") * motorPower;
                turnSpeed = Input.GetAxis("Horizontal") * turnPower;
            }
            if (torque != 0)
            {
                this.GetComponent<Rigidbody>().drag = 0;

                //Back Wheels Drive
                wheels[wheels.Length - 1].move(torque);
                wheels[wheels.Length - 2].move(torque);
            }
            else
            {
                this.GetComponent<Rigidbody>().drag = 7;
            }

            //Front Wheels Steer
            wheels[0].turn(turnSpeed * turnPower * turnSensitivity);
            wheels[1].turn(turnSpeed * turnPower * turnSensitivity);

        }
    }

}
