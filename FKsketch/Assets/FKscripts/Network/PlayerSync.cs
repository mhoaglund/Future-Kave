using UnityEngine;
using System.Collections;

public class PlayerSync : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = transform.position;
			stream.Serialize(ref syncPosition);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			transform.position = syncPosition;
		}
	}
}
