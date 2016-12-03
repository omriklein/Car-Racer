using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {

    public Wheel[] wheels;
    public float speed = 500f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(wheels.Length >= 2)
        {
            float torque = Input.GetAxis("Vertical") * speed;
            wheels[wheels.Length - 1].move(torque);
            wheels[wheels.Length - 2].move(torque);

        }
    }
}
