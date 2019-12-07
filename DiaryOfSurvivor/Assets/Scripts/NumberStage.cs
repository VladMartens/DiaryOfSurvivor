using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// КЛАСС ДЛЯ ОТОБРАЖЕНИЯ ВОЛНЫ В РЕЖИМЕ ВЫЖИВАНИЯ
public class NumberStage : MonoBehaviour
{
    public float speed = 0.1f, checSize = 6f;   //скорость увелечения и максимальный размер окно
    public Text textNumberStage;    // текстовое поле для вывода этапа

    public RectTransform pos;   // компонент для умправления позицией окна

    // функция срабатывающая при запуске скрипта
    private void Start()
    {
        StartCoroutine(ShowNumberStage(1));
    }

    // анимацыя отображения этапа 
    public IEnumerator ShowNumberStage(int numberStage)
    {
        textNumberStage.text = "Этап " + numberStage + " вперед";
        while (transform.localScale.x <= checSize)
        {
            yield return new WaitForSeconds(0f);
            transform.localScale = new Vector3(pos.localScale.x + speed, pos.localScale.y + speed, pos.localScale.z);

        }
        while (transform.localScale.x >= 5)
        {
            yield return new WaitForSeconds(0f);
            transform.localScale = new Vector3(pos.localScale.x - speed, pos.localScale.y - speed, pos.localScale.z);
        }
        yield return new WaitForSeconds(1f);
        while (transform.localScale.x >= 0)
        {
            yield return new WaitForSeconds(0f);
            transform.localScale = new Vector3(pos.localScale.x - speed * 2, pos.localScale.y - speed * 2, pos.localScale.z);
        }
    }
}