using UnityEngine;
using DG.Tweening;

public class HeartAnimator : MonoBehaviour
{
    RectTransform rect;
    CanvasGroup canvas;

    Tween currentTween;
    Tween idleTween;

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        canvas = GetComponent<CanvasGroup>();
        if (canvas == null)
            canvas = gameObject.AddComponent<CanvasGroup>();
    }

    public void AnimateIncrease()
    {
        StopIdleEffects();

        currentTween?.Kill();
        rect.localScale = Vector3.one;
        canvas.alpha = 1f;

        currentTween = rect
            .DOScale(1.25f, 0.15f)
            .SetEase(Ease.OutBack)
            .SetLoops(2, LoopType.Yoyo);
    }

    public void AnimateDecrease()
    {
        StopIdleEffects();

        currentTween?.Kill();
        rect.localScale = Vector3.one;
        canvas.alpha = 1f;

        currentTween = rect
            .DOShakePosition(
                0.25f,
                new Vector3(10f, 0f, 0f),
                10,
                90
            );
    }

    public void StartLowHealthBlink()
    {
        StopIdleEffects();

        idleTween = canvas
            .DOFade(0.3f, 0.4f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void StartMaxHealthPulse()
    {
        StopIdleEffects();

        idleTween = rect
            .DOScale(1.1f, 0.8f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void StopIdleEffects()
    {
        idleTween?.Kill();
        idleTween = null;

        rect.localScale = Vector3.one;
        canvas.alpha = 1f;
    }
}
