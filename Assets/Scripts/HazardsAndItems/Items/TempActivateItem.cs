/////////////////////////////////////////// Temporary ///////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempActivateItem : MonoBehaviour
{
    bool activated;
    Item item;

    void Update()
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
}
