using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

	public player player;
	// Use this for initialization
	private void Start()
	{

	}

	// Update is called once per frame
	private void Update()
	{
		control();
	}

	private void control()
	{
		if (Input.GetKey("a")) player.goLeft();
		if (Input.GetKey("d")) player.goRight();
		if (Input.GetKeyDown("space")) player.goJump();
		if (Input.GetKeyUp("space")) player.release();

	}
}
