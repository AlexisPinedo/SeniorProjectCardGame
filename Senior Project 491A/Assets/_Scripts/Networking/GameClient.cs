using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using PhotonPlayer = Photon.Realtime.Player;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

/// <summary>
/// Information saved when saving the game.
/// </summary>
public class SaveGameInfo
{
    public int MyPlayerId;
    public string RoomName;
    public string DisplayName;
    public bool MyTurn;
    public Dictionary<string, object> AvailableProperties;

    public string ToStringFull()
    {
        return string.Format("\"{0}\"[{1}] {2} ({3})", RoomName, MyPlayerId, MyTurn, SupportClass.DictionaryToString(AvailableProperties));
    }
}

/// <summary>
/// The network/connection handling class.
/// </summary>
/// <remarks>
/// This class keeps the general game state and is able to write and read it as "Room Properties".
/// Photon's room properties can be saved between sessions with Photon Turnbased.
///
/// This class also adds some properties which are useful to keep track of the game's state.
///
/// This demo uses a fair amount of room properties to save the state plus two room properties
/// that are made available in the save-game list (and the lobby):
/// "turn"     is the id of the player who's turn is next. not necessarily "who's turn was done last".
/// "players"  is a colon-separated list of the 2 player names.
/// </remarks>
public class GameClient : LoadBalancingClient
{
    // Game references
    public GameBoard board;
    public GameGui gameGui;

    // NOTE: Might not need
    private const byte MaxPlayers = 2;

    public const string PropTurn = "turn";
    public const string PropNames = "names";

    public List<SaveGameInfo> SavedGames = new List<SaveGameInfo>();

    public int TurnNumber = 1;  // NOTE: Might not need this
    public int PlayerIdToMakeThisTurn;  // who's turn this is

    // NOTE: Might not need || might just reference
    public byte MyPower = 0;
    public byte MyCurrency = 0;
    public byte OtherPower = 0;
    public byte OtherCurrency = 0;

    /// <summary>
    /// Compares the game client's turn player with the networked room's turn player.
    /// </summary>
    public bool IsMyTurn
    {
        get
        {
            // Photon rltd
            return this.PlayerIdToMakeThisTurn == this.LocalPlayer.ActorNumber;
        }
    }

    /// <summary>
    /// The Opponent, i.e., the next LocalPlayer.
    /// </summary>
    /// <returns>Null if there is no other player yet or anymore.</returns>
    public PhotonPlayer Opponent
    {
        get
        {
            PhotonPlayer opp = LocalPlayer.GetNext();
            Debug.Log("You: " + LocalPlayer.ToString() + " Opp: " + opp.ToString());

            return opp;
        }
    }

    #region Game Status' via Network
    /// <summary>
    /// True if the CurrentRoom is not null, has custom properties, and contains the key "pt".
    /// </summary>
    public bool GameIsLoaded
    {
        get
        {
            return CurrentRoom != null
                && CurrentRoom.CustomProperties != null
                && CurrentRoom.CustomProperties.ContainsKey("pt");
        }
    }

    /// <summary>
    /// True if the CurrentRoom is not null and has both players present.
    /// </summary>
    public bool GameCanStart
    {
        get
        {
            return CurrentRoom != null
                && CurrentRoom.Players.Count == MaxPlayers;
        }
    }

    /// <summary>
    /// TODO
    /// </summary>
    public bool GameWasAbandoned
    {
        get
        {
            return CurrentRoom != null
                && CurrentRoom.Players.Count < 2
                && CurrentRoom.CustomProperties.ContainsKey("flips");
        }
    }
    #endregion

    #region Overridden LBC Methods
    public override void OnOperationResponse(OperationResponse operationResponse)
    {
        base.OnOperationResponse(operationResponse);

        switch (operationResponse.OperationCode)
        {
            case OperationCode.WebRpc:
                if (operationResponse.ReturnCode == 0)
                {
                    // TODO
                }
                break;

            case OperationCode.JoinGame:
            case OperationCode.CreateGame:
                // TODO
                break;

            case OperationCode.JoinRandomGame:
                // TODO
                break;
        }
    }

