using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KaveNode : MonoBehaviour
{
	public List<int> contributors = new List<int>();
	public string nodeType = "";

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void stripUser(int userId)
	{
		int index = contributors.IndexOf(userId);
		contributors.RemoveAt(index);
	}
}

