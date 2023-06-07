﻿using System.Collections.ObjectModel;

namespace blazorServerApp.Data
{
    public class PokemonTeam : ObservableCollection<SmartPokemon?>
    {
        public static readonly int MaxTeamSize = 6;

        public bool IsEmpty
        {
            get
            {
                foreach (SmartPokemon? p in this)
                {
                    if (p != null)
                        return true;
                }
                return false;
            }
            private set => IsEmpty = value;
        }

        public PokemonTeam() 
        {
            // fill team with 6 null pokemon
            for (int i = 0; i < MaxTeamSize; i++)
            {
                Add(null);
            }
        }
        public int CountPokemon()
        {
            int count = 0;
            for (int i = 0; i < MaxTeamSize; i++)
            {
                SmartPokemon? p = this[i];
                if (p != null) count++;
            }
            return count;
        }

        // these functions should probs have been one function but oh well
        public int CountWeaknesses(string typeName)
        {
            int weaknesses = 0;
            for (int i = 0; i < MaxTeamSize; i++)
            {
                SmartPokemon? p = this[i];
                if (p == null) continue;

                Multipliers multipliers = p.GetMultipliers();

                if (multipliers.defense.TryGetValue(typeName, out double value)
                    &&
                    value > 1.0)
                {
                    weaknesses++;
                }
            }

            return weaknesses;
        }

        public int CountResistances(string typeName)
        {
            int resistances = 0;
            for (int i = 0; i < MaxTeamSize; i++)
            {
                SmartPokemon? p = this[i];
                if (p == null) continue;

                Multipliers multipliers = p.GetMultipliers();

                if (multipliers.defense.TryGetValue(typeName, out double value)
                    &&
                    value < 1.0)
                {
                    resistances++;
                }
            }

            return resistances;
        }

        public int CountCoverage(string typeName)
        {
            int coverage = 0;
            for (int i = 0; i < MaxTeamSize; i++)
            {
                SmartPokemon? p = this[i];
                if (p == null) continue;

                Multipliers multipliers = p.GetMultipliers();

                if (multipliers.coverage.TryGetValue(typeName, out bool value)
                    &&
                    value)
                {
                    coverage++;
                }
            }

            return coverage;
        }
    }
}
