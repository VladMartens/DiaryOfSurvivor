using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// КЛАСС ГЛАВНОГО ГЕРОЯ
public class MainHero : MonoBehaviour
{
    public int damage = 50; // стартовые значения урона 
    public int hp = 200;    // стартовые значения жизней 
    public int heal = 20;   // стартовое значение эфективности лечения

    public Text hpText,     // текстовое поле для отображения состояния жизней
        healingHeroText,    // текстовое поле для отображения лечения
        damageHeroText;     // текстовое поле для отображения полученного урона

    public Image damageToHero,   // отображение спрайта вражеской атаки
        healsBar;           // шкала здоровья 

    public Sprite axeDamage,    // спрайт для отображения урона топором 
        gunDamage;          // спрайт для отображения урона обрезом 

    public AnimationController animationController; //класс для управления анимациями

    public AudioSource audioSourse;

    public AudioClip[] axe;     // аудио эфекты удара топором
    public AudioClip[] shot;    // аудио эфекты выстрела
    public AudioClip[] backgraundeMusic;    // фоновая музыка
    public AudioClip medkit;    // аудио эфект аптечки
    

    // анимация лечения главного героя
    public IEnumerator AnimationHealHero(int healing)
    {
        healingHeroText.text = "+" + healing;
        for (float f = 1; f >= 0; f -= 0.01f)
        {
            healingHeroText.color = new Color(0, 255, 0, f);
            yield return null;
        }
    }
}