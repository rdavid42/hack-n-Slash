using UnityEngine;
using System.Collections;

public class lifePotion : MonoBehaviour {

	public bool				dying;
	public Particle[]		particles;
	public ParticleSystem	ps;

	// Use this for initialization
	void Start () {
		dying = false;
		ps = gameObject.GetComponent<ParticleSystem>();
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.tag == "player" && !dying)
		{
			player m = c.gameObject.GetComponent<player>();
			m.st.hp += (int)((float)m.st.maxHp * 30.0f / 100.0f);
			m.pLifeGlobe.Play();
			if (m.st.hp > m.st.maxHp)
				m.st.hp = m.st.maxHp;
			dying = true;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (dying)
		{
//			Debug.Log (ps.startColor.a);
			ps.startColor = new Color(ps.startColor.r, ps.startColor.g, ps.startColor.b, ps.startColor.a / 2.0f);
			if (ps.startColor.a <= 0.001f)
			{
				Destroy(gameObject);
			}
		}
	}
}
