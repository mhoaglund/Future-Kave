using UnityEngine;
using System;
using System.Timers;
using System.Collections;
using System.Collections.Generic;

public class CollisionMgmt : MonoBehaviour {

	public Dictionary<int, GameObject> Partners = new Dictionary<int, GameObject>();
	public Dictionary<int, GameObject> Nodes = new Dictionary<int, GameObject>();

	public int me;
	public int PartnerCount = 0;
	public int NodeCount = 0;
	public bool isPaired = false;
	public bool isContributor = false;
	public Rigidbody rBody;
	public GameObject fkinfo;

	public delegate void del(Collider coll);
	public delegate void del_empty();

	void Start () {
		Enable ();
	}

	void Update () {
		PartnerCount = Partners.Count;
		NodeCount = Nodes.Count;
	}

	//delay the trigger by half sec because users get spawned at map zero and can inadv. trigger
	void Enable ()
	{
		me = this.gameObject.GetInstanceID();
		fkinfo = GameObject.Find("FKinfo");
		StartCoroutine(delayRB (0.5f));
	}
	
	//multipurpose delay. can this be safely moved to utility class?
	IEnumerator delay(float time, del_empty callback)
	{
		yield return new WaitForSeconds(time);
		callback();
	}

	IEnumerator delay(float time, Collider c, del callback)
	{
		yield return new WaitForSeconds(time);
		callback(c);
	}

	IEnumerator delayRB(float time)
	{
		yield return new WaitForSeconds(time);
		rBody = this.gameObject.AddComponent<Rigidbody>() as Rigidbody;
		rBody.isKinematic = true;
		rBody.useGravity = false;
		rBody.GetComponent<TrailRenderer>().enabled = true;
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject == null) return;

		int id = collider.gameObject.GetInstanceID();
		var go = collider.gameObject;
		var value = gameObject;

		//we don't want to spawn nodes when users are contributing to existing nodes.
		if(collider.gameObject.tag == "User")
		{

			if(Partners.TryGetValue(id, out value)) return; //how would this happen?

			Partners.Add(id, go);
			isPaired = true;

			if(Nodes.Count == 0)
			{
				fkinfo.GetComponent<TriggerManager>().pairMap(this.gameObject, go);
			}
		}
		else if (collider.gameObject.tag == "Node")
		{
			collider.gameObject.GetComponent<KaveNode>().addUser(me, go);
			if(Nodes.TryGetValue(id, out value)) return;
			Nodes.Add (id, go);
			isContributor = true;
		}


	}

	void OnTriggerExit(Collider collider)
	{
		if (collider == null) return;
		//Wait 5 seconds, then remove from list of concurrent partners.
		if(collider.gameObject.tag == "User")
		{
			//StartCoroutine(delay (3.0f, collider, leavePartner));
			leavePartner(collider);
		}

		else if (collider.gameObject.tag == "Node")
		{
			//Immediately remove self from node.
			var kn = collider.GetComponent<KaveNode>();
			kn.stripUser(me);
			Nodes.Remove(collider.gameObject.GetInstanceID());
			//delay the user's ability to produce another node
			if(Nodes.Count > 0) return;
			//StartCoroutine(delay (3.0f, stopContributing));
			stopContributing();
		}
	}

	void leavePartner(Collider collider)
	{
		if(collider != null)
		{
			int pid = collider.gameObject.GetInstanceID();
			var value = gameObject;
			if(Partners.TryGetValue(pid, out value)) Partners.Remove (pid);
		}
		if(Partners.Count == 0) isPaired = false;
	}

	public void join()
	{
		isContributor = true;
	}

	public void stopContributing()
	{
		if(Nodes.Count == 0) isContributor = false;
	}

	public void stripNode(int id)
	{
		Nodes.Remove(id);
		stopContributing();
	}
}
