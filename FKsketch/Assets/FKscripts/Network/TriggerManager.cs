using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerManager : MonoBehaviour {

	//TriggerManager: This is meant to be a tool to manage proximity triggers between users.
	//Users are all triggers, so trigger events need to be organized into pairs.
	//Paired triggers will have their midpoint mapped and a 'node' will be created. The node handles other participants.

	//Maintain a list of concurrent pairs of users.
	//public List<KeyValuePair<int, int>> concPairs = new List<KeyValuePair<int, int>>();
	public GameObject Node = new GameObject();

	// Use this for initialization
	void Start () {
	
	}

	public void pairMap (Vector3 userpos1, Vector3 userpos2 )
	{
		//Get the position and instanceIDs of two participants in a trigger, map their midpoint.
		//concPairs.Add (new KeyValuePair<int, int>(userid1, userid2));
		var mid = GeoMath.midpoint(userpos1, userpos2);
		GameObject node = Instantiate(Node, mid, Quaternion.identity) as GameObject;
	}

	void pullPair()
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
