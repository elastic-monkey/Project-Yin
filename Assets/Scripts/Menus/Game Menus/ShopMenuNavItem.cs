using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopMenuNavItem : GameNavItem
{
    public Item Item;
    public Image ItemIcon;

    void Start()
    {
        UpdateSlot();
    }

    public override void OnSelect(IMenu manager)
    {
        if (Item == null)
            return;
        
        Data = new string[1];
        Data[0] = ((int)Item.Type).ToString();

        base.OnSelect(manager);
    }

    public void UpdateSlot()
    {
        if (Item != null)
        {
            ItemIcon.sprite = Item.Icon;
        }
        else
        {
            var tempColor = Color.white;
            tempColor.a = 0f;
            ItemIcon.color = tempColor;
        }
    }
}
