using UnityEngine;
using System.Collections;

public class SwapToFade : MonoBehaviour
{
    public Material Main;
    public Material DefaultFade;
    public Material FadeMaterial;

    public void Awake()
    {
        if (Main != null)
        {
            FadeMaterial = new Material(Shader.Find("Standard"));
            FadeMaterial.CopyPropertiesFromMaterial(DefaultFade);

            FadeMaterial.name = Main.name + "_Fade";
            FadeMaterial.SetColor("_Color", Main.GetColor("_Color"));
        }
    }
}
