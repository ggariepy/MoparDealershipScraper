using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB;
using MongoDB.Configuration;
using MongoDB.Linq;


namespace VehicleRecords
{
    public class VEHICLE_DB
    {
        public static string DataBaseName = "VehicleRecords";
    }

    public class DEALERSHIP
    {
        public static string TableName = "Dealerships";

        public Oid _id;

        public string DEALER_NAME;
        public string DEALER_URL;
        public string PARSER_TYPE;
        public string MARKET_AREA_NAME;

        public static bool InsertDocumentFromMEMBER(IMongoCollection<Document> coll, DEALERSHIP val)
        {
            bool ret = true;
            Document doc = new Document();
            doc["DEALER_NAME"] = val.DEALER_NAME;
            doc["DEALER_URL"] = val.DEALER_URL;
            doc["PARSER_TYPE"] = val.PARSER_TYPE;
            doc["MARKET_AREA_NAME"] = val.MARKET_AREA_NAME;
            coll.Insert(doc);
            return ret;
        }

        public static bool UpdateDocumentFromMEMBER(IMongoCollection<Document> coll, DEALERSHIP val)
        {
            bool ret = true;
            Document doc = new Document();

            var q = new Document();
            q["_id"] = val._id;
            doc = coll.FindOne(q);

            if (doc.Count == 0)
            {
                return false;
            }
            doc["DEALER_NAME"] = val.DEALER_NAME;
            doc["DEALER_URL"] = val.DEALER_URL;
            doc["PARSER_TYPE"] = val.PARSER_TYPE;
            doc["MARKET_AREA_NAME"] = val.MARKET_AREA_NAME;
            coll.Save(doc);
            return ret;
        }

        public static DEALERSHIP GetDEALERSHIPFromDocument(Document doc)
        {
            DEALERSHIP theVal = new DEALERSHIP();
            theVal._id = new Oid(doc["_id"].ToString());
            theVal.DEALER_NAME = doc["DEALER_NAME"].ToString();
            theVal.DEALER_URL = doc["DEALER_URL"].ToString();
            theVal.PARSER_TYPE = doc["PARSER_TYPE"].ToString();
            theVal.MARKET_AREA_NAME = doc["MARKET_AREA_NAME"].ToString();
            return theVal;
        }

        public static DEALERSHIP[] GetDEALERSHIPsFromQueryDocument(IMongoCollection<Document> coll, Document query)
        {
            DEALERSHIP[] arr = null;
            var cursor = coll.Find(query);

            if (cursor.Documents.Count() > 0)
            {
                arr = new DEALERSHIP[cursor.Documents.Count()];

                int ind = 0;
                foreach (Document doc in cursor.Documents)
                {
                    arr[ind++] = GetDEALERSHIPFromDocument(doc);
                }
            }

            return arr;
        }
    }

    public class IMAGE
    {
        public static string TableName = "Image";

        public Oid _id;

        public int ImageId;
        public string ContentType;
        public byte[] ImageData;
        public string ImageName;
        public int TimesServed;

        public static bool InsertDocumentFromMEMBER(IMongoCollection<Document> coll, IMAGE val)
        {
            bool ret = true;
            
            Document doc = new Document();
            doc["IMAGEID"] = val.ImageId;
            doc["CONTENTTYPE"] = val.ContentType;
            doc["IMAGEDATA"] = new Binary(val.ImageData); ;
            doc["IMAGENAME"] = val.ImageName;
            doc["TIMESSERVED"] = val.TimesServed;
            coll.Insert(doc);
            return ret;
        }

        public static bool UpdateDocumentFromMEMBER(IMongoCollection<Document> coll, IMAGE val)
        {
            bool ret = true;
            Document doc = new Document();

            var q = new Document();
            q["_id"] = val._id;
            doc = coll.FindOne(q);

            if (doc.Count == 0)
            {
                return false;
            }

            doc["IMAGEID"] = val.ImageId;
            doc["CONTENTTYPE"] = val.ContentType;
            doc["IMAGEDATA"] = new Binary(val.ImageData);
            doc["IMAGENAME"] = val.ImageName;
            doc["TIMESSERVED"] = val.TimesServed;
            coll.Save(doc);
            return ret;
        }

