using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour {

	private Rigidbody rigidBody;
	public float speed;
	public Boundary boundary;
	public float tiltFactor;
	public GameObject boltPrefab;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	private AudioSource audioSource;
	private Quaternion calibrationQuaternion;
	public SimpleTouchPad touchPad;
	public SimpleTouchArea simpleTouchArea;



	void Start() 
	{
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource> ();
		CalibrateAccelerometer ();
		Debug.Log(System.Environment.Version);
	}

	void Update() 
	{
		//if (Input.GetKeyDown("space") || Input.GetButton("Fire1")  && Time.time > nextFire) 
		if (simpleTouchArea.CanFire() && Time.time > nextFire) 
		{
			GameObject clone = Instantiate (boltPrefab, shotSpawn.position, shotSpawn.rotation) as GameObject;
			nextFire = Time.time + fireRate;
			audioSource.Play ();
		}
	}

	void CalibrateAccelerometer() {
		Vector3 accelerometerSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerometerSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAcceleration (Vector3 accelleration) {
		Vector3 fixedAcceleration = calibrationQuaternion * accelleration;
		return fixedAcceleration;
	}

	void FixedUpdate()
	{
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");
//		Vector3 movementVector = new Vector3 (moveHorizontal, 0.0f, moveVertical);
//		Vector3 accelerationRaw = Input.acceleration;
//		Vector3 acceleration = FixAcceleration (accelerationRaw);
//		Vector3 movementVector = new Vector3 (acceleration.x, 0.0f, acceleration.y);
		Vector2 direction = touchPad.GetDirection();
		Vector3 movementVector = new Vector3 (direction.x, 0.0f, direction.y);
		rigidBody.velocity = movementVector * speed;
		rigidBody.position = new Vector3 (
			Mathf.Clamp (rigidBody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rigidBody.position.z, boundary.zMin, boundary.zMax)
		);
		rigidBody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidBody.velocity.x * -tiltFactor);
	}
}
