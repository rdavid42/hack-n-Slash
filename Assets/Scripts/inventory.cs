using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
	public GameObject			grid;
	public GameObject[]			slots;
	public int					size;
	public GameObject[]			items;
	public GameObject			rightHand;
	public GameObject			leftHand;

	// slots
	public GameObject			rightHandSlot;
	// Use this for initialization
	void Start()
	{
		int i;

		size = 18;
		items = new GameObject[size];
		for (i = 0; i < size; ++i)
			items[i] = null;
		slots = new GameObject[size];
		i = 0;
		foreach (Transform child in grid.transform)
		{
			slots[i] = child.gameObject;
			i++;
		}
	}

	public void addItem(GameObject item)
	{
		int i;

		for (i = 0; i < size; i++)
		{
			if (items[i] == null)
			{
				Destroy(item.GetComponent<Rigidbody>());
				item.GetComponent<MeshCollider>().enabled = false;
				Image img = slots[i].transform.GetChild(0).GetComponent<Image>();
				img.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				img.sprite = item.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
				items[i] = item;
				item.SetActive(false);
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
