using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public Text textError;
    void Start()
    {
        // A correct website page.
        // StartCoroutine(GetDate("http://localhost/UnityBackendTutorial/GetDate.php"));

        // A non-existing page.
        // StartCoroutine(GetDate("https://error.html"));

        // Users
        // StartCoroutine(GetUsers("http://localhost/UnityBackendTutorial/GetUsers.php"));

        // Login
        // StartCoroutine(Login("testuser", "123456"));

        // Register User
        //StartCoroutine(RegisterUser("testuser3", "123458"));
    }

    public void ShowUserItems()
    {
        // Show items on Console
        //StartCoroutine(GetItemsIDs(Main.Instance.userInfo.UserID, ?));
    }

    IEnumerator GetDate(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    IEnumerator GetUsers(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
                textError.text = pages[page] + ": Error: " + webRequest.error;
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                textError.text = pages[page] + ":\nReceived: " + webRequest.downloadHandler.text;
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        //form.AddField("coins", Main.Instance.userInfo.coins);
        //form.AddField("level", Main.Instance.userInfo.level);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error");
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Login ok");
                textError.text = www.downloadHandler.text;
                // Save User, pass and ID
                Main.Instance.userInfo.SetCredentials(username, password);
                Main.Instance.userInfo.SetID(www.downloadHandler.text);

                if(www.downloadHandler.text.Contains("Wrong Credentials") ||
                www.downloadHandler.text.Contains("Username doesn't exists"))
                {
                    Debug.Log("Try again");
                }else
                {
                // If logged in correctly
                Main.Instance.userProfile.SetActive(true);
                }
            }
        }
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                textError.text = www.error;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetItemsIDs(string userID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/GetItemsIDs.php", form))
        {
            // Request and wait for the desired page.
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                textError.text = www.error;
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                string jsonArrayString = www.downloadHandler.text;
                //Call callback function to pass results
                callback(jsonArrayString);
            }
        }
    }

    public IEnumerator GetItem(string itemID, System.Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendTutorial/GetItem.php", form))
        {
            // Request and wait for the desired page.
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                textError.text = www.error;
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                string jsonArray = www.downloadHandler.text;
                //Call callback function to pass results
                callback(jsonArray);
            }
        }
    }
}
