using UnityEngine;
using System.Collections;
public class TouchControl : MonoBehaviour {

	public AudioSource mew;
	public AudioSource end;
	public AudioSource mid;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		Touch finger = Input.GetTouch(0);
		if (finger.phase == TouchPhase.Began) {
			//if( Input. GetMouseButtonDown(0)){
			mew.PlayOneShot (mew.clip);
		}
		if (finger.phase == TouchPhase.Moved) {
			mid.PlayOneShot (mid.clip);
		}
		if (finger.phase == TouchPhase.Ended) {
			end.PlayOneShot (end.clip);
		}
	}
}
