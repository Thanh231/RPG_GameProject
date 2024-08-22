using UnityEngine;


public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}
public class SwordSkill : Skill
{
    public SwordType type;

    public GameObject swordPrefabs;
    public Vector2 swordDir;
    public float gravity;
    public float freezeTime;
    public float returnSpeed;

    [Header("DOT")]
    public int numberOfDots;
    public float spaceBetWeenDots;
    public Transform dotParent;
    private GameObject[] dots;
    private Vector2 finalSwordDir;
    public GameObject dotPrefab;

    [Header("Bounce")]
    public int amountBounce;
    public int bounceGravity;

    [Header("Pirece")]
    public int pireceAmount;
    public float pireceGravity;

    [Header("Spin")]
    public float maxDistance;
    public float spinDuration;
    public float gravitySpin;
    public float hitDuration;

    public override void Start()
    {
        base.Start();
        GenerateDot();
        if(type == SwordType.Bounce)
        {
            gravity = bounceGravity;
        }
        else if(type == SwordType.Pierce)
        {
            gravity = pireceGravity;
            
        }
        else if(type == SwordType.Spin)
        {
            gravity = gravitySpin;
        }
        SetBoolDot(false);
    }
    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            finalSwordDir = new Vector2(SwordDir().normalized.x * swordDir.x, SwordDir().normalized.y * swordDir.y);
        }
        if(Input.GetMouseButton(0)) 
        {
            for(int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = SetPosDots(i * spaceBetWeenDots);
            }
        }
        
    }
    public void CreateSword()
    {
        GameObject sword = Instantiate(swordPrefabs,player.transform.position,Quaternion.identity);
        SwordController controller = sword.GetComponent<SwordController>();

        if(type == SwordType.Bounce)
        {
            controller.SetUpBounce(amountBounce,true);
        }
        else if(type == SwordType.Pierce)
        {
            controller.SetUpPirece(pireceAmount);
        }
        else if(type == SwordType.Spin)
        {
            controller.SetUpSpin(spinDuration,maxDistance,true,hitDuration);
        }

        controller.SetUpSword(finalSwordDir,gravity,player,freezeTime,returnSpeed,hitDuration);

        player.AssignSword(sword);

        SetBoolDot(false);
    }
    #region Aim
    public Vector2 SwordDir()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = player.transform.position;
        Vector2 dir = mousePos - playerPos;
        return dir;
    }

    public void SetBoolDot(bool isActive)
    {
        for(int i = 0;i < dots.Length; i++)
        {
            dots[i].SetActive(isActive);
        }
    }    
    private void GenerateDot()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0;i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity,dotParent);
        }
    }
    private Vector2 SetPosDots(float t)
    {
        Vector2 pos = (Vector2)player.transform.position 
            + new Vector2(SwordDir().normalized.x * swordDir.x,SwordDir().normalized.y * swordDir.y) * t 
            + 0.5f *t*t *(Physics2D.gravity * gravity);
        return pos;
    }
    #endregion
}
