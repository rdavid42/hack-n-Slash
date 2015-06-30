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
		int		i;

		if (c.gameObject.tag == "enemy" && player != null && player.attacking)
		{
			enemy en = c.gameObject.GetComponentInParent<enemy>();
			if (en != null && !en.dead)
			{
				stats e = en.st;
				if (e != null)
				{
					if (player.st.hitChance(e))
					{
						i = Random.Range(0, player.bloodEffects.Length);
						GameObject blood = (GameObject)Instantiate(player.bloodEffects[i], c.ClosestPointOnBounds(player.inv.equipedItems[0].transform.position), Quaternion.identity);
						blood.SetActive(true);
						player.slashSounds[Random.Range(0, player.slashSounds.Length)].Play();
						e.hp -= player.st.finalDamage(e) + ist.finalDamage(e);
						if (e.hp <= 0)
							e.hp = 0;
					}
				}
			}
		}
	}

	void Update () {
	
	}
}
