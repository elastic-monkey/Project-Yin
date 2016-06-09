using UnityEngine;
using System.Collections;

public class VerticalNavMenu : NavMenu
{
    public bool SendHorizontalInput;
    public NavItem[] Items;

    [SerializeField]
    private int _currentIndex;

    protected override void HandleInput()
    {
        if (PlayerInput.IsButtonDown(Axes.Vertical))
        {
            var v = -PlayerInput.GetAxisRaw(Axes.Vertical);

            if (v > 0)
            {
                FocusNext();
            }
            else if (v < 0)
            {
                FocusPrevious();
            }
        }
        else if (PlayerInput.IsButtonDown(Axes.Horizontal) && SendHorizontalInput)
        {
            OnHorizontalInput(_currentIndex, PlayerInput.GetAxisRaw(Axes.Horizontal));
        }
        else if (PlayerInput.IsButtonDown(Axes.Confirm))
        {
            OnItemSelected(_currentIndex);
        }
    }

    public override void SetActive(bool value)
    {
        base.SetActive(value);

        if (value)
        {
            if (Reset)
            {
                _currentIndex = 0;
            }
            FocusCurrent();
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

    protected override void FocusItem(NavItem item)
    {
        base.FocusItem(item);

        if (item != null)
        {
            item.Focus(true);
        }

        foreach (var other in Items)
        {
            other.Focus(other == item);
        }
    }

    private void FocusNext()
    {
        if (_currentIndex < Items.Length - 1)
        {
            _currentIndex += 1;
            FocusCurrent();
        }
        else if (Cyclic)
        {
            _currentIndex = 0;
            FocusCurrent();
        }
    }

    private void FocusPrevious()
    {
        if (_currentIndex > 0)
        {
            _currentIndex -= 1;
            FocusCurrent();
        }
        else if (Cyclic)
        {
            _currentIndex = Items.Length - 1;
            FocusCurrent();
        }
    }

    public override void UnfocusAll()
    {
        foreach (var item in Items)
        {
            item.Focus(false);
        }
    }

    public override void FocusCurrent()
    {
        FocusItem(GetItem(_currentIndex));
    }

    private void OnItemSelected(int index)
    {
        var item = GetItem(index);

        if (item != null)
            item.OnSelect(MenuManager);
    }

    private NavItem GetItem(int index)
    {
        if (index < 0 || index >= Items.Length)
        {
            Debug.Log("Tried to select invalid item.");
            return null;
        }

        return Items[index];
    }
}
