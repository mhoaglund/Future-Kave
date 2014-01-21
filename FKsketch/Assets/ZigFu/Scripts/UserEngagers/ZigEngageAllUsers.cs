using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZigEngageAllUsers : MonoBehaviour {
	
	public GameObject InstantiatePerUser;
	public GameObject UserLamp;
	private Vector3 ZERO = new Vector3(0,0,0);
	Dictionary<int, GameObject> objects = new Dictionary<int, GameObject>();
	
	void Zig_UserFound(ZigTrackedUser user) 
	{
		if(Network.isServer) 
		{
			Debug.Log ("Spawned User!");
		}

		//TODO: ensure that arg-heavy network instantiation isn't destroying the start coordinates of the player in real space
		GameObject o = (!Network.isServer) ? 
			Instantiate(InstantiatePerUser) as GameObject :
			Network.Instantiate(InstantiatePerUser, ZERO, this.transform.rotation, 0) as GameObject;

		GameObject l = Instantiate(UserLamp) as GameObject;
		l.transform.parent = o.transform;
		objects[user.Id] = o;
		user.AddListener(o);
	}
	
	void Zig_UserLost(ZigTrackedUser user)
	{
		if(Network.isServer || Network.isClient) Network.Destroy(objects[user.Id]);
		else Destroy(objects[user.Id]);
		objects.Remove(user.Id);
	}
}
