using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    //Game Objects for Reference Data
    public GameObject mainCamera;
    
    //Position Variables
    public TextMeshProUGUI  tmpXPos, tmpYPos, tmpZPos;
    private string posX, posY, posZ, FPS;

    //Frame Rate Variables
    public TextMeshProUGUI  tmpFPS;
    private float frameDuration = 0f;
    private float fpsCalcCooldown = 0.5f;
    private float fpsCalcTimeSinceLastUpdate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        SetTextPositionValues();
        SetTextFPSValue();
    }


    //FPS Handling

    private void SetTextFPSValue()
    {
        //Refresh FPS value every 2 seconds
        fpsCalcTimeSinceLastUpdate += Time.deltaTime;

        if  (fpsCalcTimeSinceLastUpdate >= fpsCalcCooldown)
        {
            frameDuration = 1f / Time.deltaTime;
            
            FPS = frameDuration.ToString("0");
            tmpFPS.text = string.Format(FPS);
            
            fpsCalcTimeSinceLastUpdate = 0f;
        }

    }

    //Position Handling
    private void SetTextPositionValues()
    {
        //Get X, Y, Z coordinates from camera
        posX = mainCamera.transform.position.x.ToString("0.0");
        posY = mainCamera.transform.position.y.ToString("0.0");
        posZ = mainCamera.transform.position.z.ToString("0.0");
        
        //Set Text Mesh Pro (TMP) text values
        tmpXPos.text = string.Format(posX);
        tmpYPos.text = string.Format(posY);
        tmpZPos.text = string.Format(posZ);

    }


}
