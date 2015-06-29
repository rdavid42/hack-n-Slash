using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public player					player;
	public bool						overButton;

	// main screen
	public Text						life;
	public Text						xp;
	public RectTransform			lifeBar;
	public RectTransform			xpBar;
	public Text						lvl;
	public GameObject				levelUpButton;

	// stat panel
	public GameObject				statPanel;
	public GameObject				upStrButton;
	public GameObject				upAgiButton;
	public GameObject				upCstButton;
	public Text						statUpgradePoints;
	public Text						statStr;
	public Text						statAgi;
	public Text						statCst;
	public Text						statArm;
	public Text						statDmg;
	public Text						statMaxHp;
	public Text						statXp;
	public Text						statXpNext;
	public Text						statGold;
	public Text						statLvl;
	public Text						statDps;

	// enemy panel
	public GameObject				enemyui;
	public Text						enemyName;
	public Text						enemyHp;
	public RectTransform			enemyHpBar;

	// death panel
	public GameObject				deadPanel;

	// inventory panel
	public GameObject				inventory;

	// inventory description
	public GameObject				itemPanel;
	public Text						tItemName;
	public Text						tItemStats;

	void Start()
	{
		overButton = false;
	}

	public void enableLevelUpButton()
	{
		if (!levelUpButton.activeSelf)
			levelUpButton.SetActive(true);
	}

	public void disableLevelUpButton()
	{
		if (levelUpButton.activeSelf)
			levelUpButton.SetActive(false);
	}

	public void enableEnemyPanel()
	{
		if (!enemyui.activeSelf)
			enemyui.SetActive(true);
	}

	public void disableEnemyPanel()
	{
		if (enemyui.activeSelf)
			enemyui.SetActive(false);
	}

	public void enableDeathPanel()
	{
		if (!deadPanel.activeSelf)
			deadPanel.SetActive(true);
	}

	public void disableDeathPanel()
	{
		if (deadPanel.activeSelf)
			deadPanel.SetActive(false);
	}
	
	public void disablePanel(GameObject panel)
	{
		if (panel.activeSelf)
			panel.SetActive(false);
		overButton = false;
	}
	
	public void enablePanel(GameObject panel)
	{
		if (!panel.activeSelf)
			panel.SetActive(true);
	}

	public void switchPanel(GameObject panel)
	{
		if (panel.activeSelf)
			disablePanel(panel);
		else
			enablePanel(panel);
	}

	public void upStrClick()
	{
		player.upgradeStrength();
	}

	public void upAgiClick()
	{
		player.upgradeAgility();
	}

	public void upCstClick()
	{
		player.upgradeConstitution();
	}

	public void mouseEnter()
	{
		overButton = true;
	}
	
	public void mouseExit()
	{
		overButton = false;
	}

	public void updateEnemyInfo(enemy e)
	{
		enemyHpBar.transform.localScale = new Vector3((float)(e.st.hp * 100.0f / e.st.maxHp) / 100.0f, 1, 1);
		enemyName.text = e.enemyName;
		enemyHp.text = e.st.hp + " / " + e.st.maxHp;
	}

	public void printMainPanel()
	{
		life.text = player.st.hp.ToString() + " / " + player.st.maxHp.ToString();
		xp.text = player.st.xp.ToString() + " / " + player.st.xpNext.ToString();
		lvl.text = player.st.level.ToString();
		lifeBar.transform.localScale = new Vector3((float)(player.st.hp * 100.0f / player.st.maxHp) / 100.0f, 1, 1);
		xpBar.transform.localScale = new Vector3((float)(player.st.xp * 100.0f / player.st.xpNext) / 100.0f, 1, 1);
	}

	public void showItemPanel(itemStats item)
	{
		itemPanel.SetActive(true);
		tItemName.text = item.finalName;
		tItemStats.text = "[Lv. " + item.ilvl + "]\n";
		if (item.type == 0)
		{
			// handle weapon
			tItemStats.text += "dmg: " + item.finalMinDmg.ToString() + " - " + item.finalMaxDmg.ToString();
			if (item.qualitybonusDmg > 0)
				tItemStats.text += " +" + item.qualitybonusDmg.ToString() + "\n";
			else
				tItemStats.text += "\n";
			tItemStats.text += "aps: " + item.speed.ToString() + "\n";
			tItemStats.text += "dps: " + item.estimatedDps.ToString() + "\n";
		}
		else if (item.type == 1)
		{
			// handle shield
			tItemStats.text += "armor: " + item.finalArmor.ToString() + "\n";
		}
	}

	public void disableItemPanel()
	{
		if (itemPanel.activeSelf)
		{
			tItemName.text = "";
			tItemStats.text = "";
			itemPanel.SetActive(false);
		}
	}

	public void printStatPanel()
	{
		itemStats	item;
		int			dps;

		if (statPanel.activeSelf)
		{
			statStr.text = player.st.strength.ToString();
			statAgi.text = player.st.agility.ToString();
			statCst.text = player.st.constitution.ToString();
			statArm.text = player.st.armor.ToString();
			statDmg.text = player.st.minDamage.ToString() + "-" + player.st.maxDamage.ToString();
			statMaxHp.text = player.st.maxHp.ToString();
			statXp.text = player.st.xp.ToString();
			statXpNext.text = player.st.xpNext.ToString();
			statGold.text = player.st.money.ToString();
			statUpgradePoints.text = player.upgradePoints.ToString();
			statLvl.text = "[Lv." + player.st.level + "]";
			if (player.inv.equipedItems[0] != null)
			{
				item = player.inv.equipedItems[0].GetComponent<itemStats>();
				dps = player.st.dmgPerSecond() + item.dmgPerSecond();
			}
			else
				dps = player.st.dmgPerSecond();
			statDps.text = dps.ToString();
			if (player.upgradePoints > 0)
			{
				upStrButton.SetActive(true);
				upAgiButton.SetActive(true);
				upCstButton.SetActive(true);
			}
			else
			{
				upStrButton.SetActive(false);
				upAgiButton.SetActive(false);
				upCstButton.SetActive(false);
				levelUpButton.SetActive(false);
				player.upgradePoints = 0;
			}
		}
	}

	void Update()
	{
		printMainPanel();
		printStatPanel();
		if (Input.GetKeyDown(KeyCode.C))
			switchPanel(statPanel);
		if (Input.GetKeyDown(KeyCode.I))
			switchPanel(inventory);
	}
}
