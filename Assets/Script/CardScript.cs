using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Card card;
    private Player me;
    private SingleGame game;
    public GameObject cardPanel;
    private GameObject instanceCardPanel;
    private string Id;

    public void ShowPanel(Vector3 pointer)
    {
        instanceCardPanel.transform.position = pointer;
        instanceCardPanel.SetActive(true);
    }

    public void SetAttribute(SingleGame game, Card card)
    {
        this.game = game;
        this.card = card;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/" + card.cardName);
        instanceCardPanel = Instantiate(cardPanel, transform);
        instanceCardPanel.SetActive(false);

        this.GetComponent<Button>().onClick.AddListener(game.ClickCard);

        this.Id = card.Id;
        Button useButton = (Button)instanceCardPanel.transform.Find("UseButton").gameObject.GetComponent<Button>();
        Button discardButton = (Button)instanceCardPanel.transform.Find("DiscardButton").gameObject.GetComponent<Button>();
        useButton.onClick.AddListener(UseCard);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        //{
        //    instanceCardPanel.SetActive(false);
        //}
    }

    public void UseCard()
    {
        Debug.Log("Use Card");
        game.UseCard(card);
    }
}
