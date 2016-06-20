using UnityEngine;
using System.Collections;

public class SlideBuilding : MonoBehaviour, IHideable
{
	public float Duration = 0.2f;
	public float HeightWhenVisible = 1f;
	public Transform TransformToSlide;

	private Vector3 startPos, visiblePos;
	private bool _visible;
	private Coroutine _lastCoroutine;

	private void Start()
	{
		startPos = transform.localPosition;
		visiblePos = transform.localPosition + Vector3.up * HeightWhenVisible;
		_visible = (Vector3.SqrMagnitude(transform.localPosition - startPos) > Vector3.SqrMagnitude(transform.localPosition - visiblePos));
	}

	public void Hide()
	{
		if (!_visible)
			return;

		if (_lastCoroutine != null)
			StopCoroutine(_lastCoroutine);

		_lastCoroutine = StartCoroutine(SlideCoroutine(startPos));
	}

	public bool IsHidden()
	{
		return !_visible;
	}

	public void Show()
	{
		if (_visible)
			return;

		if (_lastCoroutine != null)
			StopCoroutine(_lastCoroutine);

		_lastCoroutine = StartCoroutine(SlideCoroutine(visiblePos));
	}

	private IEnumerator SlideCoroutine(Vector3 targetPos)
	{
		var invDuration = 1f / Duration;
		var timeDelta = 0f;
		var start = transform.localPosition;

		while (timeDelta <= Duration)
		{
			transform.localPosition = Vector3.Lerp(start, targetPos, timeDelta * invDuration);

			timeDelta += Time.deltaTime;
			yield return null;
		}

		transform.localPosition = targetPos;

		_visible = !(targetPos == startPos);
	}
}
