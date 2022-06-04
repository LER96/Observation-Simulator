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
    public int counter;
    public bool ispress;

    [Header("Objects")]
    public Transform body;
    //public Camera cameraGameobject;

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
        Cursor.lockState = CursorLockMode.Locked;
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ispress = true;
            AddTarget();
        }
        ispress = false;
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

    public void AddTarget()
    {
        save.cam.Add(transform.localRotation);
        save.mainBody.Add(body.localRotation);
        save.name = ""+counter;
        json = JsonUtility.ToJson(save);
        Debug.Log(json);
    }

    public void GiveTarget()
    {
        TargertJson loadeRotationData = JsonUtility.FromJson<TargertJson>(json);
        transform.localRotation = loadeRotationData.cam[counter];
        body.localRotation = loadeRotationData.mainBody[counter];
    }

    private class TargertJson
    {
        public List<Quaternion> cam;
        public List<Quaternion> mainBody;
        public string name = "";
    }

}
