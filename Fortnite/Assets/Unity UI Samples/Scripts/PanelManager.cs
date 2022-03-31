using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;
using System.Text.RegularExpressions;
//using UnityEngine.SceneManagement.SceneManager;
using UnityEngine.SceneManagement;



public class PanelManager : MonoBehaviour {

    [SerializeField]
	public GameObject settingsWindow;
	[SerializeField]
	public GameObject settingsUpdateInfo;
	[SerializeField]
	public GameObject settingsUpdatePassword;
	[SerializeField]
	public GameObject enterGame;
	
   [SerializeField] private InputField nameInputField;
   [SerializeField] private InputField emailInputField;
   [SerializeField] private InputField passwordInputField;
   [SerializeField] private InputField confirmPasswordInputField;



	string session_token;
	string username;

	[SerializeField] private string updateEndPoint = "http://127.0.0.1:13756/account/update";







	public Animator initiallyOpen;

	private int m_OpenParameterId;
	private Animator m_Open;
	private GameObject m_PreviouslySelected;

	const string k_OpenTransitionName = "Open";
	const string k_ClosedStateName = "Closed";


	void Awake()
	{
		session_token = PlayerPrefs.GetString("token");
		username = PlayerPrefs.GetString("username");

	}

	public void OnEnable()
	{
		m_OpenParameterId = Animator.StringToHash (k_OpenTransitionName);

		if (initiallyOpen == null)
			return;

		OpenPanel(initiallyOpen);
	}

	public void OpenPanel (Animator anim)
	{
		if (m_Open == anim)
			return;

		anim.gameObject.SetActive(true);
		var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;

		anim.transform.SetAsLastSibling();

		CloseCurrent();

		m_PreviouslySelected = newPreviouslySelected;

		m_Open = anim;
		m_Open.SetBool(m_OpenParameterId, true);

		GameObject go = FindFirstEnabledSelectable(anim.gameObject);

		SetSelected(go);
	}

	static GameObject FindFirstEnabledSelectable (GameObject gameObject)
	{
		GameObject go = null;
		var selectables = gameObject.GetComponentsInChildren<Selectable> (true);
		foreach (var selectable in selectables) {
			if (selectable.IsActive () && selectable.IsInteractable ()) {
				go = selectable.gameObject;
				break;
			}
		}
		return go;
	}

	public void CloseCurrent()
	{
		if (m_Open == null)
			return;

		m_Open.SetBool(m_OpenParameterId, false);
		SetSelected(m_PreviouslySelected);
		StartCoroutine(DisablePanelDeleyed(m_Open));
		m_Open = null;
	}

	IEnumerator DisablePanelDeleyed(Animator anim)
	{
		bool closedStateReached = false;
		bool wantToClose = true;
		while (!closedStateReached && wantToClose)
		{
			if (!anim.IsInTransition(0))
				closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

			wantToClose = !anim.GetBool(m_OpenParameterId);

			yield return new WaitForEndOfFrame();
		}

		if (wantToClose)
			anim.gameObject.SetActive(false);
	}
/*
	private IEnumerator CheckSession()
    {
        Debug.Log("session Request Sent: "+ session_token);
        string name=nameInputField.text;
		string password=passwordInputField.text;
		string email=emailInputField.text;
		


        //UnityWebRequest request = UnityWebRequest.Get(updateEndPoint);
		UnityWebRequest request = UnityWebRequest.Get($"{updateEndPoint}?rUsername={name}&rPassword={password}&rGameid={username}&rEmail={email}");

       // request.SetRequestHeader("Authorization", "Bearer "+session_token);
                Debug.Log("session Request Sent "+request.url);


        



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
            }
            else// Session Active??? Redirect to room
            {
                Debug.Log("Session Active");
            }
        }
        else
        {
           // alertTextLogIn.text = "Login First(Session End)";
        }


        yield return null;
    }

*/

	private void SetSelected(GameObject go)
	{
		EventSystem.current.SetSelectedGameObject(go);
	}

	public void onClickSettings()
	{
        enterGame.SetActive(false);
		settingsWindow.SetActive(true);
	}

	public void onClickPlay()
	{
		settingsWindow.SetActive(false);
		settingsUpdatePassword.SetActive(false);
	    settingsUpdateInfo.SetActive(false);
		enterGame.SetActive(true);

	}

	public void onClickUpdateInfo()
	{
	   settingsUpdatePassword.SetActive(false);
	   settingsUpdateInfo.SetActive(true);
	}

	public void onClickUpdatePassword()
	{
	   settingsUpdateInfo.SetActive(false);
       settingsUpdatePassword.SetActive(true);

	}

	public void onClickEnterOwnWorld()
	{
	    UnityEngine.SceneManagement.SceneManager.LoadScene("RoomScene");

	}
	public void onClickUploadButtonPassword()
	{
		Debug.Log("Pressed Update Password");
		  if(passwordInputField.text==null || confirmPasswordInputField.text==null)
		  {
			  Debug.Log("Enter Password First!"+passwordInputField.text+"  "+confirmPasswordInputField.text);
		  }
		  else if(passwordInputField.text!=confirmPasswordInputField.text)
	      {
			  Debug.Log("Password does not Match");
		  }
		  else
             StartCoroutine(UpdateAccount());

            
	}

	public void onClickUploadButtonInfo()
	{
				Debug.Log("Pressed Update Info");

		  if(nameInputField!=null && emailInputField!=null)
             StartCoroutine(UpdateAccount());
		  else
		    Debug.Log("Enter Valid Information");
            
	}

	private IEnumerator UpdateAccount()
    {
        Debug.Log("session Request Sent: "+ session_token);
        string name=nameInputField.text;
		string password=passwordInputField.text;
		string email=emailInputField.text;
		


        //UnityWebRequest request = UnityWebRequest.Get(updateEndPoint);
		UnityWebRequest request = UnityWebRequest.Get($"{updateEndPoint}?rUsername={name}&rPassword={password}&rGameid={username}&rEmail={email}");

       // request.SetRequestHeader("Authorization", "Bearer "+session_token);
                Debug.Log("session Request Sent "+request.url);


        



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
                Debug.Log("Account Info Updated");
            }
            else// Session Active??? Redirect to room
            {
                Debug.Log("Update Failed");
            }
        }
        else
        {
           // alertTextLogIn.text = "Login First(Session End)";
        }


        yield return null;
    }

}
