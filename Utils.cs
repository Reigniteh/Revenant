using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;

namespace Phasmophobia_Save_Editor
{
  internal class Utils
  {
    public static string[] GetUsers()
    {
      IEnumerable<ManagementObject> managementObjects = new ManagementObjectSearcher("SELECT * FROM Win32_UserAccount").Get().Cast<ManagementObject>().Where<ManagementObject>((Func<ManagementObject, bool>) (u => !(bool) u["Disabled"] && !(bool) u["Lockout"] && int.Parse(u["SIDType"].ToString()) == 1 && u["Name"].ToString() != "HomeGroupUser$"));
      List<string> stringList = new List<string>();
      foreach (ManagementObject managementObject in managementObjects)
        stringList.Add(managementObject["Name"].ToString());
      return stringList.ToArray();
    }

    public static Dictionary<string, string> EnumerateSaves()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (string user in Utils.GetUsers())
      {
        string path = string.Format("C://Users/{0}/AppData/LocalLow/Kinetic Games/Phasmophobia/SaveFile.txt", (object) user);
        if (File.Exists(path))
          dictionary.Add(user, path);
      }
      return dictionary;
    }
  }
}
