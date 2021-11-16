using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Level
{
    public int experience;
    public int currentLevel;
    public int MAX_EXP;
    public int maxLevel = 100;

    public Level(int level)
    {
        MAX_EXP = GetXPForLevel(maxLevel);
        currentLevel = level;
        experience = GetXPForLevel(level);    }

    public int GetXPForLevel(int level)
    {
        if (level > maxLevel)
            return 0;
        int firstPass = 0;
        int secondPass = 0;
        for(int i=1; i<level; i++)
        {
            firstPass += (int)Math.Floor(i + (300.0f + Math.Pow(2.0f, i / 5.5f)));
            secondPass = firstPass / 4;
        }

        if (secondPass > MAX_EXP && MAX_EXP != 0)
            return MAX_EXP;
        if (secondPass < 0)
            return MAX_EXP;

        return secondPass;
    }

    public int GetLevelForXP(int exp)
    {
        if (exp > MAX_EXP)
            return MAX_EXP;

        int firstPass = 0;
        int secondPass = 0;

        for (int i = 1; i <= maxLevel; i++)
        {
            firstPass += (int)Math.Floor(i + (300.0f + Math.Pow(2.0f, i / 5.5f)));
            secondPass = firstPass / 4;
            if (secondPass > exp)
                return i;
        }
        if (exp > secondPass)
            return maxLevel;
        return 0;
    }


    public bool AddExp(int amount)
    {
        if(amount + experience < 0 || experience > MAX_EXP)
        {
            if (experience > MAX_EXP)
                experience = MAX_EXP;
            return false;
        }
        int oldLevel = GetLevelForXP(experience);
        experience += amount;
        if(oldLevel < GetLevelForXP(experience))
        {
            if(currentLevel < GetLevelForXP(experience))
            {
                currentLevel = GetLevelForXP(experience);
                return true;
            }
        }
        return false;
    }
}
