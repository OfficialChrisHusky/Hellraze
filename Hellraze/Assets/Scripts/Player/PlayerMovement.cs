using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 1f;
    [SerializeField] Transform orientation;

    [Header("Movement")]
    public float moveSpeed = 6f;
    float movementMultiplier = 10f;
    [SerializeField] float airMultiplier = 0.4f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float acceleration;

    [Header("Crouchin")]
    [SerializeField] bool isCrouching;
    public float crouchSpeedMultiplier;
    [SerializeField] float crouchHeightMultiplier = 0.5f;

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Key Bindings")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;






    float horizontalMovement;
    float verticalMovement;
    
    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 0.5f;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    bool isGrounded;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight/2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (Player.instance.canMove) {

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            MyInput();
            ControlDrag();
            ControlSpeed();

            if (Input.GetKeyDown(jumpKey) && isGrounded) {
                Jump();
            }

            if(Input.GetKeyDown(crouchKey) && !isCrouching) {

                Crouch();
                isCrouching = true;

            } else if (Input.GetKeyUp(crouchKey) && isCrouching) {

                Crouch();
                isCrouching = false;

            }

            slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        }
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ControlSpeed()
    {
        if(Input.GetKey(sprintKey) && isGrounded && !isCrouching)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if(isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }

        else if(!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    void Crouch() {

        if (isCrouching) {

            rb.transform.localScale = new Vector3(1, rb.transform.localScale.y / crouchHeightMultiplier, 1);
            movementMultiplier /= crouchSpeedMultiplier;

        } else {

            rb.transform.localScale = new Vector3(1, rb.transform.localScale.y * crouchHeightMultiplier, 1);
            movementMultiplier *= crouchSpeedMultiplier;

        }

    }

}
