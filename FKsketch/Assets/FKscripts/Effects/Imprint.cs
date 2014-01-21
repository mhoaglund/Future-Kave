using UnityEngine;
using System.Collections;

public class Imprint : MonoBehaviour {

	private bool Slowbit;
	public Transform myBase;
	public GameObject myImprint = new GameObject();

	void Start () {
		myBase = this.transform.parent;
		//Some options: pick an ARGB color to characterize imprint
		//Random number for 'strength of effect'
	}

	void Update () {
		if(Slowbit == false)
		{
			Slowbit = true;
			return;
		}

		Instantiate(myImprint, myBase.position, Quaternion.identity);

	}
}
