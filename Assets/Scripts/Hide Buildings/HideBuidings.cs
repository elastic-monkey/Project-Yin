using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(GameManager))]
public class HideBuidings : MonoBehaviour
{
    public LayerMask RaycastMask;
    public int MaxDistance = 2000;
    public IsometricCamera IsoCamera;
    public List<IHideable> HiddenObjects;
    public int HiddenCount = 0;

    private PlayerBehavior _player;

    private void Awake()
    {
        HiddenObjects = new List<IHideable>();
    }

    private void Start()
    {
        _player = GameManager.Instance.Player;
    }

    private void Update()
    {
        var direction = _player.RaycastSpot.transform.position - IsoCamera.Camera.transform.position;
        var ray = new Ray(IsoCamera.Camera.transform.position, direction);
        var maxDist = direction.magnitude;
        var hits = new List<RaycastHit>(Physics.RaycastAll(ray, maxDist, RaycastMask));

        RemoveNoLongerHit(hits);
        AddNewHits(hits);
        UpdateHiddenObjects();
    }

    private void RemoveNoLongerHit(List<RaycastHit> hits)
    {
        if (hits.Count == 0)
        {
            for (var i = 0; i < HiddenObjects.Count; i++)
            {
                HiddenObjects[i].Show();
                HiddenObjects.RemoveAt(i);
                i--;
            }
            return;
        }
        
        for (var i = 0; i < HiddenObjects.Count; i++)
        {
            var obj = HiddenObjects[i];
            var found = false;

            for (int j = 0; j < hits.Count; j++)
            {
                var hit1 = hits[j].collider.GetComponent<IHideable>();
                var hit2 = hits[j].collider.GetComponentInParent<IHideable>();
                
                if (hit1 != null && hit1 == obj)
                {
                    hits.RemoveAt(j);
                    found = true;
                    break;
                }
                else if (hit2 != null && hit2 == obj)
                {
                    hits.RemoveAt(j);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                obj.Show();
                HiddenObjects.RemoveAt(i);
                i--;
            }
        }
    }

    private void AddNewHits(List<RaycastHit> hits)
    {
        if (hits.Count == 0)
            return;
        
        foreach (var hit in hits)
        {
            var hit1 = hit.collider.GetComponent<IHideable>();
            var hit2 = hit.collider.GetComponentInParent<IHideable>();

            if (hit1 != null)
                HiddenObjects.Add(hit1);
            
            if (hit2 != null)
                HiddenObjects.Add(hit2);
        }
    }

    private void UpdateHiddenObjects()
    {
        foreach (var hideable in HiddenObjects)
        {
            hideable.Hide();
        }
        HiddenCount = HiddenObjects.Count;
    }
}
