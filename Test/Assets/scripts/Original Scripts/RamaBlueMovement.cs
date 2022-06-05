using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class RamaBlueMovement : MonoBehaviour
{

    //Input Detecters and 
    private float horizontalMovement;
    private float verticalMovement;
    //What is it { [SerializeField] private } It makes private so others cannot access it BUT you can still change the values in the inspector
    //Instead of using Public you should use it instead it keeps the code more protacted

    [SerializeField] private Camera cameraGameobject;

    [Header("Zooming Properties")]
    [SerializeField] private float sensitivity;
    private bool isZooming;
    [SerializeField] private float zoomSize;
    [SerializeField] private float zoomRatio;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private TextMeshProUGUI azimuthText;
    [SerializeField] private TextMeshProUGUI hightText;
    private float azimuth;
    private float hight;


    // Start is called before the first frame update
    void Start()
    {
        isZooming = false;
        zoomSize = 30;
        //Min Max Normlize
        zoomRatio = (zoomSize - 5) / 55;
        sensitivity = minSpeed + zoomRatio * (maxSpeed - minSpeed);
    }

    void Update()
    {

        SetInputs();
        ClampAndRotation();
    }

    private void SetInputs()
    {
        //Setting the value of the X axises movement according to built in inputs (Numpad 4 for LEFT, Numpad 6 for RIGHT)
        horizontalMovement = Input.GetAxis("Horizontal") * sensitivity * Time.deltaTime;

        //Setting and inverting the Value of the Y Axises movement according to built in inputs (Numpad 8 for UP, Numpad 2 for DOWN)
        verticalMovement -= Input.GetAxis("Vertical") * sensitivity * Time.deltaTime;

        //If the users press + Then start zooming in
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            StartCoroutine(Zoom(1));
        }
        //If the users press - Then start zooming out
        else if (Input.GetKey(KeyCode.KeypadPlus))
        {
            StartCoroutine(Zoom(-1));
        }
    }

    //A function that can add delay in various styles
    IEnumerator Zoom(int zoom)
    {
        
        if (!isZooming)
        {
            //Disable the option to zoom multiple time in a row
            isZooming = true;

            //Setting the zoom itself in the camera
            zoomSize = cameraGameobject.fieldOfView;

            //Zooming in if the input was to zoom out
            if (zoomSize <= 55 && zoom == 1)
            {
                //Zoom out slowly
                for (int i = 0; i < 5; i++)
                {
                    zoomSize++;
                    cameraGameobject.fieldOfView = zoomSize;
                    yield return new WaitForSeconds(0.02f);
                }
                //Min Max Normlize
                zoomRatio = (zoomSize - 5) / 55;
                sensitivity = minSpeed + zoomRatio * (maxSpeed - minSpeed);
            }
            //Zooming out if the input was to zoom in
            else if (zoomSize >= 6 && zoom == -1)
            {
                //Zoom in slowly
                for (int i = 0; i < 5; i++)
                {
                    zoomSize--;
                    cameraGameobject.fieldOfView = zoomSize;
                    yield return new WaitForSeconds(0.02f);
                }
                //Min Max Normlize
                zoomRatio = (zoomSize - 5) / 55;
                sensitivity = minSpeed + zoomRatio * (maxSpeed - minSpeed);
            }
            //Realsing the option to zoom again
            isZooming = false;
        }
      
    }
    
    private void ClampAndRotation()
    {
        //Sets the azimuth value in a float
        azimuth = gameObject.transform.localEulerAngles.y;

        //Inverts the amount of hight and add 360 to its (to display +10 instead of +350)
        if ((azimuth >= 285) && (azimuth <= 360))
        {
            azimuth -= 360;
        }

        //Display the azimuth in the UI          Round up the float to 2 decimal
        azimuthText.text = "Azimuth: " + azimuth.ToString("#0.00");

        //Sets the hight and round it up
        hight = -cameraGameobject.transform.localEulerAngles.x;

        //Inverts the amount of hight and add 360 to its (to display +10 instead of +350)
        if ((hight <= -350) && (hight >= -360))
        {
            hight += 360;
        }

        //Display the hight in the UI       Round up the float to 2 decimal
        hightText.text = "Hight: " + hight.ToString("#0.00");

        //Rotating the head
        transform.Rotate(0, horizontalMovement, 0);

        Vector3 eulerRotation = transform.eulerAngles;
        if (eulerRotation.y > 180)
        {
            eulerRotation.y -= 360;
        }

        //Clamping the values according to BODY values (Left map Side clamp, Right map side clamp)
        eulerRotation.y = Mathf.Clamp(eulerRotation.y, -75, 10);
        //Clampong the values according to CAMERA values (Down map side clamp, Up map side clamp)
        verticalMovement = Mathf.Clamp(verticalMovement, -10, 40);

        //Rotating the camera
        transform.rotation = Quaternion.Euler(eulerRotation);
        cameraGameobject.transform.localRotation = Quaternion.Euler(verticalMovement, 0, 0);
    }
}
