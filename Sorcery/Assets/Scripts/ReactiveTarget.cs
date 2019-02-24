using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour {

	void Start() {
	}

	public void ReactToHit() {
		StartCoroutine(Die());
	}

	private IEnumerator Die() {
		this.transform.Rotate(-75, 0, 0);

		yield return new WaitForSeconds(1.5f);

		Destroy(this.gameObject);

	}
}