using UnityEngine;
using System.Collections;

public abstract class MenuManager : MonoBehaviour
{
    public abstract void OnAction(object action, object data);
}
