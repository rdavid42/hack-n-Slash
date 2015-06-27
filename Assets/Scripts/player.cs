using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour
{
	public Camera					cam;
	public Animator					anim;
	private Quaternion				lookRotation;
	private Vector3					direction;
	public stats					st;
	public bool						canLevelUp;
	public int						upgradePoints;
	public NavMeshAgent				nma;
	public bool						dead;
	public bool						canAttack;
	public bool						attacking;
	public UI						ui;
	public itemGenerator			itemgen;
	public inventory				inv;

	private RaycastHit[]			_hits;
	private Vector3					_camOffset;

	void Start()
	{
		_camOffset = transform.position - cam.transform.position;
		canLevelUp = false;
		attacking = false;
		canAttack = false;
		moveAnim(false);
	}

	void levelUpCheck()
	{
		if (st.xp >= st.xpNext)
		{
			levelUp();
			ui.enableLevelUpButton();
		}
	}

	public void levelUp()
	{
		upgradePoints += 5;
		st.level++;
		st.xp = st.xp - st.xpNext;
		st.xpNext = st.getNextXp();
		st.hp = st.maxHp;
		ui.disableLevelUpButton();
	}

	public void upgradeStrength()
	{
		if (upgradePoints > 0)
		{
			upgradePoints--;
			st.strength += 1;
			st.calculateStats();
		}
	}

	public void upgradeAgility()
	{
		if (upgradePoints > 0)
		{
			upgradePoints--;
			st.agility += 1;
			st.calculateStats();
		}
	}

	public void upgradeConstitution()
	{
		if (upgradePoints > 0)
		{
			upgradePoints--;
			st.constitution += 1;
			st.calculateStats();
		}
	}

	public void stop()
	{
		moveAnim(false);
		nma.ResetPath();
	}

	public void getEnemyInfo(RaycastHit[] hits)
	{
		foreach (RaycastHit hit in hits)
		{
			if (hit.collider.gameObject.tag == "enemyTrigger" && !ui.overButton)
			{
				enemy e = hit.collider.gameObject.GetComponentInParent<enemy>();
				ui.enableEnemyPanel();
				ui.updateEnemyInfo(e);
				return ;
			}
		}
		ui.disableEnemyPanel();
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.tag == "enemyTrigger")
			canAttack = true;
	}
	
	void OnTriggerExit(Collider c)
	{
		if (c.gameObject.tag == "enemyTrigger")
		{
			canAttack = false;
			attacking = false;
			attackAnim1(false);
		}
	}

	void moveAnim(bool state)
	{
		anim.SetBool("run", state);
	}

	void attackAnim1(bool state)
	{
		anim.SetBool("attack", state);
	}

	bool tryAttack(RaycastHit[] hits)
	{
		foreach (RaycastHit hit in hits)
		{
			if (canAttack && (hit.collider.gameObject.tag == "enemy"))
			{
				Vector3 t = hit.collider.gameObject.transform.position;
				transform.LookAt(t);
				attacking = true;
				moveAnim(false);
				attackAnim1(true);
				return (true);
			}
		}
		attacking = false;
		attackAnim1(false);
		return (false);
	}

	bool tryMove(RaycastHit[] hits)
	{
		foreach (RaycastHit hit in hits)
		{
			if (hit.collider.gameObject.tag == "terrain" && !ui.overButton)
			{
				attackAnim1(false);
				moveAnim(true);
				nma.destination = hit.point;
				return (true);
			}
		}
		return (false);
	}

	IEnumerator Die()
	{
		dead = true;
		st.hp = 0;
		moveAnim(false);
		attackAnim1(false);
		anim.SetBool("die", true);
		ui.enableDeathPanel();
		yield return new WaitForSeconds(3.0f);
		Application.LoadLevel(Application.loadedLevel);
	}

	
	void Update ()
	{
		if (!dead)
		{
			_hits = Physics.RaycastAll(cam.ScreenPointToRay(Input.mousePosition), 400.0f);
			getEnemyInfo(_hits);
			if (nma.remainingDistance < 1.0f)
				stop();
			levelUpCheck();
			if (Input.GetMouseButton(0))
			{
				if (!tryAttack(_hits))
					tryMove(_hits);
			}
			else
				attackAnim1(false);
			if (st.hp <= 0)
				StartCoroutine(Die());
			cam.transform.position = transform.position - _camOffset;
			if (Input.GetKeyDown(KeyCode.G))
				itemgen.tryGenerateItem(transform.position, st.level, true);
		}
	}
}
