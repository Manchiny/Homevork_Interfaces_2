using System;
using UnityEngine;

public class Player
{
    private const float DamageValue = 10f;
    private const float HealValue = 10f;

    public float MaxHealth { get; private set; } = 100f;
    public float Health { get; private set; }

    public event Action Damaged;
    public event Action Healed;

    public event Action Died;

    public Player(PlayerView view)
    {
        Health = MaxHealth;

        view.HealthBar.Init(this);
        view.ControlWindow.Init(this);

        Died += view.OnDie;
        Damaged += view.OnGetDamage;
    }

    public void GetDamage()
    {
        if (Health <= 0)
            return;

        Health = Mathf.Clamp(Health - DamageValue, 0, MaxHealth);

        if (Health > 0)
            Damaged?.Invoke();
        else
            Died?.Invoke();
    }

    public void Heal()
    {
        if (Health <= 0)
            return;

        Health = Mathf.Clamp(Health + HealValue, 0, MaxHealth);
        Healed?.Invoke();
    }
}
