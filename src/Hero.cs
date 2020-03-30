using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTracker
{
    public enum SupportHero { Ana, Baptiste, Brigitte, Lucio, Mercy, Moira, Zenyatta }
    class Hero
    {
        public Hero()
        {

        }
        public SupportHero hero;

        public void SetHero(string input)
        {
            switch (input.ToUpper())
            {
                case "ANA":
                    hero = SupportHero.Ana;
                    break;
                case "BAPTISTE":
                case "BAP":
                    hero = SupportHero.Baptiste;
                    break;
                case "BRIGITTE":
                case "BRIG":
                    hero = SupportHero.Brigitte;
                    break;
                case "LUCIO":
                    hero = SupportHero.Lucio;
                    break;
                case "MERCY":
                    hero = SupportHero.Mercy;
                    break;
                case "MOIRA":
                    hero = SupportHero.Moira;
                    break;
                case "ZENYATTA":
                case "ZEN":
                    hero = SupportHero.Zenyatta;
                    break;
                default:
                    throw new Exception("Invalid hero selection");
            }
        }
    }
}