        public static IMAGE GetIMAGEFromDocument(Document doc)
        {
            IMAGE theVal = new IMAGE();
            theVal._id = new Oid(doc["_id"].ToString());
            theVal.ImageId = int.Parse(doc["IMAGEID"].ToString());
            theVal.ContentType = doc["CONTENTTYPE"].ToString();
            theVal.ImageData = ((Binary)doc["IMAGEDATA"]).ToArray<byte>();
            theVal.ImageName = doc["IMAGENAME"].ToString();
            theVal.TimesServed = int.Parse(doc["TIMESSERVED"].ToString()) + 1;
            return theVal;
        }

        public static IMAGE[] GetIMAGEsFromQueryDocument(IMongoCollection<Document> coll, Document query)
        {
            IMAGE[] arr = null;
            var cursor = coll.Find(query);

            if (cursor.Documents.Count() > 0)
            {
                arr = new IMAGE[cursor.Documents.Count()];

                int ind = 0;
                foreach (Document doc in cursor.Documents)
                {
                    arr[ind++] = GetIMAGEFromDocument(doc);
                }
            }

            return arr;
        }
    }


    public class PRICERANGE
    {
        public static string TableName = "PriceRange";

        public Oid _id;

        public int DISPLAYORDER;
        public string VALUENAME;
        public string DISPLAYNAME;
        public int LOWBOUND;
        public int HIGHBOUND;
        public string DISPLAY_THIS_RANGE;

        public static bool InsertDocumentFromMEMBER(IMongoCollection<Document> coll, PRICERANGE val)
        {
            bool ret = true;
            Document doc = new Document();
            doc["DISPLAYORDER"] = val.DISPLAYORDER;
            doc["VALUENAME"] = val.VALUENAME;
            doc["DISPLAYNAME"] = val.DISPLAYNAME;
            doc["LOWBOUND"] = val.LOWBOUND;
            doc["HIGHBOUND"] = val.HIGHBOUND;
            doc["DISPLAY_THIS_RANGE"] = val.DISPLAY_THIS_RANGE;
            coll.Insert(doc);
            return ret;
        }

        public static bool UpdateDocumentFromMEMBER(IMongoCollection<Document> coll, PRICERANGE val)
        {
            bool ret = true;
            Document doc = new Document();

            var q = new Document();
            q["_id"] = val._id;
            doc = coll.FindOne(q);

            if (doc.Count == 0)
            {
                return false;
            }
            doc["DISPLAYORDER"] = val.DISPLAYORDER;
            doc["VALUENAME"] = val.VALUENAME;
            doc["DISPLAYNAME"] = val.DISPLAYNAME;
            doc["LOWBOUND"] = val.LOWBOUND;
            doc["HIGHBOUND"] = val.HIGHBOUND;
            doc["DISPLAY_THIS_RANGE"] = val.DISPLAY_THIS_RANGE;
            coll.Save(doc);
            return ret;
        }

        public static PRICERANGE GetPRICERANGEFromDocument(Document doc)
        {
            PRICERANGE theVal = new PRICERANGE();
            theVal._id = new Oid(doc["_id"].ToString());
            theVal.DISPLAYORDER = int.Parse(doc["DISPLAYORDER"].ToString());
            theVal.VALUENAME = doc["VALUENAME"].ToString();
            theVal.DISPLAYNAME = doc["DISPLAYNAME"].ToString();
            theVal.LOWBOUND = int.Parse(doc["LOWBOUND"].ToString());
            theVal.HIGHBOUND = int.Parse(doc["HIGHBOUND"].ToString());
            theVal.DISPLAY_THIS_RANGE = doc["DISPLAY_THIS_RANGE"].ToString();
            return theVal;
        }

        public static PRICERANGE[] GetPRICERANGEsFromQueryDocument(IMongoCollection<Document> coll, Document query)
        {
            PRICERANGE[] arr = null;
            var cursor = coll.Find(query);

            if (cursor.Documents.Count() > 0)
            {
                arr = new PRICERANGE[cursor.Documents.Count()];

                int ind = 0;
                foreach (Document doc in cursor.Documents)
                {
                    arr[ind++] = GetPRICERANGEFromDocument(doc);
                }
            }

            return arr;
        }
    }

