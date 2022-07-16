using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide
{
    private static string spritePath = "Sprites/DiceSides/";

    public string name;
    public Sprite sprite;
    public string description;

    public DiceSide(string name, string description) {
        this.name = name;
        this.sprite = Resources.Load<Sprite>(spritePath + name);
        this.description = description;
	}
}
