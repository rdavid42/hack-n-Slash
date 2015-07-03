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
		int			i;
		GameObject	tmp;

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
						tmp = (GameObject)Instantiate(player.bloodEffects[i], c.ClosestPointOnBounds(player.inv.equipedItems[0].transform.position), Quaternion.identity);
						tmp.SetActive(true);
						player.slashSounds[Random.Range(0, player.slashSounds.Length)].Play();
						int dmg = player.st.finalDamage(e) + ist.finalDamage(e);
						e.hp -= dmg;
						if (e.hp <= 0)
							e.hp = 0;
						tmp = (GameObject)Instantiate(player.textPop, c.ClosestPointOnBounds(player.inv.equipedItems[0].transform.position), Quaternion.identity);
						tmp.SetActive(true);
						tmp.transform.LookAt(player.cam.transform.position);
						tmp.transform.eulerAngles = new Vector3(tmp.transform.eulerAngles.x, tmp.transform.eulerAngles.y + 180, tmp.transform.eulerAngles.z);
						textPop tp = tmp.GetComponent<textPop>();
						tp.addConfig(dmg.ToString());
						tp.addConfig(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.25f, 20.0f);
						tp.addConfig(new Vector3(0.0f, 1.0f, 0.0f), 2.0f);
					}
					else
					{
						tmp = (GameObject)Instantiate(player.textPop, c.ClosestPointOnBounds(player.inv.equipedItems[0].transform.position), Quaternion.identity);
						tmp.SetActive(true);
						tmp.transform.LookAt(player.cam.transform.position);
						tmp.transform.eulerAngles = new Vector3(tmp.transform.eulerAngles.x, tmp.transform.eulerAngles.y + 180, tmp.transform.eulerAngles.z);
						textPop tp = tmp.GetComponent<textPop>();
						tp.addConfig("miss");
						tp.addConfig(new Color(1.0f, 1.0f, 1.0f, 1.0f), 0.25f, 20.0f);
						tp.addConfig(new Vector3(0.0f, 1.0f, 0.0f), 2.0f);
					}
				}
			}
		}
	}

	void Update () {
	
	}
}
