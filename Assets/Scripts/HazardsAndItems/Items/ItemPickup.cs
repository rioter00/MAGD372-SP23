using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item[] items;
    [SerializeField] float resetTime;
    int currentItem;

    void Start()
    {
        ResetItem();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            Collected(player);
        }
    }

    void Collected(GameObject player)
    {
        player.AddComponent(items[currentItem].GetType()); // unsure if this will return Item.cs or specific item script
        StartCoroutine(StartReset());
    }

    void ResetItem()
    {
        currentItem = Random.Range(0, items.Length);
    }

    IEnumerator StartReset()
    {
        yield return new WaitForSeconds(resetTime);
        ResetItem();
    }
}
