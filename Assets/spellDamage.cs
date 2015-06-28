using UnityEngine;
using System.Collections;

public class spellDamage : MonoBehaviour {

	private	float		elapsedTime;

	// Use this for initialization
	void Start () {
	}

	void Awake()
	{
		elapsedTime = gameObject.GetComponentInParent<spellEffect>().firerate;
	}

	void OnParticleCollision(GameObject c)
	{
		Debug.Log (gameObject.name);
		if (c.gameObject.tag == "enemy")
		{
			enemy en = c.gameObject.GetComponentInParent<enemy>();
			if (en != null && !en.dead)
			{
				stats e = en.st;
				if (e != null)
				{
					e.hp -= gameObject.GetComponentInParent<spellEffect>().damage;
					if (e.hp <= 0)
						e.hp = 0;
				}
			}
		}
	}

	void OnTriggerEnter(Collider c)
	{
		Debug.Log("awd");
		if (c.gameObject.tag == "enemy")
		{
			enemy en = c.gameObject.GetComponentInParent<enemy>();
			if (en != null && !en.dead)
			{
				stats e = en.st;
				if (e != null)
				{
					e.hp -= gameObject.GetComponentInParent<spellEffect>().damage;
					if (e.hp <= 0)
						e.hp = 0;
				}
			}
		}
	}

	void OnTriggerStay(Collider c)
	{
		if (gameObject.name == "StormHerald")
		{
			elapsedTime += Time.deltaTime;	
			if (elapsedTime > gameObject.GetComponentInParent<spellEffect>().firerate)
			{
				if (c.gameObject.tag == "enemy")
				{
					enemy en = c.gameObject.GetComponentInParent<enemy>();
					if (en != null && !en.dead)
					{
						stats e = en.st;
						if (e != null)
						{
							e.hp -= gameObject.GetComponentInParent<spellEffect>().damage;
							if (e.hp <= 0)
								e.hp = 0;
						}
						Debug.Log("degats");
					}
				}
				elapsedTime = 0.0f;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
