using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB;
using MongoDB.Configuration;
using MongoDB.Linq;


namespace VEHICLE_DATA
{
    public class VEHICLE_DATA
    {
        public static string DataBaseName = "VEHICLE_DATA";
    }

    public class VEHICLE
    {
        public static string TableName = "VEHICLE";
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
        public string MILEAGE;
        public string CURRENT_PRICE;
        public string VEHICLE_HISTORY;
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
            doc["MILEAGE"] = val.MILEAGE;
            doc["CURRENT_PRICE"] = val.CURRENT_PRICE;
            doc["VEHICLE_HISTORY"] = val.VEHICLE_HISTORY;
            doc["STOCK_NUMBER"] = val.STOCK_NUMBER;
            doc["DEALERSHIP_NAME"] = val.DEALERSHIP_NAME;
            doc["MODEL_CODE"] = val.MODEL_CODE;
            doc["CARFAX_URL"] = val.CARFAX_URL;
            doc["DATE_LAST_SEEN"] = val.DATE_LAST_SEEN.ToShortDateString();
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
            doc["MILEAGE"] = val.MILEAGE;
            doc["CURRENT_PRICE"] = val.CURRENT_PRICE;
            doc["VEHICLE_HISTORY"] = val.VEHICLE_HISTORY;
            doc["STOCK_NUMBER"] = val.STOCK_NUMBER;
            doc["DEALERSHIP_NAME"] = val.DEALERSHIP_NAME;
            doc["MODEL_CODE"] = val.MODEL_CODE;
            doc["CARFAX_URL"] = val.CARFAX_URL;
            doc["DATE_LAST_SEEN"] = val.DATE_LAST_SEEN.ToShortDateString();
            coll.Update(doc);
            return ret;
        }

        public static VEHICLE GetVEHICLEFromDocument(Document doc)
        {
            VEHICLE val = new VEHICLE();
            val._id = new Oid(doc["_id"].ToString());
            val.VIN = doc["VIN"].ToString();
            val.YEAR = doc["YEAR"].ToString();
            val.MAKE = doc["MAKE"].ToString();
            
            val.DATE_LAST_SEEN = DateTime.Parse(doc["DATE_LAST_SEEN"].ToString());
            return val;
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
}
