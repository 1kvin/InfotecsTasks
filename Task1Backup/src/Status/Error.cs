﻿namespace Task1Backup.Status
{
    public class Error : Status
    {
        public Error(string message) : base(message, (int)StatusLevel.Error) { }
    }
}