using UnityEngine;
using System.Collections;

public static class GeoMath {

	public static Vector3 midpoint(Vector3 begin, Vector3 end)
	{
		var mp = new Vector3();
		mp.x = (begin.x + end.x)/2;
		mp.y = (begin.y + end.y)/2;
		mp.z = (begin.z + end.z)/2;

		return mp;
	}
}
