using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// КЛАСС ГЛАВНОГО МЕНЮ
public class StartMenu : MonoBehaviour
{
    int wave;   // переменная для хранения уровня

    public AnimationController animationController; //класс для управления анимациями

    public Text day, textLevel;  // текстовое поле вывода номера и описания уровня 
    public Image imageLevel;    // локация уровня

    public Level[] levels;  // массив уровней

    // функция срабатывающая при запуске скрипта
    void Start()
    {
      
        //PlayGamesPlatform.Activate();
        //Social.localUser.Authenticate((bool success) => {
        //    if (success)
        //        Debug.Log("Secces");
        //});

        int firstEnter = PlayerPrefs.GetInt("FirstEnter",0);

        if (firstEnter == 0)
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);

        wave = PlayerPrefs.GetInt("Wave", 0);

        if (wave > levels.Length - 1)
        {
            //Social.ReportProgress("CgkImMmJ_L4MEAIQBA", 100.0f, (bool success) =>
            //{
            //    Debug.Log("achievent get");
            //});

            SceneManager.LoadScene("FinalScene", LoadSceneMode.Single);
        }

        animationController.BlackoutOff();
        textLevel.text = levels[wave].textLevel;
        imageLevel.sprite = levels[wave].imageLevel;
        day.text = "Запись " + ++wave;

        
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
    }
}