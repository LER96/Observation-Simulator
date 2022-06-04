using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CamMovment : MonoBehaviour
{
    float mouseX;
    float mouseY;
    string json;
    TargertJson save;

    [Header("Objects")]
    public Transform body;
    public Camera cameraGameobject;

    private bool isZooming;
    [SerializeField] private float zoomSize;
    [SerializeField] private float zoomRatio;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    [Header("Movement Speed")]
    public float sensitivityX = 5;
    public float sensitivityY = 5;
    public float inc = 25;
    float xrotation;


    [Header ("Movement TEXT")]
    public Text side;
    public Text top;




    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        save = new TargertJson();
    }

    private void Update()
    {
        side.text = "" + sensitivityX * 10;
        top.text = "" + sensitivityY * 10;
        if(Input.GetKeyDown(KeyCode.RightArrow) && sensitivityX<700)
        {
            sensitivityX += inc;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && sensitivityX>0)
        {
            sensitivityX -= inc;
        }
        if (sensitivityX <= 0)
        {
            sensitivityX = 0;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && sensitivityY<500)
        {
            sensitivityY += inc;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && sensitivityY>0)
        {
            sensitivityY -= inc;
        }
        if (sensitivityY <= 0)
        {
            sensitivityY = 0;
        }
        //movment// left / right
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Move(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Move(1, 0);
        }
        //movement// up/down
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            Move(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Move(0, -1);
        }

        //movement// sideways
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            Move(1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            Move(-1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Move(1, -1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Move(-1, -1);
        }

        //SaveTarget
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddTarget(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddTarget(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddTarget(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddTarget(4);
        }

        //LoadTarget
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GiveTarget(1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            GiveTarget(2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GiveTarget(3);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GiveTarget(4);
        }

        //Zoomin/out
        if (Input.GetKey(KeyCode.KeypadMultiply))
        {
            StartCoroutine(Zoom(1));
        }
        //If the users press / Then start zooming out
        else if (Input.GetKey(KeyCode.KeypadDivide))
        {
            StartCoroutine(Zoom(-1));
        }


    }
    void Move(int x, int y)
    {
        mouseX = x * sensitivityX * Time.deltaTime;
        mouseY = y * sensitivityY * Time.deltaTime;

        xrotation -= mouseY;
        xrotation = Mathf.Clamp(xrotation, -60f, 60f);

        transform.localRotation = Quaternion.Euler(xrotation, 0, 0);
        body.Rotate(Vector3.up * mouseX);
    }

    void AddTarget(int num)
    {
        int num1 = num - 1;
        save.cam[num1] = transform.localRotation;
        save.mainBody[num1] = body.localRotation;
        json = JsonUtility.ToJson(save);
        Debug.Log(json);
    }

    void GiveTarget(int num)
    {
        int num1 = num - 1;
        TargertJson loadeRotationData = JsonUtility.FromJson<TargertJson>(json);
        transform.localRotation = loadeRotationData.cam[num1];
        body.localRotation = loadeRotationData.mainBody[num1];
    }


    IEnumerator Zoom(int zoom)
    {
        if (!isZooming)
        {
            //Disable the option to zoom multiple time in a row
            isZooming = true;

            //Setting the zoom itself in the camera
            zoomSize = cameraGameobject.fieldOfView;

            //Zooming in if the input was to zoom in
            if (zoomSize <= 55 && zoom == 1)
            {
                //Zoom in slowly
                for (int i = 0; i < 5; i++)
                {
                    zoomSize++;
                    cameraGameobject.fieldOfView = zoomSize;
                    yield return new WaitForSeconds(0.02f);
                }
               /* //Min Max Normlize
                zoomRatio = (zoomSize - 5) / 55;
                sensitivityX = minSpeed + zoomRatio * (maxSpeed - minSpeed);
                sensitivityY = minSpeed + zoomRatio * (maxSpeed - minSpeed);*/
            }
            //Zooming out if the input was to zoom out
            else if (zoomSize >= 6 && zoom == -1)
            {
                //Zoom in slowly
                for (int i = 0; i < 5; i++)
                {
                    zoomSize--;
                    cameraGameobject.fieldOfView = zoomSize;
                    yield return new WaitForSeconds(0.02f);
                }
              /*  //Min Max Normlize
                zoomRatio = (zoomSize - 5) / 55;
                sensitivityX = minSpeed + zoomRatio * (maxSpeed - minSpeed);
                sensitivityY = minSpeed + zoomRatio * (maxSpeed - minSpeed);*/
            }
            //Realsing the option to zoom again
            isZooming = false;
        }

    }


    private class TargertJson
    {
        public Quaternion []cam= new Quaternion[5];
        public Quaternion []mainBody= new Quaternion[5];
    }

}
