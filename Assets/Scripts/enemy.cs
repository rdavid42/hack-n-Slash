using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

	public stats					st;
	public string					enemyName;
	public bool						dead = false;
	public NavMeshAgent				nma;
	public Animator					anim;
	public bool						attacking;
	public AnimationState			animState;
	public float					attackRate;
	public float					elapsedAttackTime;
	public maya						player;
	public spawner					spawn;
	public GameObject				lifePotion;

	// Use this for initialization
	void Start () {
		elapsedAttackTime = 0.0f;
	}

	public void move(GameObject target)
	{
		if (!dead)
		{
			nma.destination = target.transform.position;
			anim.SetBool("run", true);
		}
	}

	public void attack(GameObject target)
	{
		if (!dead)
		{
			transform.LookAt(target.transform);
			nma.ResetPath();
			if (anim.GetBool("run"))
				anim.SetBool("run", false);
			if (!anim.GetBool("attack"))
				anim.SetBool("attack", true);
			attacking = true;
		}
	}

	public void stopAttacking()
	{
		if (!dead)
		{
			nma.ResetPath();
			anim.SetBool("attack", false);
			attacking = false;
		}
	}

	public IEnumerator die()
	{
		float i;
		yield return new WaitForSeconds(2.20f);
		for (i = 0; i < 2.0f; i += 0.15f)
		{
			nma.enabled = false;
			gameObject.GetComponent<CharacterController>().enabled = false;
			transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
			yield return new WaitForSeconds(0.15f);
		}
		Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (!dead)
		{
			if (attacking && elapsedAttackTime > attackRate)
			{
				if (st.hitChance(player.st))
				{
					player.st.hp -= st.finalDamage(player.st);
					if (player.st.hp <= 0)
						player.st.hp = 0;
				}
				elapsedAttackTime = 0.0f;
			}
			elapsedAttackTime += Time.deltaTime;
			if (st.hp <= 0)
			{
				st.hp = 0;
				if (anim.GetBool("run"))
					anim.SetBool("run", false);
				if (anim.GetBool("attack"))
					anim.SetBool("attack", false);
				anim.SetBool("die", true);
				dead = true;
				spawn.canSpawn = true;
				player.st.xp += st.xp;
				player.st.money += st.money;
				if (Random.Range(0, 3) == 0)
				{
					GameObject pot = (GameObject)Instantiate(lifePotion, transform.position, Quaternion.identity);
					pot.SetActive(true);
				}
				StartCoroutine(die());
			}
		}
	}
}
