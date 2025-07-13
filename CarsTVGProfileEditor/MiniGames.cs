using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsTVGProfileEditor
{
    internal class MiniGames
    {
        private int _minigameId { get; set; }
        private string _minigameName { get; set; }
        private Races.eventState _eventState { get; set; }
        public int minigameId
        {
            get { return _minigameId; }
            set
            {
                _minigameId = value;
            }
        }
        public string RaceName { get { return _minigameName; } set { _minigameName = value; } }
        public Races.eventState EventState { get { return _eventState; } set { EventState = value; } }

    }
}
