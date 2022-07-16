using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private static float rollInterval = 0.1f;
    private static float rollAmount = 10;

    private List<DiceSide> sides;
    private SpriteRenderer spriteRenderer;

    private DiceSide currentSide;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        sides = new List<DiceSide>();
        for (int i = 0; i < 6; ++i) {
            sides.Add(new DiceSide("DiceSideBlank", "Blank", "NO DESCRIPTION"));
		}

        for(int i = 0; i < 6; ++i) {
            SetSide(i, new DiceSide("DiceSide" + (i + 1), (i+1).ToString(), "NO DESCRIPTION"));
		}
        Roll();
    }

    /// <summary>
    /// Overrides one of the 6 dice sides to a new one
    /// </summary>
    /// <param name="index">The side to override</param>
    /// <param name="side">The new side</param>
    public void SetSide(int index, DiceSide side) {
        sides[index] = side;
	}

	void Roll() {
        StartCoroutine(RollDiceCoroutine());
	}

    private IEnumerator RollDiceCoroutine() {
        for (int i = 0; i < rollAmount; ++i) {
            currentSide = sides[Random.Range(1, 6)];
            spriteRenderer.sprite = currentSide.sprite;
            yield return new WaitForSeconds(rollInterval);
        }
        Debug.Log("DICE RESULT IS : " + currentSide.name);
	}
}
