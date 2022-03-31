using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Text.RegularExpressions;

public class Login : MonoBehaviourPunCallbacks
{

    public GameObject connectionPanel;

    private const string PASSWORD_REGEX = "(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{8,24})";
    [SerializeField] private string sessionEndPoint = "http://127.0.0.1:13756/account/session";
    [SerializeField] private string loginEndpoint = "http://127.0.0.1:13756/account/login";
    [SerializeField] private string createEndpoint = "http://127.0.0.1:13756/account/create";

    public GameObject LogInorSignUpPanel;
    public GameObject SignUpPanel;
    public GameObject LogInPanel;


    PlayerOwnWorldData playerOwnWorldData;

    private string userName;



    [SerializeField] private Button loginButton;
    [SerializeField] private Button createButton;
    [SerializeField] private InputField usernameInputFieldLogin;
    [SerializeField] private InputField passwordInputFieldLogin;
    [SerializeField] private InputField gameIDInputFieldLogIn;
    [SerializeField] private TextMeshProUGUI alertTextLogIn;


    [SerializeField] private TextMeshProUGUI alertTextSignUp;
    [SerializeField] private InputField nameInputFieldSignUp;
    [SerializeField] private InputField emailInputFieldSignUp;
    [SerializeField] private InputField usernameInputFieldSignUp;
    [SerializeField] private InputField passwordInputFieldSignUp;
    [SerializeField] private InputField confPasswordInputFieldSignUp;
    [SerializeField] private InputField genderInputFieldSignUp;

    bool flag = true;


    private string session_token;



