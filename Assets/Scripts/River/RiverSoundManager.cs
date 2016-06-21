using UnityEngine;
using Utilities;

public class RiverSoundManager : MonoBehaviour
{
	public SoundManager SoundManager;
	public float MinDistanceToPlay = 10f;
	[Range(0, 2)]
	public float VolumeMultiplier = 1f;
	public RiverSection[] RiverSections;
	public AudioClip RiverSounds;
	public bool NearOrInside;

	private AudioSource _currentSource;
	private float _currentDistFactor;

	public GameManager GameManager
	{
		get
		{
			return GameManager.Instance;
		}
	}

	private void Awake()
	{
		foreach (var section in RiverSections)
		{
			section.WorldPosition = Vector3Helper.RotateAroundPivot(transform.position + section.Center, transform.position, transform.rotation);
			section.WorldRotation = transform.rotation;
			section.CalcCorners();
		}

		NearOrInside = false;
	}

	private void Start()
	{
		_currentSource = SoundManager.Play(RiverSounds, true, 0.05f);
		_currentSource.volume = 0;
	}

	private void Update()
	{
		var minDist = float.MaxValue;
		var sqrMinDist = (MinDistanceToPlay * MinDistanceToPlay);

		foreach (var section in RiverSections)
		{
			if (!section.Active)
				continue;

			var target = GameManager.Player.transform;
			var dist = section.SqrDistance(target);
			if (dist <= sqrMinDist)
			{
				NearOrInside = true;
			}

			if (dist < minDist)
			{
				minDist = dist;
				_currentDistFactor = 1 - (dist / sqrMinDist);
			}
		}

		_currentSource.volume = VolumeMultiplier * _currentDistFactor;
	}

	private void OnDrawGizmos()
	{
		if (RiverSections == null)
			return;

		foreach (var section in RiverSections)
		{
			if (!section.Active)
				continue;

			var pos = Vector3Helper.RotateAroundPivot(transform.position + section.Center, transform.position, transform.rotation);
			GizmosHelper.DrawSquareArena(pos, transform.rotation, section.Width, section.Depth, Color.blue);
		}
	}

	[System.Serializable]
	public class RiverSection
	{
		public bool Active;
		public Vector3 Center = Vector3.zero;
		public float Width = 0, Depth = 0;
		[HideInInspector]
		public Vector3 WorldPosition;
		[HideInInspector]
		public Quaternion WorldRotation;
		[HideInInspector]
		public Vector3[] Corners;

		public void CalcCorners()
		{
			Corners = new Vector3[4];

			Corners[0] = Vector3Helper.RotateAroundPivot(WorldPosition + new Vector3(-Width / 2, 0, Depth / 2), WorldPosition, WorldRotation);
			Corners[1] = Vector3Helper.RotateAroundPivot(WorldPosition + new Vector3(Width / 2, 0, Depth / 2), WorldPosition, WorldRotation);
			Corners[2] = Vector3Helper.RotateAroundPivot(WorldPosition + new Vector3(Width / 2, 0, -Depth / 2), WorldPosition, WorldRotation);
			Corners[3] = Vector3Helper.RotateAroundPivot(WorldPosition + new Vector3(-Width / 2, 0, -Depth / 2), WorldPosition, WorldRotation);
		}

		public float SqrDistance(Transform t)
		{
			if (BoxHelper.InsideXZ(t.position, Corners))
				return 0;

			var min = float.MaxValue;
			var minIndex = -1;

			for (int i = 0; i < Corners.Length; i++)
			{
				var d = Vector3Helper.SqrDistanceXZ(t.position, Corners[i]);
				if (d < min)
				{
					min = d;
					minIndex = i;
				}
			}

			var indexBefore = minIndex - 1 < 0 ? 3 : minIndex - 1;
			var indexAfter = (minIndex + 1) % 4;

			var dirBefore = Corners[minIndex].SubractXZ(Corners[indexBefore]).normalized;
			var distance = Vector3.Cross(dirBefore, Corners[minIndex].SubractXZ(t.position)).sqrMagnitude;

			var dirAfter = Corners[minIndex].SubractXZ(Corners[indexAfter]).normalized;
			distance = Mathf.Min(distance, Vector3.Cross(dirAfter, Corners[minIndex].SubractXZ(t.position)).sqrMagnitude);

			return distance;
		}
	}
}
