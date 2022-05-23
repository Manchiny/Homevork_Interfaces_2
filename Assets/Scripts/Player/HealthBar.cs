using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    private const float AnimationDuration = 0.5f;
    private const float SsliderErrorRate = 0.01f;

    private Slider _slider;
    private Player _player;
    private Image _fill;

    private Coroutine _setSliderValueCoroutine;

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    public void Init(Player player)
    {
        _slider = GetComponent<Slider>();

        _slider.maxValue = player.MaxHealth;
        _slider.value = player.Health;

        _fill = _slider.fillRect.GetComponent<Image>();

        _player = player;

        _player.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        if (_setSliderValueCoroutine != null)
            StopCoroutine(_setSliderValueCoroutine);

        _setSliderValueCoroutine = StartCoroutine(SetValue(_player.Health));
    }

    private IEnumerator SetValue(float endValue)
    {
        while (Mathf.Abs(endValue - _slider.value) > SsliderErrorRate)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, endValue, 1f / AnimationDuration * Time.fixedDeltaTime);
            yield return null;
        }

        _slider.value = endValue;

        if (endValue == 0)
        {
            _player.HealthChanged -= OnHealthChanged;
            _fill.enabled = false;
        }    
    }
}
