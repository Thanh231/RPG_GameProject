
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    public GameObject skeletons;
    public Enemy_Skeleton[] enemy_Skeletons;
    public GameObject platform;
    public RedHood boss;
    private void Start()
    {
        
    }
    private void Awake()
    {
        if (ins == null) 
        {
            ins = this; 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        enemy_Skeletons = skeletons.GetComponentsInChildren<Enemy_Skeleton>();
        if (enemy_Skeletons.Length == 3)
        {
            platform.SetActive(true);
        }
    }
}
