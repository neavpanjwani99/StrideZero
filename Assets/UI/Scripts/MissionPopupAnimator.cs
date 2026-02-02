using UnityEngine;
using DG.Tweening;

public class MissionPopupAnimator : MonoBehaviour
{
    RectTransform rect;
    CanvasGroup canvas;
    Tween currentTween;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvas = GetComponent<CanvasGroup>();

        if (canvas == null)
            canvas = gameObject.AddComponent<CanvasGroup>();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        // SAFETY: kill previous tweens
        currentTween?.Kill();

        rect.localScale = Vector3.zero;
        canvas.alpha = 0f;

        currentTween = DOTween.Sequence()
            .Append(rect.DOScale(1f, 0.35f).SetEase(Ease.OutBack))
            .Join(canvas.DOFade(1f, 0.25f))
            .SetUpdate(true);
    }

    public void Hide()
    {
        currentTween?.Kill();

        currentTween = DOTween.Sequence()
            .Append(rect.DOScale(0f, 0.2f).SetEase(Ease.InBack))
            .Join(canvas.DOFade(0f, 0.15f))
            .SetUpdate(true)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
