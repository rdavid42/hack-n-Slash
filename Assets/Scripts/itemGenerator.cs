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

		items = new GameObject[itemCount];
		i = 0;
		foreach (Transform child in transform)
		{
			items[i] = child.gameObject;
			i++;
		}
	}

	public GameObject tryGenerateItem(Vector3 pos, int enemylvl, bool force)
	{
		if (Random.Range(0, 3) == 0 || force)
		{
			int id = Random.Range(0, items.Length - 1);
			GameObject go = items[id];
			itemStats ist = go.GetComponent<itemStats>();
			int ilvl = enemylvl + Random.Range(-2, 3);
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
