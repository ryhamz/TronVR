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
		//cameraTransform = mainCamera.transform;
		forceDirection = playerTransform.right;
	}

	// Use this for initialization
	void Start () {
		
		//GetComponent<Rigidbody>().AddRelativeForce (forceDirection * 1000);
		devicePose = Cardboard.SDK.HeadPose;


	}

	// Update is called once per frame
	void Update () {
	//	Turn ();
		bikeBody.velocity = Vector3.zero;
		bikeBody.rotation = playerTransform.rotation;
		bikeBody.AddRelativeForce (Vector3.right * 1000);
		//mainCamera.transform.rotation = cameraTransform.rotation;
		Debug.Log(devicePose.Orientation.eulerAngles.y);
		if (Input.GetKeyDown ("space")) {
			float lookAngle = devicePose.Orientation.eulerAngles.y;
			Vector3 turnQuaternion = playerTransform.rotation.eulerAngles;
			if (lookAngle > 0 && lookAngle < 40) {
				turnQuaternion.y +=90;
		
				playerTransform.rotation = Quaternion.Euler(turnQuaternion);
				bikeBody.rotation = Quaternion.Euler(turnQuaternion);
				Cardboard.SDK.Recenter ();


			}
			if (lookAngle > 40 && lookAngle < 360) {
				turnQuaternion.y += 270;

				playerTransform.rotation = Quaternion.Euler(turnQuaternion);
				bikeBody.rotation = Quaternion.Euler(turnQuaternion);
				Cardboard.SDK.Recenter ();

			}
		}
	}

	void LateUpdate() {
		//mainCamera.transform.rotation = cameraTransform.rotation;
	}

	public void Turn() {
		/*float turnAngle = devicePose.Orientation.eulerAngles.y;
		int turnDirection = -1;
		if (turnAngle > 0 && turnAngle < 360) {
			turnDirection = 1;
		}
		Quaternion turnQuaternion = Quaternion.identity;
		turnQuaternion.y = turnDirection * (turnAngle%20);
		//bikeBody.rotation = turnQuaternion;
		//playerTransform.localRotation = turnQuaternion;
		*/

		float turnAngle = devicePose.Orientation.eulerAngles.y;
		//Debug.Log(devicePose.Orientation.eulerAngles.y);
		int turnDirection = -1;
		if (turnAngle > 0 && turnAngle < 180) {
			turnDirection = 1;
		} 
		if (turnAngle >= 180 && turnAngle <= 360) { 
			turnAngle = 360 - turnAngle;
		}

		 
		Quaternion turnQuaternion = playerTransform.localRotation;
		Debug.Log ("Player rotation is" + turnQuaternion);
			turnQuaternion.y += turnDirection * turnAngle;
				Debug.Log (turnQuaternion.y);
		playerTransform.localRotation = turnQuaternion ;
		Debug.Log ("Player rotation is" + playerTransform.localRotation + " after turn");

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
