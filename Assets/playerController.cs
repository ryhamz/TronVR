using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddRelativeForce (Vector3.right * 1000);
	}
	
	// Update is called once per frame
	void Update () {
		

	}
}
