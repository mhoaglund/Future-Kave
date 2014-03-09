using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class KaveNode : MonoBehaviour
{
	public Dictionary<int, GameObject> Users = new Dictionary<int, GameObject>();
	public GameObject info;
	public AudioClip grow;

	private Vector3 start;
	public int UserCount = 0;
	public int me;
	public int RnG;
	private float lifespan;
	public static int MIN_LIFESPAN = 25;
	public static int MAX_LIFESPAN = 35;

	void Start ()
	{
		me = this.gameObject.GetInstanceID();
		lifespan = Random.Range(MIN_LIFESPAN, MAX_LIFESPAN);
		start = this.transform.position;
		RnG = Random.Range(5, 7);
		info = GameObject.Find("FKinfo");

		audio.PlayOneShot(grow, 6.0f);

		StartCoroutine(emerge());
		Destroy(this.gameObject, lifespan);

	}

	void Update ()
	{
		UserCount = Users.Count;
	}

	IEnumerator emerge()
	{
		var t = 0f;
		while(t < RnG)
		{
			t += Time.deltaTime;
			this.gameObject.transform.position = Vector3.Lerp(start, start + Vector3.up * t * 0.8f,  Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, t)));
			yield return null;
		}
	}

	void OnDestroy()
	{
		info.GetComponent<TriggerManager>().clearNode(this.gameObject);
		
		foreach(var entry in Users)
		{
			if(entry.Value != null) entry.Value.gameObject.GetComponent<CollisionMgmt>().removeItem(me, "Node");
		}
	}

	public void stripUser(int userId)
	{
		var value = gameObject;
		if(Users.TryGetValue(userId, out value)) Users.Remove(userId);
	}

	public void addUser(int id, GameObject go)
	{
		var value = gameObject;
		if(!Users.TryGetValue(id, out value)) Users.Add(id, go);
	}
}

