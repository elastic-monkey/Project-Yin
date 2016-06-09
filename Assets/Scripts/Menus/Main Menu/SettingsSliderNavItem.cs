using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsSliderNavItem : MenuNavItem
{
    public Slider Slider;
    public float Increment;

    public override void OnSelect(Menu manager)
    {
        Data = new string[1];
        Data[0] = Slider.value.ToString();

        manager.OnNavItemSelected(this, Action, Data);
    }

    public override void OnHorizontalInput(float value)
    {
        if (value == 0)
            return;
        
        if (Slider.direction == Slider.Direction.LeftToRight)
        {
            Slider.value += value > 0 ? Increment : -Increment;
        }
        else if (Slider.direction == Slider.Direction.RightToLeft)
        {
            Slider.value += value > 0 ? -Increment : Increment;
        }
    }

    public override void OnVerticalInput(float value)
    {
        if (value == 0)
            return;

        if (Slider.direction == Slider.Direction.BottomToTop)
        {
            Slider.value += value > 0 ? Increment : -Increment;
        }
        else if (Slider.direction == Slider.Direction.TopToBottom)
        {
            Slider.value += value > 0 ? -Increment : Increment;
        }
    }
}
