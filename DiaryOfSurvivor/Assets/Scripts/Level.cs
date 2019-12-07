using UnityEngine;

// КЛАСС ОПИСАНИЯ УРОВНЯ
public class Level : MonoBehaviour
{
    public int damageEnemy;         // урон врага
    public int maxEnemyhp; // текущий показатель здоровья и максимальный

    public Sprite spriteEnemy;      // спрайт врага
    public Sprite iconDamage;       // спрайт вражеской атаки 

    [TextArea]
    public string textLevel;        // описание уровня
    public Sprite imageLevel;       // спрайт локации

    public AudioClip damageAudio;   // звук вражеской атаки 
}