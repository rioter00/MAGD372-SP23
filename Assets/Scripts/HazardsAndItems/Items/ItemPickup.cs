using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    [SerializeField] GameObject[] items;
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
        TransferScript(items[currentItem].GetComponent<Item>(), player);
        StartCoroutine(StartReset());
        Destroy(currentItemModel);
    }

    private void TransferScript(Component sourceComponent, GameObject targetObject)
    {
        // Check if the source and target are valid
        if (sourceComponent == null || targetObject == null)
        {
            return;
        }

        // Get the source component's type
        System.Type sourceType = sourceComponent.GetType();

        // Add the source component's type to the target object
        Component targetComponent = targetObject.AddComponent(sourceType);

        // Copy the values from the source component to the target component
        EditorUtility.CopySerialized(sourceComponent, targetComponent);
    }

    void ResetItem()
    {
        currentItem = Random.Range(0, items.Length);
        currentItemModel = Instantiate(items[currentItem], spawnPoint);
    }

    IEnumerator StartReset()
    {
        yield return new WaitForSeconds(resetTime);
        ResetItem();
    }
}
