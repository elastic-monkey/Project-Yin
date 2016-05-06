using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class ImageNavItem : NavItem
{
    public Color SelectedColor;

    private Color _initialColor;
    protected Image _image;

    public Image Image
    {
        get
        {
            if (_image == null)
                _image = GetComponent<Image>();

            return _image;
        }
    }

    protected void Awake()
    {
        if (Image.Exists())
            _initialColor = Image.color;
    }

    protected override void OnFocus(bool value)
    {
        if (Image.Exists())
            Image.color = value ? SelectedColor : _initialColor;
    }
}
