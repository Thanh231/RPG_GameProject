using System.Collections;
using UnityEngine;

public class EnetityFx : MonoBehaviour
{
    public Material wasDamage;
    public Material ignite;
    public Material chill;
    public Material shock;
    private SpriteRenderer sprite;
    private Material normal;
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();    
        normal = sprite.material;
    }
    public IEnumerator Flash()
    {
        sprite.material = wasDamage;
        yield return new WaitForSeconds(0.2f);
        sprite.material = normal;

    }
    private void BlinkRed()
    {
        if(sprite.color != Color.white)
        {
            sprite.color = Color.white;
        }
        else
        {
            sprite.color = Color.red;
        }
    }
    private void CancelBlinkRed()
    {

        CancelInvoke();
        sprite.color = Color.white;
        
    }
    public IEnumerator ApplyAlimentForEntity(Material effect, float second)
    {
        sprite.material = effect;
        yield return new WaitForSeconds(second/3);
        sprite.material = normal;
        yield return new WaitForSeconds(0.01f);
        sprite.material = effect;
        yield return new WaitForSeconds(second/3);
        sprite.material = normal;
        yield return new WaitForSeconds(0.01f);
        sprite.material = effect;
        yield return new WaitForSeconds(second / 3);
        sprite.material = normal;

    }
}
