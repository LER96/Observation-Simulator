using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovment : MonoBehaviour
{
    float mouseX;
    float mouseY;
    public float sensitivityX = 5;
    public float sensitivityY = 5;
    public float inc = 25;
    public Transform body;
    TargertJson save;
    string json;

    float xrotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        save = new TargertJson();
    }

    private void Update()
    {
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

    private class TargertJson
    {
        public Quaternion []cam= new Quaternion[5];
        public Quaternion []mainBody= new Quaternion[5];
    }

}
