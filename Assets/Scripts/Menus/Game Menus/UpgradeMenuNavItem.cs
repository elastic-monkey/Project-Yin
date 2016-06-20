using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuNavItem : GameNavItem
{
	public bool Purchased;
	public Sprite InitialSprite;
	public Sprite PurchasedSprite;
	public Sprite DisabledSprite;
	public int UpgradeLevel;

	private Image _upgradeIcon;

	public Image UpgradeIcon
	{
		get
		{
			if (_upgradeIcon == null)
				_upgradeIcon = GetComponent<Image>();

			return _upgradeIcon;
		}
	}

	public override void OnSelect(IMenu manager)
	{
		Data = new string[1];
		Data[0] = UpgradeLevel.ToString();

		manager.OnNavItemSelected(this, Action, Data);
	}

	protected override void OnFocus(bool value)
	{
		//base.OnFocus(value);
	}

	public void Purchase(bool value)
	{
		Purchased = true;
		UpdateColor();
	}

	public override void UpdateColor()
	{
		UpgradeIcon.sprite = Disabled ? DisabledSprite : Purchased ? PurchasedSprite : InitialSprite;
	}
}
