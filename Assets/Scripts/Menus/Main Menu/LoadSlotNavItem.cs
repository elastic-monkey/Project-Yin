using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadSlotNavItem : NavItem
{
    [Range(1, 4)]
    public int Slot;

    private LoadMenu _loadMenu;

    protected override void Awake()
    {
        base.Awake();

        _loadMenu = GetComponentInParent<LoadMenu>();
    }

    public void Start()
    {
        var text = TargetGraphic as Text;
        text.text = _loadMenu.GetSlotName(Slot);
    }

    public override void OnSelect()
    {
        _loadMenu.Load(Slot);
    }
}
