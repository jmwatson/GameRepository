using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    private CharacterController cc;

	// Use this for initialization
	void Start ()
    {
        cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
