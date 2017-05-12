using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using VehicleDB;
using VehicleRecords;

namespace DealerParserEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            DealerdotComParser.ProcessAllDealers();
        }
    }
}
