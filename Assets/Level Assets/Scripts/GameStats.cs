using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameStats {
	
	// The number of sheep in the game.
	private static int numSheep = 20;
	// The number of wolves in the game.
	private static int numWolves = 1;

	public static void SheepLost() {
		--numSheep;
	}

	public static int NumSheep() {
		return numSheep;
	}
}
