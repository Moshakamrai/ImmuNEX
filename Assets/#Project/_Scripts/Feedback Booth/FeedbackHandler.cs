using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackHandler : MonoBehaviour
{
    public static FeedbackHandler feedbackInstance;
    public PostRequest postRequest;
    public RFIDReader rfidReader;

    public Button submitButton;
    public InputField feedbackInput;
    public InputField rfidInput;

    string rfid;


    private void Awake()
    {
        if (feedbackInstance != null)
            Destroy(feedbackInstance);

        feedbackInstance = this;
    }
    void Start()
    {
        rfidInput.ActivateInputField();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && rfidInput.interactable)
            TurnInputOn();

        if (Input.GetMouseButtonDown(1) && rfidInput.interactable)
            TurnInputOn();

        if (Input.GetMouseButtonDown(2) && rfidInput.interactable)
            TurnInputOn();
    }
    public void ReadRFID()
    {
        if (rfidInput.text.Length == 10)
        {
            rfid = rfidInput.text;
            EnableFeedback();
            Debug.LogError("RFID is " + rfid);
        }
        else
        {
            TurnInputOn();
        }
    }

    public void EnableSubmission()
    {
        if (feedbackInput.text != null)
            submitButton.interactable = true;
    }

    private void EnableFeedback()
    {
        rfidInput.interactable = false;
        feedbackInput.interactable = true;
        feedbackInput.ActivateInputField();
    }

    public void DisableFeedback()
    {
        feedbackInput.interactable = false;
        submitButton.interactable = false;
        rfidInput.text = "";
        rfidInput.interactable = true;
        rfidInput.ActivateInputField();
    }

    public void OnSubmitPressed()
    {
        Debug.LogError(rfid);
        postRequest.SetLastFeedback(feedbackInput.text);
        postRequest.PostFeedback(rfid);
    }
    public void PostFeedback(string rfid)
    {
        postRequest.PostFeedback(rfid);
    }

    public void TurnInputOn()
    {
        rfidInput.text = "";
        rfidInput.ActivateInputField();
        Debug.LogError("Activating input field");
    }
}
