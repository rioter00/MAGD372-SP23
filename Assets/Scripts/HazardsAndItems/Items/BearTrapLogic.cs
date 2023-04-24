////////////////////////////////////////////// goes on bear trap game object //////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BearTrapLogic : MonoBehaviour
{
    [SerializeField] float despawnTime;
    [SerializeField] float clampTime;
    [SerializeField] MeshRenderer openModel;
    [SerializeField] MeshRenderer clampedModel;
    bool isClamped;
    bool gracePeriod = true;

    void Start()
    {
        StartCoroutine(StartDespawn());
    }

    IEnumerator StartDespawn()
    {
        yield return new WaitForSeconds(despawnTime);
        if (!isClamped)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gracePeriod && other.CompareTag("Player")) // either player can get trapped
        {
            openModel.enabled = false;
            clampedModel.enabled = true;

            Rigidbody player = other.GetComponent<Rigidbody>();
            Clamp(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            gracePeriod = false;
    }

    void Clamp(Rigidbody player)
    {
        isClamped = true;
        player.constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(StartRelease(player));
    }

    IEnumerator StartRelease(Rigidbody player)
    {
        yield return new WaitForSeconds(clampTime);
        player.constraints &= ~RigidbodyConstraints.FreezePositionX;
        player.constraints &= ~RigidbodyConstraints.FreezePositionY;
        player.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        Destroy(gameObject);
    }
}
