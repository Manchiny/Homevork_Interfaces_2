using System;
using UnityEngine;

public class Player
{
    private const float DamageValue = 10f;
    private const float HealValue = 10f;

    private PlayerView _view;

    public float MaxHealth { get; private set; } = 100f;
    public float Health { get; private set; }

    public event Action HealthChanged;

    public Player(PlayerView view)
    {
        Health = MaxHealth;

        view.HealthBar.Init(this);
        view.ControlWindow.Init(this);

        _view = view;
    }

    public void GetDamage()
    {
        if (Health <= 0)
            return;

        Health = Mathf.Clamp(Health - DamageValue, 0, MaxHealth);

        if (Health > 0)
            _view.GetDamage();
        else
            _view.Die();

        HealthChanged?.Invoke();
    }

    public void Heal()
    {
        if (Health <= 0)
            return;

        Health = Mathf.Clamp(Health + HealValue, 0, MaxHealth);
        HealthChanged?.Invoke();
    }
}
