using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class CarController : MonoBehaviour
{
    public InputManager inManager;
    
    public List<WheelCollider> throttleWheels;
    public List<GameObject> steerWheels;
    
    public GameObject rMesh1;
    public GameObject rMesh2;
    public GameObject rMesh3;
    public GameObject rMesh4;
    
    public GameObject meshes_1;
    public GameObject meshes_2;

    public float strengthCofficient = 20000f;
    public float maxAngle = 30f;

    public Transform CM; //cameracontrol
    public Rigidbody rb;

    private void Start()
    {
        inManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();

        if(CM)
        {
            rb.centerOfMass = CM.position;
        }
    }

    private void Update()
    {
        foreach(WheelCollider wheel  in throttleWheels)
        {
            wheel.motorTorque = strengthCofficient * Time.deltaTime * inManager.throttle;
        }
        foreach(GameObject wheel in steerWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = maxAngle * inManager.steer;
            wheel.transform.localEulerAngles = new Vector3(0f, inManager.steer , 0f);
        }
        
           
        rMesh1.transform.Rotate(0f, 0f, rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? -1 : 1) / (2 * Mathf.PI * 0.15f));
        rMesh2.transform.Rotate(0f, 0f, rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ?  1 :-1) / (2 * Mathf.PI * 0.15f));
        rMesh3.transform.Rotate(0f, 0f, rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ? -1 : 1) / (2 * Mathf.PI * 0.15f));
        rMesh4.transform.Rotate(0f, 0f, rb.velocity.magnitude * (transform.InverseTransformDirection(rb.velocity).z >= 0 ?  1 :-1) / (2 * Mathf.PI * 0.15f));
       
        //wheel.GetComponent<WheelCollider>().steerAngle = maxAngle * inManager.steer;
        meshes_1.transform.localEulerAngles = new Vector3(0f, (inManager.steer * maxAngle) + 90f , 0f);        
        meshes_2.transform.localEulerAngles = new Vector3(0f, (inManager.steer * maxAngle) - 90f , 0f);
       
    }
}
