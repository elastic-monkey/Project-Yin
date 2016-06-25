using UnityEngine;
using UnityEngine.UI;

public class UpgradableNavItem : NavItem
{
    public Upgradable.UpgradableTypes Type;
    public Image IconImage;

    private PlayerMenu _playerMenu;

    protected override void Awake()
    {
        base.Awake();

        _playerMenu = GetComponentInParent<PlayerMenu>();
    }

    public override void OnSelect()
    {
        _playerMenu.Upgrade(Type);
    }
}
