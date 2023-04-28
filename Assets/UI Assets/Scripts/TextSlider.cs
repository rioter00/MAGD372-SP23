using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
    public TextMeshProUGUI numberText;

    public void SetNumberText(float value)
    {
        numberText.text = value.ToString();
    }
}
