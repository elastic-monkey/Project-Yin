using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(GameManager))]
public class HideBuidings : MonoBehaviour
{
    public LayerMask RaycastMask;
    public int MaxDistance = 2000;
    public IsometricCamera IsoCamera;

    private GameManager _gameManager;
    private PlayerBehavior _player;
    [SerializeField]
    private List<Hideable> _hiddenObjects;

    private void Awake()
    {
        _hiddenObjects = new List<Hideable>();
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _player = _gameManager.Player;
    }

    private void Update()
    {
        var direction = _player.transform.position - IsoCamera.Camera.transform.position;
        var ray = new Ray(IsoCamera.Camera.transform.position, direction);
        var maxDist = direction.magnitude;
        var hits = new List<RaycastHit>(Physics.RaycastAll(ray, maxDist, RaycastMask));

//        Debug.DrawRay(IsoCamera.Camera.transform.position, _player.transform.position - IsoCamera.Camera.transform.position, Color.white);

        HideOrShow(hits);
    }

    private void HideOrShow(List<RaycastHit> hits)
    {
        if (hits.Count > 0)
        {
            for (var i = 0; i < _hiddenObjects.Count; i++)
            {
                var obj = _hiddenObjects[i];
                var found = false;

                for (int j = 0; j < hits.Count; j++)
                {
                    var hit = hits[j].collider.GetComponent<Hideable>();
                    if (hit.Exists() && hit == obj)
                    {
                        hits.RemoveAt(j);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    if (obj.Hidden)
                    {
                        obj.Show();
                    }
                    _hiddenObjects.RemoveAt(i);
                    i--;
                }
            }

            foreach (var hit in hits)
            {
                var hitObj = hit.collider.GetComponent<Hideable>();
                if (hitObj.Exists())
                {
                    _hiddenObjects.Add(hitObj);
                }
            }

            foreach (var hideable in _hiddenObjects)
            {
                hideable.Hide();
            }
        }
        else
        {
            for (var i = 0; i < _hiddenObjects.Count; i++)
            {
                _hiddenObjects[i].Show();
                _hiddenObjects.RemoveAt(i);
                i--;
            }
        }
    }
}
