using UnityEngine;
using System.Collections;

public class enemyAttack : MonoBehaviour {
	
	public enemy				e;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerStay(Collider c)
	{
		if (!e.dead)
		{
			if (c.gameObject.tag == "maya")
				e.attack(c.gameObject);
		}
	}

	void OnTriggerExit(Collider c)
	{
		if (!e.dead)
		{
			if (c.gameObject.tag == "maya")
				e.stopAttacking();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
