using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PicShow : MonoBehaviour
{
    [SerializeField] RenderTexture PhoneTexture;

    public Image image;
    public Image invetory;
    private Texture2D photoCapture;
    public Camera phoneCamera;
    CamMovment1 press;
    //private bool viewingPhoto = true;


    public void Update()
    {
        //yield return new WaitForEndOfFrame();

        var prevRenderTexture = RenderTexture.active;
        RenderTexture.active = PhoneTexture;

        photoCapture = new Texture2D(PhoneTexture.width, PhoneTexture.height);
        photoCapture.ReadPixels(new Rect(0, 0, PhoneTexture.width, PhoneTexture.height), 0, 0);
        photoCapture.Apply();


        Sprite photoSprite = Sprite.Create(photoCapture,
        new Rect(0.0f, 0.0f, photoCapture.width, photoCapture.height),
        new Vector2(0.5f, 0.5f), 100.0f);
        image.sprite = photoSprite;


        RenderTexture.active = prevRenderTexture;

        if(press)
        {
            CapturePhoto();
        }
        //StartCoroutine(CapturePhoto());
    }

    public void CapturePhoto()
    {
        //viewingPhoto = true;
        //yield return new WaitForEndOfFrame();

        var prevRenderTexture = RenderTexture.active;
        RenderTexture.active = PhoneTexture;

        photoCapture = new Texture2D(PhoneTexture.width, PhoneTexture.height);
        photoCapture.ReadPixels(new Rect(0, 0, PhoneTexture.width, PhoneTexture.height), 0, 0);
        photoCapture.Apply();


        Sprite photoSprite = Sprite.Create(photoCapture,
        new Rect(0.0f, 0.0f, photoCapture.width, photoCapture.height),
        new Vector2(0.5f, 0.5f), 100.0f);
        invetory.sprite = photoSprite;


        RenderTexture.active = prevRenderTexture;
    }


}
