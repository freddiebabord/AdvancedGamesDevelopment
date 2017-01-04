using UnityEngine;
using System.Collections;

public class RandomFloat : MonoBehaviour {

	public float amplitude;          //Set in Inspector 
	public float speed;                  //Set in Inspector 
	private Vector3 tempVal;
	private Vector3 tempPos;

	void Start () 
	{
		tempVal = transform.position;
	}

	void Update () 
	{        
		tempPos = tempVal;
		tempPos.y += amplitude * Mathf.Sin(speed * Time.time);
		transform.position = tempPos;
	}
}
