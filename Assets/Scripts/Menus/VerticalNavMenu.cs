﻿using UnityEngine;
using System.Collections.Generic;

public class VerticalNavMenu : NavMenu
{
    public bool SendHorizontalInput;
    public NavItem[] Items;

    [SerializeField]
    private int _currentIndex;

    protected override void HandleInput()
    {
        if (PlayerInput.IsButtonDown(Axes.MenusVertical))
        {
            var v = -PlayerInput.GetAxisRaw(Axes.MenusVertical);

            if (v > 0)
            {
                FocusNext();
            }
            else if (v < 0)
            {
                FocusPrevious();
            }
        }
        else if (PlayerInput.IsButtonDown(Axes.MenusHorizontal) && SendHorizontalInput)
        {
            OnHorizontalInput(_currentIndex, PlayerInput.GetAxisRaw(Axes.MenusHorizontal));
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
            UnfocusAll();
            FocusCurrent(true);
        }
        else if (Reset)
        {
            _currentIndex = 0;
            FocusCurrent(true);
        }
    }

    private void OnHorizontalInput(int index, float value)
    {
        var item = GetItem(index);

        if (item != null)
            item.OnHorizontalInput(value);
    }

    protected override void FocusItem(NavItem item, bool silent = false)
    {
        base.FocusItem(item, silent);

        if (item != null)
        {
            item.Focus(true);
        }

        foreach (var other in Items)
        {
            if (other == null)
            {
                Debug.LogWarning("Tried to focus null object. Check your object assignment.");
                continue;
            }

            if (other == item)
            {
                
            }

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
            FocusItem(item, true);
        }
    }

    public override void FocusCurrent(bool silent = false)
    {
        FocusItem(GetItem(_currentIndex), silent);
    }

    private void OnItemSelected(int index)
    {
        var item = GetItem(index);

        if (item != null)
        {
            item.OnSelect();
        }
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

    public override List<NavItem> GetNavItems()
    {
        return new List<NavItem>(Items);
    }

    public override NavItem GetCurrentNavItem()
    {
        return GetItem(_currentIndex);
    }
}
