using UnityEngine;
using System.Collections;

public class BuyItemNavItem : NavItem
{
    public Item.ItemType Type;

    private StoreMenu _storeMenu;

    protected override void Awake()
    {
        base.Awake();

        _storeMenu = GetComponentInParent<StoreMenu>();
    }

    public override void OnSelect()
    {
        _storeMenu.BuyItem(Type);
    }

    private void OnEnable()
    {
        if (GetComponentInParent<StoreMenu>() == null)
        {
            Debug.LogError("[BuyItemNavItem] requires a parent StoreMenu!");
            Destroy(gameObject);
        }
    }
}
