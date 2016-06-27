using UnityEngine.UI;

public class SettingsSliderNavItem : NavItem
{
    public GameAudio.AudioFacets Facet;
    public Slider Slider;
    public float Increment;

	private MenuSoundManager _soundManager;

	public MenuSoundManager SoundManager
	{
		get
		{
			if (_soundManager == null)
				_soundManager = MainMenuManager.Instance.SoundManager;

			return _soundManager;
		}
	}

    protected void Start()
    {
        Slider.value = GameAudio.GetVolume(Facet);
    }

    public override void OnSelect()
    {
        // Do stuff
    }

    public override void OnHorizontalInput(float value)
    {
        if (value == 0)
            return;
        
        if (Slider.direction == Slider.Direction.LeftToRight)
        {
			Slider.value += value > 0 ? Increment : -Increment;
			SoundManager.PlaySampleSound(Slider.value);
            GameAudio.Change(Facet, Slider.value);
		}
		else if (Slider.direction == Slider.Direction.RightToLeft)
        {
            Slider.value += value > 0 ? -Increment : Increment;
			SoundManager.PlaySampleSound(Slider.value);
            GameAudio.Change(Facet, Slider.value);
		}
    }

    public override void OnVerticalInput(float value)
    {
        if (value == 0)
            return;

        if (Slider.direction == Slider.Direction.BottomToTop)
        {
            Slider.value += value > 0 ? Increment : -Increment;
			SoundManager.PlaySampleSound(Slider.value);
            GameAudio.Change(Facet, Slider.value);
		}
        else if (Slider.direction == Slider.Direction.TopToBottom)
        {
            Slider.value += value > 0 ? -Increment : Increment;
			SoundManager.PlaySampleSound(Slider.value);
            GameAudio.Change(Facet, Slider.value);
		}
    }
}
