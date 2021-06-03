using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour{

    private static int counter;
    private static bool extremeDanger;
    
    [SerializeField] int numberOfEnemies;

    private void Update() {
        numberOfEnemies = counter;
        if(counter == 0){
            extremeDanger = false;
        }
    }

    public static void setExtremeDanger(){
        extremeDanger = true;
    }

    public static bool isExtremeDanger(){
        return extremeDanger;
    }

    public static bool isDanger(){
        return counter > 0;
    }

    public static int getcounter(){
        return counter;
    }

    public static void increaseEnemyCounter(){
        counter++;
    }

    public static void decreaseEnemyCounter(){
        counter--;
    }
}
