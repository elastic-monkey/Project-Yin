using UnityEngine;
using System.Collections.Generic;

public class SwapToFadeManager : MonoBehaviour
{
    public List<Material> Originals;
    public List<Material> Fadeables;
    public Material DefaultFade;

    [SerializeField]
    private Dictionary<Material, Material> _swapReferences;
    private Dictionary<Material, Material> _swapBackReferences;
    private Dictionary<HideByFading, Dictionary<Renderer, List<Material>>> _swapBackPool;

    private void Awake()
    {
        _swapReferences = new Dictionary<Material, Material>();
        _swapBackReferences = new Dictionary<Material, Material>();
        _swapBackPool = new Dictionary<HideByFading, Dictionary<Renderer, List<Material>>>();

        foreach (var mat in Originals)
        {
            var newMaterial = new Material(Shader.Find("Standard"));
            newMaterial.CopyPropertiesFromMaterial(DefaultFade);
            newMaterial.name = mat.name + "_Fade";
            newMaterial.SetColor("_Color", mat.GetColor("_Color"));

            _swapReferences.Add(mat, newMaterial);
            _swapBackReferences.Add(newMaterial, mat);
            Fadeables.Add(newMaterial);
        }
    }

    public void ReplaceByFadeMaterials(HideByFading obj)
    {
        if (_swapBackPool.ContainsKey(obj))
        {
            foreach (var entry in _swapBackPool[obj])
            {
                entry.Value.Clear();
                entry.Value.Capacity = 0;
            }
            _swapBackPool[obj].Clear();
            _swapBackPool[obj] = new Dictionary<Renderer, List<Material>>();
        }
        else
        {
            _swapBackPool.Add(obj, new Dictionary<Renderer, List<Material>>());
        }

        foreach (var r in obj.Renderers)
        {
            _swapBackPool[obj].Add(r, new List<Material>());

            var mats = r.sharedMaterials;

            for (var i = 0; i < mats.Length; i++)
            {
                var sub = FindSubstitute(mats[i]);
                _swapBackPool[obj][r].Add(mats[i]);
                mats[i] = sub;
            }

            r.materials = mats;
        }
    }

    public void ReplaceByOpaqueMaterials(HideByFading obj)
    {
        foreach (var r in obj.Renderers)
        {
            r.sharedMaterials = _swapBackPool[obj][r].ToArray();
        }
    }

    public Material FindSubstitute(Material original)
    {
        Material sub = original;
        if (!_swapReferences.TryGetValue(original, out sub))
        {
            return original;
        }
        else
        {
            return sub;
        }
    }

    public Material FindOriginal(Material susbtitute)
    {
        Material ori = susbtitute;
        _swapBackReferences.TryGetValue(susbtitute, out ori);

        if (ori == susbtitute)
            Debug.Log("Found no original");

        return ori;
    }
}