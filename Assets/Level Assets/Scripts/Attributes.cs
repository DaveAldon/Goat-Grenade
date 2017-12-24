using UnityEngine;

// Common attributes for critters. Inherit as needed into sheep, wolves, etc.

public abstract class Attributes : MonoBehaviour {

    public int health;

    public void Damage(int h) {
        health -= h;
        //Debug.Log("took " +health);
    }

    public void Heal(int h) {
        health += h;
    }
}
