namespace Utilities
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System;

    public static class PanelHelper
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

        public static void LerpRectTransformAnchors(RectTransform rectTransform, Vector2 targetMin, Vector2 targetMax, float speed)
        {
            rectTransform.anchorMin = Vector2.Lerp(rectTransform.anchorMin, targetMin, speed);
            rectTransform.anchorMax = Vector2.Lerp(rectTransform.anchorMax, targetMax, speed);
        }
    }

    public static class Vector3Helper
    {
        public static Vector3 SubractXZ(this Vector3 v, Vector3 other)
        {
            return new Vector3(v.x - other.x, v.y, v.z - other.z);
        }

        public static float DistanceXZ(Vector3 a, Vector3 b)
        {
            return SubractXZ(a, b).magnitude;
        }

        public static float SqrDistanceXZ(Vector3 a, Vector3 b)
        {
            return SubractXZ(a, b).sqrMagnitude;
        }

        public static Vector3 RotateAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
        {
            var dir = point - pivot;

            dir = Quaternion.Euler(angles) * dir;

            point = dir + pivot;

            return point;
        }

		public static Vector3 RotateAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
		{
			var dir = point - pivot;

			dir = rotation * dir;

			point = dir + pivot;

			return point;
		}
	}

	public static class BoxHelper
	{
		public static bool InsideXZ(Vector3 pos, Vector3[] corners)
		{
			// 0 < AM⋅AB < AB⋅AB)∧(0 < AM⋅AD < AD⋅AD)
			var AP = pos.SubractXZ(corners[0]);
			var AB = corners[1].SubractXZ(corners[0]);
			var AD = corners[3].SubractXZ(corners[0]);

			var dotAPAB = Vector3.Dot(AP, AB);
			var dotAPAD = Vector3.Dot(AP, AD);

			return (dotAPAB > 0 && Vector3.Dot(AB, AB) > dotAPAB) && (dotAPAD > 0 && Vector3.Dot(AD, AD) > dotAPAD);
		}

		private static bool Between(float value, float a, float b)
		{
			if (a > b)
			{
				return (b <= value) && (value <= a);
			}
			else
			{
				return (b >= value) && (value >= a);
			}
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

            var p1 = Vector3Helper.RotateAroundPivot(center + new Vector3(-halfWidth, 0, halfDepth), center, angles);
            var p2 = Vector3Helper.RotateAroundPivot(center + new Vector3(halfWidth, 0, halfDepth), center, angles);
            var p3 = Vector3Helper.RotateAroundPivot(center + new Vector3(halfWidth, 0, -halfDepth), center, angles);
            var p4 = Vector3Helper.RotateAroundPivot(center + new Vector3(-halfWidth, 0, -halfDepth), center, angles);

            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);
        }

		public static void DrawSquareArena(Vector3 origin, Quaternion rotation, float width, float depth, int divisions, Color color = default(Color))
		{
			Gizmos.color = color;

			var pivot = origin;
			var angles = rotation.eulerAngles;
			var offsetX = new Vector3(width / divisions, 0, 0);
			var offsetZ = new Vector3(0, 0, depth / divisions);

			for (int x = 0; x < divisions; x++)
			{
				for (int z = 0; z < divisions; z++)
				{
					var p0 = origin + (offsetX * x + offsetZ * z);
					var p1 = Vector3Helper.RotateAroundPivot(p0, pivot, angles);
					var p2 = Vector3Helper.RotateAroundPivot(p0 + offsetX, pivot, angles);
					var p3 = Vector3Helper.RotateAroundPivot(p0 + offsetZ + offsetX, pivot, angles);
					var p4 = Vector3Helper.RotateAroundPivot(p0 + offsetZ, pivot, angles);

					Gizmos.DrawLine(p1, p2);
					Gizmos.DrawLine(p2, p3);
					Gizmos.DrawLine(p3, p4);
					Gizmos.DrawLine(p4, p1);
				}
			}
		}
	}

    public static class IOHelper
    {
        public static bool FileExists(string path, bool persistentDataPath = false)
        {
            if (persistentDataPath)
                return File.Exists(string.Concat(Application.persistentDataPath, Path.AltDirectorySeparatorChar, path));
            else
                return File.Exists(string.Concat(Application.dataPath, Path.AltDirectorySeparatorChar, path));
        }

        public static void CreateFile(string path, bool persistentDataPath = false)
        {
            if (FileExists(path, persistentDataPath))
                return;

            if (persistentDataPath)
                File.Create(string.Concat(Application.persistentDataPath, Path.AltDirectorySeparatorChar, path)).Close();
            else
                File.Create(string.Concat(Application.dataPath, Path.AltDirectorySeparatorChar, path)).Close();
        }

        public static bool DirectoryExists(string path, bool persistentDataPath = false)
        {
            if (persistentDataPath)
                return Directory.Exists(string.Concat(Application.persistentDataPath, Path.AltDirectorySeparatorChar, path));
            else
                return Directory.Exists(string.Concat(Application.dataPath, Path.AltDirectorySeparatorChar, path));
        }

        public static void CreateDirectory(string path, bool persistentDataPath = false)
        {
            if (DirectoryExists(path, persistentDataPath))
                return;

            if (persistentDataPath)
                Directory.CreateDirectory(string.Concat(Application.persistentDataPath, Path.AltDirectorySeparatorChar, path));
            else
                Directory.CreateDirectory(string.Concat(Application.dataPath, Path.AltDirectorySeparatorChar, path));
        }

        public static FileStream OpenFileToRead(string path, bool persistentDataPath = false)
        {
            var df = new DirectoryAndFile(path);

            CreateFileIfNotExists(df.DirectoryPath, df.Filename, persistentDataPath);

            if(persistentDataPath)
                return File.OpenRead(string.Concat(Application.persistentDataPath, Path.AltDirectorySeparatorChar, path));
            else
                return File.OpenRead(string.Concat(Application.dataPath, Path.AltDirectorySeparatorChar, path));
        }

        public static FileStream OpenFileToWrite(string path, bool append = true, bool persistentDataPath = false)
        {
            var df = new DirectoryAndFile(path);

            CreateFileIfNotExists(df.DirectoryPath, df.Filename);

            if(persistentDataPath)
                return File.Open(string.Concat(Application.persistentDataPath, Path.AltDirectorySeparatorChar, path),
                    append ? FileMode.Append : FileMode.Create, FileAccess.Write);
            else
                return File.Open(string.Concat(Application.dataPath, Path.AltDirectorySeparatorChar, path),
                    append ? FileMode.Append : FileMode.Create, FileAccess.Write);
        }

        public static void SerializeToFile(string path, object serializableObject, bool persistentDataPath = false)
        {
            var stream = OpenFileToWrite(path, false, persistentDataPath);

            try
            {
                new BinaryFormatter().Serialize(stream, serializableObject);
            }
            catch(Exception e)
            {
                Debug.LogError(string.Concat("SerializeToFile(", path,", ", serializableObject.ToString(), ", ",
                    persistentDataPath.ToString(),"): Exception Caught!\n", e.Message));
            }
            finally
            {
                stream.Close();
            }
        }

        public static T DeserializeFromFile<T>(string path, bool persistentDataPath = false)
        {
            var stream = OpenFileToRead(path, persistentDataPath);

            try
            {
                var obj = (T)(new BinaryFormatter().Deserialize(stream));
                return obj;
            }
            catch(Exception e)
            {
                Debug.LogError(string.Concat("SerializeToFile(", path,", ", persistentDataPath.ToString(),
                    "): Exception Caught!\n", e.Message));
            }
            finally
            {
                stream.Close();
            }

            return default(T);
        }

        private static string CreateFileIfNotExists(string pathToFile, string fileName, bool persistentDataPath = false)
        {
            if (!DirectoryExists(pathToFile, persistentDataPath))
                CreateDirectory(pathToFile, persistentDataPath);

            var path = string.Concat(pathToFile, Path.AltDirectorySeparatorChar, fileName);

            if (!FileExists(path, persistentDataPath))
                CreateFile(path, persistentDataPath);

            return path;
        }

        public struct DirectoryAndFile
        {
            public string DirectoryPath;
            public string Filename;

            public DirectoryAndFile(string path)
            {
                DirectoryPath = path.Substring(0, path.LastIndexOf(Path.AltDirectorySeparatorChar) + 1);
                var fileStartIndex = Mathf.Min(DirectoryPath.Length, path.Length - 1);
                Filename = path.Substring(fileStartIndex, Mathf.Max(0, path.Length - fileStartIndex));
            }

            public static string FromSeparate(string DirectoryPath, string Filename)
            {
                return string.Concat(DirectoryPath, Path.AltDirectorySeparatorChar, Filename);
            }
        }
    }

	public static class InputHelper
	{
		public static bool IsJoystickConnected()
		{
			if (Input.GetJoystickNames().Length == 0)
				return false;
			else
			{
				foreach (var js in Input.GetJoystickNames())
				{
					if (js.Length != 0)
						return true;
				}
				return false;
			}
		}
	}
}
