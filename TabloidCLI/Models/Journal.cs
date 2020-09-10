using System;
using System.Collections.Generic;

//Created by Brett Stoudt
namespace TabloidCLI.Models
{
    public class Journal
    {
        //Created by Brett Stoudt
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDateTime { get; set; }

        public string FullJournal
        {
            get
            {
                return $"{Title}: {Content} created on: {CreateDateTime}";
            }
        }

        public override string ToString()
        {
            return FullJournal;
        }
    }
}