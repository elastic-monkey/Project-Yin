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

public static class GizmosHelper
{
    public static void DrawAngleOfSight(Vector3 center, Vector3 forward, int angle, int divisions, Color color = default(Color))
    {
        Gizmos.color = color;

        var halfAngle = angle / 2;
        var angleStep = angle / (float)divisions;

        var p1 = Quaternion.Euler(0, -halfAngle, 0) * forward;
        if (angle < 360)
            Gizmos.DrawLine(center, center + p1);

        for (int i = 0; i < divisions; i++)
        {
            var nextAngle = -halfAngle + (i + 1) * angleStep;
            var p2 = Quaternion.Euler(0, nextAngle, 0) * forward;

            Gizmos.DrawLine(center + p1, center + p2);
            p1 = p2;
        }

        if (angle > 0 && angle < 360)
            Gizmos.DrawLine(center, center + p1);
    }

	public static void DrawArena(Vector3 center, float range, int divisions, Color color = default(Color))
	{
		Gizmos.color = color;

		var forward = Vector3.forward * range + new Vector3(0, 0.5f, 0);
		var halfAngle = 360 / 2;
		var angleStep = 360 / (float)divisions;

		var p1 = Quaternion.Euler(0, -halfAngle, 0) * forward;
		for (int i = 0; i < divisions; i++)
		{
			var nextAngle = -halfAngle + (i + 1) * angleStep;
			var p2 = Quaternion.Euler(0, nextAngle, 0) * forward;

			Gizmos.DrawWireSphere(p2, 0.1f);
			Gizmos.DrawLine(center + p1, center + p2);
			p1 = p2;
		}
	}
}
