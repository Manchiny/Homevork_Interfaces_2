using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicButton : Button
{
    private RectTransform _transform;
    private Sequence _sequence;
    private Vector2 _startScale;

    private List<Action> _onClickActions = new List<Action>();

    private float _scaleAnimationTime = 0.05f;
    private float _scaleAnimationValue = 0.9f;

    protected override void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _startScale = _transform.localScale;

        onClick.AddListener(OnButtonClick);
    }

    protected override void OnDestroy()
    {
        RemoveAllListeners();
    }

    public void AddListener(Action onButtonClick)
    {
        _onClickActions.Add(onButtonClick);
    }

    public void RemoveAllListeners()
    {
        _onClickActions?.Clear();
        onClick.RemoveAllListeners();
    }

    private void OnButtonClick()
    {
        _sequence?.Kill();
        _transform.localScale = _startScale;

        _sequence = DOTween.Sequence().SetLink(gameObject).SetEase(Ease.Linear);

        _sequence.Append(_transform.DOScale(_scaleAnimationValue, _scaleAnimationTime));
        _sequence.Append(_transform.DOScale(1f, _scaleAnimationTime));

        _sequence.Play()
            .OnComplete(() =>
            {
                for (int i = 0; i < _onClickActions.Count; i++)
                {
                    var action = _onClickActions[i];
                    action?.Invoke();
                }
            });
    }
}