    void Start()
    {
        LogInorSignUpPanel.SetActive(true);
        SignUpPanel.SetActive(false);
        LogInPanel.SetActive(false);
        connectionPanel.SetActive(false);
        /*
        while(!PhotonNetwork.IsConnected)
        {
            Debug.Log("Called {PhotonNetwork.ConnectUsingSettings()}");

            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Photon Connection sent");
        }
        */
    }
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        session_token = PlayerPrefs.GetString("token");
        Debug.Log("Session token on awake: " + session_token);

    }
    void Update()
    {

    }
    #region public methods
    public void SetPlayerName(string playername) ///making a public class for player input section
    {
        if (string.IsNullOrEmpty(playername)) ///Checking if the player name is empty or not
        {
            Debug.Log("Player Name is Empty.");
            return;
        }
        PhotonNetwork.LocalPlayer.NickName = playername; //else connecting the name to the server
    }
    public void OnClickEnterGameJoinRandomRoom() ///function for joinning random room when Enter Game is Clicked
    {
        Debug.Log("Called {OnClickEnterGameJoinRandomRoom()}");

        if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("Join Random Room Called");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("Please Connect to Master Server");
        }
        connectionPanel.SetActive(true);
    }

    public void OnLoginClick()
    {
        Debug.Log("Called {OnLoginClick()}");
        alertTextLogIn.text = "     Signing in...";
        StartCoroutine(TryLogin());
    }

    public void OnCreateClick()
    {
        Debug.Log("Called {OnCreateClick()}");

        alertTextSignUp.text = "  Creating account...";
        StartCoroutine(TryCreate());
    }
    public void OnClickBackButton()
    {
        LogInPanel.SetActive(false);
        LogInorSignUpPanel.SetActive(true);
        SignUpPanel.SetActive(false);
        connectionPanel.SetActive(false);
    }

    public void onClickLoginOption() //When LogIn or create game account button is clicked
    {
        LogInPanel.SetActive(true);
        LogInorSignUpPanel.SetActive(false);
        SignUpPanel.SetActive(false);
        connectionPanel.SetActive(false);

        Debug.Log("CheckSSession called?");
        //   StartCoroutine(CheckSession());
    }
    public void onClickSignupOption() //When LogIn or create game account button is clicked
    {
        LogInPanel.SetActive(false);
        LogInorSignUpPanel.SetActive(false);
        SignUpPanel.SetActive(true);
        connectionPanel.SetActive(false);
    }

    #endregion




    #region private methods
    void CreateOrJoinRandomRoom()
    {
        Debug.Log("Called {CreateOrJoinRandomRoom()}");


        //string randomRoom = "Room " + Random.Range(0, 5000);
        string randomRoom = "OwnRoom_" + userName;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 5;
        Debug.Log("Room Create Request Sent");
        PhotonNetwork.CreateRoom(randomRoom, roomOptions);
    }


    private IEnumerator CheckSession()
    {
        Debug.Log("session Request Sent: " + session_token);


        UnityWebRequest request = UnityWebRequest.Get(sessionEndPoint);
        request.SetRequestHeader("Authorization", "Bearer " + session_token);
        Debug.Log("session Request Sent " + request.url);






        //UnityWebRequest request = UnityWebRequest.Get($"{ess}?rGameid={username}&rPassword={password}");

        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;

            if (startTime > 10.0f)
            {
                break;
            }

            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            SessionResponse response = JsonUtility.FromJson<SessionResponse>(request.downloadHandler.text);

            if (response.code == 0) // Session End??? Redirect to login layout
            {
                Debug.Log("Session End");
                LogInPanel.SetActive(true);
                LogInorSignUpPanel.SetActive(false);
                SignUpPanel.SetActive(false);
                connectionPanel.SetActive(false);
            }
            else// Session Active??? Redirect to room
            {
                Debug.Log("Session Active");
                userName = PlayerPrefs.GetString("username");

                OnClickEnterGameJoinRandomRoom();
            }
        }
        else
        {
            alertTextLogIn.text = "Login First(Session End)";
        }


        yield return null;
    }




    private IEnumerator TryLogin()
    {
        Debug.Log("Called {TryLogin()}");

        string username = usernameInputFieldLogin.text;
        string password = passwordInputFieldLogin.text;

        userName = userName;

        if (username.Length > 24)
        {
            alertTextLogIn.text = "Invalid username";
            yield break;
        }
        //!Regex.IsMatch(password, PASSWORD_REGEX)
        if (password.Length < 3)
        {
            alertTextLogIn.text = "Invalid credentials";
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("rGameid", username);
        form.AddField("rPassword", password);


        UnityWebRequest request1 = UnityWebRequest.Get(loginEndpoint);

        //www.SetRequestHeader("Pragma", "no-cache");

        //  request1.SetRequestHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJnYW1laWQiOiIyNTAiLCJwYXNzd29yZCI6IldEM2RzZmRzYWVmZHNhZiIsImlhdCI6MTY0NDU2ODUxMywiZXhwIjoxNjQ0NTcyMTEzfQ.i4JzPc4ld4NcX3aIDzyWo3spYXqvX3ki7UThTEWdy0c");



        Debug.Log(form);
        Debug.Log(request1.url);

        Debug.Log("username " + username + " pass " + password);
        UnityWebRequest request = UnityWebRequest.Get($"{loginEndpoint}?rGameid={username}&rPassword={password}");

        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;

            if (startTime > 10.0f)
            {
                break;
            }

            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);

            if (response.code == 0) // login success?
            {
                alertTextLogIn.text = "      Welcome ";
                session_token = response.data;
                PlayerPrefs.SetString("token", response.data);
                PlayerPrefs.SetString("username", response.username);

                Debug.Log("Session TOken: " + response.data);

                if (flag == true)
                {
                    PhotonNetwork.LoadLevel("MainScene");/// this will load another scene

                    // OnClickEnterGameJoinRandomRoom();
                    flag = false;
                }

            }
            else
            {
                switch (response.code)
                {
                    case 1:
                        alertTextLogIn.text = "Invalid credentials";
                        break;
                    default:
                        alertTextLogIn.text = "Corruption detected";
                        break;
                }
            }
        }
        else
        {
            alertTextLogIn.text = "Error connecting to the server...";
        }


        yield return null;
    }

    private IEnumerator TryCreate()
    {
        string name = nameInputFieldSignUp.text;
        string password = passwordInputFieldSignUp.text;
        string username = usernameInputFieldSignUp.text;
        string email = emailInputFieldSignUp.text;
        string gender = genderInputFieldSignUp.text;
        string confPassword = confPasswordInputFieldSignUp.text;


        Debug.Log("Sign Up info: " + username + " " + password + " " + username);

        if (username.Length < 3 || username.Length > 24)
        {
            alertTextSignUp.text = "Invalid username";
            yield break;
        }
        //!Regex.IsMatch(password, PASSWORD_REGEX)
        if (password.Length < 3)
        {
            alertTextSignUp.text = "Invalid Password";
            yield break;
        }
        else if (confPassword != password)
        {
            alertTextSignUp.text = "Password does not match";
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("rUsername", username);
        form.AddField("rPassword", password);


        // UnityWebRequest request = UnityWebRequest.Post(createEndpoint, form);
        UnityWebRequest request = UnityWebRequest.Get($"{createEndpoint}?rUsername={name}&rPassword={password}&rGameid={username}&rEmail={email}&rGender={gender}");

        Debug.Log($"{createEndpoint}?rUsername={name}&rPassword={password}&rGameid={username}&rEmail={email}&rGender={gender}");
        Debug.Log("Form: " + form + " web request " + request);

        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;

            if (startTime > 30.0f)
            {
                break;
            }
            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
            CreateResponse response = JsonUtility.FromJson<CreateResponse>(request.downloadHandler.text);

            if (response.code == 0)
            {
                alertTextSignUp.text = "Account has been created!";
                // user data goes here

                playerOwnWorldData = new PlayerOwnWorldData();
                playerOwnWorldData.playerData = new PlayerOwnWorldData.PlayerDataOwn();


                playerOwnWorldData.plummie_tag = username;
                playerOwnWorldData.name = name;
                playerOwnWorldData.gender = gender;
                playerOwnWorldData.playerData.woodCount = 0;
                playerOwnWorldData.playerData.stoneCount =0;
                playerOwnWorldData.playerData.coinCount = 0;


                StartCoroutine(playerOwnWorldData.PostPlayerData(result => {
                    Debug.Log("Posting user data!");
                }));





                LogInPanel.SetActive(true);
                LogInorSignUpPanel.SetActive(false);
                SignUpPanel.SetActive(false);
                connectionPanel.SetActive(false);

            }
            else
            {
                switch (response.code)
                {
                    case 1:
                        alertTextSignUp.text = "Invalid credentials2";
                        break;
                    case 2:
                        alertTextSignUp.text = "Username is already taken";
                        break;
                    case 3:
                        alertTextSignUp.text = "Password is unsafe";
                        break;
                    default:
                        alertTextSignUp.text = "Corruption detected";
                        break;

                }
            }
        }
        else
        {
            alertTextSignUp.text = "Error connecting to the server...";
        }


        yield return null;
    }

    private void ActivateButtons(bool toggle)
    {
        loginButton.interactable = toggle;
        createButton.interactable = toggle;
    }
    #endregion



    #region PUN call backs
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Called { OnJoinRandomFailed()}" + returnCode + " " + message);

        base.OnJoinRandomFailed(returnCode, message);
        CreateOrJoinRandomRoom();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Called {OnJoinedRoom()}");
        Debug.Log(PhotonNetwork.NickName + " Joined " + PhotonNetwork.CurrentRoom.Name); ///this will show who joined which room
        Debug.LogError(PhotonNetwork.NickName + " Joined " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("MainScene");/// this will load another scene
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Called {OnConnectedToMaster()}");

        flag = true;
        Debug.LogError("Connected to Master Server!");

        Debug.LogError("Region :" + PhotonNetwork.NetworkingClient.CloudRegion);
        //    PhotonNetwork.JoinRandomRoom();
        Debug.Log("Connected to Maseter Server!");
    }


    public override void OnJoinedLobby()
    {
        Debug.Log("Joined LObby");

    }



    #endregion

}
