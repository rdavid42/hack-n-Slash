using UnityEngine;
using System.Collections;

public class itemGenerator : MonoBehaviour
{
	public enum Items
	{
		weapon = 0,
		shield
	}
	public int					itemTypeCount;
	public player				player;
	public GameObject[]			items;
	public GameObject[][]		sortedItems;
	public int					itemCount;
	public int[]				itemCounts;

	// Use this for initialization
	void Start()
	{
		int i;
		itemStats it;
		int[] tmpCounts;

		itemTypeCount = Items.GetNames(typeof(Items)).Length;
		itemCount = gameObject.transform.childCount;
		items = new GameObject[itemCount];
		i = 0;
		// Get items
		foreach (Transform child in transform)
		{
			items[i] = child.gameObject;
			it = items[i].GetComponent<itemStats>();
			it.size = (int)((items[i].GetComponent<CapsuleCollider>().height / 2.0f) * 100.0f);
			i++;
		}
		// get number of each item type
		itemCounts = new int[itemTypeCount];
		for (i = 0; i < itemTypeCount; ++i)
			itemCounts[i] = 0;
		i = 0;
		foreach (GameObject item in items)
		{
			itemCounts[items[i].GetComponent<itemStats>().type]++;
			i++;
		}
		// Sort all items by type
		i = 0;
		sortedItems = new GameObject[itemTypeCount][];
		for (i = 0; i < itemTypeCount; ++i)
		{
			sortedItems[i] = new GameObject[itemCounts[i]];
		}
		tmpCounts = new int[itemTypeCount];
		for (i = 0; i < itemTypeCount; ++i)
			tmpCounts[i] = 0;
		foreach (GameObject item in items)
		{
			it = item.GetComponent<itemStats>();
			sortedItems[it.type][tmpCounts[it.type]] = item;
			tmpCounts[it.type]++;
		}
	}

	public GameObject tryGenerateItem(Vector3 pos, int enemylvl, bool force)
	{
		if (Random.Range(0, 3) == 0 || force)
		{
			int id = Random.Range(0, items.Length);
			GameObject go = items[id];
			itemStats ist = go.GetComponent<itemStats>();
			int ilvl = enemylvl + Random.Range(-1, 2);
			if (ilvl < 1)
				ilvl = 1;
			ist.generate(ilvl);
			Vector3 p = pos;
			p.y += 1.0f;
			GameObject igo = (GameObject)Instantiate(go, p, Quaternion.Euler(0.0f, 0.0f, (float)Random.Range(0, 360)));
			igo.SetActive(true);
			return (igo);
 		}
		return (null);
	}

	// Update is called once per frame
	void Update()
	{
	
	}
}
