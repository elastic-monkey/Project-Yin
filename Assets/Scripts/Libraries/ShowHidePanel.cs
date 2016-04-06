using UnityEngine;
using System.Collections;

public class ShowHidePanel : MonoBehaviour
{
	public Utils.WindowPositions HidePosition, ShowPosition;
	public RectTransform Container;
	public float AnimationSpeed;
	[Range(1, 60)]
	public int VisibleRefreshRate;
	public bool Visible;

	void Start()
	{
		StartCoroutine(ShowHide());
	}

	IEnumerator ShowHide()
	{
		var oldVisible = !Visible;
		var oldRefreshRate = int.MaxValue;
		var refreshRateSec = 1f;
		Coroutine _oldAnim = null;

		while (true)
		{
			if (oldVisible != Visible)
			{
				oldVisible = Visible;

				if (_oldAnim != null)
					StopCoroutine(_oldAnim);
				_oldAnim = StartCoroutine(AnimateWindow());
			}

			if (oldRefreshRate != VisibleRefreshRate)
			{
				oldRefreshRate = VisibleRefreshRate;
				refreshRateSec = 1f / VisibleRefreshRate;
			}

			yield return new WaitForSeconds(refreshRateSec);
		}
	}

	IEnumerator AnimateWindow()
	{
		Vector2 targetMin, targetMax;
		if (Visible)
		{
			Utils.GetAnchorsForWindowPosition(ShowPosition, out targetMin, out targetMax);
			while (true)
			{
				if (Utils.LerpRectTransformAnchors(Container, targetMin, targetMax, Time.unscaledDeltaTime * AnimationSpeed))
					yield break;

				yield return null;
			}
		}
		else
		{
			Utils.GetAnchorsForWindowPosition(HidePosition, out targetMin, out targetMax);
			while (true)
			{
				if (Utils.LerpRectTransformAnchors(Container, targetMin, targetMax, Time.unscaledDeltaTime * AnimationSpeed))
					yield break;

				yield return null;
			}
		}
	}
}
