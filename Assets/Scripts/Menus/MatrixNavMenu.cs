﻿using UnityEngine;
using System.Collections.Generic;

public class MatrixNavMenu : NavMenu
{
    public enum Organization
    {
        Column,
        Line
    }

    public bool Horizontal, Vertical;
    [Tooltip("Column organization means each array inside each Item is treated as either a column or a line, hence affecting navigation. " +
        "This is used only for buttons navigation! Changes in visual organization are defined by the user.")]
    public Organization MenuOrganization;
    public NavItemCollection[] Items;

    [SerializeField]
    private IndexPair _currentIndex;

    private void Start()
    {
        _currentIndex = new IndexPair(0, 0);
        FocusCurrent();
    }

    protected override void HandleInput()
    {
        if (PlayerInput.IsButtonDown(Axes.MenusVertical) && Vertical)
        {
            var v = PlayerInput.GetAxisRaw(Axes.MenusVertical);

            if (v != 0)
            {
                if (MenuOrganization == Organization.Column)
                {
                    if (v > 0)
                        FocusUp();
                    else
                        FocusDown();
                }
                else if (MenuOrganization == Organization.Line)
                {
                    if (v > 0)
                        FocusLeft();
                    else
                        FocusRight();
                }
            }
        }
        else if (PlayerInput.IsButtonDown(Axes.MenusHorizontal) && Horizontal)
        {
            var h = PlayerInput.GetAxisRaw(Axes.MenusHorizontal);

            if (h != 0)
            {
                if (MenuOrganization == Organization.Column)
                {
                    if (h > 0)
                        FocusRight();
                    else
                        FocusLeft();
                }
                else if (MenuOrganization == Organization.Line)
                {
                    if (h > 0)
                        FocusDown();
                    else
                        FocusUp();
                }
            }
        }
        else if (PlayerInput.IsButtonDown(Axes.Confirm))
        {
            SelectItem(_currentIndex);
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
            _currentIndex = new IndexPair(0, 0);
            FocusCurrent(true);
        }
    }

    protected override void FocusItem(NavItem item, bool silent = false)
    {
        base.FocusItem(item, silent);

        if (item != null)
        {
            item.Focus(true);
        }

        foreach (var collection in Items)
        {
            foreach (var other in collection.Items)
            {
                other.Focus(other == item);
            }
        }
    }

    private void FocusDown()
    {
        if (_currentIndex.Second < Items[_currentIndex.First].Items.Length - 1)
        {
            _currentIndex.Set(_currentIndex.First, _currentIndex.Second + 1);
            FocusCurrent();
        }
        else if (Cyclic)
        {
            _currentIndex.Set(_currentIndex.First, 0);
            FocusCurrent();
        }
    }

    private void FocusUp()
    {
        if (_currentIndex.Second > 0)
        {
            _currentIndex.Set(_currentIndex.First, _currentIndex.Second - 1);
            FocusCurrent();
        }
        else if (Cyclic)
        {
            _currentIndex.Set(_currentIndex.First, Items[_currentIndex.First].Items.Length - 1);
            FocusCurrent();
        } 
    }

    private void FocusRight()
    {
        if (_currentIndex.First < Items.Length - 1)
        {
            _currentIndex.Set(_currentIndex.First + 1, Mathf.Min(_currentIndex.Second, Items[_currentIndex.First + 1].Items.Length - 1));
            FocusCurrent();
        }
        else if (Cyclic)
        {
            _currentIndex.Set(0, Mathf.Min(_currentIndex.Second, Items[0].Items.Length - 1));
            FocusCurrent();
        } 
    }

    private void FocusLeft()
    {
        if (_currentIndex.First > 0)
        {
            _currentIndex.Set(_currentIndex.First - 1, Mathf.Min(_currentIndex.Second, Items[_currentIndex.First - 1].Items.Length - 1));
            FocusCurrent();
        }
        else if (Cyclic)
        {
            _currentIndex.Set(Items.Length - 1, Mathf.Min(_currentIndex.Second, Items[Items.Length - 1].Items.Length - 1));
            FocusCurrent();
        }
    }

    private NavItem GetItem(IndexPair index)
    {
        if (index.First < 0 || index.First >= Items.Length)
            return null;

        if (index.Second < 0 || index.Second > Items[index.First].Items.Length)
            return null;

        return Items[index.First].Items[index.Second];
    }

    public override void UnfocusAll()
    {
        foreach (var collection in Items)
        {
            foreach (var item in collection.Items)
            {
                FocusItem(item, true);
            }
        }
    }

    public override void FocusCurrent(bool silent = false)
    {
        FocusItem(GetItem(_currentIndex), silent);
    }

    private void SelectItem(IndexPair index)
    {
        if (index.First < 0 || index.First >= Items.Length || index.Second < 0 || index.Second >= Items[index.First].Items.Length)
        {
            Debug.LogWarning("Tried to select invalid item.");
            return;
        }

        Items[index.First].Items[index.Second].OnSelect();
    }

    public override List<NavItem> GetNavItems()
    {
        var list = new List<NavItem>();

        foreach (var line in Items)
        {
            foreach (var item in line.Items)
            {
                list.Add(item);
            }
        }

        return list;
    }

    public override NavItem GetCurrentNavItem()
    {
        return GetItem(_currentIndex);
    }
}

[System.Serializable]
public class NavItemCollection
{
    public string Name;
    public NavItem[] Items;
}

[System.Serializable]
public class IndexPair
{
    public int First;
    public int Second;

    public IndexPair()
        : this(-1, -1)
    {
    }

    public IndexPair(int first, int second)
    {
        Set(first, second);
    }

    public void Set(int first, int second)
    {
        First = first;
        Second = second;
    }
}