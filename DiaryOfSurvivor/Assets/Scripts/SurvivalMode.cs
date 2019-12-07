using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// КЛАСС УПРАВЛЯЮЩИЙ выживанием
public class SurvivalMode : MonoBehaviour
{
    int Wave = 0;   // текющая волна

    public MainHero hero;           //класс главного героя
    public LoseMenu loseMenu;       //класс меню проигрыша
    public NumberStage numberStage; //класс номера волны

    public Image healsBar,  // шкала здоровья       
        damageToEnemy;  //отображение спрайта атаки героя

    public Text enemyHpText,    // текстовое поле для отображения состояния жизней
        damageEnemyText;

    public Survival[] survivals;    //массив волн

    private int enemyHp,    //текущий показатель жизней
        heroHp,         // текущий показатель жизни героя
        heroMaxHp,      // максимальный запас жизней
        heroDamage,     // урон героя
        heroHeal;       // эфективность лечения

    private bool win = true;
    public AudioSource backgraundeMusic;
    private AudioSource enemyAudio;
    private Vector3 startPosEnemyHp, startPosHeroHp;

    // функция срабатывающая при запуске скрипта
    void Start()
    {
        startPosEnemyHp = damageEnemyText.transform.position;
        startPosHeroHp = hero.damageHeroText.transform.position;
        float lvlhp = PlayerPrefs.GetInt("LevelHp", 1);
        heroHp = (int)(hero.hp * (lvlhp / 10 + 1));
        heroMaxHp = heroHp;

        float lvlDamage = PlayerPrefs.GetInt("LevelDamage", 1);
        heroDamage = (int)(hero.damage * (lvlDamage / 10 + 1));

        float lvlHeal = PlayerPrefs.GetInt("LevelHeal", 1);
        heroHeal = (int)(hero.heal * (lvlHeal / 10 + 1));

        GetComponent<Image>().sprite = survivals[Wave].spriteEnemy;
        enemyHp = survivals[Wave].maxEnemyhp;
        SetHp();
        int rand = Random.Range(0, hero.backgraundeMusic.Length-1);
        backgraundeMusic.clip = hero.backgraundeMusic[rand];
        backgraundeMusic.Play();
        enemyAudio = GetComponent<AudioSource>();
    }

    // функция боя
    public void Fight(int action)
    {
        //если собралась комбинация из топором то наносится обычный урон
        if (action == 1)
        {
            int random = Random.Range(0, hero.axe.Length-1);
            hero.audioSourse.clip = hero.axe[random];
            hero.audioSourse.Play();
            enemyHp -= heroDamage;
            StartCoroutine(AnimationDamagetoEnemy(hero.axeDamage, heroDamage));
        }
        //если собралась комбинация из ружья то наносится двойной урон
        else if (action == 2)
        {
            int random = Random.Range(0, hero.shot.Length-1);
            hero.audioSourse.clip = hero.shot[random];
            hero.audioSourse.Play();
            enemyHp -= (heroDamage * 2);
            StartCoroutine(AnimationDamagetoEnemy(hero.gunDamage, heroDamage * 2));
        }

        SetHp();    //отображения очков здоровья
        //проверка на выигрышь
        if (enemyHp <= 0 && win==true)
        {
            Wave++;
            StartCoroutine(numberStage.ShowNumberStage(Wave + 1));
            enemyHp = survivals[Wave].maxEnemyhp;
            SetHp();
            GetComponent<Image>().sprite = survivals[Wave].spriteEnemy;
        }
        //если собралась комбинация из большой аптечки то герой лечится вдвойне
        else
        {
            if (action == 3)
                NormalHeal();
            else if (action == 4)
                DoubleHeal();
            SetHp();
        }
    }

    // функцыя лечения
    private void NormalHeal()
    {
        heroHp += heroHeal;
        if (heroHp >= heroMaxHp)
            heroHp = heroMaxHp;
        StartCoroutine(hero.AnimationHealHero(heroHeal));
    }
    // функцыя двойного лечения
    private void DoubleHeal()
    {
        hero.audioSourse.clip = hero.medkit;
        hero.audioSourse.Play();
        heroHp += heroHeal*2;
        if (heroHp >= heroMaxHp)
            heroHp = heroMaxHp;
        StartCoroutine(hero.AnimationHealHero(heroHeal * 2));
    }

