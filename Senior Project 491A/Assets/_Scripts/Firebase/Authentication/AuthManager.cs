using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Google;
//using Facebook.Unity;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    // Make AuthManager into a singleton
    public static AuthManager sharedInstance = null;

    // Firebase API Variables
    private Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser user;

    // Create delegates / events 
    public delegate IEnumerator AuthCallBack(Task<Firebase.Auth.FirebaseUser> task, string operation);
    public event AuthCallBack authCallback;

    private DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;

    private bool signUp;
    private bool authenticationFlow = false;

    //private GoogleSignInConfiguration googleConfiguration;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    #region SDK Initialization

    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase Dependencies");
            }
        });

        //// FACEBOOK Initialization
        //if (!FB.IsInitialized)
        //{
        //    FB.Init(InitCallBack, OnHideUnity);
        //}
        //else
        //{
        //    FB.ActivateApp();
        //}

        //// GOOGLE Configuration
        //googleConfiguration = new GoogleSignInConfiguration()
        //{
        //    RequestIdToken = true,
        //    WebClientId = "831562947652-s4r2v05kav6nde54nf02amchu3udcsaf.apps.googleusercontent.com"
        //};
    }


    void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance; // hooks up variable to project
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);

    }

    // Facebook call back for initialization
    //void InitCallBack()
    //{
    //    if (FB.IsInitialized)
    //    {
    //        FB.ActivateApp();
    //    }
    //    else
    //    {
    //        Debug.LogError("FAILED to initialize the FB SDK");
    //    }
    //}

    void OnHideUnity(bool isGameShown)
    {
        if (isGameShown)
        {
            Time.timeScale = 0; // pause game while FB app is open
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    // Gets current user by Tracking the state changes of the auth object
    // If the user closes the app while sign in, the user stays signed in
    // Also tracks if the user signs out
    void AuthStateChanged(object sender, EventArgs args)
    {
        if (auth.CurrentUser != null)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
                
            }

            user = auth.CurrentUser;
            if (signedIn)
            {
                // Do something if the user returns 
                // Load Home Screen
                if (!authenticationFlow)
                    SceneManager.LoadScene("Lobby");
            }
        }
    }

    #endregion

    #region EMAIL / PASSWORD METHODS

    // Signs the user up and creates a Firebase Account via Email / Password
    public void SignUpNewUserWithEmail(string email, string password)
    {
        authenticationFlow = true;
        
        // pass user info to Firebase Project 
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            StartCoroutine(authCallback(task, "sign_up"));
        });
    }

    // Log the user in with email and password
    public void SignInUserWithEmail(string email, string password)
    {
        // pass user info to Firebase project to look up
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            StartCoroutine(authCallback(task, "login"));
        });
    }

    #endregion

    #region FACEBOOK AUTHENTICATION


    //public void FBLogin(string operation)
    //{
    //    if (operation == "fb_sign_up")
    //        signUp = true;

    //    List<string> permissions = new List<string>
    //    {
    //        "public_profile",
    //        "email"
    //    };

    //    FB.LogInWithReadPermissions(permissions, FBAuthCallback);
    //}

    //void FBAuthCallback(ILoginResult result)
    //{
    //    if (FB.IsLoggedIn)
    //    {
    //        // Creates Access Token
    //        var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;

    //        // print current Access Token's User ID
    //        Debug.Log("FB USER ID: " + aToken.UserId);

    //        // Print the granted permissions for the access token
    //        foreach (string perm in aToken.Permissions)
    //        {
    //            Debug.Log(perm);
    //        }

    //        SignInUserWithFB(aToken);
    //    }
    //    else
    //    {
    //        Debug.Log("User cancelled login");
    //    }
    //}

    //// Generate Firebase Credentials with the created access token
    //void SignInUserWithFB(AccessToken aToken)
    //{
    //    Credential credential =
    //        FacebookAuthProvider.GetCredential(aToken.TokenString);

    //    auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
    //    {
    //        if (task.IsFaulted || task.IsCanceled)
    //        {
    //            auth.SignOut();
    //        }
    //        else
    //        {
    //            FirebaseUser newUser = task.Result;

    //            //StartCoroutine(ContinueFBAuth(task));
    //            StartCoroutine(signUp ? authCallback(task, "facebook_sign_up") : authCallback(task, "facebook_login"));
    //        }
    //    });
    //}

    /*
    // Lookup Database for whether or not the user already exists
    IEnumerator ContinueFBAuth(Task<FirebaseUser> task)
    {
        bool exists = false;
        yield return new WaitForSeconds(0.5f);

        //TODO: set exists based off the value retrieved from the database

        StartCoroutine(exists ? authCallback(task, "facebook_login") : authCallback(task, "facebook_sign_up"));

    }
    */

    #endregion

    #region GOOGLE AUTHENTICATION

    ////GOOGLE Login 
    //public void GoogleLogin(string operation)
    //{
    //    GoogleSignIn.Configuration = googleConfiguration;

    //    Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn();

    //    TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser>();

    //    // Handles the users sign in and generates credentials
    //    signIn.ContinueWith(task =>
    //    {
    //        if (task.IsCanceled)
    //        {
    //            signInCompleted.SetCanceled();
    //        }
    //        else if (task.IsFaulted)
    //        {
    //            signInCompleted.SetException(task.Exception);
    //        }
    //        else if (task.IsCompleted)
    //        {
    //            Credential credential =
    //                GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)task).Result.IdToken, null);

    //            auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
    //            {
    //                if (authTask.IsCanceled)
    //                {
    //                    signInCompleted.SetCanceled();
    //                }
    //                else if (authTask.IsFaulted)
    //                {
    //                    signInCompleted.SetException(authTask.Exception);
    //                }
    //                else
    //                {
    //                    signInCompleted.SetResult(((Task<FirebaseUser>)authTask).Result);
    //                    FirebaseUser newUser = authTask.Result;

    //                    // TODO: Check Realtime Database for whether or not the player exists

    //                    StartCoroutine(ContinueGoogleAuth(authTask));
    //                }
    //            });
    //        }
    //    });
    //}

    //IEnumerator ContinueGoogleAuth(Task<FirebaseUser> task)
    //{
    //    bool exists = false;
    //    yield return new WaitForSeconds(0.5f);

    //    // TODO: Set Exists based on value retrieved from the database

    //    StartCoroutine(exists ? authCallback(task, "google_login") : authCallback(task, "google_sign_up"));
    //}

    #endregion

    #region Firebase Utilities

    public void UpdateUserDisplayName(string username)
    {
        user = auth.CurrentUser;
        if (user != null)
        {
            UserProfile profile = new UserProfile
            {
                DisplayName = username
            };

            user.UpdateUserProfileAsync(profile).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("");
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("");
                    return;
                }

                Debug.Log("User profile updated username successfully");


            });
        }
    }

    public FirebaseUser GetCurrentUser()
    {
        user = auth.CurrentUser;

        return user;
    }

    public void Logout()
    {
        authenticationFlow = false;
        SceneManager.LoadScene("SignIn");
        auth.SignOut();

        // if current user is a google user
        //  GoogleSignIn.DefaultInstance.SignOut();
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    #endregion
}
