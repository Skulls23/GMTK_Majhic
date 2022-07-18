using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is the UI script for the health.
/// It has to be on a UI frame
/// It is called by the HealthManager inside the Jeune Celte
/// </summary>
public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Texture2D  heart;
    [SerializeField] private int        imageXSize;
    [SerializeField] private int        imageYSize;

    private float xSpace;
    private float yPos;

    private List<GameObject> imageList;

    private void Update()
    {
        RefreshUI();
    }

    public List<GameObject> GetImageList()
    {
        return imageList;
    }

    public GameObject GetHeartImage(int num)
    {
        return imageList[num];
    }

    /// <summary>
    /// Will recreate each heart box at each change.
    /// </summary>
    public void RefreshUI()
    {
        for(int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);

        xSpace = imageXSize;
        yPos = transform.position.y;

        imageList = new List<GameObject>();

        for (int i = 0; i < player.GetComponent<CharacterHealth>().Health; i++)
        {
            imageList.Add(new GameObject("Container " + i));

            RectTransform trans = imageList[i].AddComponent<RectTransform>();
            trans.sizeDelta = new Vector2(imageXSize, imageYSize);
            trans.anchoredPosition = new Vector2(0.5f, 0.5f);
            trans.localPosition = new Vector3((i * xSpace), 0, 0);
            trans.position = new Vector3(imageXSize / 2 + (i * xSpace), yPos * 2 - imageYSize / 2, 0);

            Image image = imageList[i].AddComponent<Image>();
            image.sprite = Sprite.Create(heart, new Rect(0, 0, heart.width, heart.height), new Vector2(0.5f, 0.5f));

            imageList[i].transform.SetParent(transform);
        }
    }
}
