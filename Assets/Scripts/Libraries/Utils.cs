using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public enum WindowPositions
    {
        Center,
        Right,
        Up,
        Down,
        Left
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

    public static bool Exists(this object obj)
    {
        return obj != null;
    }

    public static bool IsNull(this object obj)
    {
        return obj == null;
    }

    public static Vector3 RotateAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        var dir = point - pivot;

        dir = Quaternion.Euler(angles) * dir;

        point = dir + pivot;

        return point;
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

    public static void DrawCircleArena(Vector3 center, float range, int divisions, Color color = default(Color))
    {
        Gizmos.color = color;

        var forward = Vector3.forward * range;
        var halfAngle = 360 / 2;
        var angleStep = 360 / (float)divisions;

        var p1 = Quaternion.Euler(0, -halfAngle, 0) * forward;
        for (int i = 0; i < divisions; i++)
        {
            var nextAngle = -halfAngle + (i + 1) * angleStep;
            var p2 = Quaternion.Euler(0, nextAngle, 0) * forward;

            Gizmos.DrawLine(center + p1, center + p2);
            p1 = p2;
        }
    }

    public static void DrawSquareArena(Vector3 center, Quaternion rotation, float width, float depth, Color color = default(Color))
    {
        Gizmos.color = color;

        var halfWidth = width * 0.5f;
        var halfDepth = depth * 0.5f;
        var angles = rotation.eulerAngles;

        var p1 = Utils.RotateAroundPivot(center + new Vector3(-halfWidth, 0, halfDepth), center, angles);
        var p2 = Utils.RotateAroundPivot(center + new Vector3(halfWidth, 0, halfDepth), center, angles);
        var p3 = Utils.RotateAroundPivot(center + new Vector3(halfWidth, 0, -halfDepth), center, angles);
        var p4 = Utils.RotateAroundPivot(center + new Vector3(-halfWidth, 0, -halfDepth), center, angles);

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }
}