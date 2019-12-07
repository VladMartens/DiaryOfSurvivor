using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    private int step = 0;
    public GameObject[] tutorial;

    void Start()
    {
        PlayerPrefs.SetInt("FirstEnter", 1);
    }

    public void Next()
    {
        if (step + 1 > tutorial.Length - 1)
        {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
            //Social.ReportProgress("CgkImMmJ_L4MEAIQAQ", 100.0f, (bool success) =>
            //{
            //    Debug.Log("achievent get");
            //});
        }

        else
        {          
            tutorial[step].SetActive(false);
            tutorial[++step].SetActive(true);
        }
    }

    public void OpenPolicy()
    {
        Application.OpenURL("https://diary-of-survivor.flycricket.io/privacy.html");
    }
}
