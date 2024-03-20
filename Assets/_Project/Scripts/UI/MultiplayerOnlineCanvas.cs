using System;
using System.Collections;
using System.Net;
using _Project.Scripts.Core.API;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class MultiplayerOnlineCanvas : MonoBehaviour
    {
        private Panel _panel;
        [SerializeField] private CanvasServer Server;
        [SerializeField] private CanvasLogin Login;

        public Panel ActualPanel
        {
            get => _panel;

            set
            {
                _panel = value;

                Login.LoginObject.SetActive(false);
                Server.ServerObject.SetActive(false);

                switch (value)
                {
                    case Panel.Login:
                        Login.LoginObject.SetActive(true);
                        break;
                    case Panel.Server:
                        Server.ServerObject.SetActive(true);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid Panel");
                }
            }
        }

        private void Start()
        {
            ActualPanel = Panel.Server;
            Server.SendServer.onClick.AddListener(() => StartCoroutine(Server.DetectServer()));
        }
    }

    [Serializable]
    public class CanvasServer
    {
        public const string ServerDetection = "/.well-known/nodeinfo";
        public GameObject ServerObject;
        public TMP_InputField ServerInput;
        public Button SendServer;


        public IEnumerator DetectServer()
        {
            var server = "https://" + ServerInput.text;
            var uri = server + ServerDetection;
            if (RemoteFileExists(uri))
            {
                Debug.Log("exists");
                UserInfo.ActualServer = server;
                Debug.Log(UserInfo.ActualServer);
            }
            else
            {
                Debug.Log("not exists");
            }

            yield return null;
        }

        private bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                var request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                request.ContentType = "application/json";
                //Getting the Web Response.
                var response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    [Serializable]
    public class CanvasLogin
    {
        public GameObject LoginObject;
    }

    public enum Panel
    {
        Server,
        Login
    }
}