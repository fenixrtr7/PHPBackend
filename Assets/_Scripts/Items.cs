using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;

public class Items : MonoBehaviour
{
    Action<string> _createItemsCallback;
    public GameObject prefabInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        //Define Callback
        _createItemsCallback = (jsonArrayString) => {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };

        CreateItems();
    }

    public void CreateItems()
    {
        string userId = Main.Instance.userInfo.UserID;
        StartCoroutine(Main.Instance.web.GetItemsIDs(userId, _createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        //Parsing json array string as an aray
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++)
        {
            //Create local variables
            bool isDone = false; // are we done downloading?
            string itemID = jsonArray[i].AsObject["itemID"];
            JSONObject itemInfoJson = new JSONObject();

            // Create a callback to get the information from Web.cs
            Action<string> getItemInfoCallback = (itemInfo) => {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            // Wait untill Web.cs calls the callback we passed as parameter
            StartCoroutine(Main.Instance.web.GetItem(itemID, getItemInfoCallback));

            // Wait until the callback is called from WEB (info finished downloading)
            yield return new WaitUntil(() => isDone == true); // Wait an action
        
            // Instantiate GameObject (item prefab)
            GameObject item = Instantiate(prefabInstantiate);
            //(Resources.Load("Prefabs/Item") as GameObject);
            item.transform.SetParent(this.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;

            // Fill information
            item.transform.Find("Name").GetComponent<Text>().text = itemInfoJson["name"];
            item.transform.Find("Price").GetComponent<Text>().text = itemInfoJson["price"];
            item.transform.Find("Description").GetComponent<Text>().text = itemInfoJson["description"];

            // contiue to the next item
        }
    }
}
