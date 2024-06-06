using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] Camera playerCamera;

    float moveX, moveZ;
    Rigidbody rigidBody;
    Animator animator;

    bool onGround;
    bool isRunning;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        animator.SetBool("isWalking", moveZ != 0);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            speed *= 2;
            animator.SetBool("isRunning", isRunning);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            speed /= 2;
            animator.SetBool("isRunning", isRunning);
        }

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        
        Vector3 movement = new Vector3(0, 0, moveZ).normalized * speed;
        Vector3 newVelocity = transform.forward * movement.z + new Vector3(0, rigidBody.velocity.y, 0);
        rigidBody.velocity = newVelocity;

      
        if (moveX != 0)
        {
            float rotation = moveX * rotationSpeed * Time.deltaTime;
            Quaternion turn = Quaternion.Euler(0f, rotation, 0f);
            rigidBody.MoveRotation(rigidBody.rotation * turn);
        }
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            animator.SetTrigger("jumped");
        }
    }

    bool IsGrounded()
    {
        // Use a raycast to check if the player is on the ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            return true;
        }
        return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1.2f);
    }
}