    public override void OnEvent(EventData photonEvent)
    {
        base.OnEvent(photonEvent);

        switch (photonEvent.Code)
        {
            case EventCode.PropertiesChanged:
                // TODO
                Debug.Log(photonEvent.Code.ToString() + " not implemented yet");
                break;

            case EventCode.Join:
                Debug.Log("Room joined");
                if (CurrentRoom.Players.Count == 2 && CurrentRoom.IsOpen)
                {
                    // Not anymore
                    CurrentRoom.IsOpen = false;
                    CurrentRoom.IsVisible = false;
                    // SavePlayersInProps();
                }
                break;

            case EventCode.Leave:
                if (CurrentRoom.Players.Count == 1 && !GameWasAbandoned)
                {
                    CurrentRoom.IsOpen = true;
                    CurrentRoom.IsVisible = true;
                }
                break;
        }
    }

    /// <summary>
    /// Handles what happens when the game's network status changes.
    /// </summary>
    /// <param name="statusCode"></param>
    public override void OnStatusChanged(StatusCode statusCode)
    {
        base.OnStatusChanged(statusCode);

        switch (statusCode)
        {
            case StatusCode.Exception:
            case StatusCode.ExceptionOnReceive:
            case StatusCode.TimeoutDisconnect:
            case StatusCode.DisconnectByServerTimeout:
            case StatusCode.DisconnectByServerLogic:
                Debug.Log(string.Format(
                    "Error on connection level. StatusCode: {0}", statusCode));
                break;
            case StatusCode.ExceptionOnConnect:
                Debug.LogWarning(string.Format(
                    "Exception on connection level. Is the server running? Is the address ({0}) reachable?", CurrentServerAddress));
                break;
            case StatusCode.Disconnect:
                SavedGames.Clear();
                break;
        }
    }

    public override void DebugReturn(DebugLevel level, string message)
    {
        base.DebugReturn(level, message);
        Debug.Log(message);
    }
    #endregion

    private void OnWebRpcResponse(WebRpcResponse response)
    {
        Debug.Log(string.Format(
            "OnWebRpcResponse. Code: {0} Content: {1}",
            response.ResultCode,
            SupportClass.DictionaryToString(response.Parameters)));

        if (response.ResultCode == 0)
        {
            if (response.Parameters == null)
            {
                Debug.Log("WebRpc executed ok but didn't get content back. This happens for empty save-game lists.");
                // gameGui.GameListUpdate();
                return;
            }

            if (response.Name.Equals("GetGameList"))
            {
                SavedGames.Clear();

                foreach (KeyValuePair<string, object> pair in response.Parameters)
                {
                    // per key (room name, we send
                    // "ActorNr", which is the PlayerId/ActorNumber this user had in the room
                    // "Properties", which is another Dictionary<string, object> w/ the props that the lobby sees
                    Dictionary<string, object> roomValues = pair.Value as Dictionary<string, object>;

                    SaveGameInfo sgi = new SaveGameInfo();
                    sgi.RoomName = pair.Key;
                    sgi.DisplayName = pair.Key; // Might have a better display name for this room, see below.
                    sgi.MyPlayerId = (int)roomValues["ActorNr"];
                    sgi.AvailableProperties = roomValues["Properties"] as Dictionary<string, object>;

                    // Determine if it's our turn & if we know the opp's name
                    if (sgi.AvailableProperties != null)
                    {
                        if (sgi.AvailableProperties.ContainsKey(PropTurn))
                        {
                            int nextPlayer = (int)sgi.AvailableProperties[PropTurn];
                            sgi.MyTurn = nextPlayer == sgi.MyPlayerId;
                        }

                        // PropNames is set to a list of the player names. This can easily be turned into a name for the game to display.
                        if (sgi.AvailableProperties.ContainsKey(PropNames))
                        {
                            string display = (string)sgi.AvailableProperties[PropNames];
                            display = display.ToLower();
                            display = display.Replace(NickName.ToLower(), "");
                            display = display.Replace(";", "");
                            sgi.DisplayName = "& " + display;
                        }
                    }

                    SavedGames.Add(sgi);
                }
                // gameGui.GameListUpdate();
            }
        }
    }

    public void SaveBoardToProperties()
    {
        //PhotonHashtable boardProps = board.GetBoardAsCustomProperties();

    }
}
