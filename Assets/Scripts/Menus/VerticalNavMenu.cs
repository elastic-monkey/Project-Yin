using UnityEngine;
using System.Collections;

public class VerticalNavMenu : NavMenu
{
    public bool Cyclic, Reset;
    public bool SendHorizontalInput;
    public NavItem[] Items;

    [SerializeField]
    private int _currentIndex;

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
        else if (PlayerInput.IsButtonDown(Axis.Nav_Horizontal) && SendHorizontalInput)
        {
            OnHorizontalInput(_currentIndex, PlayerInput.GetAxisRaw(Axis.Nav_Horizontal));
        }
        else if (PlayerInput.IsButtonDown(Axis.Fire1) || PlayerInput.IsButtonDown(Axis.Submit))
        {
            OnItemSelected(_currentIndex);
        }
    }

    protected override void OnSetActive(bool value)
    {
        if (value)
        {
            if (Reset)
            {
                FocusItem(0);
            }
            else
            {
                FocusItem(_currentIndex);
            }
        }
        else
        {
            UnfocusAll();
        }
    }

    private void OnHorizontalInput(int index, float value)
    {
        var item = GetItem(index);

        if (item != null)
            item.OnHorizontalInput(value);
    }

    private void FocusItem(int index)
    {
        index = Mathf.Clamp(index, 0, Items.Length - 1);

        for (var i = 0; i < Items.Length; i++)
        {
            Items[i].OnFocus(i == index);
        }

        _currentIndex = index;
    }

    private void FocusNext()
    {
        if (_currentIndex < Items.Length - 1)
        {
            FocusItem(_currentIndex + 1);
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
        if (_currentIndex > 0)
        {
            FocusItem(_currentIndex - 1);
        }
        else
        {
            if (Cyclic)
            {
                FocusItem(Items.Length - 1);
            } 
        } 
    }

    public override void UnfocusAll()
    {
        foreach(var item in Items)
        {
            item.OnFocus(false);
        }
    }

    public override void FocusCurrent()
    {
        FocusItem(_currentIndex);
    }

    private void OnItemSelected(int index)
    {
        var item = GetItem(index);

        if(item != null)
            item.OnSelect(MenuManager);
    }

    private NavItem GetItem(int index)
    {
        if (index < 0 || index >= Items.Length)
        {
            Debug.LogWarning("Tried to select invalid item.");
            return null;
        }

        return Items[index];
    }
}
