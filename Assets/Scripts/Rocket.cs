using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Rocket : MonoBehaviour {

	public AudioClip[] audioClips;
	enum State { Alive,Dying,Transcending}
	State state = State.Alive;


	[SerializeField]
	private float thrustForce= 25 ;
	[SerializeField]
	private float rotationSpeed=150;

	private Vector3 thrust;
	private Vector3 rotation;
	private Rigidbody rigidBody;
	private AudioSource audioSource;
	private SceneBoss sceneBoss;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();	
		audioSource = GetComponent<AudioSource> ();
		sceneBoss = GameObject.FindObjectOfType<SceneBoss> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.Alive) {
			Thrust ();
			Rotation ();
		}
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
		if (collision.gameObject.tag == "Finish") {
			print ("Next level");
			state = State.Transcending;
			Invoke ("LoadNextScene", 5);
		} 
		if (collision.gameObject.tag == "Friendly") {
		
			return;
		}
		if (collision.gameObject.tag != "Friendly" || collision.gameObject.tag != "Finish") {
			state = State.Dying;
			audioSource.Stop ();
			Invoke("LoadLevelOne",2); 
			print ("Boom"); 
		}
	}
	void LoadNextScene()
	{
		state = State.Alive;
		sceneBoss.LoadNextScene ();
	}
	void LoadLevelOne()
	{
		state = State.Alive;
		sceneBoss.LoadScene ("Game");
	}
}
