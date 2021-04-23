using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovemnt : MonoBehaviour
{
	public GameObject player;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.125f);
		transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
	}
}
