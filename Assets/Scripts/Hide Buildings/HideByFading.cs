using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HideByFading : MonoBehaviour, IHideable
{
	public float Duration = 1.0f;
	public Transform HideParent;
	public Transform ShowParent;
	public Collider[] Colliders;
	[HideInInspector]
	public List<Renderer> RenderersToHide, RenderersToShow;

	private bool _hidden;
	private SwapToFadeManager _swapManager;
	private Coroutine _lastCoroutine;
	private bool _hasFoundations;

	public void Awake()
	{
		RenderersToHide = new List<Renderer>(HideParent.GetComponentsInChildren<Renderer>());
		if (ShowParent == null)
		{
			_hasFoundations = false;
			RenderersToShow = new List<Renderer>();
		}
		else
		{
			RenderersToShow = new List<Renderer>(ShowParent.GetComponentsInChildren<Renderer>());
			_hasFoundations = RenderersToShow.Count > 0;
		}
		_swapManager = GameManager.Instance.SwapFadeMaterials;

		foreach (var col in Colliders)
			col.gameObject.layer = LayerMask.NameToLayer("Hideable Building");

		foreach (var r in RenderersToShow)
			r.enabled = false;
	}

	public void Hide()
	{
		if (_hidden)
			return;

		if (_lastCoroutine != null)
		{
			_swapManager.ReplaceByOpaqueMaterials(this); // Prevent lost materials
			StopCoroutine(_lastCoroutine);
		}

		_lastCoroutine = StartCoroutine(HideCoroutine(true));
	}

	public void Show()
	{
		if (!_hidden)
			return;

		if (_lastCoroutine != null)
			StopCoroutine(_lastCoroutine);

		_lastCoroutine = StartCoroutine(HideCoroutine(false));
	}

	public bool IsHidden()
	{
		return _hidden;
	}

	private IEnumerator HideCoroutine(bool hide)
	{
		_hidden = hide;

		var hideAlpha = hide ? 0f : 1f;
		var timePassed = 0f;
		var invDuration = 1f / Duration;

		if (hide)
		{
			_swapManager.ReplaceByFadeMaterials(this);
			ToggleRenderersToShow(true);
		}

		ToggleRenderersToHide(true);

		while (timePassed <= Duration)
		{
			var lerpFactor = timePassed * invDuration;
			SetRenderersToHideFade(hideAlpha, lerpFactor);

			yield return null;
			timePassed += Time.deltaTime;
		}

		if (hide)
			ToggleRenderersToHide(false);
		else
		{
			_swapManager.ReplaceByOpaqueMaterials(this);
			ToggleRenderersToShow(false);
		}
	}

	private void SetRenderersToHideFade(float targetValue, float lerpFactor)
	{
		foreach (var r in RenderersToHide)
		{
			var mats = r.materials;
			foreach (var mat in mats)
			{
				var c = mat.color;
				c.a = Mathf.Lerp(targetValue == 0f ? 1f : 0f, targetValue, lerpFactor);
				mat.color = c;
			}
			r.materials = mats;
		}
	}

	private void ToggleRenderersToShow(bool value)
	{
		foreach (var r in RenderersToShow)
			r.enabled = value;
	}

	private void ToggleRenderersToHide(bool value)
	{
		foreach (var r in RenderersToHide)
			r.enabled = value;
	}
}
