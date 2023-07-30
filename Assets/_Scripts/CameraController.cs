using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    

    //CLASS VARIABLES
    public new Camera camera;
    public FixedJoystick moveJoystick;
    public FixedJoystick lookJoystick;
    public float sensitivity = 0.25f; 
    public float speed = 10.0f;

    private float horizontalMultiplier;
    private float verticleMultiplier; 
    private float xRotation;
    private float yRotation;
    


    // Start is called before the first frame update
    void Start()
    {
        //TO DO: show Matt difference between hard-wiring vs. getting on start
        //GET: Camera component from game object of this class
        camera = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
    }
    
    //CLEAN UP:  commments, variable naming
    private void Look()
    {
        if (lookJoystick.Horizontal != 0 || lookJoystick.Vertical != 0)
        {
            xRotation = lookJoystick.Vertical * -1;
            yRotation = lookJoystick.Horizontal;

            Vector2 wantedVelocity = new Vector2(xRotation, yRotation) * sensitivity;
        
            wantedVelocity += wantedVelocity * Time.deltaTime;

            transform.localEulerAngles += new Vector3 (wantedVelocity.x, wantedVelocity.y, 0);
        }   
    }

    //MOVE: the camera left to right, forward and back
    private void Move()
    {
        verticleMultiplier = VerticalMultiplier();
        horizontalMultiplier = HorizontalMultiplier();

        //move forward 
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || moveJoystick.Vertical > 0 )
        {
            gameObject.transform.position += transform.forward * (speed * verticleMultiplier) * Time.deltaTime;
        }

        //move left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || moveJoystick.Horizontal < 0)
        {
            gameObject.transform.position += -transform.right * (speed * horizontalMultiplier) * Time.deltaTime;
        }

        //move back
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || moveJoystick.Vertical < 0)
        {
            gameObject.transform.position += -transform.forward * (speed * verticleMultiplier) * Time.deltaTime;
        }

        //move right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || moveJoystick.Horizontal > 0)
        {
            gameObject.transform.position += transform.right * (speed * horizontalMultiplier) * Time.deltaTime;
        }
    }
    
    //allows for dynamic fwd/backward joystick movement as well as keyboard movement
    private float VerticalMultiplier ()
    {
        if (moveJoystick.Vertical == 0)
        {
            return 1;
        }
            
        else
        {
            return Mathf.Abs(moveJoystick.Vertical);
        }
    }
    
    //allows for dynamic left/right joystick movement as well as keyboard movement
    private float HorizontalMultiplier ()
    {
        if (moveJoystick.Horizontal == 0)
        {
            return 1;
        }
            
        else
        {
            return Mathf.Abs(moveJoystick.Horizontal);
        }
    }


}
