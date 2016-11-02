using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	private AudioSource audioSource;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float fireDelay;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", fireDelay, fireRate);
	
	}

	void Fire() {
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
	}

	void Update () {
	
	}
}
