﻿namespace FormBuilder.Models
{
    using FormBuilder.Enums;

    internal abstract class Node
    {
        public abstract NodeType Type { get; }
    }
}
