using UnityEngine;
using System.Collections;

public class SellComponentsNavItem : CloseMenuNavItem
{
    private StoreMenu _storeMenu;

    protected override void Awake()
    {
        base.Awake();

        _storeMenu = GetComponentInParent<StoreMenu>();
    }

    public override void OnSelect()
    {
        _storeMenu.SellComponents();

        base.OnSelect();
    }

    private void OnEnable()
    {
        if (GetComponentInParent<StoreMenu>() == null)
        {
            Debug.LogError("[SellComponentsNavItem] requires a parent StoreMenu!");
            Destroy(gameObject);
        }
    }
}
