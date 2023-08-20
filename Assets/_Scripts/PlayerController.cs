using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //CLASS PROPERTIES
    #region Private Class Properties

    //New Input System
    private PlayerInputActions playerInputActions;  //C# class auto created/updated for the Input Action asset
    
    //Movement Properties
    private InputAction movement; //OnEable: bind movement action from thep layerInputAction object
    private Vector2 currentMovement;
    private bool isMoving   = false;
    
    //Rotation Properties
    private InputAction rotation;
    private InputAction mouseRotation;
    private Vector2 currentRotationMovement;
    private Vector2 currentMouseRotationMovement;
    private bool isRotating = false;
        
    //Camera
    private new Camera camera;
    
    #endregion Private Class Properties

    #region Public Class Properties

    public GameObject playerBody;

    [Range(1, 10)]
    public float lookSensitivity = 3.0f;
    [Range(10, 50)]
    public float speed = 25.0f;
    
    #endregion Public Class Properties

    //Awake is called before start
    void Awake()
    {
        //Instantiate  a new PlayerInput asset and assign to class variable
        playerInputActions = new PlayerInputActions();

        //SET INPUT ACTIONS TO METHODS (methods do not have brackets when assigned like this)
        
        //Listen for Movement Performed 
        playerInputActions.Player.Movement.performed += SetActionPlayerMovementPerformed;

        //Listen for Player Movement Cancelled
        playerInputActions.Player.Movement.canceled += SetActionPlayerMovementCanceled; 

        //Listen for Rotation Performed
        playerInputActions.Player.Rotation.performed += SetPlayerRotationPerformed; 
        
        //Listen for Mouse Rotation Performed
        playerInputActions.Player.MouseRotation.performed += SetPlayerMouseRotationPerformed; 

        //Listen for Player Rotation Cancelled
        playerInputActions.Player.Rotation.canceled += SetPlayerRotationCancelled;

        //Listen for Mouse Rotation Cancelled
        playerInputActions.Player.MouseRotation.canceled += SetPlayerMouseRotationCancelled;

    }

    //Listener Setup Methods
    #region Set Input Action Event Listeners 
    
    private void SetActionPlayerMovementPerformed(InputAction.CallbackContext ctx)
    {
        currentMovement = ctx.ReadValue<Vector2>();
        isMoving = currentMovement.x != 0 || currentMovement.y != 0;
    }

    private void SetActionPlayerMovementCanceled(InputAction.CallbackContext ctx)
    {
        isMoving = false;
    }

    private void SetPlayerRotationPerformed(InputAction.CallbackContext ctx)
    {
        currentRotationMovement = ctx.ReadValue<Vector2>();
        isRotating = true;
    }

    private void SetPlayerMouseRotationPerformed(InputAction.CallbackContext ctx)
    {
        currentMouseRotationMovement = ctx.ReadValue<Vector2>();
    }

    private void SetPlayerRotationCancelled(InputAction.CallbackContext ctx)
    {
        isRotating = false;
    }

    private void SetPlayerMouseRotationCancelled(InputAction.CallbackContext ctx)
    {
        isRotating = false;
    }

    #endregion Set Input Action Event Listeners

    //OnEnable is called wheen the component is toggled to true 
    void OnEnable()
    {
        movement = playerInputActions.Player.Movement;
        movement.Enable();

        rotation = playerInputActions.Player.Rotation;
        rotation.Enable();

        mouseRotation = playerInputActions.Player.MouseRotation;
        mouseRotation.Enable();

    }

    //OnDisable is called when the component is toggled to false
    void OnDisable()
    {
        //Disable 
        movement.Disable();
        rotation.Disable();
        mouseRotation.Disable();

    }

    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleMouseRotation();
       
        //Debug.Log("Movement: " + movement.ReadValue<Vector2>());
        //Debug.Log("Rotation: " + rotation.ReadValue<Vector2>());
    }

    private void HandleMovement()
    {
        if(isMoving)
        {
            gameObject.transform.position += transform.forward * speed * currentMovement.y * Time.deltaTime;
            gameObject.transform.position += transform.right * speed * currentMovement.x * Time.deltaTime;
        }
    }
    
    private void HandleRotation()
    {

       if(isRotating)
       {
            Vector2 wantedVelocity = currentRotationMovement * lookSensitivity;
            //rotates parent object left & right 
            gameObject.transform.localEulerAngles += new Vector3 (gameObject.transform.rotation.x, wantedVelocity.x * Time.deltaTime, 0);

            // rotates camera up and down
            camera.transform.localEulerAngles += new Vector3 (wantedVelocity.y * Time.deltaTime, 0, 0);
        }
    }
    
    private void HandleMouseRotation()
    {
        //IF: the left mouse button isn't pressed, exit the method
        if(!Mouse.current.leftButton.isPressed)
        return;
        
        Vector2 wantedVelocity = currentMouseRotationMovement * lookSensitivity;
        
        //rotates parent object left & right 
        gameObject.transform.localEulerAngles += new Vector3 (gameObject.transform.rotation.x, wantedVelocity.x * Time.deltaTime, 0);

        // rotates camera up and down
        camera.transform.localEulerAngles += new Vector3 (wantedVelocity.y * Time.deltaTime, 0, 0);
        
    }

}
