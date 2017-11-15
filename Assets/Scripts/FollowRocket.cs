using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRocket : MonoBehaviour 
{
	private Vector3 offSet;
	private GameObject player;


	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		offSet = transform.position - player.transform.position;
	}
	void Update()
	{
		transform.position = player.transform.position + offSet;
	}

}
