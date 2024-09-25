﻿namespace LabWork.Domain.GraphElements
{
    public class Place
    {
        public required int Id { get; set; }

        public Position Сoordinates { get; set; }

        public FigureParameters Parameters { get; set; }

        public IEnumerable<Token> Tokens { get; set; } = new List<Token>();

        public Dictionary<int, Transition> Connections { get; set; } = new Dictionary<int, Transition>();
    }
}