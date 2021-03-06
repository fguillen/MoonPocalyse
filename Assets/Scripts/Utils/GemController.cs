using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GemController : MonoBehaviour
{
    [SerializeField] GemScriptable gemData;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer.sprite = gemData.sprite;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect() {
        GameManagerController.Instance.CollectMana(gemData.mana);

        // Animation
        float animationDuration = 0.5f;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove((Vector2)transform.position + (Vector2.up * 2f), animationDuration));
        sequence.Insert(animationDuration * 0.5f, DOTween.ToAlpha(()=> spriteRenderer.color, x=> spriteRenderer.color = x, 0, animationDuration * 0.5f));
        sequence.OnComplete(() => Destroy(this));
    }
}
