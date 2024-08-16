using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnetityFx : MonoBehaviour
{
    public Material wasDamage;
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

}
