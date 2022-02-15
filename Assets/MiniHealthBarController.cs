using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MiniHealthBarController : MonoBehaviour
{
    [SerializeField] GameObject slider;
    float maxSliderSize;
    Sequence sequence;

    void Awake()
    {
        maxSliderSize = slider.transform.localScale.x;
    }

    void Start()
    {
        this.transform.localScale = Vector3.zero;
    }

    void OnDestroy()
    {
        if(sequence != null)
            sequence.Kill();
    }

    public void SetValue(float actualValue, float maxValue)
    {
        float actualSliderScale = Mathf.Lerp(0, maxSliderSize, actualValue / maxValue);
        slider.transform.localScale = new Vector3(actualSliderScale, slider.transform.localScale.y, slider.transform.localScale.z);

        Animate();
    }

    void Animate()
    {
        if(sequence != null)
            sequence.Kill();

        float animationDuration = 0.5f;
        sequence = DOTween.Sequence();
        sequence.Append(this.transform.DOScale(new Vector3(1.2f, 1.2f, 1), animationDuration / 5));
        sequence.Append(this.transform.DOScale(new Vector3(1, 1, 1), animationDuration * 3 / 5));
        sequence.Append(this.transform.DOScale(new Vector3(0, 0, 0), animationDuration / 5f));
    }
}
