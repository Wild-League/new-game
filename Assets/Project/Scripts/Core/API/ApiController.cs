using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Core.Base;
using UnityEngine;
using UnityEngine.Networking;

public class ApiController : MonoBehaviour
{
    public static ApiController Instance;
    private int completedDownloads;

    private void Awake()
    {
        Instance = this;
    }

    private static string DECKS_ENDPOINT = "https://api.wildleague.org/v1/decks/>INFO</";

    public void GetDeck(int deckID)
    {
        var deckURL = DECKS_ENDPOINT.Replace(">INFO<", deckID.ToString(), StringComparison.Ordinal);
        StartCoroutine(SendRequest(deckURL));
    }

    private IEnumerator SendRequest(string endPoint)
    {
        PlayerController.Instance.ResetDeck();
        completedDownloads = 0;
        var request = UnityWebRequest.Get(endPoint);
        var startRequest = Time.realtimeSinceStartup;
        yield return request.SendWebRequest();


        if (request.result is UnityWebRequest.Result.ConnectionError
            or UnityWebRequest.Result.ProtocolError
            or UnityWebRequest.Result.DataProcessingError)
        {
            Debug.Log(request.error);
            Application.Quit();
        }
        else
        {
            var dataString = request.downloadHandler.text;
            var deckJson = JsonUtility.FromJson<DeckJSON>(dataString);
            PlayerController.Instance.OwnedCards = new List<Card>();

            foreach (var card in deckJson.cards)
            {
                StartCoroutine(GetCard(card));
            }

            yield return new WaitUntil(() => completedDownloads == deckJson.cards.Count);
            Debug.Log("ended request: " + (Time.realtimeSinceStartup - startRequest));
            PlayerController.Instance.GetDeck();
        }
    }

    private Sprite GenerateSprite(Texture2D texture)
    {
        var rect = new Rect(0.0f, 0.0f, texture.width, texture.height);
        var sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 100.0f);
        return sprite;
    }

    private IEnumerator GetCard(CardJSON card)
    {
        Sprite imgPreview = null;
        var imgCardRequest = UnityWebRequestTexture.GetTexture(card.img_card);
        yield return imgCardRequest.SendWebRequest();

        var imgCard = GenerateSprite(((DownloadHandlerTexture)imgCardRequest.downloadHandler).texture);

        if (card.type != "spell")
        {
            var imgPreviewRequest = UnityWebRequestTexture.GetTexture(card.img_preview);
            yield return imgPreviewRequest.SendWebRequest();

            imgPreview = GenerateSprite(((DownloadHandlerTexture)imgPreviewRequest.downloadHandler).texture);
        }

        var gameCard = new Card()
        {
            id = card.id
            , name = card.name
            , type = card.type
            , cooldown = card.cooldown
            , damage = card.damage
            , attack_range = card.attack_range
            , speed = card.speed
            , life = card.life
            , img_card = imgCard
            , img_preview = imgPreview
        };

        PlayerController.Instance.OwnedCards.Add(gameCard);
        completedDownloads++;
    }
}