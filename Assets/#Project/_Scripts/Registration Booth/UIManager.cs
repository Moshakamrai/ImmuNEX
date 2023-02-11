using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public string currentRFID;
    int[] groupCounters;
    int counter;
    

    [Header("API References")]
    public GetRequest getRequest;
    public PostRequest postRequest;
    public RFIDReader rfidReader;

    [Header("UI References")]
    public GameObject display1Panel;
    public GameObject display2Panel;
    public Button[] groupButtons;
    public Text[] counterTexts;
    public GameObject textRestriction;
    

    [Header("User Info")]
    public Text userName;
    public Text organization;
    public Text email;
    public Text phoneNumber;
    public Text rfidText;
    public Text genderText;

    [Header("Group Info")]
    public Text rfidHeader;
    [SerializeField]
    int currentGroupNo;

    [Header("Profile Picture References")]
    public Image profilePic;
    float pictureWidth, pictureHeight;
    Dictionary<string, string> profilePictures;
    string[] files;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }
    private void Start()
    {
        groupCounters = new int[] { 0, 0, 0, 0, 0, 0 };

        pictureWidth = profilePic.preferredWidth;
        pictureHeight = profilePic.preferredHeight;

        profilePictures = new Dictionary<string, string>();
        files = Directory.GetFiles(@"C:\ImmuNEX\");

        for (int i = 0; i < files.Length; i++)
        {
            string name = Path.GetFileNameWithoutExtension(files[i]);
            profilePictures.Add(name, files[i]);
        }
    }

    public void GetCurrentRFID(string rfid)
    {
        currentRFID = rfid;
        Debug.Log("Current rfid is " + currentRFID);
        postRequest.SetCurrentRFID(rfid);
    }

    public void GetUserInfo(string rfid)
    {
        Debug.Log("Getting user info");
        getRequest.TestGet(rfid);
    }

    public void ShowUserInfo(string name, string org, string mail, string phone, string id , string gender)
    {
        userName.text = name;
        organization.text = org;
        email.text = mail;
        phoneNumber.text = phone;
        rfidText.text = id;
        genderText.text = gender;

        ShowNameAndRFID(name, id);
        LoadProfilePicture(id);
    }

    public void OnGroupButtonClicked(int groupNo)
    {
        currentGroupNo = groupNo;

        Debug.LogError("Current group is " + currentGroupNo);
    }

    public void AssignGroup(int number)
    {
        Debug.Log("Assigning group");

        if(currentRFID != null)
            postRequest.UpdateGroups(number);
    }

    public void ShowNameAndRFID(string name, string rfid)
    {
        rfidHeader.text = name + "\n\n" + rfid;
    }

    public void TurnGroupButtonOn()
    {
        for (int i = 0; i < groupButtons.Length; i++)
        {
            groupButtons[i].interactable = true;
        }
    }

    public void OnSubmitButtonClicked()
    {
        Debug.Log("Assigning player to " + currentGroupNo);
        AssignGroup(currentGroupNo);
    }

    public void TurnScannerOn()
    {
        rfidReader.buttonPressed = false;
    }

    public void IncrementGroupCounter(int index)
    {
        counter = 1 + groupCounters[index]++;
        counterTexts[index].text = counter.ToString();
        textRestriction.SetActive(false);
        if (counterTexts[index].text == "10")
        {
            textRestriction.SetActive(true);
            groupButtons[index].interactable = false;
        }
        else
        {
            groupButtons[index].interactable = true;
            textRestriction.SetActive(false);
        }  
    }

    public void LoadProfilePicture(string rfid)
    {
        StartCoroutine(LoadingPicture(rfid));
    }

    IEnumerator LoadingPicture(string rfid)
    {
        string path;
        profilePictures.TryGetValue(rfid, out path);
        byte[] byteArray = File.ReadAllBytes(path);
        Texture2D sampleTexture = new Texture2D(2, 2);
        bool isLoaded = sampleTexture.LoadImage(byteArray);

        while (!isLoaded)
        {
            yield return null;
        }

        profilePic.sprite = Sprite.Create(sampleTexture, new Rect(0.0f, 0.0f, sampleTexture.width, sampleTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        profilePic.preserveAspect = true;
        profilePic.gameObject.SetActive(true);
    }
}
