using UnityEngine;
using UnityEngine.UI;


// КЛАСС ОТКРЫТИЯ ЯЩИКОВ
public class LootBox : MonoBehaviour
{

    public Text addDamage, // количество ресурсов которые выпали из ящика
        addHeals,
        addHp,
        countBoxText,   // количество ящиков
        countDamageText,// количество элементов урона
        countHealsText, // количество элементов лечения
        countHpText,    // количество элементов жизней
        lookAdsText;    // количество просмотров рекламы

    public AnimationController animationController; // класс для управления анимациями

    private int countBox,
        countHp,    //количесвто копонентов
        countDamage,
        countHeals,
        needLookAds;
    

    // функция срабатывающая при запуске скрипта
    private void Start()
    {
        countHp = PlayerPrefs.GetInt("CountHp", 0);
        countDamage = PlayerPrefs.GetInt("CountDamage", 0);
        countHeals = PlayerPrefs.GetInt("CountHeal", 0);
        countBox = PlayerPrefs.GetInt("CountBox", 0);
        needLookAds = PlayerPrefs.GetInt("NeedLookAds", 10);

        countBoxText.text = countBox.ToString();
        countDamageText.text = countDamage.ToString();
        countHealsText.text = countHeals.ToString();
        countHpText.text = countHp.ToString();
        lookAdsText.text = "Смотри и получи\n\rосталось " + needLookAds + " раз";
    }

    //функция срабатывающая при открытии ящика
    public void OpenBox()
    {
        
        if (int.Parse(countBoxText.text) > 0)
        {
            PlayerPrefs.SetInt("CountBox",--countBox);

            int randDamage = (int)RandomRangeExponential(1, 100, 4.5f, Direction_e.Left);
            int randHeals = (int)RandomRangeExponential(1, 100, 4.5f, Direction_e.Left);
            int randHp = (int)RandomRangeExponential(1, 100, 4.5f, Direction_e.Left);
          
                
            addDamage.text = "+" + randDamage.ToString();
            addHeals.text = "+" + randHeals.ToString();
            addHp.text = "+" + randHp.ToString();

            countDamage += randDamage;
            countHeals += randHeals;
            countHp += randHp;
            
            PlayerPrefs.SetInt("CountDamage", countDamage);
            PlayerPrefs.SetInt("CountHp", countHp);
            PlayerPrefs.SetInt("CountHeal", countHeals);
            PlayerPrefs.Save();

            countBoxText.text = countBox.ToString();
            countDamageText.text = countDamage.ToString();
            countHealsText.text = countHeals.ToString();
            countHpText.text = countHp.ToString();

            animationController.OpenBox();
        }
    }

    // просмотр рекламы за сундук
    public void LookAds()
    {

            //--needLookAds;
    
        //if (needLookAds == 0)
        //{
        //    countBox += 5;
        //    countBoxText.text = countBox.ToString();
        //    needLookAds = 10;
        //}

        //PlayerPrefs.SetInt("NeedLookAds", needLookAds);
        //PlayerPrefs.SetInt("CountBox", countBox);
        //PlayerPrefs.Save();
        //lookAdsText.text = "Смотри и получи\n\rосталось " + needLookAds + " раз";


    }

    public enum Direction_e { Right, Left };


    // Случайный экспоненциальный диапазон
    public static float RandomRangeExponential(float min, float max, float exponent, Direction_e direction)
    {
        return min + RandomFromExponentialDistribution(exponent, direction) * (max - min);
    }

    // Случайное из экспоненциального распределения
    public static float RandomFromExponentialDistribution(float exponent, Direction_e direction)
    {
        float max_cdf = ExponentialRightCDF(1.0f, exponent);

        float u = UnityEngine.Random.Range(0.0f, max_cdf);
        float x_val = EponentialRightInverseCDF(u, exponent);

        if (direction == Direction_e.Left)
        {
            x_val = 1.0f - x_val;
        }

        return x_val;
    }

    // Интеграл кривой 
    private static float ExponentialRightCDF(float x, float exponent)
    {
        float integral_exp = exponent + 1.0f;
        return (Mathf.Pow(x, integral_exp)) / integral_exp;
    }

    // Обратный интеграл от кривой степени.
    private static float EponentialRightInverseCDF(float x, float exponent)
    {
        float integral_exp = exponent + 1.0f;
        return Mathf.Pow(integral_exp * x, 1.0f / integral_exp);
    }
}