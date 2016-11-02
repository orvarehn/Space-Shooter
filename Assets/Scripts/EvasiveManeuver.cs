using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour {
	
	public float dodge;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public float smoothing;
	public Boundary boundary;
	public float tilt;
	private float currentSpeed;
	private float targetManeuver;
	private Rigidbody rigidBody;



// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		currentSpeed = rigidBody.velocity.z;
		StartCoroutine (Evade ());
	}

	void FixedUpdate () {
		float newManeuver = Mathf.MoveTowards (rigidBody.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rigidBody.velocity = new Vector3 (newManeuver, 0.0f, currentSpeed);
		rigidBody.position = new Vector3 (
			Mathf.Clamp (rigidBody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rigidBody.position.z, boundary.zMin, boundary.zMax)
		);
		rigidBody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidBody.velocity.x * -tilt);
}

	IEnumerator Evade() 
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
		while (true) 
		{
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}
}
