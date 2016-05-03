using UnityEngine;
using System.Collections;

[RequireComponent(typeof(IAnimatedPanel))]
public class NavMenu : MonoBehaviour
{
    public bool Cyclic, Reset;
    public NavItem[] Items;

    [SerializeField]
    private bool _active;
    private bool _activeNextFrame;
    [SerializeField]
    private int _currentItem;

    private IAnimatedPanel _animatedPanel;
    private MainMenuManager _mainMenuManager;

    private void Awake()
    {
        _animatedPanel = GetComponent<IAnimatedPanel>();
        _mainMenuManager = GetComponentInParent<MainMenuManager>();
    }

    private void Start()
    {
        FocusItem(0);
    }

    private void Update()
    {
        if (_activeNextFrame)
        {
            _activeNextFrame = false;
            _active = true;
            return;
        }

        if (!_active)
            return;
		
        if (PlayerInput.IsButtonDown(Axis.Nav_Vertical))
        {
			Debug.Log ("asdasdasd");
            var v = -PlayerInput.GetAxis(Axis.Nav_Vertical);

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
			Debug.Log ("asdasdas");
            OnItemSelected(_currentItem);
        }
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

    public void SetActive(bool value)
    {
        _activeNextFrame = value;
        _active = false;

        if (!value && Reset)
            FocusItem(0);

        _animatedPanel.SetVisible(value);
    }

    private void OnItemSelected(int index)
    {
        if (index < 0 || index >= Items.Length)
        {
            Debug.LogWarning("Tried to select invalid item.");
            return;
        }

        Items[index].OnSelect(_mainMenuManager);
    }
}
