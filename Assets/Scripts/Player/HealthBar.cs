using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    private const float AnimationDuration = 0.75f;

    private Slider _slider;
    private Player _player;

    private Image _fill;

    private Tween _valueChahgeAnimation;

    public void Init(Player player)
    {
        _slider = GetComponent<Slider>();

        _slider.maxValue = player.MaxHealth;
        _slider.value = player.Health;

        _fill = _slider.fillRect.GetComponent<Image>();

        _player = player;

        _player.HealthChanged += OnHealthChanged;
    }

    public void OnHealthChanged()
    {
        _valueChahgeAnimation?.Kill();

        _valueChahgeAnimation = _slider.DOValue(_player.Health, AnimationDuration).SetLink(gameObject).SetEase(Ease.OutSine)
            .OnComplete(() => _fill.enabled = _player.Health > 0);
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }
}
