using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Rocket : MonoBehaviour {

	public AudioClip[] audioClips;


	[SerializeField]
	private float thrustForce= 25 ;
	[SerializeField]
	private float rotationSpeed=150;

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
		Thrust ();
		Rotation ();
	}

	 void Rotation ()
	{
		rigidBody.freezeRotation = true;//manual control of rotation
		rotation.y = 0;
		rotation.z= CrossPlatformInputManager.GetAxis("Horizontal");
		rotation.x = 0;

		transform.Rotate (-rotation * rotationSpeed*Time.deltaTime);
		rigidBody.freezeRotation= false;//resume phsyics rotation
	}

	void Thrust ()
	{
		thrust.y = CrossPlatformInputManager.GetAxis ("Jump") * thrustForce;
		thrust.x = 0;
		thrust.z = 0;
		rigidBody.AddRelativeForce (thrust );
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
	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "Friendly") {
			return;
		} else if (collision.gameObject.tag != "Friendly") {
			Destroy (gameObject);
		}
	}
}
