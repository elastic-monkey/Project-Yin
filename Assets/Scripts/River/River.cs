using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class River : MonoBehaviour
{
	public Material RiverMaterial;
	public float Width = 10f, Depth = 10f;
	[Range(10, 100)]
	public int Resolution = 10;
	[Range(0, 5)]
	public float Speed = 1f;
	[Range(0, 1)]
	public float Scale = 1f;
	public bool ShowGizmos;

	private List<Vector3> _vertices;
	private Vector3[] _newVertices;
	private List<int> _tris;
	private MeshFilter _filter;
	private MeshRenderer _renderer;

	public Vector3 Center
	{
		get
		{
			return transform.position + new Vector3(Width * 0.5f, 0, Depth * 0.5f);
		}
	}

	public void Awake()
	{
		_filter = gameObject.AddComponent<MeshFilter>();
		_renderer = gameObject.AddComponent<MeshRenderer>();

		_vertices = new List<Vector3>();
		_tris = new List<int>();
	}

	public void Start()
	{
		_renderer.sharedMaterial = RiverMaterial;

		Build();
	}

	public void Update()
	{
		for (int i = 0; i < _vertices.Count; i++)
		{
			var v = _vertices[i];
			v.y += Mathf.Sin(Time.time * Speed + v.x + v.y + v.z) * Scale * Random.Range(0.9f, 1.1f);
			_newVertices[i] = v;
		}

		_filter.mesh.vertices = _newVertices;
		_filter.mesh.RecalculateNormals();
	}

	private void Build()
	{
		_vertices.Clear(); _vertices.Capacity = 0;
		_tris.Clear(); _tris.Capacity = 0;

		var stepX = Width / Resolution;
		var stepZ = Depth / Resolution;

		for (int z = 0; z < Resolution; z++)
		{
			for (int x = 0; x < Resolution; x++)
			{
				var position = new Vector3(stepX * x, 0, stepZ * z);
				_vertices.Add(position);
			}
		}

		for (int z = 0; z < Resolution - 1; z++)
		{
			for (int x = 0; x < Resolution - 1; x++)
			{
				var offset = x + z * Resolution;

				_tris.Add(offset);
				_tris.Add(offset + Resolution);
				_tris.Add(offset + 1);

				_tris.Add(offset + Resolution);
				_tris.Add(offset + Resolution + 1);
				_tris.Add(offset + 1);
			}
		}

		if (_filter.mesh != null)
			_filter.mesh.Clear();

		var mesh = new Mesh();
		mesh.name = "River Mesh";
		mesh.vertices = _vertices.ToArray();
		mesh.triangles = _tris.ToArray();
		mesh.RecalculateNormals();

		_filter.mesh = mesh;

		_newVertices = new Vector3[_vertices.Count];
	}

	private void OnDrawGizmos()
	{
		if (!ShowGizmos)
			return;

		GizmosHelper.DrawSquareArena(transform.position, transform.rotation, Width, Depth, Resolution, Color.blue);
	}
}
