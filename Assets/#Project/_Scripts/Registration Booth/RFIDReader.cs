using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RFIDReader : MonoBehaviour
{
    public InputField mainInputField;
    public string RFIDuser;

    private bool dataOn;
    public bool buttonPressed;


    void Start()
    {
        buttonPressed = false;

        mainInputField.ActivateInputField();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !buttonPressed)
            TurnInputOn();

        if (Input.GetMouseButtonDown(1) && !buttonPressed)
            TurnInputOn();

        if (Input.GetMouseButtonDown(2) && !buttonPressed)
            TurnInputOn();
    }
    public void DataReciever()
    {
        if (!buttonPressed && mainInputField.text.Length == 10)
        {
            buttonPressed = true;
            Debug.LogError("Received new data");
            RFIDuser = mainInputField.text;
            UIManager.instance.GetUserInfo(RFIDuser);
            UIManager.instance.GetCurrentRFID(RFIDuser);

            Debug.Log(RFIDuser);
        }
    }

    private void ClearField()
    {
        mainInputField.text = "";
    }
    public void TurnInputOn() 
    {
        mainInputField.text = "";
        mainInputField.ActivateInputField();
        Debug.LogError("Activating input field");
    }
}
