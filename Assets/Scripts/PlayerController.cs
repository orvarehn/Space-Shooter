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


	void Start() 
	{
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource> ();
	}

	void Update() 
	{
		if (Input.GetKeyDown("space") || Input.GetButton("Fire1")  && Time.time > nextFire) 
		{
			GameObject clone = Instantiate (boltPrefab, shotSpawn.position, shotSpawn.rotation) as GameObject;
			nextFire = Time.time + fireRate;
			audioSource.Play ();
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movementVector = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidBody.velocity = movementVector * speed;
		rigidBody.position = new Vector3 (
			Mathf.Clamp (rigidBody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rigidBody.position.z, boundary.zMin, boundary.zMax)
		);
		rigidBody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidBody.velocity.x * -tiltFactor);
	}
}
