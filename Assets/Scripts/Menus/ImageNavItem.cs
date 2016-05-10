using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class ImageNavItem : NavItem
{
    public Color SelectedColor;

    protected Color _initialColor;
    protected Image _image;

    public Image Image
    {
        get
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();

                if (_image == null)
                {
                    _image = GetComponentInChildren<Image>();
                }
            }

            return _image;
        }
    }

    protected void Awake()
    {
        if (Image != null)
            _initialColor = Image.color;
    }

    protected override void OnFocus(bool value)
    {
        if (Image != null)
			Image.color = value ? SelectedColor : _initialColor;
    }
}
