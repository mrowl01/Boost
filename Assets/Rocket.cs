using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Rocket : MonoBehaviour {

	public float thrustForce= 25;
	public AudioClip[] audioClips;

	private Vector3 thrust;
	private Vector3 rotation;
	private Rigidbody rigidBody;
	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();	
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		ProcessInput ();
	}

	 void ProcessInput ()
	{
		thrust.y = CrossPlatformInputManager.GetAxis ("Jump") * thrustForce;
		thrust.x = 0;
		thrust.z = 0;

		rotation.y = 0;
		rotation.z= CrossPlatformInputManager.GetAxis("Horizontal");
		rotation.x = 0;

		transform.Rotate (-rotation);
		rigidBody.AddRelativeForce (thrust);
		ThrustSound (thrust.y);

	}
	void ThrustSound (float sound)
	{
		if (CrossPlatformInputManager.GetButtonDown ("Jump")) {
			audioSource.clip = audioClips [0];
			audioSource.Play ();

		}  
		else if (CrossPlatformInputManager.GetButtonUp ("Jump")) {
			audioSource.clip = audioClips [1];
			audioSource.Play ();
		}
	}	
}
