using System.Collections;
using UnityEngine;

public class Dot : MonoBehaviour
{

    [Header("Board Vatiables")]
    public int column, row, previousColumn, previousRow, targetX, targetY;
    public bool isMatched = false;

    [Header("Params")]
    public bool damage = false;
    public bool doubledamage = false;
    public bool Heal = false;
    public bool doubleHeal = false;


    private FindMaches findMaches;
    private Board board;
    private GameObject otherDot;
    private Vector2 firstTouchPosition, tempPosition, secondTouchPositin;

    private  LevelMode level;
    private SurvivalMode survival;

    public bool move = true;


    public float swipeAngle = 0, swipeResist = 1f;

    void Start()
    {
        board = FindObjectOfType<Board>();
        findMaches = FindObjectOfType<FindMaches>();

        if (findMaches.Mode == "level")
            level = FindObjectOfType<LevelMode>();
        else if (findMaches.Mode == "survival")
            survival = FindObjectOfType<SurvivalMode>();

        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
        previousRow = row;
        previousColumn = column;

    }

    void Update()
    {
        FindMathces();
        targetX = column;
        targetY = row;
        //Установка позиций элементов по осе Х
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //Падение новых элементов
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
            }
            findMaches.FindAllMatches();
        }
        else
        {
            //Установка позиций
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;

        }
        //Установка позиций элементов по осе У
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //Падение новых элементов
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allDots[column, row] != this.gameObject)
                board.allDots[column, row] = this.gameObject;

            findMaches.FindAllMatches();

        }
        else
        {
            //Установка позиций
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
    }

    //Проверка на движение
    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.5f);
        if (otherDot != null)
        {
            //Возвращение элементов на старые позиции
            if (!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;
                row = previousRow;
                column = previousColumn;
                move = true;
                yield return new WaitForSeconds(.5f);

            }
            //Уничтожение элементов
            else
            {
                board.DestroyMatches();
                //Проверка на наличие необходимого параметра и вызов необходимых функций боя.
                if (damage)
                {
                    if (level != null)
                        level.Fight(1);
                    else
                        survival.Fight(1);
                }
                else if (doubledamage)
                {
                    if (level != null)
                        level.Fight(2);
                    else
                        survival.Fight(2);
                }
                else if (Heal)
                {
                    if (level != null)
                        level.Fight(3);
                    else
                        survival.Fight(3);
                }
                else if (doubleHeal)
                {
                    if (level != null)
                        level.Fight(4);
                    else
                        survival.Fight(4);
                }
            }
        }
    }

    //Стартовая позиция перетягивания
    private void OnMouseDown()
    {
        Swipe(1);
    }
    //Позиция отпускания 
    private void OnMouseUp()
    {
        Swipe(2);
    }

    private void Swipe(int action)
    {
        if (move == true)
        {
            if (action == 1)
                firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            else if (action == 2)
            {
                if (level != null)
                    level.Damage();
                else
                    survival.Damage();
                secondTouchPositin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CalculateAngle();
            }
        }
        else
            Update();
    }
    //Расчет передвижения
    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(secondTouchPositin.y - firstTouchPosition.y, 
            secondTouchPositin.x - firstTouchPosition.x) * 180 / Mathf.PI;
        MovePieces();
    }
    //Движение объекта
    void MovePieces()
    {
        move = false;
        if (swipeAngle > -45 && swipeAngle != 0 &&
            swipeAngle <= 45 && column < board.width - 1)
        {
            //Движение вправо
            otherDot = board.allDots[column + 1, row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().column -= 1;
            column += 1;
        }
        else if (swipeAngle > 45 && swipeAngle != 0 &&
            swipeAngle <= 135 && row < board.heigth - 1)
        {
            //Движение вверх
            otherDot = board.allDots[column, row + 1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
        }
        else if ((swipeAngle > 135 && swipeAngle != 0 || swipeAngle <= -135) && column > 0)
        {
            //Движение влево
            otherDot = board.allDots[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle != 0 && swipeAngle >= -135 && row > 0)
        {
            //Движение вниз
            otherDot = board.allDots[column, row - 1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMoveCo());
    }

    //Проверка на совпадения
    void FindMathces()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftDot1 = board.allDots[column - 1, row];
            GameObject rightDot1 = board.allDots[column + 1, row];
            //Проверка на наличии элементов слева и справа
            if (leftDot1 != null && rightDot1 != null)
            {
                //Проверка на совпадения слева и справа
                if (leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag)
                {
                    leftDot1.GetComponent<Dot>().isMatched = true;
                    rightDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
        if (row > 0 && row < board.heigth - 1)
        {
            GameObject upDot1 = board.allDots[column, row + 1];
            GameObject downDot1 = board.allDots[column, row - 1];
            //Проверка на наличии элементов снизу или сверху
            if (upDot1 != null && downDot1 != null)
            {
                //Проверка на совпадения сверху и снизу
                if (upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag)
                {
                    upDot1.GetComponent<Dot>().isMatched = true;
                    downDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }
}