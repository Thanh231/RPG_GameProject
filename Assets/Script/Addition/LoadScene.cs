using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void LoadGame()
    {
        StartCoroutine(LoadDelay());
    } 
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ClickOnAnim()
    {
        anim.SetBool("Click",true);
    }
    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
    
}