    public class VEHICLE
    {
        public static string TableName = "Vehicles";

        public Oid _id;

        public string VIN;
        public string YEAR;
        public string MAKE;
        public string MODEL;
        public string TRIM;
        public string BODY_STYLE;
        public string ENGINE;
        public string TRANSMISSION;
        public string DRIVE_TRAIN;
        public string WHEEL_SIZE;
        public string COLOR_EXTERIOR;
        public string COLOR_INTERIOR;
        public string MARKET;
        public string MILEAGE;
        public string IMAGEIDCSV;
        public string CURRENT_PRICE;
        public string VEHICLE_HISTORY;
        public string STILL_FOR_SALE;
        public string STOCK_NUMBER;
        public string DEALERSHIP_NAME;
        public string MODEL_CODE;
        public string CARFAX_URL;
        public string DEALER_DETAIL_URL;
        public DateTime DATE_LAST_SEEN;

        public static bool InsertDocumentFromMEMBER(IMongoCollection<Document> coll, VEHICLE val)
        {
            bool ret = true;
            Document doc = new Document();
            doc["VIN"] = val.VIN;
            doc["YEAR"] = val.YEAR;
            doc["MAKE"] = val.MAKE;
            doc["MODEL"] = val.MODEL;
            doc["TRIM"] = val.TRIM;
            doc["BODY_STYLE"] = val.BODY_STYLE;
            doc["ENGINE"] = val.ENGINE;
            doc["TRANSMISSION"] = val.TRANSMISSION;
            doc["DRIVE_TRAIN"] = val.DRIVE_TRAIN;
            doc["WHEEL_SIZE"] = val.WHEEL_SIZE;
            doc["COLOR_EXTERIOR"] = val.COLOR_EXTERIOR;
            doc["COLOR_INTERIOR"] = val.COLOR_INTERIOR;
            doc["MARKET"] = val.MARKET;
            doc["IMAGEIDCSV"] = val.IMAGEIDCSV;
            doc["MILEAGE"] = val.MILEAGE;
            doc["CURRENT_PRICE"] = val.CURRENT_PRICE;
            doc["VEHICLE_HISTORY"] = val.VEHICLE_HISTORY;
            doc["STILL_FOR_SALE"] = val.STILL_FOR_SALE;
            doc["STOCK_NUMBER"] = val.STOCK_NUMBER;
            doc["DEALERSHIP_NAME"] = val.DEALERSHIP_NAME;
            doc["MODEL_CODE"] = val.MODEL_CODE;
            doc["CARFAX_URL"] = val.CARFAX_URL;
            doc["DEALER_DETAIL_URL"] = val.DEALER_DETAIL_URL;
            doc["DATE_LAST_SEEN"] = val.DATE_LAST_SEEN;
            coll.Insert(doc);
            return ret;
        }

        public static bool UpdateDocumentFromMEMBER(IMongoCollection<Document> coll, VEHICLE val)
        {
            bool ret = true;
            Document doc = new Document();

            var q = new Document();
            q["_id"] = val._id;
            doc = coll.FindOne(q);

            if (doc.Count == 0)
            {
                return false;
            }
            doc["VIN"] = val.VIN;
            doc["YEAR"] = val.YEAR;
            doc["MAKE"] = val.MAKE;
            doc["MODEL"] = val.MODEL;
            doc["TRIM"] = val.TRIM;
            doc["BODY_STYLE"] = val.BODY_STYLE;
            doc["ENGINE"] = val.ENGINE;
            doc["TRANSMISSION"] = val.TRANSMISSION;
            doc["DRIVE_TRAIN"] = val.DRIVE_TRAIN;
            doc["WHEEL_SIZE"] = val.WHEEL_SIZE;
            doc["COLOR_EXTERIOR"] = val.COLOR_EXTERIOR;
            doc["COLOR_INTERIOR"] = val.COLOR_INTERIOR;
            doc["MARKET"] = val.MARKET;
            doc["IMAGEIDCSV"] = val.IMAGEIDCSV;
            doc["MILEAGE"] = val.MILEAGE;
            doc["CURRENT_PRICE"] = val.CURRENT_PRICE;
            doc["VEHICLE_HISTORY"] = val.VEHICLE_HISTORY;
            doc["STILL_FOR_SALE"] = val.STILL_FOR_SALE;
            doc["STOCK_NUMBER"] = val.STOCK_NUMBER;
            doc["DEALERSHIP_NAME"] = val.DEALERSHIP_NAME;
            doc["MODEL_CODE"] = val.MODEL_CODE;
            doc["CARFAX_URL"] = val.CARFAX_URL;
            doc["DEALER_DETAIL_URL"] = val.DEALER_DETAIL_URL;
            doc["DATE_LAST_SEEN"] = val.DATE_LAST_SEEN;

            coll.Save(doc);
            return ret;
        }

