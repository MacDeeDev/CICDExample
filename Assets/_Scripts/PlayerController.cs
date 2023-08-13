using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    //New Input System
    private PlayerInputActions playerInputActions;  //C# class auto created/updated for the Input Action asset
    
    private InputAction movement; //OnEable: bind movement action from thep layerInputAction object
    private Vector2 currentMovement;
    
    private InputAction rotation;
    private InputAction mouseRotation;
    private Vector2 currentRotationMovement;
    private Vector2 currentMouseRotationMovement;
    
    public bool isMoving   = false;
    public bool isRotating = false;
    
    public GameObject playerBody;
    public new Camera camera;
    public float sensitivity = 0.5f; 
    public float speed = 10.0f;


    //Awake is called before start
    void Awake()
    {
        //Instantiate  a new PlayerInput asset and assign to class variable
        playerInputActions = new PlayerInputActions();

        //Bind the Movement action to class variable and Enable
        playerInputActions.Player.Movement.performed += ctx => {
            currentMovement = ctx.ReadValue<Vector2>();
            isMoving = currentMovement.x != 0 || currentMovement.y != 0;
        };


        //Listen for Player.Movement.cancelled input action; set isMoving to false
        playerInputActions.Player.Movement.canceled += ctx => {
            isMoving = false;
        };

        //Bind the Rotation action to class variable
        playerInputActions.Player.Rotation.performed += ctx => {
            currentRotationMovement = ctx.ReadValue<Vector2>();
            isRotating = true;
        };

        playerInputActions.Player.MouseRotation.performed += ctx => {
            currentMouseRotationMovement = ctx.ReadValue<Vector2>();
            
        };

        //Listen for Player.Rotation.cancelled and set isRotation to false
        playerInputActions.Player.Rotation.canceled += ctx => {
            isRotating = false;
        };

        playerInputActions.Player.MouseRotation.canceled += ctx => {
            isRotating = false;
        };


    }

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
        //Look();
       
       //Debug.Log("Movement: " + movement.ReadValue<Vector2>());
        Debug.Log("Rotation: " + rotation.ReadValue<Vector2>());
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
            Vector2 wantedVelocity = currentRotationMovement * sensitivity;
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
        
        isRotating = true;

        if(isRotating)
       {
            Vector2 wantedVelocity = currentMouseRotationMovement * sensitivity;
            //rotates parent object left & right 
            gameObject.transform.localEulerAngles += new Vector3 (gameObject.transform.rotation.x, wantedVelocity.x * Time.deltaTime, 0);

            // rotates camera up and down
            camera.transform.localEulerAngles += new Vector3 (wantedVelocity.y * Time.deltaTime, 0, 0);
       }

        
    }
    

}
