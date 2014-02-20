using UnityEngine;
using System.Collections;

public class ConRot : MonoBehaviour
{

	public GameObject subject;
	public string type;
	private float xr = 0;
	private float yr = 0;
	private float zr = 0;
	// Use this for initialization
	void Start ()
	{
		type = subject.tag.ToString();
		switch(type)
		{
		case "Glyph":
			xr = 0;
			yr = 35;
			zr = 0;
			break;
		case "Sky":
			xr = 1;
			yr = 0;
			zr = 1;
			break;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		subject.transform.Rotate (xr*Time.deltaTime,yr*Time.deltaTime,zr*Time.deltaTime); 
	}

	void pick()
	{
		switch(type)
		{
		case "Glyph":
			xr = 0;
			yr = 12;
			zr = 0;
			break;
		case "Sky":
			xr = 1;
			yr = 0;
			zr = 1;
			break;
		}
	}

}

