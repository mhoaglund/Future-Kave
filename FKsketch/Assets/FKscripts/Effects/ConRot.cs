using UnityEngine;
using System.Collections;

public class ConRot : MonoBehaviour
{

	public GameObject subject = new GameObject();
	public float xrotspeed = 1f;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		subject.transform.Rotate (1*Time.deltaTime,0,1*Time.deltaTime); 
	}
}

