using UnityEngine;
using System.Collections;

public class Savepoint : OpenMenuInteraction
{
    protected override void Awake()
    {
        base.Awake();

        Target = GameManager.Instance.SaveTerminal;
    }
}
