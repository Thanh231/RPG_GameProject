using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stats strength; //1 point increase damgage by 1 and crit.power 1%
    public Stats agility; // 1 point increase evasion by 1% and crit.chance by 1%
    public Stats intelligence; // 1 point increase magic damage 1 and magic resistance by 3
    public Stats vitality; // point increase health by 3 or 5 

    [Header("Defensive stats")]
    public Stats damage;
    public Stats maxHealth;
    public Stats evasion;
    public Stats armor;
    public Stats resistanceMagical;

    public float curentHealth;

    [Header("Offensive stats")]
    public Stats cirtChance;
    public Stats critPower;

    [Header("Magical Damage")]
    public Stats fireDamage;
    public Stats iceDamage;
    public Stats ligtningDamage;

    public bool isIgnite; // dang chay
    public bool isChill; // dang dong bang
    public bool isShock;


    protected virtual void Start()
    {
        curentHealth = maxHealth.GetValue();
        critPower.SetDefaltValue(150);
    }
    public void DoDamage(CharacterStats _target)
    {
        if (AvoidAttack(_target))
            return;

        int finalDamage = strength.GetValue() + damage.GetValue();
        if(CanCrit())
        {
            finalDamage = CalculateCritDamage(finalDamage);
        }

        finalDamage = MinusArmor(finalDamage);

        DoMagicalDamage(_target);
    }
    public void DoMagicalDamage(CharacterStats _target)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightningDamage = ligtningDamage.GetValue();

        int damage = _fireDamage + _iceDamage + _lightningDamage + intelligence.GetValue();
        damage -= _target.resistanceMagical.GetValue() + _target.intelligence.GetValue() * 3;

        _target.TakeDamage(damage);

        if (Mathf.Max(_fireDamage, _iceDamage, _lightningDamage) <= 0) return;

        bool applyIgnite = _fireDamage > _iceDamage && _iceDamage >  _lightningDamage;
        bool applyChill = _iceDamage > _fireDamage && _iceDamage > _lightningDamage;
        bool applyShock = _lightningDamage > _fireDamage && _lightningDamage > _iceDamage;


        while (!applyIgnite && !applyChill && !applyShock)
        {
            if(_fireDamage > 0 && Random.value < 0.3f)
            {
                applyIgnite = true;
                Debug.Log("was Ignite");
                _target.ApplyAliment(applyIgnite, applyChill, applyShock);
                return;
            }
            if (_iceDamage > 0 && Random.value < 0.5f)
            {
                applyChill = true;
                Debug.Log("was Chill");
                _target.ApplyAliment(applyIgnite, applyChill, applyShock);
                return;
            }
            if (_lightningDamage > 0 && Random.value < 0.5f)
            {
                applyShock = true;
                Debug.Log("was Shock");
                _target.ApplyAliment(applyIgnite, applyChill, applyShock);
                return;
            }

        }


        _target.ApplyAliment(applyIgnite, applyChill, applyShock);

    }
    public void ApplyAliment(bool _isIgnit,bool  _isChill,bool _isShock)
    {
        if (isIgnite || isChill || isShock  ) return;

        isIgnite = _isIgnit;
        isChill = _isChill;
        isShock = _isShock;
        
    }
    private bool CanCrit()
    {
        int crit = cirtChance.GetValue() + agility.GetValue();
        if (crit > Random.Range(0, 100))
        {
            Debug.Log("Crit Hit");
            return true;
        }
        return false;
    }

    private int MinusArmor(int finalDamage)
    {
        finalDamage -= armor.GetValue();
        finalDamage = Mathf.Clamp(finalDamage, 0, int.MaxValue);
        return finalDamage;
    }

    private bool AvoidAttack(CharacterStats _target)
    {
        int totalEvasion = _target.evasion.GetValue() + _target.agility.GetValue();

        if (totalEvasion > Random.Range(0, 100))
        {
            Debug.Log("Attack Avoid");
            return true;
        }
        return false;
    }
    private int CalculateCritDamage(int damage)
    {
        float totalCritChance = (strength.GetValue() + critPower.GetValue()) * 0.01f;

        float totalCritDamage = damage * totalCritChance;

        return Mathf.RoundToInt(totalCritDamage);
    }
    public virtual void TakeDamage(float damage)
    {
        curentHealth -= damage;
        if (curentHealth <= 0) 
        {
            Die();
        }
    }
    public virtual void Die()
    {
        
    }
    
}
