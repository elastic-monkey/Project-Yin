using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHit : MonoBehaviour
{
	public Image SplashHit;
	public float Duration = 1f;
	public AnimationCurve curve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);

	private Coroutine _lastHit;

	public void Show(float quantity)
	{
		if (_lastHit != null)
			StopCoroutine(_lastHit);

		_lastHit = StartCoroutine(HitCoroutine(Duration, quantity));
	}

	private IEnumerator HitCoroutine(float duration, float highValue)
	{
		var deltaTime = 0f;
		var color = new Color(1, 0, 0, highValue);
		var invDuration = 1f / duration;
		var t = deltaTime * invDuration;

		while (deltaTime <= Duration)
		{
			SplashHit.color = color;

			t = deltaTime * invDuration;
			color.a = Mathf.Lerp(highValue, 0, curve.Evaluate(t));

			yield return null;

			deltaTime += Time.deltaTime;
		}

		color.a = 0;
		SplashHit.color = color;
	}
}
