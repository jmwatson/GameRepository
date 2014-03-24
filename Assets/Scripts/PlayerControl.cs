using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour
{
    public float maxMoveSpeed;
    public float acceleration;
    public float gravity;
    public float jumpSpeed;
    public float slopeEffect;

    CharacterController controller;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Get move axis
        var inputAxis = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        var moveAxis = Camera.main.transform.TransformDirection(inputAxis);
        moveAxis.y = 0;

        //Get delta velocity
        float deltaVelocity = acceleration * Time.deltaTime;
        //deltaVelocity = 1;

        //Speedup and slowdown
        if (moveAxis.magnitude > 0 && flatVelocity.magnitude < maxMoveSpeed)
        {
            //Speedup
            moveAxis.Normalize();
            flatVelocity += moveAxis * deltaVelocity;
        }
        else
        {
            //Slowdown
            if (flatVelocity.magnitude > deltaVelocity)
                flatVelocity -= flatVelocity.normalized * deltaVelocity;
            else
                flatVelocity = Vector3.zero;
        }

        //Gravity
        velocity.y -= gravity * Time.deltaTime;

        //Jumping
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            velocity.y = jumpSpeed;

        //Movement
        controller.Move(velocity * Time.deltaTime);

        //Rotation
        if (flatVelocity.magnitude > 0)
            transform.forward = flatVelocity.normalized;
    }

    public void AddVelocity(Vector3 amount)
    {
        velocity += amount;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        velocity.x *= 1 - Mathf.Abs(hit.normal.x * slopeEffect);
        velocity.y *= 1 - Mathf.Abs(hit.normal.y);
        velocity.z *= 1 - Mathf.Abs(hit.normal.z * slopeEffect);
    }

    public Vector3 flatVelocity
    {
        get { return new Vector3(velocity.x, 0, velocity.z); }
        set
        {
            velocity.x = value.x;
            velocity.z = value.z;
        }
    }
}