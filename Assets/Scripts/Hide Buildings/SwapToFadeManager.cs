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

    private void Awake()
    {
        _swapReferences = new Dictionary<Material, Material>();
        _swapBackReferences = new Dictionary<Material, Material>();

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
