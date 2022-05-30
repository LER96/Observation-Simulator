using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovment1Test : MonoBehaviour
{
    float mouseX;
    float mouseY;
    public float sensitivityX = 5;
    public float sensitivityY = 5;
    public Transform body;
    TargertJson save;
    string json;

    float xrotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //movment// left / right
        if(Input.GetKeyDown(KeyCode.Keypad4))
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
            //save = new TargertJson();
            //save.cam = transform.localRotation;
            //save.mainBody = body.localRotation;
            //json = JsonUtility.ToJson(save);
            //Debug.Log(json);

        }

        //LoadTarget
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //TargertJson loadeRotationData = JsonUtility.FromJson<TargertJson>(json);
            //transform.localRotation = loadeRotationData.cam;
            //body.localRotation = loadeRotationData.mainBody;
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

    void Save(int num)
    {
        save = new TargertJson();
        save.cam.Add(transform.localRotation);
        save.mainBody.Add(body.localRotation);
        json = JsonUtility.ToJson(save);
        Debug.Log(json);
    }

    void Load(int num)
    {
        TargertJson loadeRotationData = JsonUtility.FromJson<TargertJson>(json);
        transform.localRotation = loadeRotationData.cam[num-1];
        body.localRotation = loadeRotationData.mainBody[num-1];
    }

    private class TargertJson
    {
        public List <Quaternion> cam;
        public List <Quaternion> mainBody;
    }
}
