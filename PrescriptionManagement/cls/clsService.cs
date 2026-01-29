using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using PrescriptionManagement.cls;
using System.Data;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using System.Globalization;

namespace PrescriptionManagement.cls
{
    class clsService
    {
        clsPackagemaster clsPackagemaster = new clsPackagemaster();
        public static async Task GetbasketNumber(string rfid)
        {
            #region creatdb_packagemaster

            //Packagemaster
            DataTable db_basket = new DataTable();
            db_basket.Columns.Add("_id", typeof(String));
            db_basket.Columns.Add("no", typeof(String));
            db_basket.Columns.Add("rfid", typeof(String));
            db_basket.TableName = "db_basket";
            #endregion
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://office.gd4.co.th:6426/packagemaster/order/basketrfid";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/packagemaster/order/basketrfid";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                                ""rfid"": ""{rfid}""
                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            JArray dataArray = (JArray)obj["data"];
                            // Loop through "data" objects
                            foreach (JObject dataObject in dataArray)
                            {
                                DataRow row = db_basket.Rows.Add();
                                row["_id"] = dataObject["_id"].ToString();
                                row["no"] = dataObject["no"].ToString();
                                row["rfid"] = dataObject["rfid"].ToString();
                            }
                        }
                    }

                    clsPackagemaster.dtbasket = db_basket;
                }
                catch (Exception ex)
                {
                    
                }
            }
        }
        public static async Task<DataTable> RequestDataQueue(string keyword, string type)
        {
            DataTable dtobjdata = new DataTable();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string startdate = DateTime.Now.ToString("yyyy-MM-dd");
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/queue/all";
                    apiUrl = string.Format(apiUrl);

                    string jsonContent = "";
                    // สร้างข้อมูล JSON ที่ต้องการส่ง

                    if (type == "Q")
                    {
                        jsonContent = $@"{{
                                                ""queue"": ""{ keyword}""
                                        
                                            }}";
                    }
                    else
                    {
                        jsonContent = $@"{{
                                                ""hn"": ""{ keyword}""

                                            }}";
                    }


                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            var dataToken = obj["data"];

                            if (dataToken is JArray dataArray)
                            {
                                clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();
                            }
                            else if (dataToken is JObject dataObject)
                            {
                                // ถ้าข้อมูลเป็น object เดี่ยว
                                var list = new List<Dictionary<string, object>>
                                {
                                    dataObject.ToObject<Dictionary<string, object>>()
                                };
                                clsPackagemaster.objresponseBody = list;
                            }

                            if (clsPackagemaster.objresponseBody.Count > 0 || clsPackagemaster.objresponseBody == null)
                            {
                                JArray dataArraydata = new JArray();
                                List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                                if (clsPackagemaster.objresponseBody == null || clsPackagemaster.objresponseBody.Count == 0)
                                {
                                    Console.WriteLine($"Can't Find Success");
                                }
                                else
                                {
                                    // วนลูปแสดงผลข้อมูลแต่ละ Object
                                    foreach (var dataObject in clsPackagemaster.objresponseBody)
                                    {
                                        foreach (var key in dataObject.Keys)
                                        {
                                            if (key != "authorized")
                                            {
                                                Console.WriteLine($"{key}: {dataObject[key]}");
                                                //dtobjdata.Columns.Add($"{key}", typeof(object));

                                                string columnName = key.ToString();
                                                if (dtobjdata.Columns.Contains(columnName))
                                                {
                                                    Console.WriteLine($"Column '{columnName}' exists.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Column '{columnName}' does not exist.");
                                                    dtobjdata.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                                }
                                            }

                                        }
                                        //dataArraydata = (JArray)dataObject["data"];
                                        //objdata = dataArraydata.ToObject<List<Dictionary<string, object>>>();
                                        Console.WriteLine("Data Details:");
                                        DataRow row = dtobjdata.NewRow();
                                        foreach (var key in dataObject.Keys)
                                        {
                                            if (key != "authorized")
                                            {
                                                Console.WriteLine($"{key}: {dataObject[key]}");
                                                row[key] = $"{dataObject[key]}"; // ถ้า null ให้ใส่ DBNull.Value                                       

                                            }

                                        }
                                        dtobjdata.Rows.Add(row);
                                        Console.WriteLine();
                                    }


                                    if (dtobjdata.Rows.Count > 0)
                                    {

                                        //clsPackagemaster.db_data.Merge(dtobjdata);
                                    }







                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($" Can't Find Success ");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }

            return (dtobjdata);
        }

        public static async Task<DataTable> LoadWardData(string typeword, string keyword, DateTime selectdate)
        {
            DataTable dtobjdata = new DataTable();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string startdate = selectdate.ToString("yyyy-MM-dd");
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/prescriptionstatusward";
                    apiUrl = string.Format(apiUrl);
                    string jsonContent = "";
                    if (typeword == "Take Home")
                    {
                        // สร้างข้อมูล JSON ที่ต้องการส่ง
                        jsonContent = $@"{{
                                                ""startdate"": ""{startdate}"",
                                                ""enddate"": ""{startdate}"",
                                                ""ordertypedesc"": ""{keyword}""

                                            }}";

                    }
                    else if (typeword == "wardcode")
                    {
                        // สร้างข้อมูล JSON ที่ต้องการส่ง
                        jsonContent = $@"{{
                                                ""startdate"": ""{startdate}"",
                                                ""enddate"": ""{startdate}"",
                                                ""wardcode"": ""{keyword}""

                                            }}";
                    }
                    else
                    {
                        jsonContent = $@"{{
                                                ""startdate"": ""{startdate}"",
                                                ""enddate"": ""{startdate}""

                                            }}";
                    }


                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            var dataToken = obj["data"];

                            if (dataToken is JArray dataArray)
                            {
                                clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();
                            }
                            else if (dataToken is JObject dataObject)
                            {
                                // ถ้าข้อมูลเป็น object เดี่ยว
                                var list = new List<Dictionary<string, object>>
                                {
                                    dataObject.ToObject<Dictionary<string, object>>()
                                };
                                clsPackagemaster.objresponseBody = list;
                            }

                            if (clsPackagemaster.objresponseBody.Count > 0 || clsPackagemaster.objresponseBody == null)
                            {
                                JArray dataArraydata = new JArray();
                                List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                                if (clsPackagemaster.objresponseBody == null || clsPackagemaster.objresponseBody.Count == 0)
                                {
                                    Console.WriteLine($"Can't Find Success");
                                }
                                else
                                {
                                    // วนลูปแสดงผลข้อมูลแต่ละ Object
                                    foreach (var dataObject in clsPackagemaster.objresponseBody)
                                    {
                                        foreach (var key in dataObject.Keys)
                                        {
                                            if (key != "authorized")
                                            {
                                                Console.WriteLine($"{key}: {dataObject[key]}");
                                                //dtobjdata.Columns.Add($"{key}", typeof(object));

                                                string columnName = key.ToString();
                                                if (dtobjdata.Columns.Contains(columnName))
                                                {
                                                    Console.WriteLine($"Column '{columnName}' exists.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Column '{columnName}' does not exist.");
                                                    dtobjdata.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                                }
                                            }

                                        }
                                        //dataArraydata = (JArray)dataObject["data"];
                                        //objdata = dataArraydata.ToObject<List<Dictionary<string, object>>>();
                                        Console.WriteLine("Data Details:");
                                        DataRow row = dtobjdata.NewRow();
                                        foreach (var key in dataObject.Keys)
                                        {
                                            if (key != "authorized")
                                            {
                                                Console.WriteLine($"{key}: {dataObject[key]}");
                                                row[key] = $"{dataObject[key]}"; // ถ้า null ให้ใส่ DBNull.Value                                       

                                            }

                                        }
                                        dtobjdata.Rows.Add(row);
                                        Console.WriteLine();
                                    }


                                    if (dtobjdata.Rows.Count > 0)
                                    {

                                        //clsPackagemaster.db_data.Merge(dtobjdata);
                                    }







                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($" Can't Find Success ");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }

            return (dtobjdata);
        }
        public static async Task<DataTable> RequestDataQueue(string keyword)
        {
            DataTable dtobjdata = new DataTable();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string startdate = DateTime.Now.ToString("yyyy-MM-dd");
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/queue/all";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                                ""hn"": ""{ keyword}""

                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            var dataToken = obj["data"];

                            if (dataToken is JArray dataArray)
                            {
                                clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();
                            }
                            else if (dataToken is JObject dataObject)
                            {
                                // ถ้าข้อมูลเป็น object เดี่ยว
                                var list = new List<Dictionary<string, object>>
                                {
                                    dataObject.ToObject<Dictionary<string, object>>()
                                };
                                clsPackagemaster.objresponseBody = list;
                            }

                            if (clsPackagemaster.objresponseBody.Count > 0 || clsPackagemaster.objresponseBody == null)
                            {
                                JArray dataArraydata = new JArray();
                                List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                                if (clsPackagemaster.objresponseBody == null || clsPackagemaster.objresponseBody.Count == 0)
                                {
                                    Console.WriteLine($"Can't Find Success");
                                }
                                else
                                {
                                    // วนลูปแสดงผลข้อมูลแต่ละ Object
                                    foreach (var dataObject in clsPackagemaster.objresponseBody)
                                    {
                                        foreach (var key in dataObject.Keys)
                                        {
                                            if (key != "authorized")
                                            {
                                                Console.WriteLine($"{key}: {dataObject[key]}");
                                                //dtobjdata.Columns.Add($"{key}", typeof(object));

                                                string columnName = key.ToString();
                                                if (dtobjdata.Columns.Contains(columnName))
                                                {
                                                    Console.WriteLine($"Column '{columnName}' exists.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Column '{columnName}' does not exist.");
                                                    dtobjdata.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                                }
                                            }

                                        }
                                        //dataArraydata = (JArray)dataObject["data"];
                                        //objdata = dataArraydata.ToObject<List<Dictionary<string, object>>>();
                                        Console.WriteLine("Data Details:");
                                        DataRow row = dtobjdata.NewRow();
                                        foreach (var key in dataObject.Keys)
                                        {
                                            if (key != "authorized")
                                            {
                                                Console.WriteLine($"{key}: {dataObject[key]}");
                                                row[key] = $"{dataObject[key]}"; // ถ้า null ให้ใส่ DBNull.Value                                       

                                            }

                                        }
                                        dtobjdata.Rows.Add(row);
                                        Console.WriteLine();
                                    }


                                    if (dtobjdata.Rows.Count > 0)
                                    {

                                        //clsPackagemaster.db_data.Merge(dtobjdata);
                                    }







                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($" Can't Find Success ");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }

            return (dtobjdata);
        }
        //ward
        public static async Task<DataTable> RequestGetWard(string keyword)
        {
            DataTable dtobjdata = new DataTable();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string startdate = DateTime.Now.ToString("yyyy-MM-dd");
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/master/ward/all";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{

                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            var dataToken = obj["data"];

                            if (dataToken is JArray dataArray)
                            {
                                clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();
                            }
                            else if (dataToken is JObject dataObject)
                            {
                                // ถ้าข้อมูลเป็น object เดี่ยว
                                var list = new List<Dictionary<string, object>>
                                {
                                    dataObject.ToObject<Dictionary<string, object>>()
                                };
                                clsPackagemaster.objresponseBody = list;
                            }

                            if (clsPackagemaster.objresponseBody.Count > 0 || clsPackagemaster.objresponseBody == null)
                            {
                                JArray dataArraydata = new JArray();
                                List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                                if (clsPackagemaster.objresponseBody == null || clsPackagemaster.objresponseBody.Count == 0)
                                {
                                    Console.WriteLine($"Can't Find Success");
                                }
                                else
                                {
                                    // วนลูปแสดงผลข้อมูลแต่ละ Object
                                    foreach (var dataObject in clsPackagemaster.objresponseBody)
                                    {
                                        foreach (var key in dataObject.Keys)
                                        {
                                            if (key != "authorized")
                                            {
                                                Console.WriteLine($"{key}: {dataObject[key]}");
                                                //dtobjdata.Columns.Add($"{key}", typeof(object));

                                                string columnName = key.ToString();
                                                if (dtobjdata.Columns.Contains(columnName))
                                                {
                                                    Console.WriteLine($"Column '{columnName}' exists.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Column '{columnName}' does not exist.");
                                                    dtobjdata.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                                }
                                            }

                                        }
                                        //dataArraydata = (JArray)dataObject["data"];
                                        //objdata = dataArraydata.ToObject<List<Dictionary<string, object>>>();
                                        Console.WriteLine("Data Details:");
                                        DataRow row = dtobjdata.NewRow();
                                        foreach (var key in dataObject.Keys)
                                        {
                                            if (key != "authorized")
                                            {
                                                Console.WriteLine($"{key}: {dataObject[key]}");
                                                row[key] = $"{dataObject[key]}"; // ถ้า null ให้ใส่ DBNull.Value                                       

                                            }

                                        }
                                        dtobjdata.Rows.Add(row);
                                        Console.WriteLine();
                                    }


                                    if (dtobjdata.Rows.Count > 0)
                                    {

                                        //clsPackagemaster.db_data.Merge(dtobjdata);
                                    }

                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($" Can't Find Success ");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }

            return (dtobjdata);
        }
        public static async Task RequestPackagemaster(string keyword, DateTime stStartdate)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string startdate = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                    string startdate = stStartdate.ToString("yyyy-MM-dd");
                    string enddate = DateTime.Now.ToString("yyyy-MM-dd");
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/detail/all";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                                ""startdate"":""{startdate}"",
                                                ""enddate"":""{enddate}"",
                                                ""prescriptionno"": ""{ keyword}""

                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);


                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            Console.WriteLine($"Find Success");

                            // ใช้ JArray เพื่อเข้าถึง "data"
                            JArray dataArray = (JArray)obj["data"];
                            DataTable dtobjdata = new DataTable();

                            // แปลง JArray เป็น List ของ Dictionary
                            clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();

                            JArray dataArraydata = new JArray();
                            List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                foreach (var key in dataObject.Keys)
                                {
                                    if (key != "drugallergies" && key != "packagemaster" && key != "drugs" && key != "labs")
                                    {
                                        Console.WriteLine($"{key}: {dataObject[key]}");
                                        dtobjdata.Columns.Add($"{key}", typeof(object));
                                    }

                                }
                                //dataArraydata = (JArray)dataObject["data"];
                                //objdata = dataArraydata.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("Data Details:");
                                DataRow row = dtobjdata.NewRow();
                                foreach (var key in dataObject.Keys)
                                {
                                    if (key != "drugallergies" && key != "packagemaster" && key != "drugs" && key != "labs")
                                    {
                                        Console.WriteLine($"{key}: {dataObject[key]}");
                                        row[key] = $"{dataObject[key]}"; // ถ้า null ให้ใส่ DBNull.Value                                       

                                    }

                                }
                                dtobjdata.Rows.Add(row);
                                Console.WriteLine();
                            }


                            if (dtobjdata.Rows.Count > 0)
                            {

                                clsPackagemaster.db_data.Merge(dtobjdata);
                            }




                            JArray dataArraypackagemaster = new JArray();
                            List<Dictionary<string, object>> objpackage = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                dataArraypackagemaster = (JArray)dataObject["packagemaster"];
                                objpackage = dataArraypackagemaster.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("Packagemaster Details:");
                                foreach (var key in dataObject.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObject[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjpackage = new DataTable();
                            if (objpackage == null || objpackage.Count == 0)
                                Console.WriteLine($"Can't Find Success");
                            // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                            foreach (var key in objpackage.First().Keys)
                            {
                                dtobjpackage.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                            }
                            // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                            foreach (var dict in objpackage)
                            {
                                DataRow row = dtobjpackage.NewRow();
                                foreach (var key in dict.Keys)
                                {
                                    row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                }
                                dtobjpackage.Rows.Add(row);

                            }
                            if (dtobjpackage.Rows.Count > 0)
                            {

                                clsPackagemaster.db_packagemaster.Merge(dtobjpackage);
                            }


                            JArray dataArraydrugs = new JArray();
                            List<Dictionary<string, object>> objdrugs = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                dataArraydrugs = (JArray)dataObject["drugs"];
                                objdrugs = dataArraydrugs.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("drug Details:");
                                foreach (var key in dataObject.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObject[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjdrugse = new DataTable();
                            if (objdrugs == null || objdrugs.Count == 0)
                                Console.WriteLine($"Can't Find Success");
                            // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                            foreach (var key in objdrugs.First().Keys)
                            {
                                dtobjdrugse.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                            }
                            // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                            foreach (var dict in objdrugs)
                            {
                                DataRow row = dtobjdrugse.NewRow();
                                foreach (var key in dict.Keys)
                                {
                                    row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                }
                                dtobjdrugse.Rows.Add(row);

                            }
                            if (dtobjdrugse.Rows.Count > 0)
                            {

                                clsPackagemaster.db_drug.Merge(dtobjdrugse);
                            }

                            // Labs
                            JArray dataArraylabs = new JArray();
                            List<Dictionary<string, object>> objlabs = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObjectlabs in clsPackagemaster.objresponseBody)
                            {
                                dataArraylabs = (JArray)dataObjectlabs["labs"];
                                objlabs = dataArraylabs.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("labs Details:");
                                foreach (var key in dataObjectlabs.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObjectlabs[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjlabs = new DataTable();
                            if (objlabs == null || objlabs.Count == 0)
                                Console.WriteLine($"Can't Find Success");
                            // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                            foreach (var key in objlabs.First().Keys)
                            {
                                dtobjlabs.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                            }
                            // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                            foreach (var dict in objlabs)
                            {
                                DataRow row = dtobjlabs.NewRow();
                                foreach (var key in dict.Keys)
                                {
                                    row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                }
                                dtobjlabs.Rows.Add(row);

                            }
                            if (dtobjlabs.Rows.Count > 0)
                            {

                                clsPackagemaster.db_labs.Merge(dtobjlabs);
                            }

                            // Labs
                            JArray dataArraydrugallergies = new JArray();
                            List<Dictionary<string, object>> objdrugallergies = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObjectdrugallergies in clsPackagemaster.objresponseBody)
                            {
                                dataArraydrugallergies = (JArray)dataObjectdrugallergies["drugallergies"];
                                objdrugallergies = dataArraydrugallergies.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("labs Details:");
                                foreach (var key in dataObjectdrugallergies.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObjectdrugallergies[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjdrugallergies = new DataTable();
                            if (objdrugallergies == null || objdrugallergies.Count == 0)
                                Console.WriteLine($"Can't Find Success");
                            // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                            foreach (var key in objdrugallergies.First().Keys)
                            {
                                dtobjdrugallergies.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                            }
                            // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                            foreach (var dict in objdrugallergies)
                            {
                                DataRow row = dtobjdrugallergies.NewRow();
                                foreach (var key in dict.Keys)
                                {
                                    row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                }
                                dtobjdrugallergies.Rows.Add(row);

                            }
                            if (dtobjdrugallergies.Rows.Count > 0)
                            {

                                clsPackagemaster.db_drugallergies.Merge(dtobjdrugallergies);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Can't Find Success");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }

        }
        public static async Task update_prescription(List<object> ListJson)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://192.168.30.14:6426/order/update";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/update";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = "";
                    var jsonString = System.Text.Json.JsonSerializer.Serialize(ListJson, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine(jsonString);
                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    Console.WriteLine(content);
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            //clsPackagemaster.obprescription = obj["data"].ToObject<List<object>>();
                            Console.WriteLine($" Find Success ");
                        }
                        else
                        {
                            Console.WriteLine($" Can't Find Success ");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                }
                finally
                {

                }
            }
        }
        public static async Task<bool> update_matching(List<object> ListJson)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://192.168.30.14:6426/packagemaster/order/update";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/packagemaster/matching/update";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = "";
                    var jsonString = System.Text.Json.JsonSerializer.Serialize(ListJson, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine(jsonString);
                    if (jsonString.Length > 0)
                    {
                        // สร้าง HttpContent (StringContent)
                        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                        Console.WriteLine(content);
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                            Console.WriteLine(responseBody);
                            JObject obj = JObject.Parse(responseBody);
                            if (obj["status"].ToString() == "200")
                            {
                                //clsPackagemaster.obprescription = obj["data"].ToObject<List<object>>();
                                Console.WriteLine($" Find Success ");
                            }
                            else
                            {
                                Console.WriteLine($" Can't Find Success ");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                   
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                    return false;
                }
                finally
                {
                    
                }
            }
        }
        public async Task<bool> GetUserLoginScan(string user)
        {
            bool chk = false;
            // DataTable DtUserScan = new DataTable();
            user = user.ToLower();
            string strUrlGetUserScan = "";
            string ChkIO = Properties.Settings.Default.IO;
           
            strUrlGetUserScan = PrescriptionManagement.Properties.Settings.Default.apiUrl+ "/master/login/user"; 

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
             {
                 {"username",user}
             };
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(values, Newtonsoft.Json.Formatting.Indented); //System.Text.Json.JsonSerializer.Serialize(values, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                //var content = new FormUrlEncodedContent(jsonString);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(strUrlGetUserScan, content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responsedata = await response.Content.ReadAsStringAsync();
                        dynamic jsonDict = JsonConvert.DeserializeObject<RootUserScan>(responsedata);

                        if (jsonDict.data != null)
                        {
                            cls.clsuser.name = jsonDict.data.fullname;
                            cls.clsuser.userid = jsonDict.data.userID;
                            //UserPwd = jsonDict.data.password;
                            chk = true;
                        }
                    }
                }
            }
            return chk;
        } // GetUserLoginScan
        public class RootUserScan
        {
            public int status { get; set; }
            public DataScan data { get; set; }
        }

        public class DataScan
        {
            public string _id { get; set; }
            public string username { get; set; }
            public string __v { get; set; }
            public string[] authorized { get; set; }
            public string createddate { get; set; }
            public string dateupdate { get; set; }
            public string firstname { get; set; }
            public string fullname { get; set; }
            public string lastname { get; set; }
            public string password { get; set; }
            public string type { get; set; }
            public string userID { get; set; }
            public string userbarcode { get; set; }
        }



        public static async Task<bool> update_checkout(List<object> ListJson)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://192.168.30.14:6426/packagemaster/order/update";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/packagemaster/checkout/update";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = "";
                    var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(ListJson, Newtonsoft.Json.Formatting.Indented);// System.Text.Json.JsonSerializer.Serialize(ListJson, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine(jsonString);
                    if (jsonString.Length > 0)
                    {
                        // สร้าง HttpContent (StringContent)
                        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                        Console.WriteLine(content);
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                            Console.WriteLine(responseBody);
                            JObject obj = JObject.Parse(responseBody);
                            if (obj["status"].ToString() == "200")
                            {
                                //clsPackagemaster.obprescription = obj["data"].ToObject<List<object>>();
                                Console.WriteLine($" Find Success ");
                            }
                            else
                            {
                                Console.WriteLine($" Can't Find Success ");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                    return false;
                }
                finally
                {

                }
            }
        }

        public static async Task RequestPackagemasterfirstzone_bk(string computername)
        {
            try
            {
                #region creatdb_packagemaster

                //Packagemaster
                DataTable db_packagemaster = new DataTable();
                db_packagemaster.Columns.Add("_id", typeof(String));
                db_packagemaster.Columns.Add("__v", typeof(String));
                db_packagemaster.Columns.Add("appointmentdate", typeof(String));
                db_packagemaster.Columns.Add("cid", typeof(String));
                db_packagemaster.Columns.Add("computername", typeof(String));
                db_packagemaster.Columns.Add("confirmdatetime", typeof(String));
                db_packagemaster.Columns.Add("confirmuserid", typeof(String));
                db_packagemaster.Columns.Add("doctorcode", typeof(String));
                db_packagemaster.Columns.Add("doctorname", typeof(String));
                db_packagemaster.Columns.Add("expressmed", typeof(String));
                db_packagemaster.Columns.Add("hn", typeof(String));
                db_packagemaster.Columns.Add("lastupdate", typeof(String));
                db_packagemaster.Columns.Add("ordercreatedate", typeof(String));
                db_packagemaster.Columns.Add("patientdob", typeof(String));
                db_packagemaster.Columns.Add("patientname", typeof(String));
                db_packagemaster.Columns.Add("pharmacyitemcode", typeof(String));
                db_packagemaster.Columns.Add("pharmacyitemdesc", typeof(String));
                db_packagemaster.Columns.Add("qn", typeof(String));
                db_packagemaster.Columns.Add("qrrdumix", typeof(String));
                db_packagemaster.Columns.Add("rcvmedno", typeof(String));
                db_packagemaster.Columns.Add("readdatetime", typeof(String));
                db_packagemaster.Columns.Add("rightid", typeof(String));
                db_packagemaster.Columns.Add("rightname", typeof(String));
                db_packagemaster.Columns.Add("sex", typeof(String));
                db_packagemaster.Columns.Add("sphmlct", typeof(String));
                db_packagemaster.Columns.Add("sphmname", typeof(String));
                db_packagemaster.Columns.Add("total_norefund", typeof(String));
                db_packagemaster.Columns.Add("total_refund", typeof(String));
                db_packagemaster.Columns.Add("totalprice", typeof(String));
                db_packagemaster.Columns.Add("vn", typeof(String));
                db_packagemaster.Columns.Add("voiddatetime", typeof(String));
                db_packagemaster.Columns.Add("wardcode", typeof(String));
                db_packagemaster.Columns.Add("wardname", typeof(String));
                db_packagemaster.Columns.Add("genorderdatetime", typeof(String));
                db_packagemaster.Columns.Add("id", typeof(String));

                db_packagemaster.Columns.Add("seqrun", typeof(String));
                db_packagemaster.Columns.Add("dateupdate", typeof(String));
                db_packagemaster.Columns.Add("drugaccountcode", typeof(String));
                db_packagemaster.Columns.Add("edned", typeof(String));
                db_packagemaster.Columns.Add("itemidentify", typeof(String));
                db_packagemaster.Columns.Add("orderitembarcode", typeof(String));
                db_packagemaster.Columns.Add("orderitemcode", typeof(String));
                db_packagemaster.Columns.Add("orderitemname", typeof(String));
                db_packagemaster.Columns.Add("orderqty", typeof(String));
                db_packagemaster.Columns.Add("orderunitcode", typeof(String));
                db_packagemaster.Columns.Add("orderunitdesc", typeof(String));
                db_packagemaster.Columns.Add("poison", typeof(String));
                db_packagemaster.Columns.Add("pregnancy", typeof(String));
                db_packagemaster.Columns.Add("prescriptionno", typeof(String));
                db_packagemaster.Columns.Add("prescriptionno_sup", typeof(String));
                db_packagemaster.Columns.Add("seq", typeof(String));
                db_packagemaster.Columns.Add("seqmax", typeof(String));
                db_packagemaster.Columns.Add("shelfname", typeof(String));
                db_packagemaster.Columns.Add("shelfzone", typeof(String));
                db_packagemaster.Columns.Add("id_packagemaster", typeof(String));
                db_packagemaster.Columns.Add("firstzone", typeof(String));
                db_packagemaster.Columns.Add("orderzone", typeof(String));
                db_packagemaster.TableName = "db_packagemaster";
                #endregion

                #region creatdb_drug
                //drug
                DataTable db_drug = new DataTable();
                db_drug.Columns.Add("_id", typeof(String));
                db_drug.Columns.Add("__v", typeof(String));
                db_drug.Columns.Add("appointmentdate", typeof(String));
                db_drug.Columns.Add("cid", typeof(String));
                db_drug.Columns.Add("computername", typeof(String));
                db_drug.Columns.Add("confirmdatetime", typeof(String));
                db_drug.Columns.Add("confirmuserid", typeof(String));
                db_drug.Columns.Add("doctorcode", typeof(String));
                db_drug.Columns.Add("doctorname", typeof(String));
                db_drug.Columns.Add("expressmed", typeof(String));
                db_drug.Columns.Add("hn", typeof(String));
                db_drug.Columns.Add("lastupdate", typeof(String));
                db_drug.Columns.Add("ordercreatedate", typeof(String));
                db_drug.Columns.Add("patientdob", typeof(String));
                db_drug.Columns.Add("patientname", typeof(String));
                db_drug.Columns.Add("pharmacyitemcode", typeof(String));
                db_drug.Columns.Add("pharmacyitemdesc", typeof(String));
                db_drug.Columns.Add("qn", typeof(String));
                db_drug.Columns.Add("qrrdumix", typeof(String));
                db_drug.Columns.Add("rcvmedno", typeof(String));
                db_drug.Columns.Add("readdatetime", typeof(String));
                db_drug.Columns.Add("rightid", typeof(String));
                db_drug.Columns.Add("rightname", typeof(String));
                db_drug.Columns.Add("sex", typeof(String));
                db_drug.Columns.Add("sphmlct", typeof(String));
                db_drug.Columns.Add("sphmname", typeof(String));
                db_drug.Columns.Add("total_norefund", typeof(String));
                db_drug.Columns.Add("total_refund", typeof(String));
                db_drug.Columns.Add("totalprice", typeof(String));
                db_drug.Columns.Add("vn", typeof(String));
                db_drug.Columns.Add("voiddatetime", typeof(String));
                db_drug.Columns.Add("wardcode", typeof(String));
                db_drug.Columns.Add("wardname", typeof(String));
                db_drug.Columns.Add("genorderdatetime", typeof(String));
                db_drug.Columns.Add("id", typeof(String));

                db_drug.Columns.Add("seq", typeof(String));
                db_drug.Columns.Add("orderitemcode_ori", typeof(String));
                db_drug.Columns.Add("orderitemname_ori", typeof(String));
                db_drug.Columns.Add("orderqty_ori", typeof(String));
                db_drug.Columns.Add("strength", typeof(String));
                db_drug.Columns.Add("strengthunit", typeof(String));
                db_drug.Columns.Add("instructioncode", typeof(String));
                db_drug.Columns.Add("instructiondesc", typeof(String));
                db_drug.Columns.Add("dosage", typeof(String));
                db_drug.Columns.Add("dosageunitcode", typeof(String));
                db_drug.Columns.Add("dosageunitdesc", typeof(String));
                db_drug.Columns.Add("frequencycode", typeof(String));
                db_drug.Columns.Add("frequencydesc", typeof(String));
                db_drug.Columns.Add("freetext1", typeof(String));
                db_drug.Columns.Add("freetext2", typeof(String));
                db_drug.Columns.Add("freetext3", typeof(String));
                db_drug.Columns.Add("freetext4", typeof(String));
                db_drug.Columns.Add("freetext5", typeof(String));
                db_drug.Columns.Add("price", typeof(String));
                db_drug.Columns.Add("precaution_advice_text", typeof(String));
                db_drug.Columns.Add("special_advice_text", typeof(String));
                db_drug.Columns.Add("refund", typeof(String));
                db_drug.Columns.Add("norefund", typeof(String));
                db_drug.Columns.Add("icsale", typeof(String));
                db_drug.Columns.Add("icrefund", typeof(String));
                db_drug.Columns.Add("icnorefund", typeof(String));
                db_drug.Columns.Add("store", typeof(String));
                db_drug.Columns.Add("firstzone", typeof(String));
                db_drug.Columns.Add("pregnancy", typeof(String));
                db_drug.Columns.Add("edned", typeof(String));
                db_drug.Columns.Add("drugaccountcode", typeof(String));
                db_drug.Columns.Add("poison", typeof(String));
                db_drug.Columns.Add("itemidentify", typeof(String));
                db_drug.TableName = "db_drug";
                #endregion

                #region drugallergies
                DataTable db_drugallergies = new DataTable();
                db_drugallergies.Columns.Add("allergycode", typeof(String));
                db_drugallergies.Columns.Add("code", typeof(String));
                db_drugallergies.Columns.Add("genericname", typeof(String));
                db_drugallergies.Columns.Add("adverbs", typeof(String));
                db_drugallergies.Columns.Add("memo", typeof(String));
                db_drugallergies.Columns.Add("lastmodified", typeof(String));
                db_drugallergies.Columns.Add("hispharmacos", typeof(String));
                db_drugallergies.Columns.Add("ministries", typeof(String));
                db_drugallergies.Columns.Add("genorderdatetime", typeof(String));
                db_drugallergies.TableName = "db_drug";
                #endregion

                #region labs

                DataTable db_labs = new DataTable();
                db_labs.Columns.Add("lab1", typeof(String));
                db_labs.Columns.Add("lab2", typeof(String));
                db_labs.Columns.Add("lab3", typeof(String));
                db_labs.Columns.Add("lab4", typeof(String));
                db_labs.Columns.Add("lab5", typeof(String));
                db_labs.TableName = "db_labs";
                #endregion

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        //string apiUrl = "http://office.gd4.co.th:6426/packagemaster/order/firstzone";
                        string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/packagemaster/order/firstzone";
                        apiUrl = string.Format(apiUrl);
                        // สร้างข้อมูล JSON ที่ต้องการส่ง
                        string jsonContent = $@"{{
                                                ""computername"": ""{computername}""
                                            }}";

                        // สร้าง HttpContent (StringContent)
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                            Console.WriteLine(responseBody);
                            JObject obj = JObject.Parse(responseBody);
                            if (obj["status"].ToString() == "200")
                            {

                                Console.WriteLine($" Find Success ");

                                // Access the "data" array
                                JArray dataArray = (JArray)obj["data"];

                                // Loop through "data" objects
                                foreach (JObject dataObject in dataArray)
                                {
                                    JArray labsArray = (JArray)dataObject["labs"];
                                    Console.WriteLine("labs Details:");
                                    // Loop through "packagemaster" items
                                    foreach (JObject labs in labsArray)
                                    {
                                        DataRow r = db_labs.Rows.Add();
                                        //r["lab1"] = labs["lab1"]?.ToString();
                                        //r["lab2"] = labs["lab2"]?.ToString();
                                        //r["lab3"] = labs["lab3"]?.ToString();
                                        //r["lab4"] = labs["lab4"]?.ToString();
                                        //r["lab5"] = labs["lab5"]?.ToString();
                                        // Access the "packagemaster" array
                                    }
                                    JArray packageMasterArray = (JArray)dataObject["packagemaster"];
                                    Console.WriteLine("Packagemaster Details:");
                                    // Loop through "packagemaster" items
                                    foreach (JObject package in packageMasterArray)
                                    {
                                        DataRow r = db_packagemaster.Rows.Add();
                                        r["_id"] = dataObject["_id"]?.ToString() ?? "";
                                        r["__v"] = dataObject["__v"]?.ToString() ?? "";
                                        
                                        if(dataObject["appointmentdate"].ToString() != "")
                                        {
                                            r["appointmentdate"] = Convert.ToDateTime(dataObject["appointmentdate"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["appointmentdate"] = "";
                                        }
                                        r["cid"] = dataObject["cid"]?.ToString() ?? "";
                                        r["computername"] = dataObject["computername"]?.ToString() ?? "";
                                        //r["confirmdatetime"] = Convert.ToDateTime(dataObject["confirmdatetime"]?.ToString()?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["confirmdatetime"].ToString() != "")
                                        {
                                            r["confirmdatetime"] = Convert.ToDateTime(dataObject["confirmdatetime"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["confirmdatetime"] = "";
                                        }
                                        r["confirmuserid"] = dataObject["confirmuserid"]?.ToString() ?? "";
                                        r["doctorcode"] = dataObject["doctorcode"]?.ToString() ?? "";
                                        r["doctorname"] = dataObject["doctorname"]?.ToString() ?? "";
                                        r["expressmed"] = dataObject["expressmed"]?.ToString() ?? "";
                                        r["hn"] = dataObject["hn"]?.ToString() ?? "";
                                        //r["lastupdate"] = Convert.ToDateTime(dataObject["lastupdate"]?.ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["lastupdate"].ToString() != "")
                                        {
                                            r["lastupdate"] = Convert.ToDateTime(dataObject["lastupdate"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["lastupdate"] = "";
                                        }
                                        //r["ordercreatedate"] = Convert.ToDateTime(dataObject["ordercreatedate"]?.ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["ordercreatedate"].ToString() != "")
                                        {
                                            r["ordercreatedate"] = Convert.ToDateTime(dataObject["ordercreatedate"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["ordercreatedate"] = "";
                                        }
                                        //r["patientdob"] = Convert.ToDateTime(dataObject["patientdob"]?.ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["patientdob"].ToString() != "")
                                        {
                                            r["patientdob"] = Convert.ToDateTime(dataObject["patientdob"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["patientdob"] = "";
                                        }
                                        r["patientname"] = dataObject["patientname"]?.ToString() ?? "";
                                        r["pharmacyitemcode"] = dataObject["pharmacyitemcode"]?.ToString() ?? "";
                                        r["pharmacyitemdesc"] = dataObject["pharmacyitemdesc"]?.ToString() ?? "";
                                        r["qn"] = dataObject["qn"]?.ToString() ?? "";
                                        r["qrrdumix"] = dataObject["qrrdumix"]?.ToString() ?? "";
                                        r["rcvmedno"] = dataObject["rcvmedno"]?.ToString() ?? "";
                                        //r["readdatetime"] = Convert.ToDateTime(dataObject["readdatetime"].ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["readdatetime"].ToString() != "")
                                        {
                                            r["readdatetime"] = Convert.ToDateTime(dataObject["readdatetime"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["readdatetime"] = "";
                                        }
                                        r["rightid"] = dataObject["rightid"]?.ToString() ?? "";
                                        r["rightname"] = dataObject["rightname"]?.ToString() ?? "";
                                        r["sex"] = dataObject["sex"]?.ToString() ?? "";
                                        r["sphmlct"] = dataObject["sphmlct"]?.ToString() ?? "";
                                        r["sphmname"] = dataObject["sphmname"]?.ToString() ?? "";
                                        r["total_norefund"] = dataObject["total_norefund"]?.ToString() ?? "";
                                        r["total_refund"] = dataObject["total_refund"]?.ToString() ?? "";
                                        r["totalprice"] = dataObject["totalprice"]?.ToString() ?? "";
                                        r["vn"] = dataObject["vn"]?.ToString() ?? "";
                                        //r["voiddatetime"] = Convert.ToDateTime(dataObject["voiddatetime"].ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["voiddatetime"].ToString() != "")
                                        {
                                            r["voiddatetime"] = Convert.ToDateTime(dataObject["voiddatetime"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["voiddatetime"] = "";
                                        }
                                        r["wardcode"] = dataObject["wardcode"]?.ToString() ?? "";
                                        r["wardname"] = dataObject["wardname"]?.ToString() ?? "";
                                        //r["genorderdatetime"] = Convert.ToDateTime(dataObject["genorderdatetime"].ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["genorderdatetime"].ToString() != "")
                                        {
                                            r["genorderdatetime"] = Convert.ToDateTime(dataObject["genorderdatetime"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["genorderdatetime"] = "";
                                        }
                                        r["firstzone"] = dataObject["firstzone"]?.ToString() ?? "";

                                        r["_id"] = package["_id_prescription"]?.ToString() ?? "";
                                        r["seqrun"] = package["seqrun"]?.ToString() ?? "";
                                        //r["dateupdate"] = Convert.ToDateTime(package["dateupdate"].ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (package["dateupdate"].ToString() != "")
                                        {
                                            r["dateupdate"] = Convert.ToDateTime(package["dateupdate"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["dateupdate"] = "";
                                        }
                                        r["drugaccountcode"] = package["drugaccountcode"]?.ToString() ?? "";
                                        r["edned"] = package["edned"]?.ToString() ?? "";
                                        //r["genorderdatetime"] = Convert.ToDateTime(package["genorderdatetime"].ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (package["genorderdatetime"].ToString() != "")
                                        {
                                            r["genorderdatetime"] = Convert.ToDateTime(package["genorderdatetime"]).AddHours(7).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["genorderdatetime"] = "";
                                        }
                                        r["itemidentify"] = package["itemidentify"]?.ToString() ?? "";
                                        r["orderitembarcode"] = package["orderitembarcode"]?.ToString() ?? "";
                                        r["orderitemcode"] = package["orderitemcode"]?.ToString() ?? "";
                                        r["orderitemname"] = package["orderitemname"]?.ToString() ?? "";
                                        r["orderqty"] = package["orderqty"]?.ToString() ?? "";
                                        r["orderunitcode"] = package["orderunitcode"]?.ToString() ?? "";
                                        r["orderunitdesc"] = package["orderunitdesc"]?.ToString() ?? "";
                                        r["poison"] = package["poison"]?.ToString() ?? "";
                                        r["pregnancy"] = package["pregnancy"]?.ToString() ?? "";
                                        r["prescriptionno"] = package["prescriptionno"]?.ToString() ?? "";
                                        r["prescriptionno_sup"] = package["prescriptionno_sup"]?.ToString() ?? "";
                                        r["seq"] = package["seq"]?.ToString() ?? "";
                                        r["seqmax"] = package["seqmax"]?.ToString() ?? "";
                                        r["shelfname"] = package["shelfname"]?.ToString() ?? "";
                                        r["shelfzone"] = package["shelfzone"]?.ToString() ?? "";
                                        r["id_packagemaster"] = package["_id"]?.ToString() ?? "";
                                        r["orderzone"] = package["orderzone"]?.ToString() ?? "";
                                    }


                                    JArray drugsArray = (JArray)dataObject["drugs"];
                                    Console.WriteLine("drugs Details:");
                                    // Loop through "drugs" items
                                    foreach (JObject drugs in drugsArray)
                                    {
                                        DataRow r = db_drug.Rows.Add();
                                        r["_id"] = dataObject["_id"]?.ToString() ?? "";
                                        r["__v"] = dataObject["__v"]?.ToString() ?? "";
                                        //r["appointmentdate"] = Convert.ToDateTime(dataObject["appointmentdate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["appointmentdate"].ToString() != "")
                                        {
                                            r["appointmentdate"] = Convert.ToDateTime(dataObject["appointmentdate"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["appointmentdate"] = "";
                                        }
                                        r["cid"] = dataObject["cid"]?.ToString() ?? "";
                                        r["computername"] = dataObject["computername"]?.ToString() ?? "";
                                        if (dataObject["confirmdatetime"].ToString() != "")
                                        {
                                            r["confirmdatetime"] = Convert.ToDateTime(dataObject["confirmdatetime"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["confirmdatetime"] = "";
                                        }
                                        r["confirmuserid"] = dataObject["confirmuserid"]?.ToString() ?? "";
                                        r["doctorcode"] = dataObject["doctorcode"]?.ToString() ?? "";
                                        r["doctorname"] = dataObject["doctorname"]?.ToString() ?? "";
                                        r["expressmed"] = dataObject["expressmed"]?.ToString() ?? "";
                                        r["hn"] = dataObject["hn"]?.ToString() ?? "";

                                        //r["lastupdate"] = Convert.ToDateTime(dataObject["lastupdate"].ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["lastupdate"].ToString() != "")
                                        {
                                            r["lastupdate"] = Convert.ToDateTime(dataObject["lastupdate"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["lastupdate"] = "";
                                        }
                                        //r["ordercreatedate"] = Convert.ToDateTime(dataObject["ordercreatedate"].ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["ordercreatedate"].ToString() != "")
                                        {
                                            r["ordercreatedate"] = Convert.ToDateTime(dataObject["ordercreatedate"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["ordercreatedate"] = "";
                                        }
                                        if (dataObject["patientdob"].ToString() != "")
                                        {
                                            r["patientdob"] = Convert.ToDateTime(dataObject["patientdob"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["patientdob"] = "";
                                        }
                                        r["patientname"] = dataObject["patientname"]?.ToString() ?? "";
                                        r["pharmacyitemcode"] = dataObject["pharmacyitemcode"]?.ToString() ?? "";
                                        r["pharmacyitemdesc"] = dataObject["pharmacyitemdesc"]?.ToString() ?? "";
                                        r["qn"] = dataObject["qn"]?.ToString() ?? "";
                                        r["qrrdumix"] = dataObject["qrrdumix"]?.ToString() ?? "";
                                        r["rcvmedno"] = dataObject["rcvmedno"]?.ToString() ?? "";
                                        //r["readdatetime"] = Convert.ToDateTime(dataObject["readdatetime"].ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["readdatetime"].ToString() != "")
                                        {
                                            r["readdatetime"] = Convert.ToDateTime(dataObject["readdatetime"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["readdatetime"] = "";
                                        }
                                        r["rightid"] = dataObject["rightid"]?.ToString() ?? "";
                                        r["rightname"] = dataObject["rightname"]?.ToString() ?? "";
                                        r["sex"] = dataObject["sex"]?.ToString() ?? "";
                                        r["sphmlct"] = dataObject["sphmlct"]?.ToString() ?? "";
                                        r["sphmname"] = dataObject["sphmname"]?.ToString() ?? "";
                                        r["total_norefund"] = dataObject["total_norefund"]?.ToString() ?? "";
                                        r["total_refund"] = dataObject["total_refund"]?.ToString() ?? "";
                                        r["totalprice"] = dataObject["totalprice"]?.ToString() ?? "";
                                        r["vn"] = dataObject["vn"]?.ToString() ?? "";
                                        //r["voiddatetime"] = Convert.ToDateTime(dataObject["voiddatetime"].ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["voiddatetime"].ToString() != "")
                                        {
                                            r["voiddatetime"] = Convert.ToDateTime(dataObject["voiddatetime"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["voiddatetime"] = "";
                                        }
                                        r["wardcode"] = dataObject["wardcode"]?.ToString() ?? "";
                                        r["wardname"] = dataObject["wardname"]?.ToString() ?? "";
                                        //r["genorderdatetime"] = Convert.ToDateTime(dataObject["genorderdatetime"].ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (dataObject["genorderdatetime"].ToString() != "")
                                        {
                                            r["genorderdatetime"] = Convert.ToDateTime(dataObject["genorderdatetime"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["genorderdatetime"] = "";
                                        }
                                        r["seq"] = drugs["seq"]?.ToString() ?? "";
                                        r["orderitemcode_ori"] = drugs["orderitemcode"]?.ToString() ?? "";
                                        r["orderitemname_ori"] = drugs["orderitemname"]?.ToString() ?? "";
                                        r["orderqty_ori"] = drugs["orderqty"]?.ToString() ?? "";
                                        r["strength"] = drugs["strength"]?.ToString() ?? "";
                                        r["strengthunit"] = drugs["strengthunit"]?.ToString() ?? "";
                                        r["instructioncode"] = drugs["instructioncode"]?.ToString() ?? "";
                                        r["instructiondesc"] = drugs["instructiondesc"]?.ToString() ?? "";
                                        r["dosage"] = drugs["dosage"]?.ToString() ?? "";
                                        r["dosageunitcode"] = drugs["dosageunitcode"]?.ToString() ?? "";
                                        r["dosageunitdesc"] = drugs["dosageunitdesc"]?.ToString() ?? "";
                                        r["frequencycode"] = drugs["frequencycode"]?.ToString() ?? "";
                                        r["frequencydesc"] = drugs["frequencydesc"]?.ToString() ?? "";
                                        r["freetext1"] = drugs["freetext1"]?.ToString() ?? "";
                                        r["freetext2"] = drugs["freetext2"]?.ToString() ?? "";
                                        r["freetext3"] = drugs["freetext3"]?.ToString() ?? "";
                                        r["freetext4"] = drugs["freetext4"]?.ToString() ?? "";
                                        r["freetext5"] = drugs["freetext5"]?.ToString() ?? "";
                                        r["price"] = drugs["price"]?.ToString() ?? "";
                                        r["precaution_advice_text"] = drugs["precaution_advice_text"]?.ToString() ?? "";
                                        r["special_advice_text"] = drugs["special_advice_text"]?.ToString() ?? "";
                                        r["refund"] = drugs["refund"]?.ToString() ?? "";
                                        r["norefund"] = drugs["norefund"]?.ToString() ?? "";
                                        r["icsale"] = drugs["icsale"]?.ToString() ?? "";
                                        r["icrefund"] = drugs["icrefund"]?.ToString() ?? "";
                                        r["icnorefund"] = drugs["icnorefund"]?.ToString() ?? "";
                                        r["store"] = drugs["store"]?.ToString() ?? "";

                                    }

                                    JArray drugallergiesArray = (JArray)dataObject["drugallergies"];

                                    // Loop through "data" objects
                                    foreach (JObject drugallergiesObject in drugallergiesArray)
                                    {
                                        DataRow r = db_drugallergies.Rows.Add();
                                        JArray hisArray = (JArray)drugallergiesObject["his"];
                                        foreach (JObject his in hisArray)
                                        {
                                            r["allergycode"] = his["allergycode"]?.ToString() ?? "";
                                            r["code"] = his["code"]?.ToString() ?? "";
                                            r["genericname"] = his["genericname"]?.ToString() ?? "";
                                            r["adverbs"] = his["adverbs"]?.ToString() ?? "";
                                            r["memo"] = his["memo"]?.ToString() ?? "";
                                            //r["lastmodified"] = Convert.ToDateTime(his["lastmodified"].ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                            if (his["lastmodified"].ToString() != "")
                                            {
                                                r["lastmodified"] = Convert.ToDateTime(his["lastmodified"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                            }
                                            else
                                            {
                                                r["lastmodified"] = "";
                                            }
                                        }                                        
                                        
                                        r["hispharmacos"] = drugallergiesObject["hispharmacos"]?.ToString() ?? "";
                                        r["ministries"] = drugallergiesObject["ministries"]?.ToString() ?? "";
                                        //r["genorderdatetime"] = Convert.ToDateTime(drugallergiesObject["genorderdatetime"].ToString() ?? "").ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        if (drugallergiesObject["genorderdatetime"].ToString() != "")
                                        {
                                            r["genorderdatetime"] = Convert.ToDateTime(drugallergiesObject["genorderdatetime"]).AddHours(7).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).ToString();
                                        }
                                        else
                                        {
                                            r["genorderdatetime"] = "";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine($" Can't Find Success ");
                            }


                            if (db_packagemaster.Rows.Count > 0)
                            {
                                clsPackagemaster.db_packagemaster = new DataTable();
                                clsPackagemaster.db_packagemaster.Merge(db_packagemaster);
                            }
                            if (db_drug.Rows.Count > 0)
                            {
                                clsPackagemaster.db_drug = new DataTable();
                                clsPackagemaster.db_drug.Merge(db_drug);
                            }
                            if (db_drugallergies.Rows.Count > 0)
                            {
                                clsPackagemaster.db_drugallergies = new DataTable();
                                clsPackagemaster.db_drugallergies.Merge(db_drugallergies);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        Console.WriteLine($" Can't Find Success ");
                    }
                }
            }
            catch(Exception ex)
            {               
                MessageBox.Show(ex.ToString());

            }
            
        }

        public static async Task RequestPackagemasterfirstzone(string computername)
        {
            try
            {                
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        clsPackagemaster.db_data = new DataTable();
                        clsPackagemaster.db_drug = new DataTable();
                        clsPackagemaster.db_drugallergies = new DataTable();
                        clsPackagemaster.db_labs = new DataTable();
                        clsPackagemaster.db_packagemaster = new DataTable();

                        //string apiUrl = "http://192.168.30.14:6426/packagemaster/order/firstzone";
                        string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl.Trim() + "/packagemaster/order/firstzone";
                        apiUrl = string.Format(apiUrl);
                        // สร้างข้อมูล JSON ที่ต้องการส่ง
                        string jsonContent = $@"{{
                                                ""computername"": ""{computername}""
                                            }}";

                        // สร้าง HttpContent (StringContent)
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);


                        if (response.IsSuccessStatusCode)
                        {

                            string responseBody = await response.Content.ReadAsStringAsync();
                            //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                            Console.WriteLine(responseBody);
                            JObject obj = JObject.Parse(responseBody);
                            if (obj["status"].ToString() == "200")
                            {
                                Console.WriteLine($"Find Success");

                                // ใช้ JArray เพื่อเข้าถึง "data"
                                JArray dataArray = (JArray)obj["data"];
                                DataTable dtobjdata = new DataTable();
                                
                                // แปลง JArray เป็น List ของ Dictionary
                                clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();
                                if (clsPackagemaster.objresponseBody.Count > 0)
                                {
                                    JArray dataArraydata = new JArray();
                                    List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                                    if (clsPackagemaster.objresponseBody == null || clsPackagemaster.objresponseBody.Count == 0)
                                    {
                                        Console.WriteLine($"Can't Find Success");
                                    }
                                    else
                                    {
                                        // วนลูปแสดงผลข้อมูลแต่ละ Object
                                        foreach (var dataObject in clsPackagemaster.objresponseBody)
                                        {
                                            foreach (var key in dataObject.Keys)
                                            {
                                                if (key != "drugallergies" && key != "packagemaster" && key != "drugs" && key != "labs")
                                                {
                                                    Console.WriteLine($"{key}: {dataObject[key]}");
                                                    dtobjdata.Columns.Add($"{key}", typeof(object));
                                                }

                                            }
                                            //dataArraydata = (JArray)dataObject["data"];
                                            //objdata = dataArraydata.ToObject<List<Dictionary<string, object>>>();
                                            Console.WriteLine("Data Details:");
                                            DataRow row = dtobjdata.NewRow();
                                            foreach (var key in dataObject.Keys)
                                            {
                                                if (key != "drugallergies" && key != "packagemaster" && key != "drugs" && key != "labs")
                                                {
                                                    Console.WriteLine($"{key}: {dataObject[key]}");
                                                    row[key] = $"{dataObject[key]}"; // ถ้า null ให้ใส่ DBNull.Value                                       

                                                }

                                            }
                                            dtobjdata.Rows.Add(row);
                                            Console.WriteLine();
                                        }


                                        if (dtobjdata.Rows.Count > 0)
                                        {

                                            clsPackagemaster.db_data.Merge(dtobjdata);
                                        }

                                    }




                                    JArray dataArraypackagemaster = new JArray();
                                    List<Dictionary<string, object>> objpackage = new List<Dictionary<string, object>>();
                                    // วนลูปแสดงผลข้อมูลแต่ละ Object
                                    foreach (var dataObject in clsPackagemaster.objresponseBody)
                                    {
                                        dataArraypackagemaster = (JArray)dataObject["packagemaster"];
                                        objpackage = dataArraypackagemaster.ToObject<List<Dictionary<string, object>>>();
                                        Console.WriteLine("Packagemaster Details:");
                                        foreach (var key in dataObject.Keys)
                                        {
                                            Console.WriteLine($"{key}: {dataObject[key]}");
                                        }
                                        Console.WriteLine();
                                    }
                                    DataTable dtobjpackage = new DataTable();
                                    if (objpackage == null || objpackage.Count == 0)
                                    {
                                        Console.WriteLine($"Can't Find Success");
                                    }
                                    else
                                    {
                                        // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                        foreach (var key in objpackage.First().Keys)
                                        {
                                            dtobjpackage.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                        }
                                        // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                        foreach (var dict in objpackage)
                                        {
                                            DataRow row = dtobjpackage.NewRow();
                                            foreach (var key in dict.Keys)
                                            {
                                                row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                            }
                                            dtobjpackage.Rows.Add(row);

                                        }
                                        if (dtobjpackage.Rows.Count > 0)
                                        {

                                            clsPackagemaster.db_packagemaster.Merge(dtobjpackage);
                                        }
                                    }



                                    //  drugs

                                    JArray dataArraydrugs = new JArray();
                                    List<Dictionary<string, object>> objdrugs = new List<Dictionary<string, object>>();
                                    // วนลูปแสดงผลข้อมูลแต่ละ Object
                                    foreach (var dataObject in clsPackagemaster.objresponseBody)
                                    {
                                        dataArraydrugs = (JArray)dataObject["drugs"];
                                        objdrugs = dataArraydrugs.ToObject<List<Dictionary<string, object>>>();
                                        Console.WriteLine("drug Details:");
                                        foreach (var key in dataObject.Keys)
                                        {
                                            Console.WriteLine($"{key}: {dataObject[key]}");
                                        }
                                        Console.WriteLine();
                                    }
                                    DataTable dtobjdrugse = new DataTable();
                                    if (objdrugs == null || objdrugs.Count == 0)
                                    {
                                        Console.WriteLine($"Can't Find Success");
                                    }
                                    else
                                    {
                                        // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                        foreach (var key in objdrugs.First().Keys)
                                        {
                                            dtobjdrugse.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                        }
                                        // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                        foreach (var dict in objdrugs)
                                        {
                                            DataRow row = dtobjdrugse.NewRow();
                                            foreach (var key in dict.Keys)
                                            {
                                                row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                            }
                                            dtobjdrugse.Rows.Add(row);

                                        }
                                        if (dtobjdrugse.Rows.Count > 0)
                                        {

                                            clsPackagemaster.db_drug.Merge(dtobjdrugse);
                                        }
                                    }



                                    // Labs
                                    JArray dataArraylabs = new JArray();
                                    List<Dictionary<string, object>> objlabs = new List<Dictionary<string, object>>();
                                    // วนลูปแสดงผลข้อมูลแต่ละ Object
                                    foreach (var dataObjectlabs in clsPackagemaster.objresponseBody)
                                    {
                                        dataArraylabs = (JArray)dataObjectlabs["labs"];
                                        objlabs = dataArraylabs.ToObject<List<Dictionary<string, object>>>();
                                        Console.WriteLine("labs Details:");

                                    }
                                    DataTable dtobjlabs = new DataTable();
                                    if (objlabs == null || objlabs.Count == 0)
                                    {
                                        Console.WriteLine($"Can't Find Success");
                                    }
                                    else
                                    {
                                        // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                        foreach (var key in objlabs.First().Keys)
                                        {
                                            dtobjlabs.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                        }
                                        // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                        foreach (var dict in objlabs)
                                        {
                                            DataRow row = dtobjlabs.NewRow();
                                            foreach (var key in dict.Keys)
                                            {
                                                row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                            }
                                            dtobjlabs.Rows.Add(row);

                                        }
                                        if (dtobjlabs.Rows.Count > 0)
                                        {

                                            clsPackagemaster.db_labs.Merge(dtobjlabs);
                                        }
                                    }



                                    // drugallergies
                                    JArray dataArraydrugallergies = new JArray();
                                    List<Dictionary<string, object>> objdrugallergies = new List<Dictionary<string, object>>();
                                    // วนลูปแสดงผลข้อมูลแต่ละ Object
                                    foreach (var dataObjectdrugallergies in clsPackagemaster.objresponseBody)
                                    {
                                        dataArraydrugallergies = (JArray)dataObjectdrugallergies["drugallergies"];
                                        objdrugallergies = dataArraydrugallergies.ToObject<List<Dictionary<string, object>>>();
                                        Console.WriteLine("labs Details:");
                                        foreach (var key in dataObjectdrugallergies.Keys)
                                        {
                                            Console.WriteLine($"{key}: {dataObjectdrugallergies[key]}");
                                        }
                                        Console.WriteLine();
                                    }
                                    DataTable dtobjdrugallergies = new DataTable();
                                    if (objdrugallergies == null || objdrugallergies.Count == 0)
                                    {
                                        Console.WriteLine($"Can't Find Success");
                                    }
                                    else
                                    {
                                        foreach (var key in objdrugallergies.First().Keys)
                                        {
                                            dtobjdrugallergies.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                        }
                                        // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                        foreach (var dict in objdrugallergies)
                                        {
                                            DataRow row = dtobjdrugallergies.NewRow();
                                            foreach (var key in dict.Keys)
                                            {
                                                row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                            }
                                            dtobjdrugallergies.Rows.Add(row);

                                        }
                                        if (dtobjdrugallergies.Rows.Count > 0)
                                        {

                                            clsPackagemaster.db_drugallergies.Merge(dtobjdrugallergies);
                                        }
                                    }
                                }
                                
                                    
                                // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                
                            }
                            else
                            {
                                Console.WriteLine($"Can't Find Success");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                        }


                    }
                    catch (Exception ex)
                    {
                        clsPackagemaster.db_packagemaster = new DataTable();
                        clsPackagemaster.db_drug = new DataTable();
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        Console.WriteLine($" Can't Find Success ");
                    }
                }
            }
            catch (Exception ex)
            {
                clsPackagemaster.db_packagemaster = new DataTable();
                clsPackagemaster.db_drug = new DataTable();
                MessageBox.Show(ex.ToString());

            }

        }
        public static async Task RequestPackagemasterByZone_BK(string zonename, string itemidentify)
        {
            #region creatdb_packagemaster

            //Packagemaster
            DataTable db_packagemaster = new DataTable();
            db_packagemaster.Columns.Add("_id", typeof(String));
            db_packagemaster.Columns.Add("__v", typeof(String));
            db_packagemaster.Columns.Add("appointmentdate", typeof(String));
            db_packagemaster.Columns.Add("cid", typeof(String));
            db_packagemaster.Columns.Add("computername", typeof(String));
            db_packagemaster.Columns.Add("confirmdatetime", typeof(String));
            db_packagemaster.Columns.Add("confirmuserid", typeof(String));
            db_packagemaster.Columns.Add("doctorcode", typeof(String));
            db_packagemaster.Columns.Add("doctorname", typeof(String));
            db_packagemaster.Columns.Add("expressmed", typeof(String));
            db_packagemaster.Columns.Add("hn", typeof(String));
            db_packagemaster.Columns.Add("lastupdate", typeof(String));
            db_packagemaster.Columns.Add("ordercreatedate", typeof(String));
            db_packagemaster.Columns.Add("patientdob", typeof(String));
            db_packagemaster.Columns.Add("patientname", typeof(String));
            db_packagemaster.Columns.Add("pharmacyitemcode", typeof(String));
            db_packagemaster.Columns.Add("pharmacyitemdesc", typeof(String));
            db_packagemaster.Columns.Add("qn", typeof(String));
            db_packagemaster.Columns.Add("qrrdumix", typeof(String));
            db_packagemaster.Columns.Add("rcvmedno", typeof(String));
            db_packagemaster.Columns.Add("readdatetime", typeof(String));
            db_packagemaster.Columns.Add("rightid", typeof(String));
            db_packagemaster.Columns.Add("rightname", typeof(String));
            db_packagemaster.Columns.Add("sex", typeof(String));
            db_packagemaster.Columns.Add("sphmlct", typeof(String));
            db_packagemaster.Columns.Add("sphmname", typeof(String));
            db_packagemaster.Columns.Add("total_norefund", typeof(String));
            db_packagemaster.Columns.Add("total_refund", typeof(String));
            db_packagemaster.Columns.Add("totalprice", typeof(String));
            db_packagemaster.Columns.Add("vn", typeof(String));
            db_packagemaster.Columns.Add("voiddatetime", typeof(String));
            db_packagemaster.Columns.Add("wardcode", typeof(String));
            db_packagemaster.Columns.Add("wardname", typeof(String));
            db_packagemaster.Columns.Add("genorderdatetime", typeof(String));
            db_packagemaster.Columns.Add("id", typeof(String));

            db_packagemaster.Columns.Add("seqrun", typeof(String));
            db_packagemaster.Columns.Add("dateupdate", typeof(String));
            db_packagemaster.Columns.Add("drugaccountcode", typeof(String));
            db_packagemaster.Columns.Add("edned", typeof(String));
            db_packagemaster.Columns.Add("itemidentify", typeof(String));
            db_packagemaster.Columns.Add("orderitembarcode", typeof(String));
            db_packagemaster.Columns.Add("orderitemcode", typeof(String));
            db_packagemaster.Columns.Add("orderitemname", typeof(String));
            db_packagemaster.Columns.Add("orderqty", typeof(String));
            db_packagemaster.Columns.Add("orderunitcode", typeof(String));
            db_packagemaster.Columns.Add("orderunitdesc", typeof(String));
            db_packagemaster.Columns.Add("poison", typeof(String));
            db_packagemaster.Columns.Add("pregnancy", typeof(String));
            db_packagemaster.Columns.Add("prescriptionno", typeof(String));
            db_packagemaster.Columns.Add("prescriptionno_sup", typeof(String));
            db_packagemaster.Columns.Add("seq", typeof(String));
            db_packagemaster.Columns.Add("seqmax", typeof(String));
            db_packagemaster.Columns.Add("shelfname", typeof(String));
            db_packagemaster.Columns.Add("shelfzone", typeof(String));
            db_packagemaster.Columns.Add("id_packagemaster", typeof(String));

            #endregion

            #region creatdb_drug
            //drug
            DataTable db_drug = new DataTable();
            db_drug.Columns.Add("_id", typeof(String));
            db_drug.Columns.Add("__v", typeof(String));
            db_drug.Columns.Add("prescriptionno", typeof(String));
            db_drug.Columns.Add("appointmentdate", typeof(String));
            db_drug.Columns.Add("cid", typeof(String));
            db_drug.Columns.Add("computername", typeof(String));
            db_drug.Columns.Add("confirmdatetime", typeof(String));
            db_drug.Columns.Add("confirmuserid", typeof(String));
            db_drug.Columns.Add("doctorcode", typeof(String));
            db_drug.Columns.Add("doctorname", typeof(String));
            db_drug.Columns.Add("expressmed", typeof(String));
            db_drug.Columns.Add("hn", typeof(String));
            db_drug.Columns.Add("lastupdate", typeof(String));
            db_drug.Columns.Add("ordercreatedate", typeof(String));
            db_drug.Columns.Add("patientdob", typeof(String));
            db_drug.Columns.Add("patientname", typeof(String));
            db_drug.Columns.Add("pharmacyitemcode", typeof(String));
            db_drug.Columns.Add("pharmacyitemdesc", typeof(String));
            db_drug.Columns.Add("qn", typeof(String));
            db_drug.Columns.Add("qrrdumix", typeof(String));
            db_drug.Columns.Add("rcvmedno", typeof(String));
            db_drug.Columns.Add("readdatetime", typeof(String));
            db_drug.Columns.Add("rightid", typeof(String));
            db_drug.Columns.Add("rightname", typeof(String));
            db_drug.Columns.Add("sex", typeof(String));
            db_drug.Columns.Add("sphmlct", typeof(String));
            db_drug.Columns.Add("sphmname", typeof(String));
            db_drug.Columns.Add("total_norefund", typeof(String));
            db_drug.Columns.Add("total_refund", typeof(String));
            db_drug.Columns.Add("totalprice", typeof(String));
            db_drug.Columns.Add("vn", typeof(String));
            db_drug.Columns.Add("voiddatetime", typeof(String));
            db_drug.Columns.Add("wardcode", typeof(String));
            db_drug.Columns.Add("wardname", typeof(String));
            db_drug.Columns.Add("genorderdatetime", typeof(String));
            db_drug.Columns.Add("id", typeof(String));

            db_drug.Columns.Add("orderitemcode_ori", typeof(String));
            db_drug.Columns.Add("orderqty_ori", typeof(String));
            db_drug.Columns.Add("strength", typeof(String));
            db_drug.Columns.Add("strengthunit", typeof(String));
            db_drug.Columns.Add("instructioncode", typeof(String));
            db_drug.Columns.Add("instructiondesc", typeof(String));
            db_drug.Columns.Add("dosage", typeof(String));
            db_drug.Columns.Add("dosageunitcode", typeof(String));
            db_drug.Columns.Add("dosageunitdesc", typeof(String));
            db_drug.Columns.Add("frequencycode", typeof(String));
            db_drug.Columns.Add("frequencydesc", typeof(String));
            db_drug.Columns.Add("freetext1", typeof(String));
            db_drug.Columns.Add("freetext2", typeof(String));
            db_drug.Columns.Add("freetext3", typeof(String));
            db_drug.Columns.Add("freetext4", typeof(String));
            db_drug.Columns.Add("freetext5", typeof(String));
            db_drug.Columns.Add("price", typeof(String));
            db_drug.Columns.Add("precaution_advice_text", typeof(String));
            db_drug.Columns.Add("special_advice_text", typeof(String));
            db_drug.Columns.Add("refund", typeof(String));
            db_drug.Columns.Add("norefund", typeof(String));
            db_drug.Columns.Add("icsale", typeof(String));
            db_drug.Columns.Add("icrefund", typeof(String));
            db_drug.Columns.Add("icnorefund", typeof(String));
            db_drug.Columns.Add("store", typeof(String));
            db_drug.TableName = "db_packagemaster";
            #endregion

            #region creatdb_drug
            DataTable db_drugallergies = new DataTable();
            db_drugallergies.Columns.Add("_id", typeof(String));
            db_drugallergies.Columns.Add("__v", typeof(String));
            db_drugallergies.Columns.Add("appointmentdate", typeof(String));
            db_drugallergies.Columns.Add("computername", typeof(String));
            #endregion

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://office.gd4.co.th:6426/packagemaster/order/itemidentiy";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/packagemaster/order/itemidentify";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                                ""computername"": ""{clsSetting.COM_NAME}"", 
                                                ""itemidentify"": ""{itemidentify}""
                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {

                            Console.WriteLine($" Find Success ");

                            // Access the "data" array
                            JArray dataArray = (JArray)obj["data"];

                            // Loop through "data" objects
                            foreach (JObject dataObject in dataArray)
                            {
                                // Access the "packagemaster" array
                                JArray packageMasterArray = (JArray)dataObject["packagemaster"];
                                Console.WriteLine("Packagemaster Details:");
                                // Loop through "packagemaster" items
                                foreach (JObject package in packageMasterArray)
                                {
                                    DataRow r = db_packagemaster.Rows.Add();
                                    r["_id"] = dataObject["_id"]?.ToString();
                                    r["__v"] = dataObject["__v"]?.ToString();
                                    r["appointmentdate"] = dataObject["appointmentdate"]?.ToString();
                                    r["cid"] = dataObject["cid"]?.ToString();
                                    r["computername"] = dataObject["computername"]?.ToString();
                                    r["confirmdatetime"] = dataObject["confirmdatetime"]?.ToString();
                                    r["confirmuserid"] = dataObject["confirmuserid"]?.ToString();
                                    r["doctorcode"] = dataObject["doctorcode"]?.ToString();
                                    r["doctorname"] = dataObject["doctorname"]?.ToString();
                                    r["expressmed"] = dataObject["expressmed"]?.ToString();
                                    r["hn"] = dataObject["hn"]?.ToString();
                                    r["lastupdate"] = dataObject["lastupdate"]?.ToString();
                                    r["ordercreatedate"] = dataObject["ordercreatedate"]?.ToString();
                                    r["patientdob"] = dataObject["patientdob"]?.ToString();
                                    r["patientname"] = dataObject["patientname"]?.ToString();
                                    r["pharmacyitemcode"] = dataObject["pharmacyitemcode"]?.ToString();
                                    r["pharmacyitemdesc"] = dataObject["pharmacyitemdesc"]?.ToString();
                                    r["qn"] = dataObject["qn"]?.ToString();
                                    r["qrrdumix"] = dataObject["qrrdumix"]?.ToString();
                                    r["rcvmedno"] = dataObject["rcvmedno"]?.ToString();
                                    r["readdatetime"] = dataObject["readdatetime"]?.ToString();
                                    r["rightid"] = dataObject["rightid"]?.ToString();
                                    r["rightname"] = dataObject["rightname"]?.ToString();
                                    r["sex"] = dataObject["sex"]?.ToString();
                                    r["sphmlct"] = dataObject["sphmlct"]?.ToString();
                                    r["sphmname"] = dataObject["sphmname"]?.ToString();
                                    r["total_norefund"] = dataObject["total_norefund"]?.ToString();
                                    r["total_refund"] = dataObject["total_refund"]?.ToString();
                                    r["totalprice"] = dataObject["totalprice"]?.ToString();
                                    r["vn"] = dataObject["vn"]?.ToString();
                                    r["voiddatetime"] = dataObject["voiddatetime"]?.ToString();
                                    r["wardcode"] = dataObject["wardcode"]?.ToString();
                                    r["wardname"] = dataObject["wardname"]?.ToString();
                                    r["genorderdatetime"] = dataObject["genorderdatetime"]?.ToString();

                                    r["_id"] = package["_id_prescription"]?.ToString();
                                    r["seqrun"] = package["seqrun"]?.ToString();
                                    r["dateupdate"] = package["dateupdate"]?.ToString();
                                    r["drugaccountcode"] = package["drugaccountcode"]?.ToString();
                                    r["edned"] = package["edned"]?.ToString();
                                    r["genorderdatetime"] = package["genorderdatetime"]?.ToString();
                                    r["itemidentify"] = package["itemidentify"]?.ToString();
                                    r["orderitembarcode"] = package["orderitembarcode"]?.ToString();
                                    r["orderitemcode"] = package["orderitemcode"]?.ToString();
                                    r["orderitemname"] = package["orderitemname"]?.ToString();
                                    r["orderqty"] = package["orderqty"]?.ToString();
                                    r["orderunitcode"] = package["orderunitcode"]?.ToString();
                                    r["orderunitdesc"] = package["orderunitdesc"]?.ToString();
                                    r["poison"] = package["poison"]?.ToString();
                                    r["pregnancy"] = package["pregnancy"]?.ToString();
                                    r["prescriptionno"] = package["prescriptionno"]?.ToString();
                                    r["prescriptionno_sup"] = package["prescriptionno_sup"]?.ToString();
                                    r["seq"] = package["seq"]?.ToString();
                                    r["seqmax"] = package["seqmax"]?.ToString();
                                    r["shelfname"] = package["shelfname"]?.ToString();
                                    r["shelfzone"] = package["shelfzone"]?.ToString();
                                    r["id_packagemaster"] = package["_id"]?.ToString();
                                }

                                JArray drugsArray = (JArray)dataObject["drugs"];
                                Console.WriteLine("drugs Details:");
                                // Loop through "drugs" items
                                foreach (JObject drugs in drugsArray)
                                {
                                    DataRow r = db_drug.Rows.Add();
                                    r["_id"] = dataObject["_id"]?.ToString();
                                    r["__v"] = dataObject["__v"]?.ToString();
                                    r["appointmentdate"] = dataObject["appointmentdate"]?.ToString();
                                    r["cid"] = dataObject["cid"]?.ToString();
                                    r["computername"] = dataObject["computername"]?.ToString();
                                    r["confirmdatetime"] = dataObject["confirmdatetime"]?.ToString();
                                    r["confirmuserid"] = dataObject["confirmuserid"]?.ToString();
                                    r["doctorcode"] = dataObject["doctorcode"]?.ToString();
                                    r["doctorname"] = dataObject["doctorname"]?.ToString();
                                    r["expressmed"] = dataObject["expressmed"]?.ToString();
                                    r["hn"] = dataObject["hn"]?.ToString();
                                    r["lastupdate"] = dataObject["lastupdate"]?.ToString();
                                    r["ordercreatedate"] = dataObject["ordercreatedate"]?.ToString();
                                    r["patientdob"] = dataObject["patientdob"]?.ToString();
                                    r["patientname"] = dataObject["patientname"]?.ToString();
                                    r["pharmacyitemcode"] = dataObject["pharmacyitemcode"]?.ToString();
                                    r["pharmacyitemdesc"] = dataObject["pharmacyitemdesc"]?.ToString();
                                    r["qn"] = dataObject["qn"]?.ToString();
                                    r["qrrdumix"] = dataObject["qrrdumix"]?.ToString();
                                    r["rcvmedno"] = dataObject["rcvmedno"]?.ToString();
                                    r["readdatetime"] = dataObject["readdatetime"]?.ToString();
                                    r["rightid"] = dataObject["rightid"]?.ToString();
                                    r["rightname"] = dataObject["rightname"]?.ToString();
                                    r["sex"] = dataObject["sex"]?.ToString();
                                    r["sphmlct"] = dataObject["sphmlct"]?.ToString();
                                    r["sphmname"] = dataObject["sphmname"]?.ToString();
                                    r["total_norefund"] = dataObject["total_norefund"]?.ToString();
                                    r["total_refund"] = dataObject["total_refund"]?.ToString();
                                    r["totalprice"] = dataObject["totalprice"]?.ToString();
                                    r["vn"] = dataObject["vn"]?.ToString();
                                    r["voiddatetime"] = dataObject["voiddatetime"]?.ToString();
                                    r["wardcode"] = dataObject["wardcode"]?.ToString();
                                    r["wardname"] = dataObject["wardname"]?.ToString();
                                    r["genorderdatetime"] = dataObject["genorderdatetime"]?.ToString();

                                    r["orderitemcode_ori"] = drugs["orderitemcode"]?.ToString();
                                    r["special_advice_text"] = drugs["special_advice_text"]?.ToString();
                                    r["orderqty_ori"] = drugs["orderqty"]?.ToString();
                                    r["strength"] = drugs["strength"]?.ToString();
                                    r["strengthunit"] = drugs["strengthunit"]?.ToString();
                                    r["instructioncode"] = drugs["instructioncode"]?.ToString();
                                    r["instructiondesc"] = drugs["instructiondesc"]?.ToString();
                                    r["dosage"] = drugs["dosage"]?.ToString();
                                    r["dosageunitcode"] = drugs["dosageunitcode"]?.ToString();
                                    r["dosageunitdesc"] = drugs["dosageunitdesc"]?.ToString();
                                    r["frequencycode"] = drugs["frequencycode"]?.ToString();
                                    r["frequencydesc"] = drugs["frequencydesc"]?.ToString();
                                    r["freetext1"] = drugs["freetext1"]?.ToString();
                                    r["freetext2"] = drugs["freetext2"]?.ToString();
                                    r["freetext3"] = drugs["freetext3"]?.ToString();
                                    r["freetext4"] = drugs["freetext4"]?.ToString();
                                    r["freetext5"] = drugs["freetext5"]?.ToString();
                                    r["price"] = drugs["price"]?.ToString();
                                    r["precaution_advice_text"] = drugs["precaution_advice_text"]?.ToString();
                                    r["special_advice_text"] = drugs["special_advice_text"]?.ToString();
                                    r["refund"] = drugs["refund"]?.ToString();
                                    r["norefund"] = drugs["norefund"]?.ToString();
                                    r["icsale"] = drugs["icsale"]?.ToString();
                                    r["icrefund"] = drugs["icrefund"]?.ToString();
                                    r["icnorefund"] = drugs["icnorefund"]?.ToString();
                                    r["store"] = drugs["store"]?.ToString();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($" Can't Find Success ");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }
                    if (db_packagemaster.Rows.Count > 0)
                    {
                        clsPackagemaster.db_packagemaster = new DataTable();
                        clsPackagemaster.db_packagemaster.Merge(db_packagemaster);
                    }
                    if (db_drug.Rows.Count > 0)
                    {
                        clsPackagemaster.db_drug = new DataTable();
                        clsPackagemaster.db_drug.Merge(db_drug);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }

        }
        public static async Task RequestPackagemasterByZone(string zonename, string itemidentify)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://office.gd4.co.th:6426/packagemaster/order/itemidentiy";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/packagemaster/order/itemidentify";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                                ""computername"": ""{clsSetting.COM_NAME}"", 
                                                ""itemidentify"": ""{itemidentify}""
                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            Console.WriteLine($"Find Success");

                            // ใช้ JArray เพื่อเข้าถึง "data"
                            JArray dataArray = (JArray)obj["data"];
                            DataTable dtobjdata = new DataTable();

                            // แปลง JArray เป็น List ของ Dictionary
                            clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();

                            JArray dataArraydata = new JArray();
                            List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                foreach (var key in dataObject.Keys)
                                {
                                    if (key != "drugallergies" && key != "packagemaster" && key != "drugs" && key != "labs")
                                    {
                                        Console.WriteLine($"{key}: {dataObject[key]}");
                                        dtobjdata.Columns.Add($"{key}", typeof(object));
                                    }

                                }
                                //dataArraydata = (JArray)dataObject["data"];
                                //objdata = dataArraydata.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("Data Details:");
                                DataRow row = dtobjdata.NewRow();
                                foreach (var key in dataObject.Keys)
                                {
                                    if (key != "drugallergies" && key != "packagemaster" && key != "drugs" && key != "labs")
                                    {
                                        Console.WriteLine($"{key}: {dataObject[key]}");
                                        row[key] = $"{dataObject[key]}"; // ถ้า null ให้ใส่ DBNull.Value                                       

                                    }

                                }
                                dtobjdata.Rows.Add(row);
                                Console.WriteLine();
                            }


                            if (dtobjdata.Rows.Count > 0)
                            {

                                clsPackagemaster.db_data.Merge(dtobjdata);
                            }




                            JArray dataArraypackagemaster = new JArray();
                            List<Dictionary<string, object>> objpackage = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                dataArraypackagemaster = (JArray)dataObject["packagemaster"];
                                objpackage = dataArraypackagemaster.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("Packagemaster Details:");
                                foreach (var key in dataObject.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObject[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjpackage = new DataTable();
                            if (objpackage == null || objpackage.Count == 0)
                                Console.WriteLine($"Can't Find Success");
                            // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                            foreach (var key in objpackage.First().Keys)
                            {
                                dtobjpackage.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                            }
                            // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                            foreach (var dict in objpackage)
                            {
                                DataRow row = dtobjpackage.NewRow();
                                foreach (var key in dict.Keys)
                                {
                                    row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                }
                                dtobjpackage.Rows.Add(row);

                            }
                            if (dtobjpackage.Rows.Count > 0)
                            {

                                clsPackagemaster.db_packagemaster.Merge(dtobjpackage);
                            }


                            JArray dataArraydrugs = new JArray();
                            List<Dictionary<string, object>> objdrugs = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                dataArraydrugs = (JArray)dataObject["drugs"];
                                objdrugs = dataArraydrugs.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("drug Details:");
                                foreach (var key in dataObject.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObject[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjdrugse = new DataTable();
                            if (objdrugs == null || objdrugs.Count == 0)
                                Console.WriteLine($"Can't Find Success");
                            // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                            foreach (var key in objdrugs.First().Keys)
                            {
                                dtobjdrugse.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                            }
                            // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                            foreach (var dict in objdrugs)
                            {
                                DataRow row = dtobjdrugse.NewRow();
                                foreach (var key in dict.Keys)
                                {
                                    row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                }
                                dtobjdrugse.Rows.Add(row);

                            }
                            if (dtobjdrugse.Rows.Count > 0)
                            {

                                clsPackagemaster.db_drug.Merge(dtobjdrugse);
                            }




                        }
                        else
                        {
                            Console.WriteLine($"Can't Find Success");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }

        }

        public static async Task RequestdetailAll(string startdate, string enddate, string prescriptionno)
        {
            clsPackagemaster.db_drug = new DataTable();
            clsPackagemaster.db_data = new DataTable();
            clsPackagemaster.db_packagemaster = new DataTable();
            clsPackagemaster.db_labs = new DataTable();
            clsPackagemaster.db_drugallergies = new DataTable();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://office.gd4.co.th:6426/packagemaster/order/all";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/detail/all";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                                ""startdate"": ""{startdate}"",
                                                ""enddate"": ""{enddate}"",
                                                ""prescriptionno"": ""{prescriptionno}""
                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);


                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            Console.WriteLine($"Find Success");

                            // ใช้ JArray เพื่อเข้าถึง "data"
                            JArray dataArray = (JArray)obj["data"];
                            DataTable dtobjdata = new DataTable();

                            // แปลง JArray เป็น List ของ Dictionary
                            clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();

                            JArray dataArraydata = new JArray();
                            List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                foreach (var key in dataObject.Keys)
                                {
                                    if (key != "drugallergies" && key != "packagemaster" && key != "drugs" && key != "labs")
                                    {
                                        Console.WriteLine($"{key}: {dataObject[key]}");
                                        dtobjdata.Columns.Add($"{key}", typeof(object));
                                    }

                                }
                                //dataArraydata = (JArray)dataObject["data"];
                                //objdata = dataArraydata.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("Data Details:");
                                DataRow row = dtobjdata.NewRow();
                                foreach (var key in dataObject.Keys)
                                {
                                    if (key != "drugallergies" && key != "packagemaster" && key != "drugs" && key != "labs")
                                    {
                                        Console.WriteLine($"{key}: {dataObject[key]}");
                                        row[key] = $"{dataObject[key]}"; // ถ้า null ให้ใส่ DBNull.Value                                       

                                    }

                                }
                                dtobjdata.Rows.Add(row);
                                Console.WriteLine();
                            }


                            if (dtobjdata.Rows.Count > 0)
                            {

                                clsPackagemaster.db_data.Merge(dtobjdata);
                            }

                            JArray dataArraypackagemaster = new JArray();
                            List<Dictionary<string, object>> objpackage = new List<Dictionary<string, object>>();

                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                dataArraypackagemaster = (JArray)dataObject["packagemaster"];
                                objpackage = dataArraypackagemaster.ToObject<List<Dictionary<string, object>>>();

                                Console.WriteLine("Packagemaster Details:");
                                foreach (var key in dataObject.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObject[key]}");
                                }
                                Console.WriteLine();
                            }

                            DataTable dtobjpackage = new DataTable();

                            if (objpackage == null || objpackage.Count == 0)
                            {
                                Console.WriteLine("Can't Find Success");
                            }
                            else
                            {
                                // รวม key ทั้งหมดจากทุก dictionary
                                var allKeys = objpackage.SelectMany(d => d.Keys).Distinct();

                                // สร้างคอลัมน์ทั้งหมดใน DataTable
                                foreach (var key in allKeys)
                                {
                                    if (!dtobjpackage.Columns.Contains(key))
                                    {
                                        dtobjpackage.Columns.Add(key, typeof(object)); // รองรับทุกประเภทข้อมูล
                                    }
                                }

                                // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                foreach (var dict in objpackage)
                                {
                                    DataRow row = dtobjpackage.NewRow();
                                    foreach (var key in dict.Keys)
                                    {
                                        row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                    }
                                    dtobjpackage.Rows.Add(row);
                                }

                                // Merge กับตารางหลัก
                                if (dtobjpackage.Rows.Count > 0)
                                {
                                    clsPackagemaster.db_packagemaster.Merge(dtobjpackage);
                                }
                            }


                                // drugs
                                JArray dataArraydrugs = new JArray();
                            List<Dictionary<string, object>> objdrugs = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                dataArraydrugs = (JArray)dataObject["drugs"];
                                objdrugs = dataArraydrugs.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("drug Details:");
                                foreach (var key in dataObject.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObject[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjdrugse = new DataTable();
                            if (objdrugs == null || objdrugs.Count == 0)
                            {
                                Console.WriteLine($"Can't Find Success");
                            }
                            else
                            {
                                // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                foreach (var key in objdrugs.First().Keys)
                                {
                                    dtobjdrugse.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                }
                                // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                foreach (var dict in objdrugs)
                                {
                                    DataRow row = dtobjdrugse.NewRow();
                                    foreach (var key in dict.Keys)
                                    {
                                        row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                    }
                                    dtobjdrugse.Rows.Add(row);

                                }
                                if (dtobjdrugse.Rows.Count > 0)
                                {

                                    clsPackagemaster.db_drug.Merge(dtobjdrugse);
                                }
                            }
                                
                            
                            // Labs
                            JArray dataArraylabs = new JArray();
                            List<Dictionary<string, object>> objlabs = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObjectlabs in clsPackagemaster.objresponseBody)
                            {
                                dataArraylabs = (JArray)dataObjectlabs["labs"];
                                objlabs = dataArraylabs.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("labs Details:");
                                foreach (var key in dataObjectlabs.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObjectlabs[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjlabs = new DataTable();
                            if (objlabs == null || objlabs.Count == 0)
                            {
                                Console.WriteLine($"Can't Find Success");
                            }
                            else
                            {

                                // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                foreach (var key in objlabs.First().Keys)
                                {
                                    dtobjlabs.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                }
                                // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                foreach (var dict in objlabs)
                                {
                                    DataRow row = dtobjlabs.NewRow();
                                    foreach (var key in dict.Keys)
                                    {
                                        row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                    }
                                    dtobjlabs.Rows.Add(row);

                                }
                                if (dtobjlabs.Rows.Count > 0)
                                {

                                    clsPackagemaster.db_labs.Merge(dtobjlabs);
                                }


                            }

                            // drugallergies
                            JArray dataArraydrugallergies = new JArray();
                            List<Dictionary<string, object>> objdrugallergies = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObjectdrugallergies in clsPackagemaster.objresponseBody)
                            {
                                dataArraydrugallergies = (JArray)dataObjectdrugallergies["drugallergies"];
                                objdrugallergies = dataArraydrugallergies.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("labs Details:");
                                foreach (var key in dataObjectdrugallergies.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObjectdrugallergies[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjdrugallergies = new DataTable();
                            if (objdrugallergies == null || objdrugallergies.Count == 0)
                            {
                                Console.WriteLine($"Can't Find Success");
                            }
                            else
                            {
                                // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                foreach (var key in objdrugallergies.First().Keys)
                                {
                                    dtobjdrugallergies.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                }
                            }
                          
                            // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                            foreach (var dict in objdrugallergies)
                            {
                                DataRow row = dtobjdrugallergies.NewRow();
                                foreach (var key in dict.Keys)
                                {
                                    row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                }
                                dtobjdrugallergies.Rows.Add(row);

                            }
                            if (dtobjdrugallergies.Rows.Count > 0)
                            {

                                clsPackagemaster.db_drugallergies.Merge(dtobjdrugallergies);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Can't Find Success");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }

        }
        public static async Task<DataSet> RequestdetailHN(string startdate, string enddate, string hn)
        {
            DataSet ds = new DataSet();
            //clsPackagemaster.db_drug = new DataTable();
            //clsPackagemaster.db_data = new DataTable();
            //clsPackagemaster.db_packagemaster = new DataTable();
            //clsPackagemaster.db_labs = new DataTable();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://office.gd4.co.th:6426/packagemaster/order/all";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/detail/historyall";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                                ""startdate"": ""{startdate}"",
                                                ""enddate"": ""{enddate}"",
                                                ""hn"": ""{hn}""
                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);


                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            Console.WriteLine($"Find Success");
                            // ใช้ JArray เพื่อเข้าถึง "data"
                            //JArray dataArray = (JArray)obj["data"];
                            JToken token = obj["data"];

                            if (token is JArray dataArray)
                            {
                                // data เป็นอาร์เรย์
                                clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();
                            }
                            else if (token is JObject dataObj)
                            {
                                // data เป็นอ็อบเจกต์เดี่ยว
                                clsPackagemaster.objresponseBody = new List<Dictionary<string, object>> { dataObj.ToObject<Dictionary<string, object>>() };
                            }
                            else
                            {
                                // ไม่มีข้อมูลหรือไม่ตรงรูปแบบ
                                clsPackagemaster.objresponseBody = new List<Dictionary<string, object>>();
                            }

                            
                            DataTable dtobjdata = new DataTable();

                            // แปลง JArray เป็น List ของ Dictionary
                            //clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();

                            JArray dataArraydata = new JArray();
                            List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                foreach (var key in dataObject.Keys)
                                {
                                    if (key != "drugallergies" && key != "packagemaster" && key != "drugs" && key != "labs")
                                    {
                                        Console.WriteLine($"{key}: {dataObject[key]}");
                                        dtobjdata.Columns.Add($"{key}", typeof(object));
                                    }

                                }
                                //dataArraydata = (JArray)dataObject["data"];
                                //objdata = dataArraydata.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("Data Details:");
                                DataRow row = dtobjdata.NewRow();
                                foreach (var key in dataObject.Keys)
                                {
                                    if (key != "drugallergies" && key != "packagemaster" && key != "drugs" && key != "labs")
                                    {
                                        Console.WriteLine($"{key}: {dataObject[key]}");
                                        row[key] = $"{dataObject[key]}"; // ถ้า null ให้ใส่ DBNull.Value                                       

                                    }

                                }
                                dtobjdata.Rows.Add(row);
                                Console.WriteLine();
                            }


                            if (dtobjdata.Rows.Count > 0)
                            {

                            }

                            JArray dataArraypackagemaster = new JArray();
                            List<Dictionary<string, object>> objpackage = new List<Dictionary<string, object>>();

                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                dataArraypackagemaster = (JArray)dataObject["packagemaster"];
                                objpackage = dataArraypackagemaster.ToObject<List<Dictionary<string, object>>>();

                                Console.WriteLine("Packagemaster Details:");
                                foreach (var key in dataObject.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObject[key]}");
                                }
                                Console.WriteLine();
                            }

                            DataTable dtobjpackage = new DataTable();

                            if (objpackage == null || objpackage.Count == 0)
                            {
                                Console.WriteLine("Can't Find Success");
                            }
                            else
                            {
                                // รวม key ทั้งหมดจากทุก dictionary
                                var allKeys = objpackage.SelectMany(d => d.Keys).Distinct();

                                // สร้างคอลัมน์ทั้งหมดใน DataTable
                                foreach (var key in allKeys)
                                {
                                    if (!dtobjpackage.Columns.Contains(key))
                                    {
                                        dtobjpackage.Columns.Add(key, typeof(object)); // รองรับทุกประเภทข้อมูล
                                    }
                                }

                                // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                foreach (var dict in objpackage)
                                {
                                    DataRow row = dtobjpackage.NewRow();
                                    foreach (var key in dict.Keys)
                                    {
                                        row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                    }
                                    dtobjpackage.Rows.Add(row);
                                }

                                // Merge กับตารางหลัก
                                if (dtobjpackage.Rows.Count > 0)
                                {
                                    
                                }
                            }


                            // drugs
                            JArray dataArraydrugs = new JArray();
                            List<Dictionary<string, object>> objdrugs = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                dataArraydrugs = (JArray)dataObject["drugs"];
                                objdrugs = dataArraydrugs.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("drug Details:");
                                foreach (var key in dataObject.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObject[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjdrugse = new DataTable();
                            if (objdrugs == null || objdrugs.Count == 0)
                            {
                                Console.WriteLine($"Can't Find Success");
                            }
                            else
                            {
                                // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                foreach (var key in objdrugs.First().Keys)
                                {
                                    dtobjdrugse.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                }
                                // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                foreach (var dict in objdrugs)
                                {
                                    DataRow row = dtobjdrugse.NewRow();
                                    foreach (var key in dict.Keys)
                                    {
                                        row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                    }
                                    dtobjdrugse.Rows.Add(row);

                                }
                                if (dtobjdrugse.Rows.Count > 0)
                                {

                                    
                                }
                            }


                            // Labs
                            JArray dataArraylabs = new JArray();
                            List<Dictionary<string, object>> objlabs = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObjectlabs in clsPackagemaster.objresponseBody)
                            {
                                dataArraylabs = (JArray)dataObjectlabs["labs"];
                                objlabs = dataArraylabs.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("labs Details:");
                                foreach (var key in dataObjectlabs.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObjectlabs[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjlabs = new DataTable();
                            if (objlabs == null || objlabs.Count == 0)
                            {
                                Console.WriteLine($"Can't Find Success");
                            }
                            else
                            {

                                // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                foreach (var key in objlabs.First().Keys)
                                {
                                    dtobjlabs.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                }
                                // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                foreach (var dict in objlabs)
                                {
                                    DataRow row = dtobjlabs.NewRow();
                                    foreach (var key in dict.Keys)
                                    {
                                        row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                    }
                                    dtobjlabs.Rows.Add(row);

                                }
                                if (dtobjlabs.Rows.Count > 0)
                                {

                                   
                                }


                            }

                            // drugallergies
                            JArray dataArraydrugallergies = new JArray();
                            List<Dictionary<string, object>> objdrugallergies = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObjectdrugallergies in clsPackagemaster.objresponseBody)
                            {
                                dataArraydrugallergies = (JArray)dataObjectdrugallergies["drugallergies"];
                                objdrugallergies = dataArraydrugallergies.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("labs Details:");
                                foreach (var key in dataObjectdrugallergies.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObjectdrugallergies[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjdrugallergies = new DataTable();
                            if (objdrugallergies == null || objdrugallergies.Count == 0)
                            {
                                Console.WriteLine($"Can't Find Success");
                            }
                            else
                            {
                                // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                foreach (var key in objdrugallergies.First().Keys)
                                {
                                    dtobjdrugallergies.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                }
                            }

                            // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                            foreach (var dict in objdrugallergies)
                            {
                                DataRow row = dtobjdrugallergies.NewRow();
                                foreach (var key in dict.Keys)
                                {
                                    row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                }
                                dtobjdrugallergies.Rows.Add(row);

                            }
                            if (dtobjdrugallergies.Rows.Count > 0)
                            {

                               
                            }

                            // ตั้งชื่อ
                            dtobjdata.TableName = "data";
                            dtobjpackage.TableName = "packagemaster";
                            dtobjdrugse.TableName = "drugs";
                            dtobjlabs.TableName = "labs";
                            dtobjdrugallergies.TableName = "drugallergies";

                            // รวมใน DataSet
                           
                            ds.Tables.Add(dtobjdata);
                            ds.Tables.Add(dtobjpackage);
                            ds.Tables.Add(dtobjdrugse);
                            ds.Tables.Add(dtobjlabs);
                            ds.Tables.Add(dtobjdrugallergies);

                            
                        }
                        else
                        {
                            Console.WriteLine($"Can't Find Success");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }
                    return ds;

                }
                catch (Exception ex)
                {
                    return ds;
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }


        }
        public static async Task RequestDevice()
        {
            #region creatdb_drug
            DataTable db_device = new DataTable();
            db_device.Columns.Add("_id", typeof(String));
            db_device.Columns.Add("__v", typeof(String));
            db_device.Columns.Add("computerName", typeof(String));
            db_device.Columns.Add("deviceCode", typeof(String));
            db_device.Columns.Add("deviceDesc", typeof(String));
            db_device.Columns.Add("deviceIP", typeof(String));
            db_device.Columns.Add("deviceName", typeof(String));
            db_device.Columns.Add("isEnabled", typeof(String));
            db_device.Columns.Add("pharmacyCode", typeof(String));
            db_device.Columns.Add("orderzone", typeof(String));
            db_device.Columns.Add("sortOrder", typeof(String));

            #endregion

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://office.gd4.co.th:6426/device/all";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/device/all";

                    apiUrl = string.Format(apiUrl);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            Console.WriteLine($" Find Success ");
                            // Access the "data" array
                            JArray dataArray = (JArray)obj["data"];

                            // Loop through "data" objects
                            foreach (JObject device in dataArray)
                            {

                                DataRow r = db_device.Rows.Add();
                                r["_id"] = device["_id"]?.ToString();
                                r["__v"] = device["__v"]?.ToString();
                                r["computerName"] = device["computerName"]?.ToString();
                                r["deviceCode"] = device["deviceCode"]?.ToString();
                                r["deviceDesc"] = device["deviceDesc"]?.ToString();
                                r["deviceIP"] = device["deviceIP"]?.ToString();
                                r["deviceName"] = device["deviceName"]?.ToString();
                                r["isEnabled"] = device["isEnabled"]?.ToString();
                                r["pharmacyCode"] = device["pharmacyCode"]?.ToString();
                                r["orderzone"] = device["orderzone"]?.ToString();
                                r["sortOrder"] = device["sortOrder"]?.ToString();

                            }
                        }
                        else
                        {
                            Console.WriteLine($" Can't Find Success ");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }
                    if (db_device.Rows.Count > 0)
                    {
                        clsPackagemaster.db_device = new DataTable();
                        clsPackagemaster.db_device.Merge(db_device);
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }
        }
        public static async Task<(DataTable,DataTable)> RequestLogig(string username,string password)
        {
            #region creat db_user
            DataTable db_user = new DataTable();
            db_user.Columns.Add("_id", typeof(String));
            db_user.Columns.Add("userID", typeof(String));
            db_user.Columns.Add("fullname", typeof(String));
            db_user.Columns.Add("username", typeof(String));
            db_user.Columns.Add("password", typeof(String));
            db_user.Columns.Add("type", typeof(String));
            DataTable dtobjdata = new DataTable();
            DataTable dtauthorized = new DataTable();
            #endregion

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://office.gd4.co.th:6426/auth/permissions/login";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/master/login";

                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                                ""username"": ""{username}"",
                                                ""password"":""{password}""
                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            var dataToken = obj["data"];

                            if (dataToken is JArray dataArray)
                            {
                                clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();
                            }
                            else if (dataToken is JObject dataObject)
                            {
                                // ถ้าข้อมูลเป็น object เดี่ยว
                                var list = new List<Dictionary<string, object>>
                                {
                                    dataObject.ToObject<Dictionary<string, object>>()
                                };
                                clsPackagemaster.objresponseBody = list;
                            }

                            //JArray dataArray = (JArray)obj["data"];

                            // แปลง JArray เป็น List ของ Dictionary
                            //clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();
                            if (clsPackagemaster.objresponseBody != null && clsPackagemaster.objresponseBody.Count > 0)
                            {
                                JArray dataArraydata = new JArray();
                                List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                                if (clsPackagemaster.objresponseBody == null || clsPackagemaster.objresponseBody.Count == 0)
                                {
                                    Console.WriteLine($"Can't Find Success");
                                }
                                else
                                {
                                    // วนลูปแสดงผลข้อมูลแต่ละ Object
                                    foreach (var dataObject in clsPackagemaster.objresponseBody)
                                    {
                                        foreach (var key in dataObject.Keys)
                                        {
                                            if (key != "authorized")
                                            {
                                                Console.WriteLine($"{key}: {dataObject[key]}");
                                                //dtobjdata.Columns.Add($"{key}", typeof(object));

                                                string columnName = key.ToString();
                                                if (dtobjdata.Columns.Contains(columnName))
                                                {
                                                    Console.WriteLine($"Column '{columnName}' exists.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Column '{columnName}' does not exist.");
                                                    dtobjdata.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                                }
                                            }

                                        }
                                        //dataArraydata = (JArray)dataObject["data"];
                                        //objdata = dataArraydata.ToObject<List<Dictionary<string, object>>>();
                                        Console.WriteLine("Data Details:");
                                        DataRow row = dtobjdata.NewRow();
                                        foreach (var key in dataObject.Keys)
                                        {
                                            if (key != "authorized" )
                                            {
                                                Console.WriteLine($"{key}: {dataObject[key]}");
                                                row[key] = $"{dataObject[key]}"; // ถ้า null ให้ใส่ DBNull.Value                                       

                                            }

                                        }
                                        dtobjdata.Rows.Add(row);
                                        Console.WriteLine();
                                    }


                                    if (dtobjdata.Rows.Count > 0)
                                    {

                                        //clsPackagemaster.db_data.Merge(dtobjdata);
                                    }


                                    JArray dataArrayauthorized = new JArray();
                                    List<Dictionary<string, object>> objauthorized = new List<Dictionary<string, object>>();
                                    
                                    dtauthorized.Columns.Add("authorized", typeof(string)); // เพิ่มคอลัมน์เดียว

                                    foreach (var dataObject in clsPackagemaster.objresponseBody)
                                    {
                                        // ตรวจสอบว่ามี key "authorized" และไม่ใช่ null
                                        if (dataObject.TryGetValue("authorized", out object authorizedValue) && authorizedValue != null)
                                        {
                                            JToken authorizedToken = authorizedValue as JToken;

                                            if (authorizedToken.Type == JTokenType.Array)
                                            {
                                                dataArrayauthorized = (JArray)authorizedToken;

                                                foreach (JToken item in dataArrayauthorized)
                                                {
                                                    DataRow row = dtauthorized.NewRow();
                                                    row["authorized"] = item.ToString(); // ใส่เป็น string
                                                    dtauthorized.Rows.Add(row);
                                                }
                                            }
                                        }
                                    }

                                    // ตรวจสอบว่ามีข้อมูล และ merge ไปยัง clsPackagemaster.dt_authorized
                                    if (dtauthorized.Rows.Count > 0)
                                    {
                                        clsPackagemaster.dt_authorized = new DataTable();
                                        clsPackagemaster.dt_authorized.Merge(dtauthorized);
                                    }



                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($" Can't Find Success ");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }

                    return (dtobjdata,dtauthorized);
                }
                catch (Exception ex)
                {
                    return (dtobjdata, dtauthorized);
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }
        }
        public static async Task<DataTable> RequestUserID(string userid)
        {
            #region creat db_user
            DataTable db_user = new DataTable();
            db_user.Columns.Add("_id", typeof(String));
            db_user.Columns.Add("userID", typeof(String));
            db_user.Columns.Add("fullname", typeof(String));
            db_user.Columns.Add("username", typeof(String));
            db_user.Columns.Add("password", typeof(String));
            db_user.Columns.Add("type", typeof(String));
            db_user.Columns.Add("firstname", typeof(String));
            db_user.Columns.Add("lastname", typeof(String));


            #endregion

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://office.gd4.co.th:6426/users/userid";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/master/login";

                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                                ""userID"": ""{userid}""
                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            Console.WriteLine($" Find Success ");
                            // Access the "data" array
                            JArray dataArray = (JArray)obj["data"];
                            clsPackagemaster.obuser = obj["data"].ToObject<List<object>>();
                            // Loop through "data" objects
                            foreach (JObject user in dataArray)
                            {
                                DataRow r = db_user.Rows.Add();
                                r["_id"] = user["_id"]?.ToString();
                                r["userID"] = user["userID"]?.ToString();
                                r["fullname"] = user["fullname"]?.ToString();
                                r["username"] = user["username"]?.ToString();
                                r["password"] = user["password"]?.ToString();
                                r["type"] = user["type"]?.ToString();
                                r["firstname"] = user["firstname"]?.ToString();
                                r["lastname"] = user["lastname"]?.ToString();

                            }
                        }
                        else
                        {
                            Console.WriteLine($" Can't Find Success ");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }

                    return db_user;
                }
                catch (Exception ex)
                {
                    return new DataTable();
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }
        }
        public static async Task<DataTable> Requestopdqueuepharmacy()
        {
            clsPackagemaster.db_data = new DataTable();
            clsPackagemaster.db_drug = new DataTable();
            clsPackagemaster.db_drugallergies = new DataTable();
            clsPackagemaster.db_labs = new DataTable();
            clsPackagemaster.db_packagemaster = new DataTable();

            DataTable dtobjdata = new DataTable();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        

                        //string apiUrl = "http://192.168.30.14:6426/packagemaster/order/firstzone";
                        string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl.Trim() + "/dashboard/opdqueuepharmacy";
                        apiUrl = string.Format(apiUrl);
                        // สร้างข้อมูล JSON ที่ต้องการส่ง
                        string jsonContent = $@"";

                        // สร้าง HttpContent (StringContent)
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.GetAsync(apiUrl);


                        if (response.IsSuccessStatusCode)
                        {

                            string responseBody = await response.Content.ReadAsStringAsync();
                            //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                            Console.WriteLine(responseBody);
                            JObject obj = JObject.Parse(responseBody);
                            if (obj["status"].ToString() == "200")
                            {
                                Console.WriteLine($"Find Success");

                                // ใช้ JArray เพื่อเข้าถึง "data"
                                JArray dataArray = (JArray)obj["data"][0];
                                

                                // แปลง JArray เป็น List ของ Dictionary
                                clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();
                                if (clsPackagemaster.objresponseBody.Count > 0)
                                {
                                    JArray dataArraydata = new JArray();
                                    List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                                    if (clsPackagemaster.objresponseBody == null || clsPackagemaster.objresponseBody.Count == 0)
                                    {
                                        Console.WriteLine($"Can't Find Success");
                                    }
                                    else
                                    {
                                        // วนลูปแสดงผลข้อมูลแต่ละ Object
                                        foreach (var dataObject in clsPackagemaster.objresponseBody)
                                        {
                                            foreach (var key in dataObject.Keys)
                                            {
                                                //Console.WriteLine($"{key}: {dataObject[key]}");
                                                dtobjdata.Columns.Add($"{key}", typeof(object));

                                            }
                                            //dataArraydata = (JArray)dataObject["data"];
                                            //objdata = dataArraydata.ToObject<List<Dictionary<string, object>>>();
                                            //Console.WriteLine("Data Details:");
                                            DataRow row = dtobjdata.NewRow();
                                            foreach (var key in dataObject.Keys)
                                            {
                                                //Console.WriteLine($"{key}: {dataObject[key]}");
                                                row[key] = $"{dataObject[key]}"; // ถ้า null ให้ใส่ DBNull.Value           
                                            }
                                            dtobjdata.Rows.Add(row);
                                            Console.WriteLine();
                                        }


                                        if (dtobjdata.Rows.Count > 0)
                                        {

                                            //clsPackagemaster.db_data.Merge(dtobjdata);
                                        }

                                    }
                                }

                            }
                            else
                            {
                                Console.WriteLine($"Can't Find Success");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                        }

                        return dtobjdata;
                    }
                    catch (Exception ex)
                    {
                        clsPackagemaster.db_packagemaster = new DataTable();
                        clsPackagemaster.db_drug = new DataTable();
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        Console.WriteLine($" Can't Find Success ");
                        return dtobjdata;
                    }
                }
            }
            catch (Exception ex)
            {
                clsPackagemaster.db_packagemaster = new DataTable();
                clsPackagemaster.db_drug = new DataTable();
                MessageBox.Show(ex.ToString());
                return dtobjdata;

            }

        }
        public static async Task<string> RequestNewQueue(string AN)
        {
            string QN = "";

            using (HttpClient client = new HttpClient())
            {
                try
                {

                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/queue/new/" + AN + "";


                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);

                        if (responseBody != "")
                        {
                            QN = responseBody;
                        }
                        else
                        {
                            Console.WriteLine($" Can't Find Success ");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }

                    return QN;
                }
                catch (Exception ex)
                {
                    return QN;
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }
        }
        public static async Task<DataTable> callorder()
        {
            DataTable dtj = new DataTable();
            DataTable dt = new DataTable();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string startdate = DateTime.Now.ToString("yyyy-MM-dd");
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/queue/callqueue";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                               
                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();

                        Console.WriteLine(responseBody);

                        // var person = JsonSerializer.Deserialize<Person>(responseBody);
                        //dynamic item = JsonConvert.DeserializeObject(responseBody);
                        //var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);

                        // แปลง JSON array เป็น JArray
                        var jArray = JArray.Parse(responseBody);


                        // สร้าง column ตาม property
                        foreach (JProperty prop in ((JObject)jArray[0]).Properties())
                        {
                            if (prop.Name == "eventstate")
                                dt.Columns.Add(prop.Name, typeof(long[]));  // กำหนด array
                            else
                                dt.Columns.Add(prop.Name);
                        }

                        // เติมข้อมูล
                        foreach (JObject obj in jArray)
                        {
                            DataRow row = dt.NewRow();
                            foreach (JProperty prop in obj.Properties())
                            {
                                if (prop.Name == "eventstate")
                                {
                                    // แปลง JArray เป็น long[]
                                    JArray ja = (JArray)prop.Value;
                                    row["eventstate"] = ja.ToObject<long[]>();
                                }
                                else
                                {
                                    row[prop.Name] = prop.Value.Type == JTokenType.Null ? DBNull.Value : prop.Value.ToObject<object>();
                                }
                            }
                            dt.Rows.Add(row);
                        }



                        //*******************************************/

                        // dt = JsonConvert.DeserializeObject<DataTable>(responseBody);

                        //return dt;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }

            return dt;
        }
        public static async Task<bool> UpdateEvenQueue(string keyword, string even)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/queue/event";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                                ""_id"": ""{ keyword}"",
                                                ""eventstate"":{even}

                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                    Console.WriteLine(content);
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        //JObject obj = JObject.Parse(responseBody);
                        //if (obj["status"].ToString() == "200")
                        //{
                        //    //clsPackagemaster.obprescription = obj["data"].ToObject<List<object>>();
                        //    Console.WriteLine($" Find Success ");
                        //}
                        //else
                        //{
                        //    Console.WriteLine($" Can't Find Success ");
                        //}
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }
                    return true;


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                    return false;
                }
                finally
                {

                }
            }
        }
        public static async Task<bool> update_logevent(List<object> ListJson)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/general/logevent";
                    string jsonString = "";
                    if (ListJson.Count > 0)
                    {
                        jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(ListJson[0], Newtonsoft.Json.Formatting.Indented); // System.Text.Json.JsonSerializer.Serialize(
                            //ListJson[0],  // ✅ serialize object เดี่ยว
                           // new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
                        //);
                    }

                    if (!string.IsNullOrWhiteSpace(jsonString))
                    {
                        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        string responseBody = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            try
                            {
                                JObject obj = JObject.Parse(responseBody);

                                if (obj["status"]?.ToString() == "200")
                                {
                                    Console.WriteLine("Find Success");
                                    // คุณสามารถใช้ obj["data"] ได้ที่นี่ถ้าต้องการ
                                }
                                else
                                {
                                    Console.WriteLine("Can't Find Success");
                                }
                            }
                            catch (JsonReaderException ex)
                            {
                                Console.WriteLine("JSON Parse Error: " + ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                        }

                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message);
                    return false;
                }
            }
        }

        public static async Task<DataTable> RequestStatusAN(string an)
        {            
            DataTable dtobjdata = new DataTable();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://office.gd4.co.th:6426/packagemaster/order/all";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/prescriptionstatus";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                                ""an"": ""{an}""
                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);


                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            Console.WriteLine($"Find Success");

                            // ใช้ JArray เพื่อเข้าถึง "data"
                            JArray dataArray = (JArray)obj["data"];                            

                            // แปลง JArray เป็น List ของ Dictionary
                            clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();

                            JArray dataArraydata = new JArray();
                            List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            objdata = dataArray.ToObject<List<Dictionary<string, object>>>();

                            if (objdata == null || objdata.Count == 0)
                            {
                                Console.WriteLine($"Can't Find Success");
                            }
                            else
                            {
                                // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                foreach (var key in objdata.First().Keys)
                                {
                                    dtobjdata.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                }
                                // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                foreach (var dict in objdata)
                                {
                                    DataRow row = dtobjdata.NewRow();
                                    foreach (var key in dict.Keys)
                                    {
                                        row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                    }
                                    dtobjdata.Rows.Add(row);

                                }                               

                            }
                        }
                        else
                        {
                            Console.WriteLine($"Can't Find Success");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }

                    return dtobjdata;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                    return dtobjdata;
                }
            }

        }

        public static async Task<bool> update_rx(List<object> ListJson)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://192.168.30.14:6426/packagemaster/order/update";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/updaterxipd";
                    apiUrl = string.Format(apiUrl);
                    
                    var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(ListJson, Newtonsoft.Json.Formatting.Indented);// System.Text.Json.JsonSerializer.Serialize(
                        //ListJson,
                        //new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
                    //);

                    Console.WriteLine(jsonString);

                    Console.WriteLine(jsonString);
                    if (jsonString.Length > 0)
                    {
                        // สร้าง HttpContent (StringContent)
                        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                        Console.WriteLine(content);
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                            Console.WriteLine(responseBody);
                            JObject obj = JObject.Parse(responseBody);
                            if (obj["status"].ToString() == "200")
                            {
                                //clsPackagemaster.obprescription = obj["data"].ToObject<List<object>>();
                                Console.WriteLine($" Find Success ");
                            }
                            else
                            {
                                Console.WriteLine($" Can't Find Success ");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                    return false;
                }
                finally
                {

                }
            }
        }

        public static async Task<bool> update_barcodePredx(string prescr)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://192.168.30.14:6426/packagemaster/order/update";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/device/predx/getqr/"+ prescr;
                    apiUrl = string.Format(apiUrl);

                    if (apiUrl.Length > 0)
                    {                        
                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                            Console.WriteLine(responseBody);
                            JObject obj = JObject.Parse(responseBody);
                            if (obj["status"].ToString() == "200")
                            {
                                //clsPackagemaster.obprescription = obj["data"].ToObject<List<object>>();
                                Console.WriteLine($" Find Success ");
                                return true;
                            }
                            else
                            {
                                Console.WriteLine($" Can't Find Success ");
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                            return false;
                        }
                        
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                    return false;
                }
                finally
                {

                }
            }
        }

        public static async Task<DataSet> RequestdetailAN(string startdate, string enddate, string an)
        {
            DataSet ds = new DataSet();
            //clsPackagemaster.db_drug = new DataTable();
            //clsPackagemaster.db_data = new DataTable();
            //clsPackagemaster.db_packagemaster = new DataTable();
            //clsPackagemaster.db_labs = new DataTable();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //string apiUrl = "http://office.gd4.co.th:6426/packagemaster/order/all";
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/detail/historyall";
                    apiUrl = string.Format(apiUrl);
                    // สร้างข้อมูล JSON ที่ต้องการส่ง
                    string jsonContent = $@"{{
                                                ""startdate"": ""{startdate}"",
                                                ""enddate"": ""{enddate}"",
                                                ""an"": ""{an}""
                                            }}";

                    // สร้าง HttpContent (StringContent)
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);


                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();
                        //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                        Console.WriteLine(responseBody);
                        JObject obj = JObject.Parse(responseBody);
                        if (obj["status"].ToString() == "200")
                        {
                            Console.WriteLine($"Find Success");
                            // ใช้ JArray เพื่อเข้าถึง "data"
                            //JArray dataArray = (JArray)obj["data"];
                            JToken token = obj["data"];

                            if (token is JArray dataArray)
                            {
                                // data เป็นอาร์เรย์
                                clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();
                            }
                            else if (token is JObject dataObj)
                            {
                                // data เป็นอ็อบเจกต์เดี่ยว
                                clsPackagemaster.objresponseBody = new List<Dictionary<string, object>> { dataObj.ToObject<Dictionary<string, object>>() };
                            }
                            else
                            {
                                // ไม่มีข้อมูลหรือไม่ตรงรูปแบบ
                                clsPackagemaster.objresponseBody = new List<Dictionary<string, object>>();
                            }


                            DataTable dtobjdata = new DataTable();

                            // แปลง JArray เป็น List ของ Dictionary
                            //clsPackagemaster.objresponseBody = dataArray.ToObject<List<Dictionary<string, object>>>();

                            JArray dataArraydata = new JArray();
                            List<Dictionary<string, object>> objdata = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                foreach (var key in dataObject.Keys)
                                {
                                    if (key != "drugallergies" && key != "packagemaster" && key != "drugs" && key != "labs")
                                    {
                                        Console.WriteLine($"{key}: {dataObject[key]}");
                                        dtobjdata.Columns.Add($"{key}", typeof(object));
                                    }

                                }
                                //dataArraydata = (JArray)dataObject["data"];
                                //objdata = dataArraydata.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("Data Details:");
                                DataRow row = dtobjdata.NewRow();
                                foreach (var key in dataObject.Keys)
                                {
                                    if (key != "drugallergies" && key != "packagemaster" && key != "drugs" && key != "labs")
                                    {
                                        Console.WriteLine($"{key}: {dataObject[key]}");
                                        row[key] = $"{dataObject[key]}"; // ถ้า null ให้ใส่ DBNull.Value                                       

                                    }

                                }
                                dtobjdata.Rows.Add(row);
                                Console.WriteLine();
                            }


                            if (dtobjdata.Rows.Count > 0)
                            {

                            }

                            JArray dataArraypackagemaster = new JArray();
                            List<Dictionary<string, object>> objpackage = new List<Dictionary<string, object>>();

                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                dataArraypackagemaster = (JArray)dataObject["packagemaster"];
                                objpackage = dataArraypackagemaster.ToObject<List<Dictionary<string, object>>>();

                                Console.WriteLine("Packagemaster Details:");
                                foreach (var key in dataObject.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObject[key]}");
                                }
                                Console.WriteLine();
                            }

                            DataTable dtobjpackage = new DataTable();

                            if (objpackage == null || objpackage.Count == 0)
                            {
                                Console.WriteLine("Can't Find Success");
                            }
                            else
                            {
                                // รวม key ทั้งหมดจากทุก dictionary
                                var allKeys = objpackage.SelectMany(d => d.Keys).Distinct();

                                // สร้างคอลัมน์ทั้งหมดใน DataTable
                                foreach (var key in allKeys)
                                {
                                    if (!dtobjpackage.Columns.Contains(key))
                                    {
                                        dtobjpackage.Columns.Add(key, typeof(object)); // รองรับทุกประเภทข้อมูล
                                    }
                                }

                                // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                foreach (var dict in objpackage)
                                {
                                    DataRow row = dtobjpackage.NewRow();
                                    foreach (var key in dict.Keys)
                                    {
                                        row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                    }
                                    dtobjpackage.Rows.Add(row);
                                }

                                // Merge กับตารางหลัก
                                if (dtobjpackage.Rows.Count > 0)
                                {

                                }
                            }


                            // drugs
                            JArray dataArraydrugs = new JArray();
                            List<Dictionary<string, object>> objdrugs = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObject in clsPackagemaster.objresponseBody)
                            {
                                dataArraydrugs = (JArray)dataObject["drugs"];
                                objdrugs = dataArraydrugs.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("drug Details:");
                                foreach (var key in dataObject.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObject[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjdrugse = new DataTable();
                            if (objdrugs == null || objdrugs.Count == 0)
                            {
                                Console.WriteLine($"Can't Find Success");
                            }
                            else
                            {
                                // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                foreach (var key in objdrugs.First().Keys)
                                {
                                    dtobjdrugse.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                }
                                // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                foreach (var dict in objdrugs)
                                {
                                    DataRow row = dtobjdrugse.NewRow();
                                    foreach (var key in dict.Keys)
                                    {
                                        row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                    }
                                    dtobjdrugse.Rows.Add(row);

                                }
                                if (dtobjdrugse.Rows.Count > 0)
                                {


                                }
                            }


                            // Labs
                            JArray dataArraylabs = new JArray();
                            List<Dictionary<string, object>> objlabs = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObjectlabs in clsPackagemaster.objresponseBody)
                            {
                                dataArraylabs = (JArray)dataObjectlabs["labs"];
                                objlabs = dataArraylabs.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("labs Details:");
                                foreach (var key in dataObjectlabs.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObjectlabs[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjlabs = new DataTable();
                            if (objlabs == null || objlabs.Count == 0)
                            {
                                Console.WriteLine($"Can't Find Success");
                            }
                            else
                            {

                                // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                foreach (var key in objlabs.First().Keys)
                                {
                                    dtobjlabs.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                }
                                // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                                foreach (var dict in objlabs)
                                {
                                    DataRow row = dtobjlabs.NewRow();
                                    foreach (var key in dict.Keys)
                                    {
                                        row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                    }
                                    dtobjlabs.Rows.Add(row);

                                }
                                if (dtobjlabs.Rows.Count > 0)
                                {


                                }


                            }

                            // drugallergies
                            JArray dataArraydrugallergies = new JArray();
                            List<Dictionary<string, object>> objdrugallergies = new List<Dictionary<string, object>>();
                            // วนลูปแสดงผลข้อมูลแต่ละ Object
                            foreach (var dataObjectdrugallergies in clsPackagemaster.objresponseBody)
                            {
                                dataArraydrugallergies = (JArray)dataObjectdrugallergies["drugallergies"];
                                objdrugallergies = dataArraydrugallergies.ToObject<List<Dictionary<string, object>>>();
                                Console.WriteLine("labs Details:");
                                foreach (var key in dataObjectdrugallergies.Keys)
                                {
                                    Console.WriteLine($"{key}: {dataObjectdrugallergies[key]}");
                                }
                                Console.WriteLine();
                            }
                            DataTable dtobjdrugallergies = new DataTable();
                            if (objdrugallergies == null || objdrugallergies.Count == 0)
                            {
                                Console.WriteLine($"Can't Find Success");
                            }
                            else
                            {
                                // สร้างคอลัมน์จาก key ใน Dictionary ตัวแรก
                                foreach (var key in objdrugallergies.First().Keys)
                                {
                                    dtobjdrugallergies.Columns.Add(key, typeof(object)); // ใช้ object รองรับทุกประเภทข้อมูล
                                }
                            }

                            // เพิ่มข้อมูลแต่ละ Dictionary เป็นแถวของ DataTable
                            foreach (var dict in objdrugallergies)
                            {
                                DataRow row = dtobjdrugallergies.NewRow();
                                foreach (var key in dict.Keys)
                                {
                                    row[key] = dict[key] ?? DBNull.Value; // ถ้า null ให้ใส่ DBNull.Value
                                }
                                dtobjdrugallergies.Rows.Add(row);

                            }
                            if (dtobjdrugallergies.Rows.Count > 0)
                            {


                            }

                            // ตั้งชื่อ
                            dtobjdata.TableName = "data";
                            dtobjpackage.TableName = "packagemaster";
                            dtobjdrugse.TableName = "drugs";
                            dtobjlabs.TableName = "labs";
                            dtobjdrugallergies.TableName = "drugallergies";

                            // รวมใน DataSet

                            ds.Tables.Add(dtobjdata);
                            ds.Tables.Add(dtobjpackage);
                            ds.Tables.Add(dtobjdrugse);
                            ds.Tables.Add(dtobjlabs);
                            ds.Tables.Add(dtobjdrugallergies);


                        }
                        else
                        {
                            Console.WriteLine($"Can't Find Success");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }
                    return ds;

                }
                catch (Exception ex)
                {
                    return ds;
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($" Can't Find Success ");
                }
            }


        }
        public static async Task<bool> update_void(string _id, string voiduserid, string voidusername, string voiddesc)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/item/cancel";
                    apiUrl = string.Format(apiUrl);

                    string jsonContent = $@"{{
                                                ""_id"": ""{_id}"",
                                                ""voiduserid"": ""{voiduserid}"",
                                                ""voidusername"": ""{voidusername}"",
                                                ""voiddesc"": ""{voiddesc}""
                                            }}";

                    if (jsonContent.Length > 0)
                    {
                        // สร้าง HttpContent (StringContent)
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        Console.WriteLine(content);
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                            Console.WriteLine(responseBody);
                            JObject obj = JObject.Parse(responseBody);
                            if (obj["status"].ToString() == "200")
                            {
                                //clsPackagemaster.obprescription = obj["data"].ToObject<List<object>>();
                                Console.WriteLine($" Find Success ");
                            }
                            else
                            {
                                Console.WriteLine($" Can't Find Success ");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                    return false;
                }
                finally
                {

                }
            }
        }
        public static async Task<bool> updatePre_void(string _id, string voiduserid, string voidusername, string voiddesc)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/order/press/cancel";
                    apiUrl = string.Format(apiUrl);

                    string jsonContent = $@"{{
                                                ""_id"": ""{_id}"",
                                                ""voiduserid"": ""{voiduserid}"",
                                                ""voidusername"": ""{voidusername}"",
                                                ""voiddesc"": ""{voiddesc}""
                                            }}";

                    if (jsonContent.Length > 0)
                    {
                        // สร้าง HttpContent (StringContent)
                        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        Console.WriteLine(content);
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            //responseBody = responseBody.Substring(1, responseBody.Length - 2);
                            Console.WriteLine(responseBody);
                            JObject obj = JObject.Parse(responseBody);
                            if (obj["status"].ToString() == "200")
                            {
                                //clsPackagemaster.obprescription = obj["data"].ToObject<List<object>>();
                                Console.WriteLine($" Find Success ");
                            }
                            else
                            {
                                Console.WriteLine($" Can't Find Success ");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Failed to retrieve data. Status code: {response.StatusCode}");
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.ToString());
                    return false;
                }
                finally
                {

                }
            }
        }
        public static async Task<DataTable> mederror()
        {
            DataTable dt = new DataTable();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = PrescriptionManagement.Properties.Settings.Default.apiUrl + "/emar/medicationerrordetail";

                    string jsonContent = @"{ }";
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);

                        // ✅ Parse เป็น JObject ก่อน
                        JObject obj = JObject.Parse(responseBody);

                        // ✅ ดึง data ซึ่งเป็น JArray
                        JArray jArray = (JArray)obj["data"];

                        if (jArray.Count > 0)
                        {
                            // สร้าง columns
                            foreach (JProperty prop in ((JObject)jArray[0]).Properties())
                            {
                                dt.Columns.Add(prop.Name);
                            }

                            // เติมข้อมูล
                            foreach (JObject item in jArray)
                            {
                                DataRow row = dt.NewRow();
                                foreach (JProperty prop in item.Properties())
                                {
                                    row[prop.Name] = prop.Value.Type == JTokenType.Null ? DBNull.Value : prop.Value.ToObject<object>();
                                }
                                dt.Rows.Add(row);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine($"Can't Find Success");
                }
            }

            return dt;
        }


    }
}
