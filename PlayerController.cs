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

    private void Awake() 
    {
        InputAction = new PlayerAction();

        InputAction.Player.Move.performed += cntxt => move = cntxt.ReadValue<Vector2>();
        InputAction.Player.Move.canceled += cntxt => move = Vector2.zero;

        InputAction.Player.Jump.performed += cntext => Jump();

        InputAction.Player.Look.performed += cntxt => rotate = cntxt.ReadValue<Vector2>();
        InputAction.Player.Look.canceled += cntxt => rotate = Vector2.zero;

        InputAction.Player.Shooting.performed += cntext => Shoot();

        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

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
        Debug.Log("Jump Boi");
    }

    private void Shoot()
    {

    }

}
