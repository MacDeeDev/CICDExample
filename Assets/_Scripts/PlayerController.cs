using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    //New Input System
    private PlayerInputActions playerInputActions;  //C# class auto created/updated for the Input Action asset
    private InputAction movement; //OnEable: bind movement action from thep layerInputAction object
    public Vector2 currentMovement;
    public bool isMoving   = false;
    
    public GameObject playerBody;
    public new Camera camera;
    public FixedJoystick moveJoystick;
    public FixedJoystick lookJoystick;
    public float sensitivity = 0.5f; 
    public float speed = 10.0f;

    private float xRotation;
    private float yRotation;

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


    }

    //OnEnable is called wheen the component is toggled to true 
    void OnEnable()
    {
        movement = playerInputActions.Player.Movement;
        movement.Enable();
    }

    //OnDisable is called when the component is toggled to false
    void OnDisable()
    {
        //Disable 
        movement.Disable();

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
        Look();
       
       //Debug.Log("Movement: " + movement.ReadValue<Vector2>());
    }

    private void Look()
    {
        if (lookJoystick.Horizontal != 0 || lookJoystick.Vertical != 0)
        {
            xRotation = lookJoystick.Vertical * -1;
            yRotation = lookJoystick.Horizontal;

            Vector2 wantedVelocity = new Vector2(xRotation, yRotation) * sensitivity;
        
            wantedVelocity += wantedVelocity * Time.deltaTime;

            //rotates parent object left & right 
            gameObject.transform.localEulerAngles += new Vector3 (gameObject.transform.rotation.x, wantedVelocity.y, 0);

            // rotates camera up and down
            camera.transform.localEulerAngles += new Vector3 (wantedVelocity.x, 0, 0);
        }   
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

    }
    

    

}
