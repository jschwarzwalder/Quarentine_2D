using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxMood(int mood)
    {
        slider.maxValue = mood;
        slider.value = mood;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetMood(int mood)
    {
        slider.value = mood;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
