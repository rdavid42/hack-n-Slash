using UnityEngine;
using System.Collections;

public class lifePotion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.tag == "maya")
		{
			maya m = c.gameObject.GetComponent<maya>();
			m.st.hp += (int)((float)m.st.maxHp * 30.0f / 100.0f);
			if (m.st.hp > m.st.maxHp)
				m.st.hp = m.st.maxHp;
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
