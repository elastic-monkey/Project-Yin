using UnityEngine;
using System.Collections;

public class MatrixNavMenu : NavMenu
{
    public enum Organization
    {
        Column,
        Line
    }

    public bool Horizontal, Vertical;
    public bool Cyclic, Reset;
    [Tooltip("Column organization means each array inside each Item is treated as either a column or a line, hence affecting navigation. " +
        "This is used only for buttons navigation! Changes in visual organization are defined by the user.")]
    public Organization MenuOrganization;
    public RectTransform HoverIcon;
    public NavItemCollection[] Items;

    [SerializeField]
    private IndexPair _currentIndex;

    private void Start()
    {
        FocusItem(new IndexPair(0, 0));
    }

    protected override void OnUpdate()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (PlayerInput.IsButtonDown(Axis.Nav_Vertical) && Vertical)
        {
            var v = PlayerInput.GetAxisRaw(Axis.Nav_Vertical);

            if (v != 0)
            {
                if (MenuOrganization == Organization.Column)
                {
                    if (v > 0)
                        FocusUp();
                    else
                        FocusDown();
                }
                else if(MenuOrganization == Organization.Line)
                {
                    if (v > 0)
                        FocusLeft();
                    else
                        FocusRight();
                }
            }
        }
        else if (PlayerInput.IsButtonDown(Axis.Nav_Horizontal) && Horizontal)
        {
            var h = PlayerInput.GetAxisRaw(Axis.Nav_Horizontal);

            if (h != 0)
            {
                if (MenuOrganization == Organization.Column)
                {
                    if (h > 0)
                        FocusRight();
                    else
                        FocusLeft();
                }
                else if(MenuOrganization == Organization.Line)
                {
                    if (h > 0)
                        FocusDown();
                    else
                        FocusUp();
                }
            }
        }
        else if (PlayerInput.IsButtonDown(Axis.Fire1) || PlayerInput.IsButtonDown(Axis.Submit))
        {
            OnItemSelected(_currentIndex);
        }
    }

    public override void OnSetActive(bool value)
    {
        base.OnSetActive(value);

        HoverIcon.gameObject.SetActive(value);
        if (!value && Reset)
        {
            FocusItem(new IndexPair(0, 0));
        }
    }

    private void FocusItem(IndexPair index)
    {
        index.First = Mathf.Clamp(index.First, 0, Items.Length - 1);

        for (var i = 0; i < Items.Length; i++)
        {
            var items = Items[i].Items;
            var secondIndex = Mathf.Clamp(index.Second, 0, items.Length - 1);

            for (var j = 0; j < items.Length; j++)
            {
                if (i == index.First && j == secondIndex)
                {
                    index.Second = secondIndex;
                    items[j].OnFocus(true);
                    MenuManager.OnFocus(items[j]);
                    HoverIcon.SetParent(items[j].transform, false);
                }
                else
                {
                    items[j].OnFocus(false);
                }
            }
        }

        _currentIndex = index;
    }

    private void FocusDown()
    {
        if (_currentIndex.Second < Items[_currentIndex.First].Items.Length - 1)
        {
            FocusItem(_currentIndex.Set(_currentIndex.First, _currentIndex.Second + 1));
        }
        else if (Cyclic)
        {
            FocusItem(_currentIndex.Set(_currentIndex.First, 0));
        }
    }

    private void FocusUp()
    {
        if (_currentIndex.Second > 0)
        {
            FocusItem(_currentIndex.Set(_currentIndex.First, _currentIndex.Second - 1));
        }
        else if (Cyclic)
        {
            FocusItem(_currentIndex.Set(_currentIndex.First, Items[_currentIndex.First].Items.Length - 1));
        } 
    }

    private void FocusRight()
    {
        if (_currentIndex.First < Items.Length - 1)
        {
            FocusItem(_currentIndex.Set(_currentIndex.First + 1, _currentIndex.Second));
        }
        else if (Cyclic)
        {
            FocusItem(_currentIndex.Set(0, _currentIndex.Second));
        } 
    }

    private void FocusLeft()
    {
        if (_currentIndex.First > 0)
        {
            FocusItem(_currentIndex.Set(_currentIndex.First - 1, _currentIndex.Second));
        }
        else if (Cyclic)
        {
            FocusItem(_currentIndex.Set(Items.Length - 1, _currentIndex.Second));
        }
    }

    public override void UnfocusAll()
    {
        foreach (var collection in Items)
        {
            foreach(var item in collection.Items)
            {
                item.OnFocus(false);
            }
        }
    }

    public override void FocusCurrent()
    {
        FocusItem(_currentIndex);
    }

    private void OnItemSelected(IndexPair index)
    {
        if (index.First < 0 || index.First >= Items.Length || index.Second < 0 || index.Second >= Items[index.First].Items.Length)
        {
            Debug.LogWarning("Tried to select invalid item.");
            return;
        }

        Items[index.First].Items[index.Second].OnSelect(MenuManager);
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

    public IndexPair(int first, int second)
    {
        Set(first, second);
    }

    public IndexPair Set(int first, int second)
    {
        First = first;
        Second = second;
        return this;
    }
}