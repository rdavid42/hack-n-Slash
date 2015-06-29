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
		if (c.gameObject.tag == "enemy" && player != null && player.attacking)
		{
			enemy en = c.gameObject.GetComponentInParent<enemy>();
			if (en != null && !en.dead)
			{
				stats e = en.st;
				if (e != null)
				{
					Debug.Log ("hit");
					e.hp -= player.st.finalDamage(e) + ist.finalDamage(e);
					if (e.hp <= 0)
						e.hp = 0;
				}
			}
		}
	}

	void Update () {
	
	}
}
