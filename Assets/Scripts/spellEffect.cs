using UnityEngine;
using System.Collections;

public class spellEffect : MonoBehaviour {

	public int		damage;
	public int		heal;
	public float	cooldown;
	public float	duration;
	public string	description;
	public int		level;
	public int		tier;
	public int		firerate;
	public bool		canBeUse;
	public float	elapsedTime;
	public GameObject	spellAnimation;
	// Use this for initialization
	void Start () {
		elapsedTime = cooldown;
	}


	public void	launch()
	{
		canBeUse = false;
		spellAnimation.SetActive(true);
	}


	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if (elapsedTime >= cooldown)
		{
			canBeUse = true;
			elapsedTime = 0;
		}
		if (gameObject.name == "fireBall" && spellAnimation.activeSelf)
		{
			gameObject.transform.position += new Vector3(transform.forward.normalized.x, 0, transform.forward.normalized.z);
		}
		if (spellAnimation.activeSelf && !spellAnimation.GetComponent<ParticleSystem>().isPlaying)
		{
			spellAnimation.SetActive (false);
		}
		if (gameObject.name == "stormHerald" || gameObject.name == "Heal")
		{
			gameObject.transform.position = GameObject.FindGameObjectWithTag("player").gameObject.transform.position;
		}
	}
}
