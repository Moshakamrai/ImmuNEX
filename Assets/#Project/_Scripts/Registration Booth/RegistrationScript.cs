using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegistrationScript : MonoBehaviour
{
    [Header("InputField References")]
    public InputField rfidInputField;
    public InputField nameInputField;
    public InputField organizationInputField;
    public InputField EmailField;
    public InputField phoneInputField;
    public InputField genderInputField;

    public Button submitButton;

    bool scannerActive;


    // Start is called before the first frame update
    void Start()
    {
        rfidInputField.ActivateInputField();
        scannerActive = true;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.LogError("Mouse Pressed");

            if(scannerActive)
                rfidInputField.ActivateInputField();
        }
        EnableInputs();
    }

    public void EnableInputs()
    {
        Debug.LogError("Enabling Inputs");

        if(rfidInputField.text.Length == 10)
        {
            scannerActive = false;

            nameInputField.interactable = true;
            organizationInputField.interactable = true;
            EmailField.interactable = true;
            phoneInputField.interactable = true;
            submitButton.interactable = true;
            genderInputField.interactable = true;
        }
        else
        {
            rfidInputField.ActivateInputField();
        }
    }

    public void DisableInputs()
    {
        nameInputField.interactable = false;
        organizationInputField.interactable = false;
        EmailField.interactable = false;
        phoneInputField.interactable = false;
        submitButton.interactable = false;
        genderInputField.interactable = false;
    }

    public void ReactivateScanning()
    {
        rfidInputField.text = "";
        rfidInputField.ActivateInputField();
        scannerActive = true;
    }

    public void OnSubmitPressed()
    {
        DisableInputs();
        ReactivateScanning();
        StartCoroutine(UploadUserInfo());
    }
    
    IEnumerator UploadUserInfo()
    {
        WWWForm form = new WWWForm();

        form.AddField("email", EmailField.text);
        form.AddField("name", nameInputField.text);
        form.AddField("organization", organizationInputField.text);
        form.AddField("phone", phoneInputField.text);
        form.AddField("rfid", rfidInputField.text);
        form.AddField("gender", genderInputField.text);

        UnityWebRequest www = UnityWebRequest.Post("https://us-central1-nestle-activation-e65b3.cloudfunctions.net/players", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string results = www.downloadHandler.text;
            Debug.Log(results);
            Debug.Log("Something");
        }
        www.Dispose();

    }
}
