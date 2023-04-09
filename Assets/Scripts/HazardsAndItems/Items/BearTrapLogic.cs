////////////////////////////////////////////// goes on bear trap game object //////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class BearTrapLogic : MonoBehaviour
{
    [SerializeField] float despawnTime;
    [SerializeField] float clampTime;
    bool isClamped;

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
        if (other.CompareTag("Player")) // either player can get trapped
        {
            //PlayerInput input = other.GetComponent<PlayerInput>();
            //Clamp(input);
        }
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
