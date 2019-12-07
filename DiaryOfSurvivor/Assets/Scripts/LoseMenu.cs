using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// КЛАСС УПРАВЛЕНИЯ МЕНЮ ПРОИГРЫША
public class LoseMenu : MonoBehaviour
{
    public float speed = 5f, checkPos = 350f; // скорость движения и координаты остановки

    public RectTransform pos;   // компонент размещения окна проигрыша
    public Text loseStage,countdropbox;  // текст выводящийся при проигрыше

    private float startPosY;    // стартовая позицыя 

    // анимация показа окна проигрыша 
    public IEnumerator ShowLoseBoard()
    {
        startPosY = transform.position.y;
        while (transform.position.y <= checkPos)
        {
            transform.position = new Vector2(pos.position.x, pos.position.y + speed);
            yield return null;
        }
    }

    // анимация показа окна проигрыша для выживания
    public IEnumerator ShowLoseBoard(int stage)
    {
        startPosY = transform.position.y;
        loseStage.text = "Вы не прошли " + stage + " этап";
        int randCountBox = Random.Range(0, stage);
        for(int i=0;i<100;i++)
            Debug.Log(Random.Range(0, stage));
        PlayerPrefs.SetInt("CountBox", PlayerPrefs.GetInt("CountBox", 0) + randCountBox);
        PlayerPrefs.Save();
        countdropbox.text = randCountBox.ToString();
        while (transform.position.y <= checkPos)
        {
            transform.position = new Vector2(pos.position.x, pos.position.y + speed);
            yield return null;
        }
    }

    // анимация убирания окна проигрыша 
    public IEnumerator HideLoseBoard()
    {
        while (transform.position.y >= startPosY)
        {
            transform.position = new Vector2(pos.position.x, pos.position.y - speed);
            yield return null;
        }
    }
}