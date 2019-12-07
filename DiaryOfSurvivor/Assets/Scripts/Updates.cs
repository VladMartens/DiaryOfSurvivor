using UnityEngine;
using UnityEngine.UI;

//КЛАСС ДЛЯ УЛУЧШЕНИЙ
public class Updates : MonoBehaviour
{
    public Text countHpText,    //количесвто копонентов
        countDamageText,
        countHealsText,
        levelHpText,        //уровень прокачки
        levelDamageText,
        levelHealsText,
        needHpText,     //количество необходимых элементов  для улучшения
        needDamageText,
        needHealsText;

    private int countHp,    //количесвто копонентов
        countDamage,
        countHeals,
        levelHp,        //уровень прокачки
        levelDamage,
        levelHeals,
        needHp,     //количество необходимых элементов  для улучшения
        needDamage,
        needHeals;



    // функция срабатывающая при запуске скрипта
    private void Start()
    {
        countHp = PlayerPrefs.GetInt("CountHp", 0);
        countDamage = PlayerPrefs.GetInt("CountDamage", 0);
        countHeals = PlayerPrefs.GetInt("CountHeal", 0);
        levelHp = PlayerPrefs.GetInt("LevelHp", 1);
        levelDamage = PlayerPrefs.GetInt("LevelDamage", 1);
        levelHeals = PlayerPrefs.GetInt("LevelHeal", 1);
        needHp = (10 * levelHp)+ (10 * levelHp) / 2;
        needDamage = (10 * levelDamage) + (10 * levelDamage) / 2;
        needHeals = (10 * levelHeals) + (10 * levelHeals) / 2;

        countHpText.text = countHp.ToString();
        countDamageText.text = countDamage.ToString();
        countHealsText.text = countHeals.ToString();
        levelHpText.text = levelHp.ToString();
        levelDamageText.text = levelDamage.ToString();
        levelHealsText.text = levelHeals.ToString();
        needHpText.text = needHp.ToString();
        needDamageText.text = needDamage.ToString();
        needHealsText.text = needHeals.ToString();
    }

    // функция срабатывающая для увеличения урона
    public bool DamageUp()
    {
        if (countDamage > needDamage)
        {
            countDamageText.text = string.Format("{0}", countDamage -= needDamage);
            levelDamageText.text = string.Format("{0}", ++levelDamage);
            needDamage = (10 * levelDamage) + (10 * levelDamage) / 2;
            needDamageText.text = needDamage.ToString();

            //if(levelDamage==5)
            //    Social.ReportProgress("CgkImMmJ_L4MEAIQBg", 100.0f, (bool success) =>
            //    { Debug.Log("achievent get"); });
          
            PlayerPrefs.SetInt("CountDamage", countDamage);
            PlayerPrefs.SetInt("LevelDamage", levelDamage);
            PlayerPrefs.Save();
            return true;
        }
        else
            return false;
       
    }

    // функция срабатывающая для увеличения жизней
    public bool HpUp()
    {
        if (countHp > needHp)
        {
            countHpText.text = string.Format("{0}", countHp -= needHp);
            levelHpText.text = string.Format("{0}", ++levelHp);
            needHp = (10 * levelHp) + (10 * levelHp) / 2 ;
            needHpText.text = needHp.ToString();

            //if (levelHp == 5)
            //    Social.ReportProgress("CgkImMmJ_L4MEAIQBQ", 100.0f, (bool success) =>
            //    { Debug.Log("achievent get"); });

            PlayerPrefs.SetInt("CountHp", countHp);
            PlayerPrefs.SetInt("LevelHp", levelHp);
            PlayerPrefs.Save();
            return true;
        }
        else
            return false;
    }

    // функция срабатывающая для усиления эфективности лечения
    public bool HealsUp()
    {
        if (countHeals > needHeals)
        {
            countHealsText.text = string.Format("{0}", countHeals -= needHeals);
            levelHealsText.text = string.Format("{0}", ++levelHeals);
            needHeals = (10 * levelHeals) + (10 * levelHeals) / 2;
            needHealsText.text = needHeals.ToString();

            //if (levelHeals == 5)
            //    Social.ReportProgress("CgkImMmJ_L4MEAIQBw", 100.0f, (bool success) =>
            //    { Debug.Log("achievent get"); });

            PlayerPrefs.SetInt("CountHeal", countHeals);
            PlayerPrefs.SetInt("LevelHeal", levelHeals);
            PlayerPrefs.Save();
            return true;
        }
        else
            return false;
    }
}