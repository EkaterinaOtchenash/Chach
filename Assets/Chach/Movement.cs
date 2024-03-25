using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Movement : NetworkBehaviour
{
    public CharacterController characterController;
    public float speed = 12f;
    public float gravity = -2f;
    Vector3 velocity;
    public Transform GroundCheck;
    public float GroundDistance = 0.57f;
    public LayerMask groundMask;
    bool isTouched;
    public float JumpForce = 800;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
    }
    public override void OnStartLocalPlayer()
    {
        //GetComponent<Material>().color = Color.red;
        base.OnStartLocalPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * y;
        characterController.Move(move * speed * Time.deltaTime);
        velocity.y += gravity + Time.deltaTime;
        characterController.Move((velocity * Time.deltaTime) / 2);
        isTouched = Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);

        if (isTouched && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && isTouched)
        {
            velocity.y = Mathf.Sqrt(JumpForce * -2f * gravity);
        }
    }
}
