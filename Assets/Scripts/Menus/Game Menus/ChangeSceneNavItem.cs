using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeSceneNavItem : NavItem
{
    public Scenes Target;

    public override void OnSelect()
    {
        SceneManager.LoadScene(Target.GetSceneName());
    }
}
