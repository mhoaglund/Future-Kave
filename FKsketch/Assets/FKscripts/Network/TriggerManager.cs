using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerManager : MonoBehaviour {

	//TriggerManager: This is meant to be a tool to manage proximity triggers between users.
	//Users are all triggers, so trigger events need to be organized into pairs.
	//Paired triggers will have their midpoint mapped and a 'node' will be created. The node handles other participants.
	
	public GameObject Node;
	public Dictionary<int, GameObject> Nodes;
	public List<Vector3> Spots;
	public static float DUPE_CHECK_DIST = 3.0f;


	// Use this for initialization
	void Start () {
		Nodes = new Dictionary<int, GameObject>();
	}

	public void pairMap (GameObject user1, GameObject user2)
	{
		//Get the position and instanceIDs of two participants in a trigger, map their midpoint.
		//Shoot a ray straight down from midpoint to determine where the node grows from. spawn the node and set the id.

		var mid = GeoMath.midpoint(user1.transform.position, user2.transform.position);

		//we'll always get pairs of trigger events, so throw out every other one.
		if(!Spots.Contains(mid)) Spots.Add (mid);
		else {
			Spots.Clear ();
			return;
		}

		var hit = new RaycastHit();
		int id;

		if(Physics.Raycast(mid, Vector3.down, out hit)){
			//if we aren't over the ground, something's up.
			if(hit.collider.gameObject.tag == "Moon")
			{
				var rotation = Quaternion.Euler(0, Random.Range(0,360), 0);
				GameObject go = Instantiate(Node, GeoMath.below(hit.point), rotation) as GameObject;
				//go.transform.eulerAngles.y = Random.Range(0,360);
				//GameObject go = Instantiate(Node, hit.point, Quaternion.identity) as GameObject;

				id = go.GetInstanceID();
				Nodes.Add (id, go);

				go.GetComponent<KaveNode>().addUser(user1.GetInstanceID(), user1);
				go.GetComponent<KaveNode>().addUser(user2.GetInstanceID(), user2);

				//gotta be a better way to do this
				var u1coll = user1.GetComponent<CollisionMgmt>();
				var u2coll = user2.GetComponent<CollisionMgmt>();

				u1coll.Nodes.Add (id, go);
				u1coll.join();

				u2coll.Nodes.Add (id, go);
				u2coll.join();
			}
		}
	}

	public void clearNode(GameObject node)
	{
		Nodes.Remove(node.GetInstanceID());
	}

	void Update () {
	
	}
}
