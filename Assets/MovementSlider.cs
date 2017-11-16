using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSlider : MonoBehaviour 
{
	

	[SerializeField][Range(0,1)] float movementFactor;
	[SerializeField] Vector3 movementVector= new Vector3 (10f,10f,10f);
	[SerializeField]float  period =2f;
	Vector3 startPos;

	void Start()
	{
		startPos = transform.position; 
	}
	void Update()
	{
		MovingIt ();
	}
	void MovingIt()
	{
		if (period <= Mathf.Epsilon) {
			return;
		}
		float cycles = Time.time / period; // grows continualy from 0

		const float tau = Mathf.PI * 2; // about 6.28 (one circle)
		float rawSinWave = Mathf.Sin(cycles*tau);

		movementFactor = rawSinWave / 2f + 0.5f;	

		Vector3 offSet = movementFactor * movementVector;
		transform.position = startPos + offSet;
	}

}
