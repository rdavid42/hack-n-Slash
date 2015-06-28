using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inventory : MonoBehaviour
{
	public GameObject			grid;
	public GameObject			equiped;
	public GameObject[]			inventorySlots;
	public GameObject[]			equipedSlots;
	public GameObject[]			inventoryItems;
	public GameObject[]			equipedItems;
	public int					currentDragged;
	public GameObject			dragged;
	public UI					ui;
	public int					isize;
	public int					esize;
	// Use this for initialization
	void Start()
	{
		int i;
		
		isize = grid.transform.childCount;
		esize = equiped.transform.childCount;

		currentDragged = -1;

		inventoryItems = new GameObject[isize];
		inventorySlots = new GameObject[isize];
		equipedItems = new GameObject[esize];
		equipedSlots = new GameObject[esize];

		for (i = 0; i < isize; ++i)
			inventoryItems[i] = null;
		i = 0;
		foreach (Transform child in grid.transform)
		{
			inventorySlots[i] = child.gameObject;
			i++;
		}

		for (i = 0; i < esize; ++i)
			equipedItems[i] = null;
		i = 0;
		foreach (Transform child in equiped.transform)
		{
			equipedSlots[i] = child.gameObject;
			i++;
		}
	}

	public void beginDrag(GameObject item)
	{
		Image current = item.GetComponent<Image>();
		int id = findSlotId(item.transform.parent.gameObject);
		if (id != -1)
		{
			dragged.SetActive(true);
			Image drag = dragged.GetComponent<Image>();
			drag.sprite = current.sprite;
			current.sprite = null;
			current.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			currentDragged = id;
		}
	}

	public void dragging()
	{
		ui.overButton = true;
		dragged.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
	}

	public void endDrag(GameObject item)
	{
		int			id = findSlotId(item.transform.parent.gameObject);
		Image		current = item.GetComponent<Image>();

		if (id != -1)
		{

		}
		dragged.SetActive(false);
		currentDragged = -1;
	}

	private int findSlotId(GameObject item)
	{
		int i;

		i = 0;
		foreach (GameObject s in inventorySlots)
		{
			if (s == item)
				return (i);
			i++;
		}
		return (-1);
	}

	private int findEquipedId(GameObject item)
	{
		int i;
		
		i = 0;
		foreach (GameObject s in inventorySlots)
		{
			if (s == item)
				return (i);
			i++;
		}
		return (-1);
	}

	public void addItem(GameObject item)
	{
		int i;

		for (i = 0; i < isize; i++)
		{
			if (inventoryItems[i] == null)
			{
				Destroy(item.GetComponent<Rigidbody>());
				item.GetComponent<MeshCollider>().enabled = false;
				Image img = inventorySlots[i].transform.GetChild(0).GetComponent<Image>();
				img.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				img.sprite = item.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
				inventoryItems[i] = item;
				item.SetActive(false);
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if (currentDragged != -1)
			dragging();
	}
}
