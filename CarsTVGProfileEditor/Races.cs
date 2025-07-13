using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsTVGProfileEditor
{
    internal class Races
    {
        public enum eventState
        {
            //ids for eventState
            Locked = 0,
            UnlockedInArcadeInvisibleInStoryMode = 128,
            UnplayedUnlocked = 129,
            Unplayed = 130,
            CompletedNoTropiesAquired = 131,
            CompletedAllTrophiesAquired = 132
        }
        private int _raceId { get; set; }
        private string _raceName { get; set; }
        private eventState _raceState { get; set; }
        public int RaceId { get { return _raceId; } 
            set {
                _raceId = value;
            }
        } 

        public string RaceName { get { return _raceName; } set { _raceName = value; } }
        public eventState RaceState { get { return _raceState; } set { _raceState = value; } }


        public static List<Races> LoadRacesFromSave(List<Races> racesList, byte[] saveFileInBytes)
        {
            for (int i = 0; i < racesList.Count; i++)
            {
                if (saveFileInBytes[racesList[i].RaceId] == (byte)eventState.UnlockedInArcadeInvisibleInStoryMode)
                {
                    racesList[i].RaceState = eventState.UnlockedInArcadeInvisibleInStoryMode;
                    continue;
                }
                if (saveFileInBytes[racesList[i].RaceId] == (byte)eventState.UnplayedUnlocked)
                {
                    racesList[i].RaceState = eventState.UnplayedUnlocked;
                    continue;
                }
                if (saveFileInBytes[racesList[i].RaceId] == (byte)eventState.Unplayed)
                {
                    racesList[i].RaceState = eventState.Unplayed;
                    continue;
                }
                if (saveFileInBytes[racesList[i].RaceId] == (byte)eventState.CompletedNoTropiesAquired)
                {
                    racesList[i].RaceState = eventState.CompletedNoTropiesAquired;
                    continue;
                }
                if (saveFileInBytes[racesList[i].RaceId] == (byte)eventState.CompletedAllTrophiesAquired)
                {
                    racesList[i].RaceState = eventState.CompletedAllTrophiesAquired;
                    continue;
                }
                racesList[i].RaceState = eventState.Locked;
            }




            return racesList;
        }
        public static byte[] ApplyChangesToRacesList(List<Races> changedRaces)
        {


            return null;
        }



        public static List<Races> MakeClearRacesList()
        {
            List<Races> racesList = new List<Races>();
            racesList.Add(new Races() { RaceId = 0x5b, RaceName = "Radiator Springs Grand Prix", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0x60, RaceName = "Radiator Cap Circuit", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0x64, RaceName = "Sally's Sunshine Circuit", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0x76, RaceName = "Doc's Challenge", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0x79, RaceName = "Palm Mile Speedway", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0x93, RaceName = "Boostin' with Fillmore", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0x98, RaceName = "North Desert Dash", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xa0, RaceName = "Sarge's Off-Road Challenge", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xa4, RaceName = "Motor Speedway of the South", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xaa, RaceName = "Sheriff's Chase", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xc5, RaceName = "Rustbucket Race-O-Rama", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xc9, RaceName = "Ornament Valley Circuit", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xce, RaceName = "Sun Valley International Raceway", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xd2, RaceName = "Sally's Wheel Well Sprint", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xd5, RaceName = "Doc's Check-Up", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xda, RaceName = "Tailfin Pass Circuit", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xdd, RaceName = "Monster Truck Mayhem", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xe1, RaceName = "Chick's Challenge", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xe6, RaceName = "Delinquent Road Hazards", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xee, RaceName = "Smasherville International Speedway", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xf2, RaceName = "Radiator Springs GP", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xf6, RaceName = "Tailfin Pass GP", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xfa, RaceName = "Ornament Valley GP", RaceState = Races.eventState.Locked });
            racesList.Add(new Races() { RaceId = 0xfe, RaceName = "Los Angeles International Speedway", RaceState = Races.eventState.Locked });
            return racesList;
        }

    }

}
