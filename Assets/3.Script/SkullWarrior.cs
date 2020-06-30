using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWarrior : MonsterCommon
{
    // Start is called before the first frame update
    void Start()
    {
        monsterName = "SkullWarrior";
        Init();

        hp = 100;
        
    }

    // Update is called once per frame
    void Update()
    {
        Routine();
    }
}
