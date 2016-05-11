using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TitleBehavior : MonoBehaviour
{
    public Text MyText;
    public float Proportion = 20;

    private void Awake()
    {
        MyText = GetComponent<Text>();

        var s1 = (Proportion / 1600f) * Screen.width;
        var s2 = (Proportion / 800f) * Screen.height;
        var size = Mathf.Min(s1, s2);

        MyText.text = "<size=" + size + ">Project</size><size=" + (size * 2) + "><color=#72B7BCFf>Yin</color></size>";
    }
}
