using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public GameObject explosion;
	public Transform playerTransform;
	public Pose3D devicePose;
	public Rigidbody bikeBody;
	public Cardboard mainCamera;

	Transform cameraTransform;
	Vector3 forceDirection;

	void Awake() {
		forceDirection = playerTransform.right;
	}

	// Use this for initialization
	void Start () {
		devicePose = Cardboard.SDK.HeadPose;


	}

	// Update is called once per frame
	void Update () {
		bikeBody.velocity = Vector3.zero;
		bikeBody.rotation = playerTransform.rotation;
		bikeBody.AddRelativeForce (Vector3.right * 1000);
		Debug.Log(devicePose.Orientation.eulerAngles.y);
		if (Input.GetKeyDown ("space")) {
			ChooseTurn ();
		}
	}

	void LateUpdate() {
	}

	void ChooseTurn () {
		float lookAngle = devicePose.Orientation.eulerAngles.y;

		if (lookAngle > 0 && lookAngle < 40)
			TurnRight ();
		else if (lookAngle > 40 && lookAngle < 360)
			TurnLeft ();
	}

	/* perform a left turn */
	void TurnLeft () {
		StartCoroutine (PerformTurn(Vector3.down * 90, 0.5f));
		Cardboard.SDK.Recenter ();
	}

	/* perform a right turn */
	void TurnRight () {
		StartCoroutine (PerformTurn (Vector3.up * 90, 0.5f));
		Cardboard.SDK.Recenter ();
	}

	IEnumerator PerformTurn (Vector3 byAngles, float inTime) {
		Quaternion fromAngle = playerTransform.rotation;
		Quaternion toAngle = Quaternion.Euler (playerTransform.eulerAngles + byAngles);

		for (float t = 0f; t < 1; t += Time.deltaTime / inTime) {
			playerTransform.rotation = Quaternion.Lerp (fromAngle, toAngle, t);
			yield return null;
		}
	}

	void OnTriggerEnter(Collider other) {

		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().detectCollisions = false;
		Instantiate (explosion, playerTransform.position, playerTransform.rotation);
		GetComponent<Renderer>().enabled = false;
	
	}
}