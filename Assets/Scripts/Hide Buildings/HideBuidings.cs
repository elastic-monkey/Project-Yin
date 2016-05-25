﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(GameManager))]
public class HideBuidings : MonoBehaviour
{
    public LayerMask RaycastMask;
    public int MaxDistance = 2000;
    public IsometricCamera IsoCamera;
    public List<Hideable> HiddenObjects;

    private PlayerBehavior _player;

    private void Awake()
    {
        HiddenObjects = new List<Hideable>();
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
                HiddenObjects[i].Hidden = false;
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
                var hit = hits[j].collider.GetComponent<Hideable>();
                if (hit == null)
                    hit = hits[j].collider.GetComponentInParent<Hideable>();
                
                if (hit != null && hit == obj)
                {
                    hits.RemoveAt(j);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                obj.Hidden = false;
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
            var hitObj = hit.collider.GetComponent<Hideable>();
            if (hitObj == null)
                hitObj = hit.collider.GetComponentInParent<Hideable>();
            
            if (hitObj != null)
            {
                HiddenObjects.Add(hitObj);
            }
        }
    }

    private void UpdateHiddenObjects()
    {
        foreach (var hideable in HiddenObjects)
        {
            hideable.Hidden = true;
        }
    }
}