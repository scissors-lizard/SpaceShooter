using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseLaser : MonoBehaviour {
	[SerializeField] private ParticleSystem p;
	[SerializeField] private ParticleSystem muzzleFlash, muzzleSparks;
	[SerializeField] private AudioSource audioSource;

	private ParticleSystem.EmissionModule emission;

	// Use this for initialization
	void Start () {
		emission = p.emission;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			p.Emit (1);
			muzzleFlash.Emit (1);
			muzzleSparks.Emit (15);
			audioSource.pitch = Random.Range (0.8f, 1.0f);
			audioSource.Play ();
			Debug.Log ("Emit1");
		}

		Vector3 lookTarget = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		lookTarget.z = transform.position.z;
		transform.right = lookTarget - transform.position;
	}
}
