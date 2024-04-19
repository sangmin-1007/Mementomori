using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private PlayerStatsHandler _statsHandler;
    public float _timeSinceLastChange = float.MaxValue;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public float CurrentHealth {  get; private set; }
    public float MaxHealth => _statsHandler.allHealth;
    public float CurrentDefense { get; set; }

    private void Awake()
    {
       _statsHandler = GetComponent<PlayerStatsHandler>();
    }

    private void Start()
    {
        CurrentHealth = _statsHandler.allHealth;
        CurrentStamina = _statsHandler.CurrentStates.maxStamina;
        CurrentDefense = _statsHandler.allDefense;
    }

    private void Update()
    {
        if(_timeSinceLastChange < healthChangeDelay)
        {
            _timeSinceLastChange += Time.deltaTime;
            if(_timeSinceLastChange <= healthChangeDelay )
            {
                OnInvincibilityEnd?.Invoke();
            }
        }
        SPRechargeTime();
        SPRecover();
    }

    public bool ChangeHealth(float change)
    {
        if(change == 0 || _timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        _timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;


        if(change > 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
            if (_statsHandler.characterType == CharacterType.Player)
            {
                OnDamage?.Invoke();
              
                StartCoroutine(PlayerGetHit());
            }
        }
        if(CurrentHealth <= 0f)
        {
            CallDeath();
        }
        return true;
    }


    private void OnEnable()
    {
        CurrentHealth = MaxHealth;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }

    public float CurrentStamina { get; private set; }
    public float MaxStamina => _statsHandler.CurrentStates.maxStamina;

    [SerializeField] private float staminaRecoveryRate;
    private float rechargeTime;
    private float currentRechargeTime;
    private bool staminaUsed;

    public void DecreaseStamina(float _count)
    {
        staminaUsed = true;
        currentRechargeTime = 0;
        if (CurrentStamina - _count > 0)
        {
            CurrentStamina -= _count;
        }
        else
            CurrentStamina = 0;
    }
    private void SPRechargeTime()
    {
        if (staminaUsed)
        {
            if (currentRechargeTime < rechargeTime)
                currentRechargeTime++;
            else
                staminaUsed = false;
        }
    }
    private void SPRecover()
    {
        if (!staminaUsed && CurrentStamina < _statsHandler.CurrentStates.maxStamina)
        {
            CurrentStamina += staminaRecoveryRate * Time.deltaTime;
        }
    }
    public float GetCurrentSP()
    {
        return CurrentStamina;
    }

    IEnumerator PlayerGetHit()
    {
        Managers.SoundManager.Play("Effect/playerGetHit", Sound.Effect);
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = originalColor;
    }
}
