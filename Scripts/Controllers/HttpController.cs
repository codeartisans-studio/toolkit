using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Controllers/HttpController")]
    public class HttpController : Singleton<HttpController>
    {
        private void SetRequestHeaders(UnityWebRequest req, Dictionary<string, string> header)
        {
            if (header == null)
                return;

            foreach (var kvp in header)
            {
                req.SetRequestHeader(kvp.Key, kvp.Value);
            }
        }

        private IEnumerator SendWebRequest(UnityWebRequest req, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction)
        {
            yield return req.SendWebRequest();

            if (req.isNetworkError)
            {
                Debug.LogWarning(req.error);

                if (errorAction != null)
                {
                    errorAction(req);
                }
            }
            else
            {
                Debug.Log(req.downloadHandler.text);

                if (successAction != null)
                {
                    successAction(req);
                }
            }
        }

        private IEnumerator GetEnumerator(string uri, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            UnityWebRequest req = UnityWebRequest.Get(uri);

            SetRequestHeaders(req, header);

            yield return StartCoroutine(SendWebRequest(req, successAction, errorAction));
        }

        private IEnumerator DeleteEnumerator(string uri, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            UnityWebRequest req = UnityWebRequest.Delete(uri);

            SetRequestHeaders(req, header);

            yield return StartCoroutine(SendWebRequest(req, successAction, errorAction));
        }

        // put byte array
        private IEnumerator PutEnumerator(string uri, byte[] bodyData, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            UnityWebRequest req = UnityWebRequest.Put(uri, bodyData);

            SetRequestHeaders(req, header);

            yield return StartCoroutine(SendWebRequest(req, successAction, errorAction));
        }

        // put string
        private IEnumerator PutEnumerator(string uri, string bodyData, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            UnityWebRequest req = UnityWebRequest.Put(uri, bodyData);

            SetRequestHeaders(req, header);

            yield return StartCoroutine(SendWebRequest(req, successAction, errorAction));
        }

        // put form
        private IEnumerator PutEnumerator(string uri, WWWForm form, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            byte[] bodyData = System.Text.Encoding.UTF8.GetBytes(form.ToString());

            UnityWebRequest req = UnityWebRequest.Put(uri, bodyData);

            SetRequestHeaders(req, header);

            yield return StartCoroutine(SendWebRequest(req, successAction, errorAction));
        }

        // post byte array
        private IEnumerator PostEnumerator(string uri, byte[] bodyData, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            UnityWebRequest req = new UnityWebRequest(uri, UnityWebRequest.kHttpVerbPOST, new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));

            SetRequestHeaders(req, header);

            yield return StartCoroutine(SendWebRequest(req, successAction, errorAction));
        }

        // post string
        private IEnumerator PostEnumerator(string uri, string bodyData, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            UnityWebRequest req = UnityWebRequest.Post(uri, bodyData);

            SetRequestHeaders(req, header);

            yield return StartCoroutine(SendWebRequest(req, successAction, errorAction));
        }

        // post form
        private IEnumerator PostEnumerator(string uri, WWWForm form, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            UnityWebRequest req = UnityWebRequest.Post(uri, form);

            SetRequestHeaders(req, header);

            yield return StartCoroutine(SendWebRequest(req, successAction, errorAction));
        }

        public void Get(string uri, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header = null)
        {
            StartCoroutine(GetEnumerator(uri, successAction, errorAction, header));
        }

        public void Delete(string uri, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header = null)
        {
            StartCoroutine(DeleteEnumerator(uri, successAction, errorAction, header));
        }

        // put byte array
        public void Put(string uri, byte[] bodyData, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header = null)
        {
            StartCoroutine(PutEnumerator(uri, bodyData, successAction, errorAction, header));
        }

        // put string
        public void Put(string uri, string bodyData, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header = null)
        {
            StartCoroutine(PutEnumerator(uri, bodyData, successAction, errorAction, header));
        }

        // put form
        public void Put(string uri, WWWForm form, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header = null)
        {
            StartCoroutine(PutEnumerator(uri, form, successAction, errorAction, header));
        }

        // put json string
        public void PutJson(string uri, string json, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header = null)
        {
            if (header == null)
                header = new Dictionary<string, string>();

            header["Content-Type"] = "application/json";

            byte[] bodyData = System.Text.Encoding.UTF8.GetBytes(json);

            StartCoroutine(PutEnumerator(uri, bodyData, successAction, errorAction, header));
        }

        // post byte array
        public void Post(string uri, byte[] bodyData, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header = null)
        {
            StartCoroutine(PostEnumerator(uri, bodyData, successAction, errorAction, header));
        }

        // post string
        public void Post(string uri, string bodyData, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header = null)
        {
            StartCoroutine(PostEnumerator(uri, bodyData, successAction, errorAction, header));
        }

        // post form
        public void Post(string uri, WWWForm form, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header = null)
        {
            StartCoroutine(PostEnumerator(uri, form, successAction, errorAction, header));
        }

        // post json string
        public void PostJson(string uri, string json, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header = null)
        {
            if (header == null)
                header = new Dictionary<string, string>();

            header["Content-Type"] = "application/json";

            byte[] bodyData = System.Text.Encoding.UTF8.GetBytes(json);

            StartCoroutine(PostEnumerator(uri, bodyData, successAction, errorAction, header));
        }
    }
}