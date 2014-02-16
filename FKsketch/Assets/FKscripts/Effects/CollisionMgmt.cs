using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionMgmt : MonoBehaviour {

	public List<int> partners = new List<int>();
	public bool isOccupied = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider)
	{
		if(isOccupied == false)
		{
			if(collider.gameObject.tag == "User")
			{
				partners.Add (collider.GetInstanceID());
				isOccupied = true;
				Debug.Log("Touch");
			}
		}
	}

	void OnTriggerExit(Collider collider)
	{
		//Wait 5 seconds, then remove from list of concurrent partners.
		if(collider.gameObject.tag == "User")
		{
			System.Threading.Thread.Sleep (5000);

			int cPartner = collider.GetInstanceID();
			int cpOrder = partners.IndexOf(cPartner);
			bool isPartner = cpOrder != -1;

			if(isPartner)
			{
				partners.RemoveAt(cpOrder);
			}

			Debug.Log("Bye");
			isOccupied = false;
		}

		else if (collider.gameObject.tag == "Node")
		{
			//Immediately remove self from node.
			var me = this.GetInstanceID();
			var node = collider.GetComponent<KaveNode>();
			node.stripUser(me);

		}
	}

	void OnTriggerStay(Collider collider)
	{
		Debug.Log("Stay");
	}
}
