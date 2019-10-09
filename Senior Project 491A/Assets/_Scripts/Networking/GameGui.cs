using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;

[RequireComponent(typeof(GameBoard))]
public class GameGui : MonoBehaviour
{
    /// <summary>
    /// Keeps part of the state and connection.
    /// </summary>
    public GameClient GameClientInstance;
    public string AppId;

    private GameBoard board;
    private int saveGameLitStartIndex = 0;  // paging for saved-game list

    private bool visible;
    public bool Visible
    {
        get { return visible; }
        set
        {
            visible = value;
            this.OnVisibleChanged();
        }
    }

    public void Awake()
    {
        // Null check
        //if (string.IsNullOrEmpty(AppId))
        //{
        //    Debug.LogError("You must enter your AppId from the Dashboard in the component: Scripts, MemoryGui, AppId before you can use this demo.");
        //    Debug.Break();
        //}

        Application.runInBackground = true;
        // TODO: Check the internals of CustomTypes
        CustomTypes.Register();

        // Start it up!
        GameClientInstance = new GameClient
        {
            AppId = this.AppId,   // Photon ID - set in inspector!
            AppVersion = "1.0"
        };

        // Connect the networking components
        board = GetComponentInChildren<GameBoard>();
        board.GameClientInstance = GameClientInstance;
        GameClientInstance.board = board;
        board.GameGui = this;
        // DisableButtons();
    }

    public void OnEnable()
    {
        if (GameClientInstance.IsConnected)
        {
            Debug.Log("Already connected");
            return;
        }

        
    }

    void OnVisibleChanged()
    {
        Debug.Log("GameGui.OnVisibleChanged. now visible: " + visible);

        if (visible)
        {
            // stuff
        }
        else
        {
            // stuff
        }
    }
}
