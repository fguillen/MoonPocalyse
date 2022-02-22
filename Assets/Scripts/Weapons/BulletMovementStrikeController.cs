using UnityEngine;
using DG.Tweening;

public class BulletMovementStrikeController : BulletMovementBase
{
    Sequence sequence;

    public override void StartMovement(GunScriptable gunData)
    {
        Vector2 direction = GameManagerController.Instance.playerController.playerMovementController.lastHorizontalDirection;

        if(direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        sequence = DOTween.Sequence();

        sequence.Append(transform.DOLocalMoveX(transform.position.x + (gunData.strikeRange * direction.x), gunData.strikeRange / gunData.speed).SetEase(Ease.InCirc));
        sequence.Append(transform.DOLocalMoveX(transform.position.x, gunData.strikeRange / gunData.speed).SetEase(Ease.InCirc));
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
