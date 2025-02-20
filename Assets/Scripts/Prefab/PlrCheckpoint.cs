using UnityEngine;

public class PlrCheckpoint : MonoBehaviour, ITouchable
{
    [field: Header("Inherited from `ITouchable`")]
    [field: SerializeField] public bool Enabled { get; set; } = true;
    [field: SerializeField] public bool HideOnStartup { get; set; }

    [field: Header("Trigger Specific")]
    [field: SerializeField] private bool UseNonPersistantCheckpoints = true;

    [field: SerializeField] private bool UseSceneName = true;
    [field: SerializeField] private bool HideOutput = true;

    [field: SerializeField] private string OverrideName;
    public Transform OverrideTransform;

    public void Entered(PlayerSystem Player)
    {
        if (UseNonPersistantCheckpoints)
        {
            if (Player.CurrentCheckpoint == this) return;
            Player.CurrentCheckpoint = this;

            return;
        }

        SaveData data = DataHandler.Instance.GetCachedData();

        if (data.checkpointName == gameObject.name)
        {
            if (!HideOutput) Debug.LogWarning(name + " | You've already registered a Checkpoint with the name: " + gameObject.name + "! Please use a different Checkpoint to save!");
            return;
        }

        if (UseSceneName && GameSystem.Instance.IsCurrentSceneAValidLevel()) data.levelName = GameSystem.Instance.GetCurrentLevelName();

        data.checkpointName = (!string.IsNullOrWhiteSpace(OverrideName)) ? OverrideName : gameObject.name;

        Transform target = (OverrideTransform != null) ? OverrideTransform : transform;
        data.checkpointPosition = DataHandler.Instance.ConvertVector3ToFloatArray(target.position);
        data.checkpointRotation = DataHandler.Instance.ConvertVector3ToFloatArray(target.rotation.eulerAngles);

        DataHandler.Instance.SetCachedData(data);
        bool result = DataHandler.Instance.SaveCachedDataToFile();

        if (HideOutput) return;

        if (result) Debug.Log(name + " Successfully saved: " + DataHandler.Instance.GetFileName() + " to disk!");
        else Debug.LogWarning(name + " Failed to save: " + DataHandler.Instance.GetFileName() + " to disk... :(");
    }

    public void Left(PlayerSystem Player) { }

    public void Staying(PlayerSystem Player) { }

    private void Awake() => GetComponent<ITouchable>().SetupTrigger(gameObject);
}
