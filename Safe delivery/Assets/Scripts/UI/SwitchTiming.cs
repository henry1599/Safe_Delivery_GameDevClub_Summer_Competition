using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTiming : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetValue(float time)
    {
        slider.value = time;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxValue(float time)
    {
        slider.maxValue = time;
        slider.value = time;
        fill.color = gradient.Evaluate(1f);
    }
}
