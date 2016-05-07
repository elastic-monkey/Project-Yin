using UnityEngine;
using System.Collections;

public abstract class MenuManager : MonoBehaviour
{
    public abstract void OnFocus(NavItem target);

    public abstract void OnAction(NavItem item, object action, object data);
}
