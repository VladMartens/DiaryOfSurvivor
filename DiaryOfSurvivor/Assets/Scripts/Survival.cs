using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// КЛАСС ОПИСАНИЯ ВОЛНЫЙ В РЕЖИМЕ ВЫЖИВАНИЯ
public class Survival : MonoBehaviour
{
    public int damageEnemy; //урон врага
    public int maxEnemyhp; // текущий показатель здоровья и максимальный

    public Sprite spriteEnemy;  // спрайт врага
    public Sprite iconDamage;   // спрайт вражеской атаки 

    public AudioClip damageAudio;   // звук вражеской атаки 
}