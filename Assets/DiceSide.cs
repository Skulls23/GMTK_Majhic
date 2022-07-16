using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide
{
    private static string spritePath = "Sprites/DiceSides/";

    public string ID;
    public string name;
    public Sprite sprite;
    public string description;

    public DiceSide(string ID, string name, string description) {
        this.ID = ID;
        this.name = name;
        this.sprite = Resources.Load<Sprite>(spritePath + ID);
        this.description = description;
	}
}
