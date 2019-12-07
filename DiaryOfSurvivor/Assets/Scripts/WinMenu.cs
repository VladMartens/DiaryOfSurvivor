using UnityEngine;
using System.Collections;

// КЛАСС УПРАВЛЕНИЯ МЕНЮ ПОБЕДЫ 
public class WinMenu : MonoBehaviour
{
    public float speed = 1f, checkPos = 350f;   // скорость движения и координаты остановки

    public RectTransform pos;   // компонент размещения окна проигрыша

    // анимация показа окна победы 
    public IEnumerator ShowWinBoard()
    {
        while (transform.position.y >= checkPos)
        {
            yield return new WaitForSeconds(0f);
            transform.position = new Vector2(pos.position.x, pos.position.y - speed);
        }
    }
}