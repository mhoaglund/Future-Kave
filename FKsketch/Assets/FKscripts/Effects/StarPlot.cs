using UnityEngine;
using System.Collections;

public class StarPlot : MonoBehaviour
{

	public GameObject outside;
	public GameObject item;
	public Vector3 center = new Vector3();
	public double points = 10;
	public string contact = "Sky";

	// This is a tool to generate a random distribution of stars on the surface of an object.
	// Object must have inverted normals because mapping is being done from the inside.
	void Start ()
	{
		center = outside.transform.position;
		var hit = new RaycastHit();
		var hits = new ArrayList();
		for(int i = 0; i <= points; i++)
		{
			if(Physics.Raycast(center, Random.onUnitSphere * 2000, out hit)){
				if(hit.collider.tag == contact)
				{
					hits.Add(hit.point);
				}
			}
		}

		foreach(Vector3 point in hits)
		{
			GameObject go = Instantiate(item, point, Quaternion.identity) as GameObject;
			go.transform.parent = outside.transform;
		}
	}

}

