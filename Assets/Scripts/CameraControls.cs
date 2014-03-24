using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

    public Transform cam;
    public float minRotAng = Mathf.PI / 8;
    public float maxCamDist = 0;
    public float dist;
    public Vector3 prevPos;
    //public Vector3 desiredPos;
    Vector3 PlayerToCam { get { return transform.position - cam.position; } }

	// Use this for initialization
	void Start () {
        if (cam == null)
        {
            cam = Camera.main.transform;
            if (cam == null)
            {
                Debug.LogError("Camera not found!");
                Application.Quit();
            }
        }

        maxCamDist = PlayerToCam.magnitude;
        dist = maxCamDist;
        prevPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float X = 0;
        float Y = 0;

        if (Input.GetJoystickNames().Length > 0)
        {
            X = Input.GetAxisRaw("J_Horizontal");
            Y = Input.GetAxisRaw("J_Vertical");
        }
        else
        {
            X = Input.GetAxisRaw("Mouse X");
            Y = -Input.GetAxisRaw("Mouse Y");
        }
        
        cam.RotateAround(transform.position, Vector3.up, minRotAng * X);
        cam.RotateAround(transform.position, cam.right, minRotAng * Y);
        //cam.LookAt(transform);
        //cam.rotation = Quaternion.LookRotation(-PlayerToCam, Vector3.up);

        Vector3 desiredPos = transform.position - prevPos;
        //desiredPos = PlayerToCam.normalized * dist;
        prevPos = transform.position;
        //cam.position = transform.position + desiredPos;
        cam.position += desiredPos;
        //Debug.Log(desiredPos.magnitude);
	}
}
