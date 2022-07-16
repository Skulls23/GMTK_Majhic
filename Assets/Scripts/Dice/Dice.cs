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
    }

    /// <summary>
    /// Overrides one of the 6 dice sides to a new one
    /// </summary>
    /// <param name="index">The side to override</param>
    /// <param name="side">The new side</param>
    public void SetSide(int index, ModifiersData side) {
        sides[index] = side;
	}

	public IEnumerator Roll() {
        for (int i = 0; i < rollAmount; ++i) {
            SetActiveSide(Random.Range(0, 6));
            yield return new WaitForSeconds(rollInterval);
        }
	}

    private void SetActiveSide(int index) {
        currentSide = sides[index];
        spriteRenderer.sprite = currentSide.modifierVisual;
    }

    public void Show () {
        spriteRenderer.enabled = true;
	}

    public void Hide() {
        spriteRenderer.enabled = false;
	}

    public ModifiersData GetCurrentSide() {
        return currentSide;
	}

    public List<ModifiersData> GetAllSides()
    {
        return sides;
    }

    public ModifiersData GetSide(int i)
    {
        return sides[i];
    }
}
