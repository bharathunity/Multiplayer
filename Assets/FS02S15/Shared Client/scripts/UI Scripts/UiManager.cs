using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    #region Serialize private fields
    /// <summary>
    /// Time spent
    /// </summary>
    [SerializeField] TMP_Text _timeSpend;

    /// <summary>
    /// Ping 
    /// </summary>
    [SerializeField] TMP_Text _ping;

    /// <summary>
    /// Player state
    /// </summary>
    [SerializeField] TMP_Text _playerState;

    /// <summary>
    /// Shut down button
    /// </summary>
    [SerializeField] private RectTransform _playerCanvas;

    /// <summary>
    /// Shared mode button.
    /// </summary>
    [SerializeField] private RectTransform _sharedModeUi;
    #endregion

    #region Public properties
    public static UiManager Instance;
    #endregion

    #region Monobehaviour callbacks
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    private void Update()
    {
        if (_timeSpend.gameObject.activeInHierarchy)
        {
            _timeSpend.text = MathF.Round(Time.time, 0).ToString();
        }
        
    }
    #endregion

    #region Public methods
    /// <summary>
    /// Enable or disable shut down.
    /// </summary>
    /// <param name="value"></param>
    public void TogglePlayerCanvas(bool value)
    {
        _playerCanvas?.gameObject.SetActive(value);
    }

    /// <summary>
    /// Enable or disable shared mode button.
    /// </summary>
    /// <param name="value"></param>
    public void ToggleSharedModeUi(bool value)
    {
        _sharedModeUi?.gameObject.SetActive(value);
    }

    /// <summary>
    /// Enable or disable the time spend text
    /// </summary>
    /// <param name="value"></param>
    public void ToggleTimeSpendText(bool value)
    {
        _timeSpend.gameObject.SetActive(value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void UpdateTimeSpend(float value)
    {
        _timeSpend.text = value.ToString();
    }

    public void UpdatePing(string value)
    {
        _ping.text = value;
    }

    public void UpdatePlayerState(string message)
    {
        _playerState.text = message;
    }
    #endregion


}
