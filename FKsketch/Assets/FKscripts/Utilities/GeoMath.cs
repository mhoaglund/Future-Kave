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

	public static Vector3 below(Vector3 origin)
	{
		var under = new Vector3();
		under.x = origin.x;
		under.y = origin.y -4;
		under.z = origin.z;
		return under;
	}

	public static Vector3 above(Vector3 origin)
	{
		var over = new Vector3();
		over.x = origin.x;
		over.y = origin.y +4;
		over.z = origin.z;
		return over;
	}
}
