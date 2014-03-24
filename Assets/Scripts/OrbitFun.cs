using UnityEngine;
using System.Collections;

public class OrbitFun : MonoBehaviour {

    public Transform target;
    public Vector3 desiredPos;
    //public Vector3 prevPos;
    public Quaternion rot;
    public float maxDist;
    public float dist;
    public float ang = 180 / 8;
    Vector3 player2Obj { get { return transform.position - target.position; } }

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.FindWithTag("Player");
        if (go == null)
        {
            Debug.Log("Couldn't find target!");
            MonoBehaviour.Destroy(this);
        }
        target = go.transform;
        maxDist = 2;
        dist = maxDist;
        desiredPos = Vector3.forward * dist;
        //rot = Quaternion.AngleAxis(15, transform.right);
        //desiredPos = rot * desiredPos;
	}
	
	// Update is called once per frame
    void Update()
    {
        //transform.LookAt(target);
        desiredPos = player2Obj.normalized * dist;
        float tme = Time.deltaTime;
        rot = Quaternion.AngleAxis(22.5f * tme, target.up);// *Quaternion.AngleAxis(ang * tme, transform.right);
        desiredPos = rot * desiredPos;
        transform.position = target.position + desiredPos;
	}
}
