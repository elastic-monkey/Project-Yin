using UnityEngine;
using System.Collections;

[RequireComponent(typeof(IAnimatedPanel))]
public class VerticalNavMenu : NavMenu
{
    public bool Cyclic, Reset;
    public MenuManager MenuManager;
    public NavItem[] Items;

    [SerializeField]
    private int _currentItem;

    private void Start()
    {
        FocusItem(0);
    }

    protected override void OnUpdate()
    {
        if (PlayerInput.IsButtonDown(Axis.Nav_Vertical))
        {
            var v = -PlayerInput.GetAxisRaw(Axis.Nav_Vertical);

            if (v > 0)
            {
                FocusNext();
            }
            else if (v < 0)
            {
                FocusPrevious();
            }
        }
        else if (PlayerInput.IsButtonDown(Axis.Fire1) || PlayerInput.IsButtonDown(Axis.Submit))
        {
            OnItemSelected(_currentItem);
        }
    }

    protected override void OnSetActive(bool value)
    {
        if (!value && Reset)
            FocusItem(0);
    }

    private void FocusItem(int index)
    {
        index = Mathf.Clamp(index, 0, Items.Length - 1);

        for (var i = 0; i < Items.Length; i++)
        {
            Items[i].Focus(i == index);
        }

        _currentItem = index;
    }

    private void FocusNext()
    {
        if (_currentItem < Items.Length - 1)
        {
            FocusItem(_currentItem + 1);
        }
        else
        {
            if (Cyclic)
            {
                FocusItem(0);
            } 
        }
    }

    private void FocusPrevious()
    {
        if (_currentItem > 0)
        {
            FocusItem(_currentItem - 1);
        }
        else
        {
            if (Cyclic)
            {
                FocusItem(Items.Length - 1);
            } 
        } 
    }
        
    private void OnItemSelected(int index)
    {
        if (index < 0 || index >= Items.Length)
        {
            Debug.LogWarning("Tried to select invalid item.");
            return;
        }

        Items[index].OnSelect(MenuManager);
    }
}
