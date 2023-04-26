/////////////////////////////////////////// Temporary ///////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials.Reference_Variables.Variables;

public class TempActivateItem : MonoBehaviour
{
    bool activated;
    Item item = null; // remove this
    //[SerializeField] FloatVariable activationInput; // add this

    void Update() // remove this
    {
        if (!activated && Input.GetKeyDown(KeyCode.E))
        {
            item = GetComponent<Item>();
            if (item != null)
            {
                item.Activate();
                activated = true;
            }
        }

        if (item == null)
            activated = false;
    }

    // add everything underneath

    //private void OnEnable()
    //{
    //    activationInput.ValueChanged += ItemActivation;
    //}

    //private void OnDisable()
    //{
    //    activationInput.ValueChanged -= ItemActivation;
    //}

    //private void ItemActivation(object sender, System.EventArgs e)
    //{
    //    if (activationInput.Value != 1)
    //    {
    //        return;
    //    }

    //    Item item = null;

    //    if (!activated)
    //    {
    //        item = GetComponent<Item>();
    //        if (item != null)
    //        {
    //            item.Activate();
    //            activated = true;
    //        }
    //    }

    //    if (item == null)
    //        activated = false;
    //}
}
