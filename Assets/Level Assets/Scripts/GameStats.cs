using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameStats {
	
	// The number of sheep in the game.
    private static int numSheep = 0;

    private static int maxSheep = 0;

	// The number of wolves in the game.
	private static int numWolves = 1;

    public static void SheepSpawned()
    {
        if (numSheep < maxSheep) ++numSheep;
    }

	public static void SheepLost() {
        if(numSheep > 0) --numSheep;
	}

	public static int NumSheep() {
		return numSheep;
	}

    public static int MaxSheep()
    {
        return maxSheep;
    }

    public static void SetMaxSheep(int num)
    {
        maxSheep = num;
    }
}
