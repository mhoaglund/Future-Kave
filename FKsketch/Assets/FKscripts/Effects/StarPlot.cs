using UnityEngine;
using System.Collections;

public class StarPlot : MonoBehaviour
{

	public GameObject outside = new GameObject();
	public GameObject item = new GameObject();
	public Vector3 center = new Vector3();
	public double points = 10;
	public float maxrange = 2000;

	// This is a tool to generate a random distribution of stars on the surface of an object.
	// Object must have inverted normals because mapping is being done from the inside.
	void Start ()
	{
		center = outside.transform.position;
		var hit = new RaycastHit();
		var hits = new ArrayList();
		for(int i = 0; i <= points; i++)
		{
			//Debug.DrawRay(center, Random.onUnitSphere * 80, Color.red);
			if(Physics.Raycast(center, Random.onUnitSphere * 180, out hit)){
				hits.Add(hit.point);
			}
		}

		foreach(Vector3 point in hits)
		{
			GameObject go = Instantiate(item, point, Quaternion.identity) as GameObject;
			go.transform.parent = outside.transform;
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}

}

