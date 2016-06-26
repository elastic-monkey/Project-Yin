using UnityEngine;
using UnityEngine.UI;

public class UpgradableNavItem : NavItem
{
    public Upgradable.UpgradableTypes Type;
    [Range(1, 4)]
    public int Level = 1;

    private PlayerMenu _playerMenu;

    protected override void Awake()
    {
        base.Awake();

        _playerMenu = GetComponentInParent<PlayerMenu>();
    }

    protected override void OnFocus(bool value)
    {
        // Do Stuff
    }

    public override void OnSelect()
    {
        _playerMenu.Upgrade(Type);
    }

    public void SetActive(bool value, Upgradable upgradable, bool purchased = false)
    {
        var img = TargetGraphic as Image;
        if (value)
        {
            img.color = upgradable.ActiveColor;

            if (purchased)
            {
                img.sprite = upgradable.ActiveSprite;
            }
            else
            {
                img.sprite = upgradable.DefaultSprite;
            }
        }
        else
        {
            img.color = upgradable.DefaultColor;
            img.sprite = upgradable.DefaultSprite;
        }
    }
}
