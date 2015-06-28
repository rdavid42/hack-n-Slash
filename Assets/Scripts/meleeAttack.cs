using UnityEngine;
using System.Collections;

public class meleeAttack : MonoBehaviour {

	public player			player;
	public itemStats		ist;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.tag == "enemy" && player.attacking)
		{
			stats e = c.gameObject.GetComponentInParent<enemy>().st;
			if (e != null)
			{
				e.hp -= player.st.finalDamage(e) + ist.finalDamage(e);
				if (e.hp <= 0)
					e.hp = 0;
			}
		}
	}

	void Update () {
	
	}
}
