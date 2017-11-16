using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Rocket : MonoBehaviour {
	
	[SerializeField] AudioClip[] audioClips;
	enum State { Alive,Dying,Transcending}
	State state = State.Alive;

	[SerializeField] ParticleSystem win;
	[SerializeField] ParticleSystem lose;
	[SerializeField] ParticleSystem pThrust;

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
		rigidBody.angularVelocity = Vector3.zero;
		rotation.y = 0;
		rotation.z= CrossPlatformInputManager.GetAxis("Horizontal");
		rotation.x = 0;

		transform.Rotate (-rotation * rotationSpeed*Time.deltaTime);

	}

	void Thrust ()
	{
		thrust.y = CrossPlatformInputManager.GetAxis ("Jump") * thrustForce;
		thrust.x = 0;
		thrust.z = 0;
		rigidBody.AddRelativeForce (thrust );
		ThrustSound ();

	}

	void ThrustSound ()
	{
		if (CrossPlatformInputManager.GetButtonDown ("Jump")) {
			audioSource.clip = audioClips [0];
			audioSource.Play ();
			pThrust.Play ();

		}  
		else if (CrossPlatformInputManager.GetButtonUp ("Jump")) {
			pThrust.Stop ();
			audioSource.clip = audioClips [1];
			audioSource.Play ();
		}
	}	
	void OnCollisionEnter (Collision collision)
	{
		if (state != State.Alive) {
			return;
		}

		else if (collision.gameObject.tag == "Friendly") {

			return;
		}
		else if (collision.gameObject.tag == "Finish") {
			print ("Next level");
			state = State.Transcending;
			win.Play ();
			DelayClipNumber (3);
			Invoke ("LoadNextScene", audioSource.clip.length+1);
		} 
		else if (collision.gameObject.tag != "Friendly" || collision.gameObject.tag != "Finish") {
			state = State.Dying;
			lose.Play ();
			DelayClipNumber (2);
			Invoke("LoadLevelOne",audioSource.clip.length + 1); 
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
	void EndAudioSourceWithDelay()
	{
		audioSource.Stop ();
	}

	void DelayClipNumber (int clip)
	{
		audioSource.clip = audioClips [clip];
		audioSource.Play ();
		Invoke ("EndAudioSourceWithDelay", audioSource.clip.length);
	}

}
