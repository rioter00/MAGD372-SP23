using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item[] itemScripts;
    [SerializeField] GameObject[] itemModels;
    [SerializeField] float resetTime;
    [SerializeField] Transform spawnPoint;
    int currentItem;
    GameObject currentItemModel;

    void Start()
    {
        ResetItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            Collected(player);
        }
    }

    void Collected(GameObject player)
    {
        player.AddComponent(itemScripts[currentItem].GetType());
        StartCoroutine(StartReset());
        Destroy(currentItemModel);
    }

    void ResetItem()
    {
        currentItem = Random.Range(0, itemScripts.Length);
        currentItemModel = Instantiate(itemModels[currentItem], spawnPoint);
    }

    IEnumerator StartReset()
    {
        yield return new WaitForSeconds(resetTime);
        ResetItem();
    }
}
