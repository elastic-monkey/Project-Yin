using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyItemNavItem : NavItem
{
    public Item.ItemType Type;

    private StoreMenu _storeMenu;
    private Sprite _initialSprite;

    protected override void Awake()
    {
        base.Awake();

        var img = TargetGraphic as Image;
        img.type = Image.Type.Simple;
        img.preserveAspect = true;
        _initialSprite = img.sprite;

        _storeMenu = GetComponentInParent<StoreMenu>();
        FocusedColor = Color.white;
        DisabledColor = Color.clear;
    }

    protected override void OnFocus(bool value)
    {
        // Do stuff
    }

    public override void OnSelect()
    {
        _storeMenu.BuyItem(Type);
    }

    public void SetSprite(Sprite spr)
    {
        var img = TargetGraphic as Image;

        if (spr == null)
        {
            img.sprite = _initialSprite;
            img.color = Color.clear;
        }
        else
        {
            img.sprite = spr;
            img.color = Color.white;
        }
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
