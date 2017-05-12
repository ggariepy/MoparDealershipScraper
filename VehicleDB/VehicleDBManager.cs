using System.Collections.Generic;
using System.Linq;
using System.Data;
using MongoDB;
using MongoDB.Configuration;
using VehicleRecords;
using System.IO;
using System.Drawing;

namespace VehicleDB
{
    public class VehicleDBManager
    {
        public static string ConnectionString = "Server=127.0.0.1";
        public static MongoConfigurationBuilder config = new MongoConfigurationBuilder();

        static VehicleDBManager()
        {
            config.ConnectionString(ConnectionString);
        }

        #region Get Methods

        public static List<DEALERSHIP> GetDealerships(string ParserType)
        {
            List<DEALERSHIP> retval = null;
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);

                IMongoCollection<Document> collDealers = db.GetCollection<Document>(DEALERSHIP.TableName);

                Document qDealer = new Document();
                qDealer["PARSER_TYPE"] = ParserType;
                qDealer["COLLECT_DATA"] = "YES";

                DEALERSHIP[] founddealers = DEALERSHIP.GetDEALERSHIPsFromQueryDocument(collDealers, qDealer);

                if (founddealers != null && founddealers.Count<DEALERSHIP>() > 0 )
                {
                    retval = founddealers.ToList<DEALERSHIP>();
                }
                mongo.Disconnect();
            }

