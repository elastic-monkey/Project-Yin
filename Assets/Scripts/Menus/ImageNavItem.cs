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
            if (_image.IsNull())
            {
                _image = GetComponent<Image>();

                if (_image.IsNull())
                {
                    _image = GetComponentInChildren<Image>();
                }
            }

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
		if (Image.Exists ())
			Image.color = value ? SelectedColor : _initialColor;
    }
}
