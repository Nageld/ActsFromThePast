using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using MegaCrit.Sts2.Core.Saves;

namespace ActsFromThePast;

public static class AftpConfigManager
{
    private static AftpConfig? _cachedConfig;
    private const string FILENAME = "aftp_config.json";

    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver()
    };

    public static AftpConfig GetConfig()
    {
        return LoadConfig();
    }

    private static AftpConfig LoadConfig()
    {
        string path = SaveManager.Instance.GetProfileScopedPath(FILENAME);

        if (!Godot.FileAccess.FileExists(path))
        {
            var defaultConfig = new AftpConfig();
            _cachedConfig = defaultConfig;
            SaveConfig();
            return defaultConfig;
        }

        using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
        string json = file.GetAsText(true);

        return JsonSerializer.Deserialize<AftpConfig>(json, _jsonOptions) ?? new AftpConfig();
    }

    public static void SaveConfig()
    {
        var config = _cachedConfig ?? new AftpConfig();
        string path = SaveManager.Instance.GetProfileScopedPath(FILENAME);
        string json = JsonSerializer.Serialize(config, _jsonOptions);

        using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Write);
        file.StoreString(json);
    }
}