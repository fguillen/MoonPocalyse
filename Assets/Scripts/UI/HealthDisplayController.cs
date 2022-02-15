using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class HealthDisplayController : MonoBehaviour
{
    [SerializeField] TMP_Text textFieldPrefab;
    [SerializeField] MiniHealthBarController miniHealthBarController;

    public void Show(float damage, float life, float maxLife)
    {
        Debug.Log($"HealthDisplayController.Show({damage}, {life}, {maxLife})");
        TMP_Text textField = Instantiate(textFieldPrefab, this.transform.position, Quaternion.identity);
        textField.text = $"-{damage}";
        // textField.transform.position = this.transform.position;

        float animationDuration = 1f;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(textField.transform.DOLocalMove((Vector2)this.transform.position + (Vector2.up * 2f), animationDuration));
        sequence.Insert(animationDuration * 0.5f, DOTween.ToAlpha(()=> textField.color, x=> textField.color = x, 0, animationDuration * 0.5f));
        sequence.OnComplete(() => Destroy(textField.gameObject));

        miniHealthBarController.SetValue(life, maxLife);
    }

}
