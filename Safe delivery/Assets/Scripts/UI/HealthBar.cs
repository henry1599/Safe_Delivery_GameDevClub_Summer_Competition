using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Animator anim;
    private string currentState;
    public void SetMaxValue(float _HP)
    {
        slider.maxValue = _HP;
        slider.value = _HP;
    }

    public void SetValue(float _HP)
    {
        StartCoroutine(AnimateHealthBar());
        slider.value = _HP;
    }

    void ChangeStateAnimation(string state)
    {
        if (state == currentState) return;
        currentState = state;
        anim.Play(state);
    }

    IEnumerator AnimateHealthBar()
    {
        ChangeStateAnimation("bar-change");
        yield return new WaitForSecondsRealtime(0.25f);
        ChangeStateAnimation("idle");
    }
}
