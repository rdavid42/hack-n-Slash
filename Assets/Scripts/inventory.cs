using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class inventory : MonoBehaviour
{
	public player				player;
	public GameObject			grid;
	public GameObject			equiped;
	public GameObject[]			inventorySlots;
	public GameObject[]			equipedSlots;
	public GameObject[]			inventoryItems;
	public GameObject[]			equipedItems;
	public UI					ui;
	public int					isize;
	public int					esize;

	public GameObject			dragged;
	public GameObject			currentItemDragged;
	public GameObject			draggedFrom;
	public GameObject			inside;
	public GameObject			insideEquiped;

	public GameObject			rightHand;
	public GameObject			lefthand;
	public GameObject			itemContainer;

	public bool					dropItem;

	// Use this for initialization
	void Start()
	{
		int i;

		dropItem = false;
		inside = null;
		currentItemDragged = null;
		draggedFrom = null;
		insideEquiped = null;

		isize = grid.transform.childCount;
		esize = equiped.transform.childCount;

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
		foreach (GameObject s in equipedSlots)
		{
			if (s == item)
				return (i);
			i++;
		}
		return (-1);
	}

	public void dragging()
	{
		ui.overButton = true;
		dragged.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
	}

	public bool addItem(GameObject item)
	{
		int i;

		for (i = 0; i < isize; i++)
		{
			if (inventoryItems[i] == null)
			{
				item.transform.SetParent(itemContainer.transform);
				Destroy(item.GetComponent<Rigidbody>());
				item.GetComponent<MeshCollider>().enabled = false;
				item.transform.GetChild(1).gameObject.SetActive(false);
				if (item.GetComponent<itemStats>().type == 0)
				{
					item.GetComponent<meleeAttack>().player = player;
					item.GetComponent<meleeAttack>().ist = item.GetComponent<itemStats>();
				}
				Image img = inventorySlots[i].transform.GetChild(0).GetComponent<Image>();
				img.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
				img.sprite = item.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
				inventoryItems[i] = item;
				item.SetActive(false);
				return (true);
			}
		}
		return (false);
	}

	public void getInsideCell()
	{
		EventSystem system = EventSystem.current;
		PointerEventData pointer = new PointerEventData(system);
		List<RaycastResult> hits = new List<RaycastResult>();
		int k;
		
		pointer.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		system.RaycastAll(pointer, hits);
		
		inside = null;
		dropItem = true;
		insideEquiped = null;
		for (k = 0; k < hits.Count; k++)
		{
			if (hits[k].gameObject.tag == "cell")
				inside = hits[k].gameObject;
			else if (hits[k].gameObject.tag == "equipmentCell")
				insideEquiped = hits[k].gameObject;
			else if (hits[k].gameObject.tag == "inventory" || hits[k].gameObject.tag == "cell")
				dropItem = false;
		}
	}

	public void equipItems()
	{
		if (equipedItems[0] != null)
		{
			equipedItems[0].transform.SetParent(rightHand.transform);
			equipedItems[0].SetActive(true);
			equipedItems[0].transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			equipedItems[0].transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
		}
		if (equipedItems[1] != null)
		{
			equipedItems[1].transform.SetParent(lefthand.transform);
			equipedItems[1].SetActive(true);
			equipedItems[1].transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
			equipedItems[1].transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
		}
	}

	private void showItemDesc()
	{
		int i;

		if (inside != null)
		{
			i = findSlotId(inside);
			if (inventoryItems[i] != null)
				player.ui.showItemPanel(inventoryItems[i].GetComponent<itemStats>());
			else
				player.ui.disableItemPanel();
		}
		else if (insideEquiped != null)
		{
			i = findEquipedId(insideEquiped);
			if (equipedItems[i])
				player.ui.showItemPanel(equipedItems[i].GetComponent<itemStats>());
			else
				player.ui.disableItemPanel();
		}
		else
			player.ui.disableItemPanel();
	}

	private void resetDrag()
	{
		Image c, d;

		d = dragged.GetComponent<Image>();
		c = draggedFrom.transform.GetChild(0).gameObject.GetComponent<Image>();
		c.sprite = d.sprite;
		c.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		d.sprite = null;
		d.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		draggedFrom = null;
		currentItemDragged = null;
		dragged.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		Image c, d, p;
		int id, i;
		GameObject prev;
		itemStats ist;

		getInsideCell();
		if (currentItemDragged != null)
			dragging();
		showItemDesc();
		if (Input.GetMouseButtonDown(0))
		{
			if (inside != null)
			{
				id = findSlotId(inside);
				if (id != -1 && inventoryItems[id] != null)
				{
					d = dragged.GetComponent<Image>();
					c = inside.transform.GetChild(0).gameObject.GetComponent<Image>();
					draggedFrom = inside;
					currentItemDragged = inventoryItems[id];
					d.sprite = c.sprite;
					d.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
					c.sprite = null;
					c.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
					dragging();
					dragged.SetActive(true);
				}
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if (currentItemDragged != null)
			{
				if (inside != null)
				{
					id = findSlotId(inside);
					if (id != -1)
					{
						if (inventoryItems[id] == null)
						{
							i = findSlotId(draggedFrom);
							d = dragged.GetComponent<Image>();
							c = inside.transform.GetChild(0).gameObject.GetComponent<Image>();
							c.sprite = d.sprite;
							c.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
							d.sprite = null;
							d.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
							inventoryItems[id] = inventoryItems[i];
							inventoryItems[i] = null;
							draggedFrom = null;
							currentItemDragged = null;
							dragged.SetActive(false);
						}
						else
						{
							i = findSlotId(draggedFrom);
							d = dragged.GetComponent<Image>();
							c = inside.transform.GetChild(0).gameObject.GetComponent<Image>();
							p = draggedFrom.transform.GetChild(0).gameObject.GetComponent<Image>();
							p.sprite = c.sprite;
							p.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
							c.sprite = d.sprite;
							c.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
							d.sprite = null;
							d.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
							prev = inventoryItems[i];
							inventoryItems[i] = inventoryItems[id];
							inventoryItems[id] = prev;
							draggedFrom = null;
							currentItemDragged = null;
							dragged.SetActive(false);
						}
					}
				}
				else if (insideEquiped != null)
				{
					id = findEquipedId(insideEquiped);
					ist = inventoryItems[findSlotId(draggedFrom)].GetComponent<itemStats>();
					if (id == ist.type)
					{
						if (equipedItems[id] == null)
						{
							i = findSlotId(draggedFrom);
							d = dragged.GetComponent<Image>();
							c = insideEquiped.transform.GetChild(0).gameObject.GetComponent<Image>();
							c.sprite = d.sprite;
							c.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
							d.sprite = null;
							d.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
							equipedItems[id] = inventoryItems[i];
							inventoryItems[i] = null;
							draggedFrom = null;
							currentItemDragged = null;
							dragged.SetActive(false);
						}
						else
						{
							i = findSlotId(draggedFrom);
							d = dragged.GetComponent<Image>();
							c = insideEquiped.transform.GetChild(0).gameObject.GetComponent<Image>();
							p = draggedFrom.transform.GetChild(0).gameObject.GetComponent<Image>();
							p.sprite = c.sprite;
							p.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
							c.sprite = d.sprite;
							c.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
							d.sprite = null;
							d.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
							prev = inventoryItems[i];
							inventoryItems[i] = equipedItems[id];
							equipedItems[id] = prev;
							draggedFrom = null;
							currentItemDragged = null;
							dragged.SetActive(false);
						}
					}
					else
						resetDrag();
				}
				else
				{
					if (dropItem)
					{
						i = findSlotId(draggedFrom);
						if (inventoryItems[i] != null)
						{
							d = dragged.GetComponent<Image>();
							p = draggedFrom.transform.GetChild(0).gameObject.GetComponent<Image>();
							d.sprite = null;
							d.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
							p.sprite = null;
							p.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
							GameObject drop = inventoryItems[i];
							drop.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.0f, player.transform.position.z);
							drop.transform.SetParent(null);
							drop.AddComponent<Rigidbody>();
							drop.GetComponent<MeshCollider>().enabled = true;
							drop.transform.GetChild(1).gameObject.SetActive(true);
							drop.SetActive(true);
							inventoryItems[i] = null;
							draggedFrom = null;
							currentItemDragged = null;
							dropItem = false;
						}
					}
					else
						resetDrag();
				}
			}
		}
		else if (Input.GetMouseButtonDown(1))
		{
			if (inside != null)
			{
				id = findSlotId(inside);
				if (id != -1)
				{
					c = inside.transform.GetChild(0).gameObject.GetComponent<Image>();
					if (inventoryItems[id] != null)
					{
						ist = inventoryItems[id].GetComponent<itemStats>();
						if (equipedItems[ist.type] == null)
						{
							d = equipedSlots[ist.type].transform.GetChild(0).gameObject.GetComponent<Image>();
							p = inside.transform.GetChild(0).gameObject.GetComponent<Image>();
							d.sprite = p.sprite;
							d.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
							p.sprite = null;
							p.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
							equipedItems[ist.type] = inventoryItems[id];
							inventoryItems[id] = null;
							equipItems();
						}
						else
						{
							d = equipedSlots[ist.type].transform.GetChild(0).gameObject.GetComponent<Image>();
							p = inside.transform.GetChild(0).gameObject.GetComponent<Image>();
							dragged.GetComponent<Image>().sprite = d.sprite;
							d.sprite = p.sprite;
							d.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
							p.sprite = dragged.GetComponent<Image>().sprite;
							p.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
							prev = equipedItems[ist.type];
							equipedItems[ist.type] = inventoryItems[id];
							inventoryItems[id] = prev;
							inventoryItems[id].transform.SetParent(null);
							inventoryItems[id].SetActive(false);
							equipItems();
						}
					}
				}
			}
		}
	}
}
