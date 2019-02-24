using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextNameMap : MonoBehaviour
{
    //this is your object that you want to have the UI element hovering over
    public GameObject WorldObject;
    
    //this is the ui element
    private RectTransform MinMapCanvasRect;
    // public GameObject UI_Element1;

    public Camera MinMapCam;

    private void Start()
    {
        MinMapCanvasRect = GetComponentInParent<RectTransform>();
    }
    void LateUpdate()
    {
        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5f to get the correct position.
        Vector3 ScreenPosition = MinMapCam.WorldToScreenPoint(WorldObject.transform.position);

        /*
        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * MinMapCanvasRect.sizeDelta.x) - (MinMapCanvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * MinMapCanvasRect.sizeDelta.y) - (MinMapCanvasRect.sizeDelta.y * 0.5f)));
        */



        //now you can set the position of the ui element
        Debug.Log("Before: " + ScreenPosition + ", " + gameObject.name);
        gameObject.GetComponent<RectTransform>().position = new Vector3(ScreenPosition.x / (MinMapCam.orthographicSize * 2 * MinMapCam.aspect) * MinMapCanvasRect.rect.width + (Camera.main.pixelWidth - MinMapCanvasRect.rect.width) / 2, ScreenPosition.y, 0);
        Vector3 afterVector = new Vector3(gameObject.transform.parent.GetComponent<RectTransform>().rect.width / 2 + ScreenPosition.x / MinMapCam.scaledPixelWidth * gameObject.transform.parent.GetComponent<RectTransform>().rect.width, ScreenPosition.y / MinMapCam.scaledPixelHeight * gameObject.transform.parent.GetComponent<RectTransform>().rect.height, 0);
        // gameObject.GetComponent<RectTransform>().position = afterVector;

        /*
        if (gameObject.GetComponent<RectTransform>().localPosition.x < 700 && gameObject.GetComponent<RectTransform>().localPosition.x > -700 && gameObject.GetComponent<RectTransform>().localPosition.y < 600 && gameObject.GetComponent<RectTransform>().localPosition.y > -600)
        {
            gameObject.GetComponent<Text>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Text>().enabled = false;
        }
        */
        Debug.Log("After: " + afterVector);


    }
}