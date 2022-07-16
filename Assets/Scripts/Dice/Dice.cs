using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    private static float rollInterval = 0.1f;
    private static float rollAmount = 10;

    private List<ModifiersData> sides;
    private Image spriteRenderer;
    private Text description;

    private ModifiersData currentSide;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.Find("Image").GetComponent<Image>();
        description = transform.Find("Description").GetComponent<Text>();

        Hide();

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
        spriteRenderer.enabled = true;
        for (int i = 0; i < rollAmount; ++i) {
            SetActiveSide(Random.Range(0, 6));
            yield return new WaitForSeconds(rollInterval);
        }
        description.enabled = true;
	}

    private void SetActiveSide(int index) {
        currentSide = sides[index];
        spriteRenderer.sprite = currentSide.modifierVisual;
        description.text = currentSide.name;
    }

    public void Hide() {
        spriteRenderer.enabled = false;
        description.enabled = false;
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
