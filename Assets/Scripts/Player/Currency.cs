using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Currency : MonoBehaviour
{

    public float MaxCredits = 99999f;
    [SerializeField]
    private float _currentCredits;

    public float CurrentCredits
    {
        get
        {
            return _currentCredits;
        }
        set
        {
            _currentCredits = value;
        }
    }

    public void AddCredits(float credits)
    {
        CurrentCredits += credits;
    }

    public void RemoveCredits(float credits)
    {
        AddCredits(-credits);
    }

    void Start()
    {
        CurrentCredits = 1000f;
    }

    public bool CanBuy(float price)
    {
        if (price <= CurrentCredits)
            return true;
        return false;
    }
}
