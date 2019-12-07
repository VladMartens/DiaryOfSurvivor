using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    
    public int width;
    public int heigth;
    public string gameMode;
    public GameObject tilePrifab;
    public GameObject[] dots;
    public GameObject[,] allDots;
    private FindMaches findMaches;
    private LevelMode levelMode;
    private SurvivalMode survivalMode;


    // Use this for initialization
    void Start()
    {
        findMaches = FindObjectOfType<FindMaches>();
        allDots = new GameObject[width, heigth];

        gameMode = findMaches.Mode;
        if (gameMode == "level")
            levelMode = FindObjectOfType<LevelMode>();
        else
            survivalMode = FindObjectOfType<SurvivalMode>();

        SetUp();
    }

    //Заполнение доски
    void SetUp()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < heigth; j++)
            {
                //Заполнение фоновыми изображениями
                Vector2 tempPosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrifab, 
                    tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "(" + i + "," + j + ")";

                //Заполнение элементами
                int dotToUse = Random.Range(0, dots.Length);
                int maxIterations = 0;
				//Заполнение доски без совпадения
                while (MatchesAt(i, j, dots[dotToUse]) && maxIterations < 100)
                {
                    dotToUse = Random.Range(0, dots.Length);
                    maxIterations++;                    
                }
                maxIterations = 0;
				//Установка параметров для элементов
                GameObject dot = Instantiate(dots[dotToUse], 
                    tempPosition, Quaternion.identity);
                dot.GetComponent<Dot>().row = j;
                dot.GetComponent<Dot>().column = i;
                dot.transform.parent = this.transform;
                dot.name = "( " + i + ", " + j + " )";
                allDots[i, j] = dot;
            }
        }
    }

    //Проверка на совпадения
    private bool MatchesAt(int column, int row, GameObject piece)
    {
        if (column > 1 && row > 1)
        {
            if (allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag)
            {
                return true;
            }
            if (allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag)
            {
                return true;
            }
        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allDots[column, row - 1].tag == piece.tag && allDots[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if (allDots[column - 1, row].tag == piece.tag && allDots[column - 2, row].tag == piece.tag)
                {
                    return true;
                }
            }
        }
        
        return false;
    }

	//Уничтожение элементов
    private void DestroyMatchesAt(int colunm, int row)
    {
        if (allDots[colunm, row].GetComponent<Dot>().isMatched)
        {           
            if (gameMode == "level")
            {
                if (allDots[colunm, row].tag == "Axe")
                    levelMode.LittleDamage(false);
                else if (allDots[colunm, row].tag == "Gun")
                    levelMode.LittleDamage(true);
                else if (allDots[colunm, row].tag == "Food" || 
                    allDots[colunm, row].tag == "Tomato")
                    levelMode.LittleHealing(false);
                else if (allDots[colunm, row].tag == "Medicine")
                    levelMode.LittleHealing(true);
            }
            else
            {
                if (allDots[colunm, row].tag == "Axe")
                    survivalMode.LittleDamage(false);
                else if (allDots[colunm, row].tag == "Gun")
                    survivalMode.LittleDamage(true);
                else if (allDots[colunm, row].tag == "Food" ||
                    allDots[colunm, row].tag == "Tomato")
                    survivalMode.LittleHealing(false);
                else if (allDots[colunm, row].tag == "Medicine")
                    survivalMode.LittleHealing(true);
            }

            findMaches.currenMatches.Remove(allDots[colunm, row]);
            Destroy(allDots[colunm, row]);
            allDots[colunm, row] = null;
        }
    }
	//Проверка на наличие элементов для уничтожения
    public void DestroyMatches()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < heigth; j++)
            {
                if (allDots[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }
            }
        }        
        StartCoroutine(DecreaseRowCo());
    }
	
    private IEnumerator DecreaseRowCo()
    {
        int nullCount = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < heigth; j++)
            {
                if (allDots[i, j] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    allDots[i, j].GetComponent<Dot>().row -= nullCount;
                    allDots[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCo());
    }
	//Добавление новых элементов
    private void RefilBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < heigth; j++)
            {
                if(allDots[i,j] == null)
                {
                    Vector2 tempPosition = new Vector2(i,j);
                    int dotToUse = Random.Range(0, dots.Length);
                    GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                    allDots[i, j] = piece;
                }
            }
        }
    }
	//Проверка на наличие совпадений на доске. 
    private bool MatchesOnBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < heigth; j++)
            {
                if (allDots[i,j] != null)
                {
                    if(allDots[i,j].GetComponent<Dot>().isMatched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
	//Заполнение доски и уничтожение совпадений
    private IEnumerator FillBoardCo()
    {
        RefilBoard();
        yield return new WaitForSeconds(.5f);

        while (MatchesOnBoard())
        {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
    }
}