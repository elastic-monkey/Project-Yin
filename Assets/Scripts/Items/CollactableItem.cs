using UnityEngine;
using System.Collections;

public class CollactableItem : MonoBehaviour
{
    public Item Item;
    public Tags PlayerTag;
    [SerializeField]
    private InventoryMenu _inventory;

    public void Start(){
        _inventory = GameObject.Find("InventorySubMenu").GetComponent<InventoryMenu>();
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(PlayerTag.ToString()))
        {
            _inventory.AddItemToInventory(Item);
            Destroy(this.gameObject);
        }
    }
}
