using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMaches : MonoBehaviour
{

    private Board board;
    private Dot dot;
    public List<GameObject> currenMatches = new List<GameObject>();
    public string Mode;



    // Use this for initialization
    void Start()
    {
        board = FindObjectOfType<Board>();

    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }


    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.heigth; j++)
            {
                GameObject currentDot = board.allDots[i, j];
                if (currentDot != null)
                {
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftDot = board.allDots[i - 1, j];
                        GameObject rightDot = board.allDots[i + 1, j];
                        if (leftDot != null && rightDot != null)
                        {
                            if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                            {

                                if (!currenMatches.Contains(leftDot))
                                {
                                    currenMatches.Add(leftDot);


                                }
                                leftDot.GetComponent<Dot>().isMatched = true;
                                if (!currenMatches.Contains(rightDot))
                                {
                                    currenMatches.Add(rightDot);

                                }
                                rightDot.GetComponent<Dot>().isMatched = true;
                                if (!currenMatches.Contains(currentDot))
                                {
                                    currenMatches.Add(currentDot);

                                }
                                currentDot.GetComponent<Dot>().isMatched = true;
                            }
                        }
                    }
                    if (j > 0 && j < board.heigth - 1)
                    {
                        GameObject upDot = board.allDots[i, j + 1];
                        GameObject downDot = board.allDots[i, j - 1];
                        if (upDot != null && downDot != null)
                        {
                            if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                            {

                                if (!currenMatches.Contains(upDot))
                                {
                                    currenMatches.Add(upDot);
                                }
                                upDot.GetComponent<Dot>().isMatched = true;
                                if (!currenMatches.Contains(downDot))
                                {
                                    currenMatches.Add(downDot);
                                }
                                downDot.GetComponent<Dot>().isMatched = true;
                                if (!currenMatches.Contains(currentDot))
                                {
                                    currenMatches.Add(currentDot);
                                }
                                currentDot.GetComponent<Dot>().isMatched = true;
                            }
                        }
                    }
                }
            }
        }
    }
}