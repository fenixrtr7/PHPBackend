using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
    public string UserID{ get; private set;}
    string userName;
    string userPassword;
    public string level;
    public string coins;
    public Text userNameText, coinsText, levelText;

    public void SetCredentials(string username, string userpassword)
    {
        userName = username;
        userNameText.text = "Name: " + userName;

        // coinsText.text = "Coins: " + coins;
        // levelText.text = "Level: " + level;

        userPassword = userpassword;
    }

    public void SetID(string id){
        UserID = id;
    }
}
