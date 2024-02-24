using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atribuildhandle
{
    int Hp;
    int Attack;
    public int Curenhp;
    public Atribuildhandle(GameData gameData)
    {
        Hp = gameData.Hp;
        Attack = gameData.Attack;
    }
   public int Init()
    {
        return Curenhp = Hp;
    }
    public int  GetDame() => Attack;
    public void TakenDame(int Atk)
    {
        Curenhp -= Atk;
    }
}
