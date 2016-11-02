using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {

	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothDirection;
	public float smoothing;

	void Awake() {
		direction = Vector2.zero;
	}

	public void OnPointerDown (PointerEventData data) {
		// set the startting position
		origin = data.position;
	}
	public void OnPointerUp (PointerEventData data) {
		// reset everything
		direction = Vector2.zero;

	}
	public void OnDrag (PointerEventData data) {
		// calculate the difference
		Vector2 currentPosition = data.position;
		Vector2 directionRaw = currentPosition - origin;
		direction = directionRaw.normalized;
		Debug.Log (direction);
	}

	public Vector2 GetDirection() {
		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
	}

	public string GetName() {
		return "ORVAR";
	}
		
}
