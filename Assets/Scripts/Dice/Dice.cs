using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private static float rollInterval = 0.1f;
    private static float rollAmount = 10;

    private List<ModifiersData> sides;
    private SpriteRenderer spriteRenderer;

    private ModifiersData currentSide;

    private bool isRolling;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        sides = new List<ModifiersData>();
        for (int i = 0; i < 6; ++i) {
            if (i % 2 == 0)
                sides.Add(Resources.Load<ModifiersData>("Scriptqble/PlayerAttackSpeed"));
            else
                sides.Add(Resources.Load<ModifiersData>("Scriptqble/TestModifier"));
		}
        isRolling = false;
        Roll();
    }

    /// <summary>
    /// Overrides one of the 6 dice sides to a new one
    /// </summary>
    /// <param name="index">The side to override</param>
    /// <param name="side">The new side</param>
    public void SetSide(int index, ModifiersData side) {
        sides[index] = side;
	}

	public void Roll() {
        StartCoroutine(RollDiceCoroutine());
	}

    private IEnumerator RollDiceCoroutine() {
        isRolling = true;
        for (int i = 0; i < rollAmount; ++i) {
            currentSide = sides[Random.Range(1, 6)];
            spriteRenderer.sprite = currentSide.modifierVisual;
            yield return new WaitForSeconds(rollInterval);
        }
        ModifierManager.Instance.AddModifier(currentSide);
        Debug.Log("DICE RESULT IS : " + currentSide.modifierDisplayName);
        isRolling = false;
	}

    public bool GetIsRolling() {
        return isRolling;
	}
}
