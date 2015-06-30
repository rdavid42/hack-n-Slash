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
	public spellEffect[]			spell;
	public GameObject[]				enemies;
	public GameObject				lifePotion;

	private RaycastHit[]			_hits;

	private GameObject				_target;
	private GameObject				_pickUpTarget;

	public static bool				cheatsEnabled = true;

	public ParticleSystem			pLevelUp;

	public AudioSource[]			slashSounds;
	public GameObject[]				bloodEffects;

	void Start()
	{
		setSpawners();
		canLevelUp = false;
		attacking = false;
		canAttack = false;
		_target = null;
		_pickUpTarget = null;
		moveAnim(false);
	}

	void setSpawners()
	{
		spawner			sp;
		GameObject[]	spawners;

		spawners = GameObject.FindGameObjectsWithTag("spawner");
		foreach (GameObject s in spawners)
		{
			sp = s.GetComponent<spawner>();
			sp.enemies = enemies;
			sp.lifePotion = lifePotion;
			sp.player = this;
		}
	}

	void OnLevelWasLoaded(int level)
	{
		if (level == 0)
		{
		}
		if (level == 1)
		{
			setSpawners();
			transform.localPosition = new Vector3(71.52f, 56.19f, 45.28f);
			gameObject.GetComponent<NavMeshAgent>().enabled = true;
		}
		if (level == 2)
		{
			setSpawners();
			transform.localPosition = new Vector3(199.99f, 0.55f, 16.03f);
			gameObject.GetComponent<NavMeshAgent>().enabled = true;
		}
		if (level == 3)
		{
			setSpawners();
			transform.localPosition = new Vector3(98.6f, 0.55f, 2.15f);
			gameObject.GetComponent<NavMeshAgent>().enabled = true;
		}
	}

	void deadReplace()
	{
		int lvl = Application.loadedLevel;

		if (lvl == 0)
		{
			transform.localPosition = new Vector3(210.16f, 15.29f, 67.14f);
		}
		if (lvl == 1)
		{
			transform.localPosition = new Vector3(71.52f, 56.19f, 45.28f);
		}
		if (lvl == 2)
		{
			transform.localPosition = new Vector3(199.99f, 0.55f, 16.03f);
		}
		if (lvl == 3)
		{
			transform.localPosition = new Vector3(98.6f, 0.55f, 2.15f);
		}
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
		pLevelUp.Play();
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
				if (e)
				{
					ui.enableEnemyPanel();
					ui.updateEnemyInfo(e);
				}
				return ;
			}
		}
		if (_target != null)
		{
			enemy e = _target.GetComponent<enemy>();
			if (e)
			{
				ui.enableEnemyPanel();
				ui.updateEnemyInfo(e);
			}
		}
		else
			ui.disableEnemyPanel();
	}
	
	IEnumerator Die()
	{
		dead = true;
		st.hp = 0;
		moveAnim(false);
		stopAttack();
		anim.SetBool("die", true);
		ui.enableDeathPanel();
		yield return new WaitForSeconds(3.0f);
		deadReplace();
		ui.disableDeathPanel();
		anim.SetBool("die", false);
		st.xp = 0;
		dead = false;
		st.hp = st.maxHp;
		nma.ResetPath();
		nma.Stop();
		nma.velocity = Vector3.zero;
		yield return new WaitForSeconds(3.0f);
	}

	void OnTriggerEnter(Collider c)	
	{
		if (c.gameObject.tag == "endLevel")
		{
			gameObject.GetComponent<NavMeshAgent>().enabled = false;
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}

	void OnTriggerStay(Collider c)
	{
		if (c.gameObject.tag == "enemyTrigger")
			canAttack = true;
		if (c.gameObject.tag == "itemPickUp" && c.gameObject.transform.parent.gameObject == _pickUpTarget)
		{
			inv.addItem(c.gameObject.transform.parent.gameObject);
			_pickUpTarget = null;
		}
		if (Input.GetMouseButton(0) && c.gameObject == _target)
			attack(_target.transform.position);
	}

	void moveAnim(bool state)
	{
		anim.SetBool("run", state);
	}

	void attackAnim1(bool state)
	{
		anim.SetBool("attack", state);
	}

	void attack(Vector3 pos)
	{
		if (inv.equipedItems[0] != null)
			anim.speed = inv.equipedItems[0].GetComponent<itemStats>().speed;
		else
			anim.speed = 1.0f;
			transform.LookAt(pos);
			attacking = true;
			moveAnim(false);
			attackAnim1(true);
	}

	void stopAttack()
	{
		anim.speed = 1.0f;
		_target = null;
		canAttack = false;
		attacking = false;
		attackAnim1(false);
	}

	bool tryPickUp(RaycastHit[] hits)
	{
		foreach (RaycastHit hit in hits)
		{
			if (hit.collider.gameObject.tag == "itemPickUp")
			{
				_pickUpTarget = hit.collider.gameObject.transform.parent.gameObject;
				return (true);
			}
		}
		_pickUpTarget = null;
		return (false);
	}

	bool tryMove(RaycastHit[] hits)
	{
		foreach (RaycastHit hit in hits)
		{
			if (hit.collider.gameObject.tag == "terrain" && !ui.overButton)
			{
				stopAttack();
				moveAnim(true);
				nma.destination = hit.point;
				return (true);
			}
		}
		return (false);
	}
	
	bool tryAttack(RaycastHit[] hits)
	{
		foreach (RaycastHit hit in hits)
		{
			if (hit.collider.gameObject.tag == "enemyTrigger")
			{
				_target = hit.collider.gameObject;
				if (canAttack)
					attack(hit.collider.gameObject.transform.position);
				return (true);
			}
		}
		attacking = false;
		attackAnim1(false);
		return (false);
	}

	void handleSpells()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) && spell[0].canBeUse)
		{
			transform.LookAt(new Vector3(_hits[0].point.x, 0, _hits[0].point.z));
			spell[0].gameObject.transform.position = transform.position;
			spell[0].gameObject.transform.rotation = transform.rotation;
			spell[0].launch();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) && spell[1].canBeUse)
		{
			transform.LookAt(new Vector3(_hits[0].point.x, 0, _hits[0].point.z));
			spell[1].gameObject.transform.position = transform.position;
			spell[1].gameObject.transform.rotation = transform.rotation;
			spell[1].launch();
			st.hp += 30;
			if (st.hp > st.maxHp)
				st.hp = st.maxHp;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3) && spell[2].canBeUse)
		{
			transform.LookAt(new Vector3(_hits[0].point.x, 0, _hits[0].point.z));
			spell[2].gameObject.transform.position = new Vector3(_hits[0].point.x, _hits[0].point.y, _hits[0].point.z);
			spell[2].launch();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4) && spell[3].canBeUse)
		{
			spell[3].gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
			spell[3].launch();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5) && spell[4].canBeUse)
		{
			spell[4].launch();
		}
	}

	void handleCheats()
	{
		if (cheatsEnabled)
		{
			if (Input.GetKeyDown(KeyCode.G))
				itemgen.tryGenerateItem(transform.position, st.level, true);
			if (Input.GetKeyDown(KeyCode.L))
			{
				levelUp();
				st.xp = 0;
			}
		}
	}
	
	void Update ()
	{
		if (!dead && nma.enabled)
		{
			_hits = Physics.RaycastAll(cam.ScreenPointToRay(Input.mousePosition), 400.0f);
			getEnemyInfo(_hits);
			if (nma.remainingDistance < 1.0f)
				stop();
			levelUpCheck();
			if (Input.GetMouseButton(0))
			{
				if (_target != null)
				{
					if (canAttack)
						attack(_target.transform.position);
					else
					{
						moveAnim(true);
						nma.destination = _target.transform.position;
					}
				}
				else
				{
					if (!tryAttack(_hits))
						tryMove(_hits);
				}
			}
			if (Input.GetMouseButtonDown(0))
				tryPickUp(_hits);
			if (Input.GetMouseButtonUp(0))
				stopAttack();
			if (st.hp <= 0)
				StartCoroutine(Die());
			handleSpells();
			handleCheats();
		}
	}
}
