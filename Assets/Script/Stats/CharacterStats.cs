using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private EnetityFx fx;
    private Entity entity;

    public float applyAlimentTime;
    public Stats strength; //1 point increase damgage by 1 and crit.power 1%
    public Stats agility; // 1 point increase evasion by 1% and crit.chance by 1%
    public Stats intelligence; // 1 point increase magic damage 1 and magic resistance by 3
    public Stats vitality; // point increase health by 3 or 5 

    [Header("Defensive stats")]
    public Stats maxHealth;
    public Stats evasion;
    public Stats armor;
    public Stats resistanceMagical;

    public float curentHealth;

    [Header("Offensive stats")]
    public Stats damage;
    public Stats critChance;
    public Stats critPower;

    [Header("Magical DamageEffect")]
    public Stats fireDamage;
    public Stats iceDamage;
    public Stats ligtningDamage;

    public bool isIgnite; // dang chay // do damage over time
    public bool isChill; // dang dong bang // reduce armor 20
    public bool isShock; // tang advoid len 20

    private float burnTimer;
    private float iceTime;
    private float shockTime;
    private float burnDamageTimer;
    private float burnDamageCoolDown = 0.3f;

    private int burnDamage;

    private int thurnderDamage;
    private float thunderDamageTimer;
    private float thunderDamageCoolDown = 2f;


    public System.Action onUpdateHp;

    protected virtual void Start()
    {
        curentHealth = GetMaxHealth();
        critPower.SetDefaltValue(150);
        fx = GetComponent<EnetityFx>();
        entity = GetComponent<Entity>();
    }
    public void DoDamage(CharacterStats _target)
    {

        if (AvoidAttack(_target))
            return;

        int finalDamage = strength.GetValue() + damage.GetValue();
        if (CanCrit())
        {
            finalDamage = CalculateCritDamage(finalDamage);
        }

        finalDamage = MinusArmor(_target, finalDamage);

        DoMagicalDamage(_target,finalDamage);
    }

    protected virtual void Update()
    {
        burnTimer -= Time.deltaTime;
        iceTime -= Time.deltaTime;
        shockTime -= Time.deltaTime;


        burnDamageTimer -= Time.deltaTime;
        thunderDamageTimer -= Time.deltaTime;

        if(burnTimer < 0)
        {
            isIgnite = false;
        }
        if(iceTime < 0)
        {
            isChill = false;
        }
        if(shockTime < 0)
        {
            isShock = false;
        }
        if(burnDamageTimer < 0 && isIgnite)
        {

            DecreaseHealth(burnDamage);
            if(curentHealth < 0)
            {
                Die();
            }
            burnDamageTimer = burnDamageCoolDown;
        }
        if(thunderDamageTimer < 0 && isShock)
        {
            Debug.Log("is thurnder with damage " + thurnderDamage);
            DecreaseHealth(thurnderDamage);
            if(curentHealth < 0)
            {
                Die();
            }
            thunderDamageTimer = thunderDamageCoolDown;
            entity.CreateThunder();
        }

    }
 
    public void DoMagicalDamage(CharacterStats _target, float finalDamage)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightningDamage = ligtningDamage.GetValue();

        finalDamage += _fireDamage + _iceDamage + _lightningDamage + intelligence.GetValue();
        finalDamage -= (_target.resistanceMagical.GetValue() + _target.intelligence.GetValue() * 3);

        _target.TakeDamage(Mathf.RoundToInt(finalDamage));

        if (Mathf.Max(_fireDamage, _iceDamage, _lightningDamage) <= 0) return;

        bool applyIgnite = _fireDamage > _iceDamage && _fireDamage >  _lightningDamage;
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
        if (applyIgnite && _fireDamage != 0)
        {
            _target.SetBunrnDamge(Mathf.RoundToInt(_fireDamage * 0.5f));
        }
        if(applyShock && _lightningDamage != 0)
        {
            _target.SetThurnderDamage(Mathf.RoundToInt(_lightningDamage * 0.8f));
        }

            

        _target.ApplyAliment(applyIgnite, applyChill, applyShock);

    }
    private static int MinusArmor(CharacterStats _target, int finalDamage)
    {

        if (_target.isChill)
        {
            finalDamage -= Mathf.RoundToInt(_target.armor.GetValue() * 0.8f);
        }
        else
        {
            finalDamage -= _target.armor.GetValue();
        }

        finalDamage = Mathf.Clamp(finalDamage, 0, int.MaxValue);
        return finalDamage;
    }
    public void SetBunrnDamge(int damage)
    {
        burnDamage = damage;
    }

    public void SetThurnderDamage(int damage)
    {
        thurnderDamage = damage;
    }
    public void ApplyAliment(bool _isIgnit,bool  _isChill,bool _isShock)
    {
        if (isIgnite || isChill || isShock  ) return;

        if(_isIgnit)
        {
            isIgnite = _isIgnit;
            burnTimer = applyAlimentTime;
            fx.StartCoroutine(fx.ApplyAlimentForEntity(fx.ignite,applyAlimentTime));
            
        }
        if(_isChill)
        {
            isChill = _isChill;
            iceTime = applyAlimentTime;
            fx.StartCoroutine(fx.ApplyAlimentForEntity(fx.chill, applyAlimentTime));
            entity.DecreaseSpeed(0.5f,applyAlimentTime);
        }
        if(_isShock)
        {
            isShock = _isShock;
            shockTime = applyAlimentTime;
            fx.StartCoroutine(fx.ApplyAlimentForEntity(fx.shock, applyAlimentTime));
           
        }
        
    }
    private bool CanCrit()
    {
        int crit = critChance.GetValue() + agility.GetValue();
        if (crit > Random.Range(0, 100))
        {
            Debug.Log("Crit Hit");
            return true;
        }
        return false;
    }

    private bool AvoidAttack(CharacterStats _target)
    {
        int totalEvasion = _target.evasion.GetValue() + _target.agility.GetValue();

        if(_target.isShock)
        {
            totalEvasion += 20;
        }

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
    public virtual void TakeDamage(int damage)
    {
        DecreaseHealth(damage);

        if (curentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void DecreaseHealth(int damage)
    {
        curentHealth -= damage;
        onUpdateHp?.Invoke();
    }
    public virtual void Die()
    {
        
    }
    public int GetMaxHealth()
    {
        int maxHp =  maxHealth.GetValue() + vitality.GetValue() * 5;
        return Mathf.Clamp(maxHp, 0, maxHealth.GetValue());

    }
    
}
