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
			if (c.gameObject.tag == "player")
				e.attack(c.gameObject);
		}
	}

	void OnTriggerExit(Collider c)
	{
		if (!e.dead)
		{
			if (c.gameObject.tag == "player")
				e.stopAttacking();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
