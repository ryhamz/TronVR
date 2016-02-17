using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public GameObject explosion;
	public Transform playerTransform;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddRelativeForce (Vector3.right * 2500);
	}

	// Update is called once per frame
	void Update () {
		

	}

	void OnTriggerEnter(Collider other) {

		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().detectCollisions = false;
		Instantiate (explosion, playerTransform.position, playerTransform.rotation);
		//Destroy (gameObject);
		// Trying out this instead, because destroying the object also destroys the child camera.
		GetComponent<Renderer>().enabled = false;
	
	}
		
}
