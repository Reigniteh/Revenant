using System;
using System.Collections.Generic;
using System.Linq;

namespace Phasmophobia_Save_Editor
{
  internal class GameInfo
  {
    public static Dictionary<string, EvidenceType[]> GhostEvidence = new Dictionary<string, EvidenceType[]>()
    {
      {
        "Banshee",
        new EvidenceType[3]
        {
          EvidenceType.DOTS,
          EvidenceType.FINGERPRINTS,
          EvidenceType.GHOST_ORB
        }
      },
      {
        "Demon",
        new EvidenceType[3]
        {
          EvidenceType.FINGERPRINTS,
          EvidenceType.FREEZING_TEMPERATURES,
          EvidenceType.GHOST_WRITING
        }
      },
      {
        "Deogen",
        new EvidenceType[3]
        {
          EvidenceType.DOTS,
          EvidenceType.GHOST_WRITING,
          EvidenceType.SPIRIT_BOX
        }
      },
      {
        "Goryo",
        new EvidenceType[3]
        {
          EvidenceType.DOTS,
          EvidenceType.EMF_LEVEL_5,
          EvidenceType.FINGERPRINTS
        }
      },
      {
        "Hantu",
        new EvidenceType[3]
        {
          EvidenceType.FINGERPRINTS,
          EvidenceType.FREEZING_TEMPERATURES,
          EvidenceType.GHOST_ORB
        }
      },
      {
        "Jinn",
        new EvidenceType[3]
        {
          EvidenceType.EMF_LEVEL_5,
          EvidenceType.FINGERPRINTS,
          EvidenceType.SPIRIT_BOX
        }
      },
      {
        "Mare",
        new EvidenceType[3]
        {
          EvidenceType.GHOST_ORB,
          EvidenceType.GHOST_WRITING,
          EvidenceType.SPIRIT_BOX
        }
      },
      {
        "Moroi",
        new EvidenceType[3]
        {
          EvidenceType.FREEZING_TEMPERATURES,
          EvidenceType.GHOST_WRITING,
          EvidenceType.SPIRIT_BOX
        }
      },
      {
        "Myling",
        new EvidenceType[3]
        {
          EvidenceType.EMF_LEVEL_5,
          EvidenceType.FINGERPRINTS,
          EvidenceType.GHOST_WRITING
        }
      },
      {
        "Obake",
        new EvidenceType[3]
        {
          EvidenceType.EMF_LEVEL_5,
          EvidenceType.FINGERPRINTS,
          EvidenceType.GHOST_ORB
        }
      },
      {
        "Oni",
        new EvidenceType[3]
        {
          EvidenceType.DOTS,
          EvidenceType.EMF_LEVEL_5,
          EvidenceType.FREEZING_TEMPERATURES
        }
      },
      {
        "Onryo",
        new EvidenceType[3]
        {
          EvidenceType.FREEZING_TEMPERATURES,
          EvidenceType.GHOST_ORB,
          EvidenceType.SPIRIT_BOX
        }
      },
      {
        "Phantom",
        new EvidenceType[3]
        {
          EvidenceType.DOTS,
          EvidenceType.FINGERPRINTS,
          EvidenceType.SPIRIT_BOX
        }
      },
      {
        "Poltergeist",
        new EvidenceType[3]
        {
          EvidenceType.FINGERPRINTS,
          EvidenceType.GHOST_WRITING,
          EvidenceType.SPIRIT_BOX
        }
      },
      {
        "Raiju",
        new EvidenceType[3]
        {
          EvidenceType.DOTS,
          EvidenceType.EMF_LEVEL_5,
          EvidenceType.GHOST_ORB
        }
      },
      {
        "Revenant",
        new EvidenceType[3]
        {
          EvidenceType.FREEZING_TEMPERATURES,
          EvidenceType.GHOST_ORB,
          EvidenceType.GHOST_WRITING
        }
      },
      {
        "Shade",
        new EvidenceType[3]
        {
          EvidenceType.EMF_LEVEL_5,
          EvidenceType.FREEZING_TEMPERATURES,
          EvidenceType.GHOST_WRITING
        }
      },
      {
        "Spirit",
        new EvidenceType[3]
        {
          EvidenceType.EMF_LEVEL_5,
          EvidenceType.GHOST_WRITING,
          EvidenceType.SPIRIT_BOX
        }
      },
      {
        "Thaye",
        new EvidenceType[3]
        {
          EvidenceType.DOTS,
          EvidenceType.GHOST_WRITING,
          EvidenceType.GHOST_ORB
        }
      },
      {
        "The Twins",
        new EvidenceType[3]
        {
          EvidenceType.EMF_LEVEL_5,
          EvidenceType.FREEZING_TEMPERATURES,
          EvidenceType.SPIRIT_BOX
        }
      },
      {
        "Wraith",
        new EvidenceType[3]
        {
          EvidenceType.DOTS,
          EvidenceType.EMF_LEVEL_5,
          EvidenceType.SPIRIT_BOX
        }
      },
      {
        "Yokai",
        new EvidenceType[3]
        {
          EvidenceType.DOTS,
          EvidenceType.GHOST_ORB,
          EvidenceType.SPIRIT_BOX
        }
      },
      {
        "Yurei",
        new EvidenceType[3]
        {
          EvidenceType.DOTS,
          EvidenceType.FREEZING_TEMPERATURES,
          EvidenceType.GHOST_ORB
        }
      }
    };

    public static string TranslateEvidenceType(EvidenceType et)
    {
      switch (et)
      {
        case EvidenceType.DOTS:
          return "D.O.T.S";
        case EvidenceType.EMF_LEVEL_5:
          return "EMF Level 5";
        case EvidenceType.FINGERPRINTS:
          return "Fingerprints";
        case EvidenceType.FREEZING_TEMPERATURES:
          return "Freezing Temperatures";
        case EvidenceType.GHOST_ORB:
          return "Ghost Orb";
        case EvidenceType.GHOST_WRITING:
          return "Ghost Writing";
        case EvidenceType.SPIRIT_BOX:
          return "Spirit Box";
        default:
          return (string) null;
      }
    }

    public static string[] GetEvidenceTypesByGhostName(string name) => !GameInfo.GhostEvidence.ContainsKey(name) ? (string[]) null : ((IEnumerable<EvidenceType>) GameInfo.GhostEvidence[name]).Select<EvidenceType, string>((Func<EvidenceType, string>) (x => GameInfo.TranslateEvidenceType(x))).ToArray<string>();
  }
}
