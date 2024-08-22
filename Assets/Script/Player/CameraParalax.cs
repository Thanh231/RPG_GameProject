using UnityEngine;

public class CameraParalax : MonoBehaviour
{
    [SerializeField] private float paralaxEffect;
    float xPos;
    private GameObject mainCam;
    private float length;
    public float test;
    void Start()
    {
        mainCam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPos = mainCam.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //float deltaX = mainCam.transform.position.x - xPos;
        float distanceMove = mainCam.transform.position.x * (1 - paralaxEffect);
        float distanceToMove = mainCam.transform.position.x * paralaxEffect;


        

        
        if (distanceMove > xPos + length)
        {
            xPos = xPos + length;
        }
        else if (distanceMove < xPos - length)
        {
            xPos = xPos - length;
        }
        Vector3 targetPosition = new Vector3(xPos + distanceToMove, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, test);
    }
}
