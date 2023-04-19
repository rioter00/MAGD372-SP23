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

            //PlayerInput input = other.GetComponent<PlayerInput>();
            //Clamp(input);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            gracePeriod = false;
    }

    //void Clamp(PlayerInput input)
    //{
    //    input.SetActive(false); // or deactivate movement method(s) so player can still look around or use powerups
    //    StartCoroutine(StartRelease(input));
    //}

    //IEnumerator StartRelease(PlayerInput input)
    //{
    //    yield return new WaitForSeconds(clampTime);
    //    input.SetActive(true);
    //    Destroy(gameObject);
    //}
}
