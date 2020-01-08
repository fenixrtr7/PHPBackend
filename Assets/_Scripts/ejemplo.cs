// IEnumerator GetMonth(string url)
//     {
//         using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
//         {
//             // Request and wait for the desired page.
//             yield return webRequest.SendWebRequest();

//             string[] pages = url.Split('/');
//             int page = pages.Length - 1;

//             if (webRequest.isNetworkError)
//             {
//                 Debug.Log(pages[page] + ": Error: " + webRequest.error);
//                 textError.text = pages[page] + ": Error: " + webRequest.error;
//             }
//             else
//             {
//                 Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
//                 textError.text = pages[page] + ":\nReceived: " + webRequest.downloadHandler.text;
//             }
//         }
//     }