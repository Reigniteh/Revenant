using Newtonsoft.Json;

namespace Phasmophobia_Save_Editor
{
  public class SaveGenericProperty
  {
    [JsonProperty("__type")]
    public string Type { get; set; }

    [JsonProperty("value")]
    public object Value { get; set; }
  }
}
