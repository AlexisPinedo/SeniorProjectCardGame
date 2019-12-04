using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Name screen canvas handler with references to other relevant canvases and the Photon manager.
/// </summary>
public class NameScreen : MonoBehaviour
{
    #region UI references
    [SerializeField]
    private GameObject nameScreen, photonManager, mainMenuScreen;

    [SerializeField]
    private GameObject createNameBtn;

    [SerializeField]
    private InputField nameInp;
    #endregion

    private readonly int minNameLen = 5;

    private void OnEnable()
    {
        nameInp.text = "";
    }

    #region UI Methods
    public void OnClick_CreateNameBtn()
    {
        if (nameInp.text.Length >= minNameLen)
        {
            nameScreen.SetActive(false);
            photonManager.SetActive(true);
        }
    }

    public void OnNameField_Changed()
    {
        if (nameInp.text.Length >= minNameLen)
        {
            createNameBtn.SetActive(true);
        }
        else
        {
            createNameBtn.SetActive(false);
        }
    }
    #endregion
}
