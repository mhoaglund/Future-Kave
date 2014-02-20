using UnityEngine;
using System.Collections;

public class Lifespan : MonoBehaviour
{
	public int nodeId = 0;
	public GameObject info;

		void Start ()
		{
	
		}

		void Update ()
		{
	
		}

		void die()
		{
			//tell the trigger manager to clear my unique ID
			//info = GameObject.Find("FKinfo");
			//info.GetComponent<TriggerManager>().clearNode(this.gameObject.GetInstanceID());
	}
}

