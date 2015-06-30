using UnityEngine;
using System.Collections;

public class itemGenerator : MonoBehaviour
{
	public int					itemCount;
	public player				player;
	public GameObject[]			items;

	// Use this for initialization
	void Start()
	{
		int		i;

		itemCount = gameObject.transform.childCount;
		items = new GameObject[itemCount];
		i = 0;
		foreach (Transform child in transform)
		{
			items[i] = child.gameObject;
			items[i].GetComponent<itemStats>().size = (int)((items[i].GetComponent<CapsuleCollider>().height / 2.0f) * 100.0f);
			i++;
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
