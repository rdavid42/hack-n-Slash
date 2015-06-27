using UnityEngine;
using System.Collections;

public class meleeAttack : MonoBehaviour {

	public player			player;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.tag == "zombie" && player.attacking)
		{
			stats e = c.gameObject.GetComponentInParent<enemy>().st;
			e.hp -= player.st.finalDamage(e);
			if (e.hp <= 0)
				e.hp = 0;
		}
	}

	void Update () {
	
	}
}
