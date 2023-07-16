using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //CLASS VARIABLES
    public new Camera camera;
    private float speed = 2.0f;


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

        //MOVE: the camera left to right, up and down, forward and back

        //move up or forward w/shift key
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                gameObject.transform.position += Vector3.forward * speed * Time.deltaTime;
            } else
            gameObject.transform.position += Vector3.up * speed * Time.deltaTime;
        }

        //move left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
        }

        //move down or back w/shift key
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                gameObject.transform.position += Vector3.back * speed * Time.deltaTime;
            }
            else
                gameObject.transform.position += Vector3.down * speed * Time.deltaTime;
        }

        //move right
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }


}
