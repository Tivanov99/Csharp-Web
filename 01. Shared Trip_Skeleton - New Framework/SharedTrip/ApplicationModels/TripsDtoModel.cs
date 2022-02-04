﻿using System;

namespace SharedTrip.ApplicationModels
{
    public class TripsDtoModel
    {
        public string Id { get; set; }
        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public int Seats { get; set; }
    }
}
