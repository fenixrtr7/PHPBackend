using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField usernameIput, passwordInput, nameInput, passEnterInput, passConfirm;
    public Button loginButton, submitButton, createNewUserButton, buttonBack;

    public GameObject panelUser, panelRegisterNewUser;

    // Start is called before the first frame update
    void Start()
    {
        // Button Login
        loginButton.onClick.AddListener(() => {
            StartCoroutine(Main.Instance.web.Login(usernameIput.text, passwordInput.text));
        });
        // Button Register User
        submitButton.onClick.AddListener(() => {
            StartCoroutine(Main.Instance.web.RegisterUser(nameInput.text, passEnterInput.text));
            
            // Si es correcto o si no es incorrecto ¿?
            //BackPanelUser();
        });
        // Button Create a New User
        createNewUserButton.onClick.AddListener(() => {
            CreateNewUserButton();
        });
        // Button Create a New User
        buttonBack.onClick.AddListener(() => {
            BackPanelUser();
        });
    }

    public void CreateNewUserButton()
    {
        //panelUser.SetActive(false);
        panelRegisterNewUser.SetActive(true);
    }

    public void BackPanelUser()
    {
        //panelUser.SetActive(true);
        panelRegisterNewUser.SetActive(false);
    }
}