            return retval;
        }

        public static IMAGE GetImage(int ImageId)
        {
            IMAGE retval = null;
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);
                IMongoCollection<Document> collImage = db.GetCollection<Document>(IMAGE.TableName);

                var qIM = new Document();
                qIM["IMAGEID"] = ImageId;

                Document resultsdoc = collImage.FindOne(qIM);

                if (resultsdoc != null)
                {
                    retval = IMAGE.GetIMAGEFromDocument(resultsdoc);
                    IMAGE.UpdateDocumentFromMEMBER(collImage, retval); // Update "times served" counter
                }
            }

            return retval;
        }

        public static PRICERANGE GetPriceRange(string ValueName)
        {
            PRICERANGE retval = null;
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);
                IMongoCollection<Document> collVehicles = db.GetCollection<Document>(PRICERANGE.TableName);

                var qPR = new Document();
                qPR["VALUENAME"] = ValueName;

                Document resultsdoc = collVehicles.FindOne(qPR);

                if (resultsdoc != null)
                    retval = PRICERANGE.GetPRICERANGEFromDocument(resultsdoc);
            }
            return retval;
        }

        public static List<PRICERANGE> GetPriceRanges()
        {
            List<PRICERANGE> retval = new List<PRICERANGE>();
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);

                IMongoCollection<Document> collPriceRange = db.GetCollection<Document>(PRICERANGE.TableName);

                Document qRanges = new Document();
                qRanges["DISPLAY_THIS_RANGE"] = "YES";

                PRICERANGE[] foundranges = PRICERANGE.GetPRICERANGEsFromQueryDocument(collPriceRange, qRanges);

                if (foundranges != null && foundranges.Count<PRICERANGE>() > 0)
                {
                    foreach (PRICERANGE range in foundranges.OrderBy(range => range.DISPLAYORDER))
                        retval.Add(range);
                }
                mongo.Disconnect();
            }

            return retval;

        }

        public static List<string> GetAllMakes(string Market)
        {
            List<string> retval = new List<string>();
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);

                IMongoCollection<Document> collVehicles = db.GetCollection<Document>(VEHICLE.TableName);

                Document qVehicles = new Document();
                if (Market.Length > 0)
                    qVehicles["MARKET"] = Market;
                qVehicles["STILL_FOR_SALE"] = "YES";

                VEHICLE[] foundvehicles = VEHICLE.GetVEHICLEsFromQueryDocument(collVehicles, qVehicles);

                if (foundvehicles != null && foundvehicles.Count<VEHICLE>() > 0)
                {
                    var result = foundvehicles.GroupBy(vehicles => vehicles.MAKE.ToUpper())
                                .Select(grp => grp.First())
                                .ToList();
                    foreach (VEHICLE MakeExample in result.OrderBy(c => c.MAKE))
                        retval.Add(MakeExample.MAKE);
                }
                mongo.Disconnect();
            }

            return retval;

        }

        public static List<string> GetAllMarkets()
        {
            List<string> retval = new List<string>();
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);

                IMongoCollection<Document> collVehicles = db.GetCollection<Document>(VEHICLE.TableName);

                Document qVehicles = new Document();
                qVehicles["STILL_FOR_SALE"] = "YES";

                VEHICLE[] foundvehicles = VEHICLE.GetVEHICLEsFromQueryDocument(collVehicles, qVehicles);

                if (foundvehicles != null && foundvehicles.Count<VEHICLE>() > 0)
                {
                    var result = foundvehicles.GroupBy(vehicles => vehicles.MARKET)
                                .Select(grp => grp.First())
                                .ToList();
                    foreach (VEHICLE MarketExample in result.OrderBy(c => c.MARKET))
                        retval.Add(MarketExample.MARKET);
                }
                mongo.Disconnect();
            }

            return retval;

        }

        public static List<string> GetModelsForMake(string Make)
        {
            List<string> retval = new List<string>();
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);

                IMongoCollection<Document> collVehicles = db.GetCollection<Document>(VEHICLE.TableName);

                Document qVehicles = new Document();
                qVehicles["MAKE"] = Make;
                qVehicles["STILL_FOR_SALE"] = "YES";

                VEHICLE[] foundvehicles = VEHICLE.GetVEHICLEsFromQueryDocument(collVehicles, qVehicles);

                if (foundvehicles != null && foundvehicles.Count<VEHICLE>() > 0)
                {
                    var result = foundvehicles.GroupBy(vehicles => vehicles.MODEL.ToUpper())
                                .Select(grp => grp.First())
                                .ToList();
                    foreach (VEHICLE ModelExample in result.OrderBy(c => c.MODEL))
                        retval.Add(ModelExample.MODEL);
                }
                mongo.Disconnect();
            }

            return retval;

        }

        public static VEHICLE GetVehicleByVIN(string VIN)
        {
            VEHICLE retval = null;
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);
                IMongoCollection<Document> collVehicles = db.GetCollection<Document>(VEHICLE.TableName);

                var qVeh = new Document();
                qVeh["VIN"] = VIN;

                Document resultsdoc = collVehicles.FindOne(qVeh);

                if (resultsdoc != null)
                    retval = VEHICLE.GetVEHICLEFromDocument(resultsdoc);
            }
                return retval;
        }

        public static List<VEHICLE> GetAllVehiclesForDealer(string DealershipName)
        {
            List<VEHICLE> retval = null;
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);

                IMongoCollection<Document> collVehicles = db.GetCollection<Document>(VEHICLE.TableName);

                Document qDealer = new Document();
                qDealer["DEALERSHIP_NAME"] = DealershipName;

                VEHICLE[] foundvehicles = VEHICLE.GetVEHICLEsFromQueryDocument(collVehicles, qDealer);

                if (foundvehicles != null && foundvehicles.Count<VEHICLE>() > 0)
                {
                    retval = foundvehicles.ToList<VEHICLE>();
                }
                mongo.Disconnect();
            }

            return retval;
        }

        public static List<VEHICLE> GetAllUnsoldVehiclesForDealer(string DealershipName)
        {
            List<VEHICLE> retval = null;
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);

                IMongoCollection<Document> collVehicles = db.GetCollection<Document>(VEHICLE.TableName);

                Document qDealer = new Document();
                qDealer["DEALERSHIP_NAME"] = DealershipName;
                qDealer["STILL_FOR_SALE"] = "YES";

                VEHICLE[] foundvehicles = VEHICLE.GetVEHICLEsFromQueryDocument(collVehicles, qDealer);

                if (foundvehicles != null && foundvehicles.Count<VEHICLE>() > 0)
                {
                    retval = foundvehicles.ToList<VEHICLE>();
                }
                mongo.Disconnect();
            }

            return retval;
        }

        public static List<VEHICLE> GetVehiclesByMarketMakeModel(string market, string make, string model, string pricerange)
        {
            List<VEHICLE> retval = null;
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);

                IMongoCollection<Document> collPriceRange = db.GetCollection<Document>(PRICERANGE.TableName);
                Document qPriceRange = new Document();
                qPriceRange["VALUENAME"] = pricerange;

                Document docPriceRange = collPriceRange.FindOne(qPriceRange);
                PRICERANGE pr= PRICERANGE.GetPRICERANGEFromDocument(docPriceRange);

                IMongoCollection<Document> collVehicles = db.GetCollection<Document>(VEHICLE.TableName);

                Document qVehicles = new Document();
                qVehicles["MARKET"] = market;

                if (make.Length > 0 && make != "All")
                    qVehicles["MAKE"] = make;

                if (model.Length > 0 && model != "All")
                    qVehicles["MODEL"] = model;

                qVehicles["STILL_FOR_SALE"] = "YES";

                string where = $"(this.CURRENT_PRICE >= {pr.LOWBOUND} && this.CURRENT_PRICE <= {pr.HIGHBOUND})";
                qVehicles.Add("$where", new Code(where));

                VEHICLE[] foundvehicles = VEHICLE.GetVEHICLEsFromQueryDocument(collVehicles, qVehicles);

                if (foundvehicles != null && foundvehicles.Count<VEHICLE>() > 0)
                {
                    retval = foundvehicles.ToList<VEHICLE>();
                }
                mongo.Disconnect();
            }

            return retval;
        }


        #endregion

        #region Insert Methods

        // Convert image to bytearray
        // http://net-informations.com/q/faq/imgtobyte.html
        private static byte[] imgToByteArray(Image img)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                img.Save(mStream, img.RawFormat);
                return mStream.ToArray();
            }
        }

        public static int InsertImageFromFile(string filename, string contenttype)
        {
            int counter = 0;

            Image img = Image.FromFile(filename);
            byte[] ImageData = imgToByteArray(img);

            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);
                IMongoCollection<Document> collImage;
                collImage = db.GetCollection<Document>(IMAGE.TableName);

                var cursor = collImage.FindAll();

                if (cursor.Documents.Count() > 0)
                {
                    counter = cursor.Documents.Count();
                }

                IMAGE pic = new IMAGE();
                pic.ContentType = contenttype;
                pic.ImageData = ImageData;
                pic.ImageId = counter;
                pic.ImageName = Path.GetFileName(filename);
                pic.TimesServed = 0;
                IMAGE.InsertDocumentFromMEMBER(collImage, pic);
                mongo.Disconnect();
            }

            img = null;
            return counter;
        }

        public static void InsertVehicle(VEHICLE veh)
        {
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);
                IMongoCollection<Document> collVehicle;
                collVehicle = db.GetCollection<Document>(VEHICLE.TableName);

                VEHICLE.InsertDocumentFromMEMBER(collVehicle, veh);
                mongo.Disconnect();
            }
        }


        #endregion

        #region Update Methods

        public static void UpdateVehicleRecord(VEHICLE veh)
        {
            using (Mongo mongo = new Mongo(config.BuildConfiguration()))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(VehicleRecords.VEHICLE_DB.DataBaseName);
                IMongoCollection<Document> collVehicles;
                collVehicles = db.GetCollection<Document>(VEHICLE.TableName);

                Document qVeh = new Document();
                qVeh["VIN"] = veh.VIN;

                collVehicles.Remove(qVeh);
                VEHICLE.InsertDocumentFromMEMBER(collVehicles, veh);
                mongo.Disconnect();
            }
        }

        #endregion
    }
}
