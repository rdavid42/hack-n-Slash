using UnityEngine;
using System.Collections;

public class spellDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnParticleCollision(GameObject collid)
	{
		Debug.Log (gameObject.name);
		if (collid.tag == "enemy")
		{
			stats e = collid.GetComponentInParent<enemy>().st;
			if (e != null)
			{
				e.hp -= gameObject.GetComponentInParent<spellEffect>().damage;
				if (e.hp <= 0)
					e.hp = 0;
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
