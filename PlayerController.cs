using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerAction InputAction;
    Vector2 move;
    Vector2 rotate;
    Rigidbody rb;

    private float distanceToGround;
    bool isGrounded;
    public float jump = 5f;
    public float walkSpeed = 5f;
    public Camera playerCamera;
    Vector3 cameraRotation;

    private Animator playerAnimator;
    private bool isWalking = false;

    public GameObject projectile;
    public Transform projectilePos;

    //testing health
    CharacterStats cs;

    private void Awake() 
    {
        InputAction = new PlayerAction();

        InputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        InputAction.Player.Move.canceled += cntxt => move = Vector2.zero;

        InputAction.Player.Jump.performed += cntext => Jump();

        InputAction.Player.Look.performed += cntxt => rotate = cntxt.ReadValue<Vector2>();
        InputAction.Player.Look.canceled += cntxt => rotate = Vector2.zero;

        InputAction.Player.Shooting.performed += cntext => Shoot();

        //test health
        cs = GetComponent<CharacterStats>();
        InputAction.Player.TakeDamage.performed += cntext => cs.TakeDamage(5);

        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        distanceToGround = GetComponent<Collider>().bounds.extents.y;

        cameraRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable() 
    {
        InputAction.Player.Enable();
    }

    //private void Start() {
    //}

    //private void FixedUpdate() {
    //}

    private void Update() 
    {
        cameraRotation = new Vector3(cameraRotation.x + rotate.y, cameraRotation.y + rotate.x, cameraRotation.z); 
        transform.eulerAngles = new Vector3(transform.rotation.x, cameraRotation.y, transform.rotation.z);

        transform.Translate(Vector3.forward * move.y * Time.deltaTime * walkSpeed, Space.Self);
        transform.Translate(Vector3.right * move.x * Time.deltaTime * walkSpeed, Space.Self);

        isGrounded = Physics.Raycast(transform.position, -Vector3.up, distanceToGround);
    }

    private void LateUpdate() 
    {
        //playerCamera.transform.eulerAngles = new Vector3(cameraRotation.x, cameraRotation.y, cameraRotation.z);
        playerCamera.transform.rotation = Quaternion.Euler(cameraRotation);
    }

    private void OnDisable() 
    {
        InputAction.Player.Disable();
    }

    private void Jump() 
    {
        if(isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jump, rb.velocity.z);
            Debug.Log("Jump Boi");
        }
    }

    private void Shoot()
    {
        Rigidbody rbBullet = Instantiate(projectile, projectilePos.position, Quaternion.identity).GetComponent<Rigidbody>();
        rbBullet.AddForce(Vector3.forward * 32f, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -Vector3.up * distanceToGround);
    }
}
