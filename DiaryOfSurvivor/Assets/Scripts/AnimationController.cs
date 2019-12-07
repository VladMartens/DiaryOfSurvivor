using UnityEngine;
using UnityEngine.SceneManagement;

// КЛАСС КОНТРОЛЛЕР АНИМАЦЫЯМИ
public class AnimationController : MonoBehaviour
{
    public Updates up;
    public Transform loading;

    // запуск анимации "осветление экрана"
    public void BlackoutOff()
    {
        GetComponent<Animation>().Play("BlackoutOff");
    }

    // запуск анимации "затемнение экрана" и старт сцены уровня
    public void StartLevel()
    {
        GetComponent<Animation>().Play("BlackoutLevel");
    }
    // запуск анимации "затемнение экрана" и старт сцены улучшения
    public void StartUpdates()
    {
        GetComponent<Animation>().Play("BlackoutUpdates");
    }
    // запуск анимации "затемнение экрана" и старт сцены выживания
    public void StartSurvival()
    {
        GetComponent<Animation>().Play("BlackoutSurvival");
    }
    // запуск анимации "затемнение экрана" и старт сцены с ящиками
    public void StartLootBox()
    {
        GetComponent<Animation>().Play("BlackoutLootBox");
    }
    // запуск анимации "затемнение экрана" и старт сцены главного меню
    public void StartMenu()
    {
        GetComponent<Animation>().Play("BlackoutMenu");
    }

    // запуск анимации "затемнение экрана" и рестарт выживания
    public void Reload()
    {
        GetComponent<Animation>().Play("Reload");
    }

    // функцыя старта сцены по имени
    public void loadScene(string nameScene)
    {
        SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Single);
    }

    // запуск анимации улучшение жизней
    public void LevelUpHp()
    {
        if (up.HpUp())
            GetComponent<Animation>().Play("LevelUpHp");
    }
    // запуск анимации улучшение урона
    public void LevelUpDamage()
    {
        if (up.DamageUp())
            GetComponent<Animation>().Play("LevelUpDamage");
    }
    // запуск анимации улучшение эффективности  лечения
    public void LevelUpHeals()
    {
        if (up.HealsUp())
            GetComponent<Animation>().Play("LevelUpHeals");
    }

    // запуск анимации открытия ящика
    public void OpenBox()
    {
        GetComponent<Animation>().Play("OpenBox");
    }
    // запуск анимации открытия блокнота 
    public void OpenBook()
    {
        GetComponent<Animation>().Play("OpenBook");
    }

    public void Loading()
    {
        GetComponent<Animation>().Play("Loading");
    }

    public void GameRestart()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        loadScene("MainScene");
    }
}