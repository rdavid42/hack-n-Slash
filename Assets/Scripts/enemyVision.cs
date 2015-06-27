using UnityEngine;
using System.Collections;

public class enemyVision : MonoBehaviour {

	public enemy				e;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerStay(Collider c)
	{
		if (!e.dead && !e.attacking)
		{
			if (c.gameObject.tag == "maya")
			{
				e.move(c.gameObject);
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
