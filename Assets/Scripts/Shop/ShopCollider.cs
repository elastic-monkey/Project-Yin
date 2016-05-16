using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopCollider : MonoBehaviour {

    public Tags TagToCompare;
    public Canvas ShopUI;

	void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(TagToCompare.ToString()))
        {
            Debug.Log("I'm at the store.");
            ShopUI.gameObject.SetActive(true);

            var player = GetPlayer(collision.collider);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(TagToCompare.ToString()))
        {
            Debug.Log("I've left the store.");
            ShopUI.gameObject.SetActive(false);
        }
    }

    private PlayerBehavior GetPlayer(Collider obj)
    {
        return obj.GetComponentInParent<PlayerBehavior>();
    }
}
