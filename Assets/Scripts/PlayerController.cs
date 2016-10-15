using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour {

	private Rigidbody rigidBody;
	//private Camera camera;
	public float speed;
	public Boundary boundary;
	public float tiltFactor;

	void Start() 
	{
		rigidBody = GetComponent<Rigidbody>();
		//camera = GetComponent<Camera>();
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movementVector = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//rigidBody.AddForce (movementVector * speed);
		rigidBody.velocity = movementVector * speed;

		rigidBody.position = new Vector3 (
			Mathf.Clamp (rigidBody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rigidBody.position.z, boundary.zMin, boundary.zMax)
		);
		rigidBody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidBody.velocity.x * -tiltFactor);
	
	}
//	void clampPosToPlayArea()
//	{
//		Vector3 screenPos = camera.WorldToScreenPoint (rigidBody.position);
//		float screenHeight = UnityEngine.Screen.height;
//		float screenWidth = UnityEngine.Screen.width;
//		Vector3 clampedScreenPos = new Vector3 (
//			                           Mathf.Clamp (screenPos.x, 0.0f, screenWidth),
//			                           0.0f,
//			                           Mathf.Clamp (screenPos.z, 0.0f, 0.8f * screenHeight)
//		                           );
//		
//	}
}
