using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
	public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
	public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
	public float panSpeed = 0.1F;

	public float maxZoom = 60f; //farthest you can get
	public float minZoom = 50f; //closest you can get

	private Camera cam;

	void Start(){
		cam = Camera.main;
	}

	void Update()
	{

		//  Pan Camera
		if (Input.touchCount == 3 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			transform.Translate(-touchDeltaPosition.x * panSpeed, -touchDeltaPosition.y * panSpeed, 0);
		}

		// Zoom Camera
		// If there are two touches on the device...
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			// If the camera is orthographic...
			if (cam.orthographic)
			{

//				float i = cam.orthographicSize + deltaMagnitudeDiff * orthoZoomSpeed;
//
//				if( i >= maxZoom){ //if i is greater than maxZoom, set camera to max
//					cam.orthographicSize = maxZoom;
//				}
//				else if ( i <= minZoom){ //same but with min
//					cam.orthographicSize = minZoom;
//				}
//				else{ //otherwise just update it to i
//					cam.orthographicSize = i;
//				}

			//	 ... change the orthographic size based on the change in distance between the touches.
				cam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

//				 Make sure the orthographic size never drops below zero.
				cam.orthographicSize = Mathf.Max(cam.orthographicSize, 0.1f);
			}
			else
			{

				float i = cam.fieldOfView + deltaMagnitudeDiff * perspectiveZoomSpeed;

				if( i >= maxZoom){ //if i is greater than maxZoom, set camera to max
					cam.fieldOfView = maxZoom;
				}
				else if ( i <= minZoom){ //same but with min
					cam.fieldOfView = minZoom;
				}
				else{ //otherwise just update it to i
					cam.fieldOfView = i;
				}

				// Otherwise change the field of view based on the change in distance between the touches.
		//		cam.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

				// Clamp the field of view to make sure it's between 0 and 180.
		//		cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 0.1f, 179.9f);
			}
		}
	}

    /*
    private void FixedUpdate()
    {
        //  camera updating framerate at the same rate as player
        desiredPosition = lookAt.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(lookAt.position + Vector3.up);
    }
    */
}
