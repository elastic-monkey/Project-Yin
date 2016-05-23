using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Currency : MonoBehaviour
{

    public float MaxCredits = 99999f;
    public Text PlayerCredits;
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
        //UpgradeCreditsTotal();
    }

    public void RemoveCredits(float credits)
    {
        AddCredits(-credits);
        //UpgradeCreditsTotal();
    }

    void Start()
    {
        CurrentCredits = 1000f;
        //UpgradeCreditsTotal();
    }

    public void UpgradeCreditsTotal()
    {
        PlayerCredits.text = CurrentCredits.ToString();
    }

    public bool CanBuy(float price)
    {
        if (price <= CurrentCredits)
            return true;
        return false;
    }
}
