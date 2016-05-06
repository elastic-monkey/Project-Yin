using UnityEngine;
using System.Collections;

public class MatrixNavMenu : NavMenu
{
    public bool Horizontal, Vertical;
    public bool Cyclic, Reset;
    public MenuManager MenuManager;
    public NavItemCollection[] Items;

    [SerializeField]
    private IndexPair _currentIndex;

    private void Start()
    {
        FocusItem(new IndexPair(0, 0));
    }

    protected override void OnUpdate()
    {
        if (PlayerInput.IsButtonDown(Axis.Nav_Vertical) && Vertical)
        {
            var v = -PlayerInput.GetAxisRaw(Axis.Nav_Vertical);

            if (v > 0)
            {
                FocusDown();
            }
            else if (v < 0)
            {
                FocusUp();
            }
        }
        else if (PlayerInput.IsButtonDown(Axis.Nav_Horizontal) && Horizontal)
        {
            var v = PlayerInput.GetAxisRaw(Axis.Nav_Horizontal);

            if (v > 0)
            {
                FocusRight();
            }
            else if (v < 0)
            {
                FocusLeft();
            }
        }
        else if (PlayerInput.IsButtonDown(Axis.Fire1) || PlayerInput.IsButtonDown(Axis.Submit))
        {
            OnItemSelected(_currentIndex);
        }
    }

    protected override void OnSetActive(bool value)
    {
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
                    items[j].Focus(true);
                }
                else
                {
                    items[j].Focus(false);
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