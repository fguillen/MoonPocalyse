using UnityEngine;
using DG.Tweening;

public class BulletMovementOrbitalController : BulletMovementBase
{
    Sequence sequence;
    [SerializeField] Transform bodyElement;

    public override void StartMovement(GunScriptable gunData)
    {
        Debug.Log($"BulletMovementOrbitalController.StartMovement({gunData.speed})");
        Vector2 direction = GameManagerController.Instance.playerController.playerMovementController.lastHorizontalDirection;

        if(direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        bodyElement.localScale = new Vector3(0, 0, 1);
        float animationDuration = 1f / gunData.speed;

        sequence = DOTween.Sequence();

        sequence.Append(bodyElement.DOScale(new Vector3(1, 1, 1), animationDuration / 10).SetEase(Ease.Linear));
        sequence.Join(transform.DOLocalRotate(new Vector3(0, 0, 360), animationDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear));
        sequence.Insert(animationDuration - (animationDuration / 10), bodyElement.DOScale(new Vector3(0, 0, 0), animationDuration / 10).SetEase(Ease.Linear));
        sequence.OnComplete(() => Destroy(gameObject));
    }

    public override void Move()
    {
        transform.position = new Vector2(transform.position.x, GameManagerController.Instance.playerController.transform.position.y);
    }

    void OnDestroy()
    {
        sequence.Kill();
    }
}
