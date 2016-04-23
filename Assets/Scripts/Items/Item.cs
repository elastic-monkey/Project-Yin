using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour {

    public Tags TagToCompare;
    public int Value;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagToCompare.ToString()))
        {
            var player = other.GetComponent<PlayerBehavior>();
            StartCoroutine(ApplyItem(player));
            Destroy(gameObject);
        }
    }

    private IEnumerator ApplyItem(PlayerBehavior player)
    {
        AddItemToPlayer(player);
    }

    private void AddItemToPlayer(PlayerBehavior player)
    {
    }
}
