using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    public Tags TagToCompare;
    public int Value;
    public string ItemName;

    public RectTransform ParentPanel;
    public Text UiText;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagToCompare.ToString()))
        {
            var player = other.GetComponentInParent<PlayerBehavior>();
            StartCoroutine(ApplyItem(player));
        }
    }

    private IEnumerator ApplyItem(PlayerBehavior player)
    {
        AddItemToPlayer(player);

        yield return new WaitForSeconds(0.5f);

        var uiText = Instantiate(UiText);
        uiText.text = ItemName + " (" + Value + "$)";
        uiText.transform.SetParent(ParentPanel);

        gameObject.SetActive(false);
    }

    private void AddItemToPlayer(PlayerBehavior player)
    {
        player.PlayerInventory.AddItemToInventory(this);
    }
}
