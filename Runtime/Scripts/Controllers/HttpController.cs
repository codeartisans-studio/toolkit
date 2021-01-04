using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Toolkit
{
    [AddComponentMenu("Toolkit/Controllers/Http Controller")]
    public class HttpController : Singleton<HttpController>
    {
        private void SetRequestHeaders(UnityWebRequest uwr, Dictionary<string, string> header)
        {
            if (header == null)
                return;

            foreach (var kvp in header)
            {
                uwr.SetRequestHeader(kvp.Key, kvp.Value);
            }
        }

        private IEnumerator SendWebRequest(UnityWebRequest uwr, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction)
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.LogWarning(uwr.error);

                if (errorAction != null)
                {
                    errorAction(uwr);
                }
            }
            else
            {
                Debug.Log(uwr.downloadHandler.text);

                if (successAction != null)
                {
                    successAction(uwr);
                }
            }
        }

        private IEnumerator GetEnumerator(string uri, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            using (UnityWebRequest uwr = UnityWebRequest.Get(uri))
            {
                SetRequestHeaders(uwr, header);

                yield return StartCoroutine(SendWebRequest(uwr, successAction, errorAction));
            }
        }

        private IEnumerator DeleteEnumerator(string uri, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            using (UnityWebRequest uwr = UnityWebRequest.Delete(uri))
            {
                SetRequestHeaders(uwr, header);

                yield return StartCoroutine(SendWebRequest(uwr, successAction, errorAction));
            }
        }

        // put byte array
        private IEnumerator PutEnumerator(string uri, byte[] bodyData, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            using (UnityWebRequest uwr = UnityWebRequest.Put(uri, bodyData))
            {
                SetRequestHeaders(uwr, header);

                yield return StartCoroutine(SendWebRequest(uwr, successAction, errorAction));
            }
        }

        // put string
        private IEnumerator PutEnumerator(string uri, string bodyData, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            using (UnityWebRequest uwr = UnityWebRequest.Put(uri, bodyData))
            {
                SetRequestHeaders(uwr, header);

                yield return StartCoroutine(SendWebRequest(uwr, successAction, errorAction));
            }
        }

        // put form
        private IEnumerator PutEnumerator(string uri, WWWForm form, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            byte[] bodyData = System.Text.Encoding.UTF8.GetBytes(form.ToString());

            using (UnityWebRequest uwr = UnityWebRequest.Put(uri, bodyData))
            {
                SetRequestHeaders(uwr, header);

                yield return StartCoroutine(SendWebRequest(uwr, successAction, errorAction));
            }
        }

        // post byte array
        private IEnumerator PostEnumerator(string uri, byte[] bodyData, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            using (UnityWebRequest uwr = new UnityWebRequest(uri, UnityWebRequest.kHttpVerbPOST, new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData)))
            {
                SetRequestHeaders(uwr, header);

                yield return StartCoroutine(SendWebRequest(uwr, successAction, errorAction));
            }
        }

        // post string
        private IEnumerator PostEnumerator(string uri, string bodyData, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            using (UnityWebRequest uwr = UnityWebRequest.Post(uri, bodyData))
            {
                SetRequestHeaders(uwr, header);

                yield return StartCoroutine(SendWebRequest(uwr, successAction, errorAction));
            }
        }

        // post form
        private IEnumerator PostEnumerator(string uri, WWWForm form, Action<UnityWebRequest> successAction, Action<UnityWebRequest> errorAction, Dictionary<string, string> header)
        {
            using (UnityWebRequest uwr = UnityWebRequest.Post(uri, form))
            {
                SetRequestHeaders(uwr, header);

                yield return StartCoroutine(SendWebRequest(uwr, successAction, errorAction));
            }
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