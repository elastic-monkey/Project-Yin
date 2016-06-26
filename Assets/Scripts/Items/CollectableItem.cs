using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public Item.ItemType Type;
	public Tags PlayerTag;

    private PlayerInventory _inventory;

	public void Start()
	{
        _inventory = GameManager.Instance.Player.Inventory;
	}

	public void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag(PlayerTag.ToString()))
		{
            _inventory.IncreaseStock(Type);
			Destroy(gameObject);
		}
	}
}
