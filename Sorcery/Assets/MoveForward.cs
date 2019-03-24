using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 1.0f;

    private void Start()
    {
        StartCoroutine(disappear());
    }
    void Update ()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);	
	}

    private IEnumerator disappear()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
