using System;
using Battle.Unit;

namespace Helper
{
    public static class OptionsHelper
    {
        public static Options_generation.ExportOptions GetExportOptions() => Options_generation.UIOptions.exportOptions;

        public static UnitFaction GetFactionOfPlayer(CombatAffiliation player)
        {
            var output = player switch
            {
                CombatAffiliation.Player => Options_generation.UIOptions.exportOptions.PlayerFaction,
                CombatAffiliation.AI => Options_generation.UIOptions.exportOptions.AIFaction,
            };
            return output;
        }
    }
}