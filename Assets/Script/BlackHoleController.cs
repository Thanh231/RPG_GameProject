
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    private bool canGrow = true;

    private float maxGrowUp;

    private float speedGrowUp;

    private List<Transform> target = new List<Transform>();

    [SerializeField] private GameObject hotKeyPrefab;

    [SerializeField] private List<KeyCode> keyCodes = new List<KeyCode>();
    private List<GameObject> createHotKey = new List<GameObject>();

    private int amountAttack;
    private float cloneAttackCoolDown;
    private float cloneTime;
    private bool releaseClone;

    private bool isShrink;
    private float shrinkSpeed;
    public bool completeAttack;

    private float blackHoleDuration;

    public void SetUpBackHole(float _maxGrowUp, float _speedGrowUp, int _amountAttack, float _cloneAttackCoolDown, float _shrinkSpeed,float _blackHoleDuration)
    {
        maxGrowUp = _maxGrowUp;
        speedGrowUp = _speedGrowUp;
        amountAttack = _amountAttack;
        cloneAttackCoolDown = _cloneAttackCoolDown;
        shrinkSpeed = _shrinkSpeed;
        blackHoleDuration = _blackHoleDuration;
    }
    void Update()
    {
        if (canGrow && !isShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxGrowUp, maxGrowUp), speedGrowUp * Time.deltaTime);
        }
        if (isShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);
            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }

        cloneTime -= Time.deltaTime;

        blackHoleDuration -= Time.deltaTime;

        if(blackHoleDuration < 0)
        {
            blackHoleDuration = Mathf.Infinity;

            if(target.Count > 0)
            {
                ReleaseAttack();
            }
            else
            {
                DestroyHotKey();
                StartCoroutine(EndBlackHole(0f));
            }

        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseAttack();
        }
        CloneAttackLogic();
    }

    private void ReleaseAttack()
    {
        PlayerManager.instance.player.HideCharacter(true);
        DestroyHotKey();
        releaseClone = true;
    }

    private void CloneAttackLogic()
    {

        if (cloneTime < 0 && releaseClone && target.Count > 0)
        {
            cloneTime = cloneAttackCoolDown;
            int randomIndex = Random.Range(0, target.Count);

            float offset;

            if (Random.Range(0, 100) > 50)
            {
                offset = -1;
            }
            else
            {
                offset = 1;
            }

            SkillManager.instance.clone.CreateClone(target[randomIndex], new Vector2(offset, 0.2f));
            Debug.Log(amountAttack);
            amountAttack--;
            if (amountAttack <= 0)
            {
                releaseClone = false;
                StartCoroutine(EndBlackHole(1.5f));
            }

        }
    }

    private IEnumerator EndBlackHole(float second)
    {
        yield return new WaitForSeconds(second);
        completeAttack = true;
        releaseClone = false;
        PlayerManager.instance.player.HideCharacter(false);
        
        isShrink = true;
    }

    private void DestroyHotKey()
    {
        if (createHotKey.Count < 0) return;

        for (int i = 0; i < createHotKey.Count; i++)
        {
            Destroy(createHotKey[i]);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            AddHotKey(enemy);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Frozen(false);
        }
    }
    private void AddHotKey(Enemy enemy)
    {
        if (keyCodes.Count <= 0) return;

        if (releaseClone) return; // prevent sau khi dung R ma van tao hotKey

        enemy.Frozen(true);
        GameObject hotkey = Instantiate(hotKeyPrefab, enemy.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        createHotKey.Add(hotkey);

        HotKeyController hotKeyController = hotkey.GetComponent<HotKeyController>();

        KeyCode keyCode = keyCodes[Random.Range(0, keyCodes.Count)];

        hotKeyController.SetUpKey(keyCode, this, enemy);

        keyCodes.Remove(keyCode);
    }

    public void AddEnemyToList(Enemy enemy) => target.Add(enemy.transform);
}
