﻿namespace Task1Backup.Status
{
    public class Warning : Status
    {
        public Warning(string message) : base(message, (int)StatusLevel.Warning) { }
    }
}