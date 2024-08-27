using TMPro;
using UnityEngine;

public class HotKeyController : MonoBehaviour
{
    private KeyCode keyCode;
    private TextMeshProUGUI myText;
    private BlackHoleController blackHoleController;
    private Enemy enemy;
    private SpriteRenderer sr;
    public void SetUpKey(KeyCode _keyCode, BlackHoleController _blackHoleController, Enemy _enemy)
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
        sr = GetComponentInChildren<SpriteRenderer>();

        keyCode = _keyCode;
        blackHoleController = _blackHoleController;
        enemy = _enemy;

        myText.text = keyCode.ToString();
    }
    private void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            blackHoleController.AddEnemyToList(enemy);

            sr.color = Color.clear;
            myText.color = Color.clear;
        }
    }
}