        public static VEHICLE GetVEHICLEFromDocument(Document doc)
        {
            VEHICLE theVal = new VEHICLE();
            theVal._id = new Oid(doc["_id"].ToString());
            theVal.VIN = doc["VIN"].ToString();
            theVal.YEAR = doc["YEAR"].ToString();
            theVal.MAKE = doc["MAKE"].ToString();
            theVal.MODEL = doc["MODEL"].ToString();
            theVal.TRIM = doc["TRIM"].ToString();
            theVal.BODY_STYLE = doc["BODY_STYLE"].ToString();
            theVal.ENGINE = doc["ENGINE"].ToString();
            theVal.TRANSMISSION = doc["TRANSMISSION"].ToString();
            theVal.DRIVE_TRAIN = doc["DRIVE_TRAIN"].ToString();
            theVal.WHEEL_SIZE = doc["WHEEL_SIZE"].ToString();
            theVal.COLOR_EXTERIOR = doc["COLOR_EXTERIOR"].ToString();
            theVal.COLOR_INTERIOR = doc["COLOR_INTERIOR"].ToString();
            theVal.MARKET = doc["MARKET"].ToString();
            theVal.IMAGEIDCSV = (ReferenceEquals(null, doc["IMAGEIDCSV"])) ? "" : doc["IMAGEIDCSV"].ToString();
            theVal.MILEAGE = doc["MILEAGE"].ToString();
            theVal.CURRENT_PRICE = doc["CURRENT_PRICE"].ToString();
            theVal.VEHICLE_HISTORY = doc["VEHICLE_HISTORY"].ToString();
            theVal.STILL_FOR_SALE = (!ReferenceEquals(null, doc["STILL_FOR_SALE"])) ? doc["STILL_FOR_SALE"].ToString() : "YES";
            theVal.STOCK_NUMBER = doc["STOCK_NUMBER"].ToString();
            theVal.DEALERSHIP_NAME = doc["DEALERSHIP_NAME"].ToString();
            theVal.MODEL_CODE = doc["MODEL_CODE"].ToString();
            theVal.CARFAX_URL = doc["CARFAX_URL"].ToString();
            theVal.DEALER_DETAIL_URL = doc["DEALER_DETAIL_URL"].ToString();
            theVal.DATE_LAST_SEEN = DateTime.Parse(doc["DATE_LAST_SEEN"].ToString());

            return theVal;
        }

        public static VEHICLE[] GetVEHICLEsFromQueryDocument(IMongoCollection<Document> coll, Document query)
        {
            VEHICLE[] arr = null;
            var cursor = coll.Find(query);

            if (cursor.Documents.Count() > 0)
            {
                arr = new VEHICLE[cursor.Documents.Count()];

                int ind = 0;
                foreach (Document doc in cursor.Documents)
                {
                    arr[ind++] = GetVEHICLEFromDocument(doc);
                }
            }

            return arr;
        }

    }

    [Serializable]
    public class VehiclePriceHistory
    {
        public string VIN;
        public string Price;
        public DateTime Date_Recorded;
        public string WasFinalPrice;
    }

}