    // функцыя нанесения урона герою
    public void Damage()
    {
        enemyAudio.clip = survivals[Wave].damageAudio;
        enemyAudio.Play();
        heroHp -= survivals[Wave].damageEnemy;
        StartCoroutine(AnimationDamagetoHero());
        SetHp();
        if (heroHp <= 0)
        {
            win = false;
            StartCoroutine(loseMenu.ShowLoseBoard(Wave + 1));
        }
    }

    public void LittleDamage(bool doubleDamage)
    {
        if (doubleDamage == false)
        {
            int random = Random.Range(0, hero.axe.Length - 1);
            hero.audioSourse.clip = hero.axe[random];
            hero.audioSourse.Play();
            enemyHp -= heroDamage / 10;
            StartCoroutine(AnimationDamagetoEnemy(hero.axeDamage, heroDamage / 10));
        }
        else
        {
            int random = Random.Range(0, hero.shot.Length - 1);
            hero.audioSourse.clip = hero.shot[random];
            hero.audioSourse.Play();
            enemyHp -= heroDamage / 5;
            StartCoroutine(AnimationDamagetoEnemy(hero.axeDamage, heroDamage / 5));
        }
        if (enemyHp <= 0 && win == true)
        {
            Wave++;
            StartCoroutine(numberStage.ShowNumberStage(Wave + 1));
            enemyHp = survivals[Wave].maxEnemyhp;
            GetComponent<Image>().sprite = survivals[Wave].spriteEnemy;
        }
        SetHp();
    }
    public void LittleHealing(bool doubleHealing)
    {
        if (doubleHealing == false)
        {
            heroHp += heroHeal / 5;
            if (heroHp >= heroMaxHp)
                heroHp = heroMaxHp;
            StartCoroutine(hero.AnimationHealHero(heroHeal / 5));
        }
        else
        {
            hero.audioSourse.clip = hero.medkit;
            hero.audioSourse.Play();
            heroHp += heroHeal / 2;
            if (heroHp >= heroMaxHp)
                heroHp = heroMaxHp;
            StartCoroutine(hero.AnimationHealHero(heroHeal / 2));
        }
        SetHp();
    }

    // функцыя отображения очков здоровья
    private void SetHp()
    {
        hero.hpText.text = heroHp.ToString();
        enemyHpText.text = enemyHp.ToString();

        if (heroHp >= 0)
            hero.healsBar.fillAmount = float.Parse(heroHp.ToString()) / float.Parse(heroMaxHp.ToString());
        else
            hero.healsBar.fillAmount = 0;

        if (enemyHp >= 0)
            healsBar.fillAmount = float.Parse(enemyHp.ToString()) / float.Parse(survivals[Wave].maxEnemyhp.ToString());
        else
            healsBar.fillAmount = 0;
    }

    // анимация нанесения урона врагу
    IEnumerator AnimationDamagetoEnemy(Sprite typeDamage, int damage)
    {
        damageToEnemy.sprite = typeDamage;
        damageEnemyText.text = "-" + damage;
        for (float f = 1; f >= 0; f -= 0.01f)
        {
            damageEnemyText.transform.position = new Vector3(damageEnemyText.transform.position.x,
                damageEnemyText.transform.position.y + 0.005f, damageEnemyText.transform.position.z);
            damageEnemyText.color = new Color(255, 0, 0, f);
            damageToEnemy.color = new Color(255, 255, 255, f);
            yield return null;
        }
        damageEnemyText.transform.position = startPosEnemyHp;
    }
    // анимация нанесения урона герою
    IEnumerator AnimationDamagetoHero()
    {
        hero.damageToHero.sprite = survivals[Wave].iconDamage;
        hero.damageHeroText.text = "-" + survivals[Wave].damageEnemy;
        for (float f = 1; f >= 0; f -= 0.01f)
        {
            hero.damageHeroText.transform.position = new Vector3(hero.damageHeroText.transform.position.x,
                hero.damageHeroText.transform.position.y + 0.01f, hero.damageHeroText.transform.position.z);
            hero.damageHeroText.color = new Color(255, 0, 0, f);
            hero.damageToHero.color = new Color(255, 255, 255, f);
            yield return null;
        }
        hero.damageHeroText.transform.position = startPosHeroHp;
    }
}