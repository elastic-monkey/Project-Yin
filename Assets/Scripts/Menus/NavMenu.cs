﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(IAnimatedPanel))]
public abstract class NavMenu : MonoBehaviour
{
    public MenuManager MenuManager;

    [SerializeField]
    private bool _active;
    private bool _activeNextFrame;
    private IAnimatedPanel _animatedPanel;

    public bool IsActive
    {
        get
        {
            return _active;
        }
    }

    private void Awake()
    {
        _animatedPanel = GetComponent<IAnimatedPanel>();
    }

    private void Update()
    {
        if (!_active)
            return;

        OnUpdate();
    }

    private void LateUpdate()
    {
        if (_activeNextFrame)
        {
            _activeNextFrame = false;
            _active = true;
            return;
        }
    }

    public void SetActive(bool value)
    {
        _activeNextFrame = value;
        _active = false;

        _animatedPanel.SetVisible(value);

        OnSetActive(value);
    }

    protected abstract void OnUpdate();

    protected abstract void OnSetActive(bool value);
}
