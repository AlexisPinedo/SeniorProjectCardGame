using UnityEngine;
using UnityEngine.UI;

public class WinLoss : MonoBehaviour
{
    public Text winText;
    public Text lossText;
    public Text ratioText;

    private int wins { get; set; }
    private int losses { get; set; }

    private PlayerRecord playerRecord;

    void Awake()
    {
        DatabaseManager.sharedInstance.RetrievedPlayerRecord += SetRecord;

        DatabaseManager.sharedInstance.GetPlayerRecords();
    }

    void SetRecord(PlayerRecord record)
    {
        playerRecord = record;

        CalculateRatio(playerRecord);
    }

    void CalculateRatio(PlayerRecord record)
    {
        float ratio = record.wins /record.losses;

        winText.text = record.wins.ToString();
        lossText.text = record.wins.ToString();
        ratioText.text = $"Win/Loss Ratio: \n{ratio:0.00}";
    }

    public void IncrementWins()
    {
        playerRecord.wins++;
        DatabaseManager.sharedInstance.UpdatePlayerRecords(playerRecord.wins, playerRecord.losses);
        CalculateRatio(playerRecord);
    }

    public void IncrementLoss()
    {
        playerRecord.losses++;
        DatabaseManager.sharedInstance.UpdatePlayerRecords(playerRecord.wins, playerRecord.losses);
        CalculateRatio(playerRecord);
    }

    public void SignOut()
    {
        AuthManager.sharedInstance.Logout();
    }

    void OnDestroy()
    {
        DatabaseManager.sharedInstance.RetrievedPlayerRecord -= CalculateRatio;
    }
    
}
