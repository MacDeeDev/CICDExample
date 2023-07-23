using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    //Game Objects for Reference Data

    public GameObject mainCamera;
    public TextMeshProUGUI  centreText;

    private string posX, posY, posZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        posX = mainCamera.transform.position.x.ToString("0.00");
        posY = mainCamera.transform.position.y.ToString("0.00");
        posZ = mainCamera.transform.position.z.ToString("0.00");

        centreText.text = string.Format("X: {0} | Y: {1} | Z: {2}", posX, posY, posZ);
        

    }
}
