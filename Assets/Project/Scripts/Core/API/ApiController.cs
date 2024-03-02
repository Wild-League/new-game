using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class ApiController : MonoBehaviour
{
    public static ApiController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private static string CARDS_ENDPOINT = "https://api.wildleague.org/api/card/";
    private static string DECKS_ENDPOINT = "https://api.wildleague.org/v1/decks/>INFO</?format=json";

    public void GetDeck(int deckID)
    {
        StartCoroutine(RequestCompleted(DECKS_ENDPOINT + deckID));
    }

    public void GetCard(int cardID)
    {
        StartCoroutine(RequestCompleted(CARDS_ENDPOINT + cardID));
    }

    private IEnumerator RequestCompleted(string endPoint)
    {
        var request = UnityWebRequest.Get(endPoint);
        yield return request.SendWebRequest();

        var result = request.downloadHandler;
    }
}