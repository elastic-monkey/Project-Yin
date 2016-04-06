using System.Collections.Generic;
using UnityEngine;

public abstract class Utils
{
	public enum WindowPositions
	{
		Center, Right, Up, Down, Left
	}

	public static void GetAnchorsForWindowPosition(WindowPositions targetPosition, out Vector2 anchorMin, out Vector2 anchorMax)
	{
		switch (targetPosition)
		{
		case WindowPositions.Right:
			anchorMin = Vector2.right;
			anchorMax = new Vector2(2, 1);
			break;

		case WindowPositions.Up:
			anchorMin = Vector2.up;
			anchorMax = new Vector2(1, 2);
			break;

		case WindowPositions.Down:
			anchorMin = new Vector2(0, -1);
			anchorMax = Vector2.right;
			break;

		case WindowPositions.Left:
			anchorMin = new Vector2(-1, 0);
			anchorMax = Vector2.up;
			break;

		default:
			anchorMin = Vector2.zero;
			anchorMax = Vector2.one;
			break;
		}
	}

	public static bool LerpRectTransformAnchors(RectTransform rectTransform, Vector2 targetMin, Vector2 targetMax, float speed)
	{
		rectTransform.anchorMin = Vector2.Lerp(rectTransform.anchorMin, targetMin, speed);
		rectTransform.anchorMax = Vector2.Lerp(rectTransform.anchorMax, targetMax, speed);

		if (Vector2.Distance(rectTransform.anchorMin, targetMin) < 0.001f || Vector2.Distance(rectTransform.anchorMax, targetMax) < 0.001f)
		{
			rectTransform.anchorMin = targetMin;
			rectTransform.anchorMax = targetMax;
			return true;
		}

		return false;
	}
}
