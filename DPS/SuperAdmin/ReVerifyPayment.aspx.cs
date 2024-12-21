using DPS.SuperAdmin.PaymentConfigurationClassFIle;
using DPS.SuperAdmin.SchoolClassFile;
using Payrequest;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using DPS.SuperAdmin.Reverify;
using DPS.Student.FeeClassFile;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using DPS.SuperAdmin.ReVerifyResult;

namespace DPS.SuperAdmin
{
    public partial class ReVerifyPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSchools();
            }
        }
        private void BindSchools()
        {
            try
            {
                // Instantiate SchoolBLL and call the method
                SchoolBLL schoolBLL = new SchoolBLL();
                DataTable dt = schoolBLL.GetAllActiveSchools();

                // Clear existing items
                ddlSchool.Items.Clear();

                // Bind the DataTable to the DropDownList
                ddlSchool.DataSource = dt;
                ddlSchool.DataTextField = "Name";  // Field to display in the dropdown
                ddlSchool.DataValueField = "ID";   // Field to use for postback
                ddlSchool.DataBind();

                ddlSchool.Items.Insert(0, new ListItem("Select School", "0"));
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while retrieving the data.');", true);
            }
        }

        protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindNonVerified();
        }
        public void BindNonVerified()
        {
            SchoolBLL schoolBLL = new SchoolBLL();
            DataTable dt = schoolBLL.GetSchoolById(int.Parse(ddlSchool.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                string databaseName = dt.Rows[0]["ID_DATABASE"].ToString();
                Session["databaseName"] = databaseName;
                string sqlServerConnectionString = @"Data Source=150.242.203.229;Initial Catalog=" + databaseName + ";User Id=dpsuser;Password=dps@123;Integrated Security=False;MultipleActiveResultSets=True;Connect Timeout=50000;";
                SqlConnection con = new SqlConnection(sqlServerConnectionString);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string query = "SELECT [ID],[ScholarNumber],[StudentName],[Amount],[AtomID],[TransactionID],[TransactionDate],[IsReverified]  FROM [FeeTransactionRequest]";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(com);
                DataTable dt2 = new DataTable();
                sda.Fill(dt2);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                GridView1.DataSource = dt2;
                GridView1.DataBind();
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            SchoolPaymentConfigurationDAL dal = new SchoolPaymentConfigurationDAL();
            DataTable dt = new DataTable();
            int schoolID = int.Parse(ddlSchool.SelectedValue.ToString());
            dt = dal.GetSchoolPaymentConfigurationByClientId(schoolID);
            if(dt.Rows.Count>0)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    // Accessing specific columns (cells) of each row
                    string id = ((System.Web.UI.WebControls.Label)row.FindControl("lblId")).Text;
                    //string bpNumber = ((Label)row.FindControl("lblDeptName")).Text;
                    string amount = ((System.Web.UI.WebControls.Label)row.FindControl("lblAmount")).Text;
                    string transactionID = ((System.Web.UI.WebControls.Label)row.FindControl("lblTransactionID")).Text;
                    string transactionDate = ((System.Web.UI.WebControls.Label)row.FindControl("lblTransactionDate")).Text;
                  //  bool isVerified = ((CheckBox)row.FindControl("CheckBox1")).Checked;

                    //DataTable dt=new DataTable();
                    //dt=paymentBLL.GetRequestedPaymentTempAsync(id);
                    //e[merchID + password + merchTxnID + amount + txnCurrency + api]


                    string key = dt.Rows[0]["REQUEST_HASH_KEY"].ToString();
                    string merchantID = dt.Rows[0]["MERCHANT_ID"].ToString();
                    string password = dt.Rows[0]["MERCHANT_PASSWORD"].ToString();
                    string transactionCurrency = dt.Rows[0]["TRANSACTION_CURRENCY"].ToString();
                    string api = "TXNVERIFICATION";//ConfigurationManager.AppSettings["ReqHashKey"].ToString();

                    // Example of string array input
                    string[] param = { merchantID, password, transactionID, amount, transactionCurrency, api };

                    // Create a StringBuilder and concatenate the string array elements
                    StringBuilder sb = new StringBuilder();
                    foreach (string s in param)
                    {
                        sb.Append(s); // Concatenate each string
                    }

                    // Use the concatenated string
                    string concatenatedMessage = sb.ToString();


                    string hashpassphrase = dt.Rows[0]["REQUEST_HASH_KEY"].ToString();
                    //string hashsalt = ConfigurationManager.AppSettings["ReqHashKey"].ToString();
                    //byte[] hashiv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                    //int hashiterations = 65536;
                    //int hashkeysize = 256;
                    //string signature = Encrypt(concatenatedMessage, hashpassphrase, hashsalt, hashiv, hashiterations);


                    string signature = "";
                    string strsignature = merchantID + password + transactionID + amount + transactionCurrency + api;
                    byte[] bytes = Encoding.UTF8.GetBytes(hashpassphrase);
                    byte[] bt = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
                    // byte[] b = new HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(prodid));
                    signature = byteToHexString(bt).ToLower();


                    // Pass the concatenated string to the HMACSHA2Helper
                    //byte[] hmacBytes = HMACSHA2Helper.EncodeWithHMACSHA2(concatenatedMessage, key);
                    //string signature = HMACSHA2Helper.ByteToHexString(hmacBytes);

                    transactionDate = Regex.Replace(transactionDate, @"\s+", " "); // Replaces multiple spaces with a single space

                    string inputFormat = "MMM dd yyyy h:mmtt";
                    // DateTime parsedDate = DateTime.ParseExact(transactionDate, inputFormat, CultureInfo.InvariantCulture);
                    DateTime parsedDate;

                    if (DateTime.TryParseExact(transactionDate.Trim(), inputFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                    {
                        // Parsing succeeded, you can use parsedDate here
                    }
                    else
                    {
                        // Handle the error, e.g., log it or show a message to the user
                        // Example: throw new FormatException("Invalid date format.");
                    }

                    // Convert the DateTime object to the desired format "yyyy-MM-dd"
                    string formattedDate = parsedDate.ToString("yyyy-MM-dd");

                    // Create an instance of PayInstrument and populate it with data
                    Reverify.PayInstrument payInstrument = new Reverify.PayInstrument
                    {
                        headDetails = new Reverify.HeadDetails
                        {
                            api = "TXNVERIFICATION",
                            source = "OTS"
                        },
                        merchDetails = new Reverify.MerchDetails
                        {
                            merchId = int.Parse(merchantID),
                            password = password,
                            merchTxnId = transactionID,
                            merchTxnDate = formattedDate
                        },
                        payDetails = new Reverify.PayDetails
                        {
                            amount = int.Parse(amount),
                            txnCurrency = transactionCurrency,
                            signature = signature
                        }
                    };

                    // Wrap in the PayInstrumentWrapper
                    PayInstrumentWrapper wrapper = new PayInstrumentWrapper
                    {
                        payInstrument = payInstrument
                    };


                    // Serialize the object to JSON
                    var json = new JavaScriptSerializer().Serialize(wrapper);


                    string passphrase = dt.Rows[0]["REQUEST_AES_KEY"].ToString();
                    string salt = dt.Rows[0]["REQUEST_AES_KEY"].ToString();
                    byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                    int iterations = 65536;
                    int keysize = 256;
                    string Encryptval = Encrypt(json, passphrase, salt, iv, iterations);
                    //Console.WriteLine("HMAC (Hex): " + hmacHexString);
                    string testurleq = ConfigurationManager.AppSettings["ReverifyURL"].ToString() + merchantID.ToString() + "&encData=" + Encryptval;//"https://caller.atomtech.in/ots/aipay/auth?merchId="+ md.merchId.ToString() + "&encData=" + Encryptval;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(testurleq);
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                    request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                    Encoding encoding = new UTF8Encoding();
                    byte[] data = encoding.GetBytes(json);
                    request.ProtocolVersion = HttpVersion.Version11;
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = data.Length;
                    //request.Timeout = 600000;
                    Stream stream = request.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    //Console.WriteLine(stream);
                    // Console.WriteLine(json);
                    stream.Close();
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    string jsonresponse = response.ToString();

                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    ////  string jsonresponse = post;
                    string temp = null;
                    string status = "";
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    //InitiateOrderResEq.RootObject objectres = new InitiateOrderResEq.RootObject();
                    // JavaScriptSerializer serializer = new JavaScriptSerializer();
                    var result = jsonresponse.Replace("System.Net.HttpWebResponse", "");

                    //Response Decrypt
                    //NameValueCollection nvc = Request.Form;
                    //byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                    //int iterations = 65536;
                    //int keysize = 256;
                    // string plaintext = "{\"payInstrument\":{\"headDetails\":{\"version\":\"OTSv1.1\",\"payMode\":\"SL\",\"channel\":\"ECOMM\",\"api\":\"SALE\",\"stage\":1,\"platform\":\"WEB\"},\"merchDetails\":{\"merchId\":8952,\"userId\":\"\",\"password\":\"Test@123\",\"merchTxnId\":\"1234567890\",\"merchType\":\"R\",\"mccCode\":562,\"merchTxnDate\":\"2019-12-24 20:46:00\"},\"payDetails\":{\"prodDetails\":[{\"prodName\": \"NSE\",\"prodAmount\": 10.00}],\"amount\":10.00,\"surchargeAmount\":0.00,\"totalAmount\":10.00,\"custAccNo\":null,\"custAccIfsc\":null,\"clientCode\":\"12345\",\"txnCurrency\":\"INR\",\"remarks\":null,\"signature\":\"7c643bbd9418c23e972f5468377821d9f0486601e1749930816c409fddbc7beb5d2943d832b6382d3d4a8bd7755e914922fb85aa8c234210bf2993566686a46a\"},\"responseUrls\":{\"returnUrl\":\"http://172.21.21.136:9001/payment/ots/v1/merchresp\",\"cancelUrl\":null,\"notificationUrl\":null},\"payModeSpecificData\":{\"subChannel\":[\"BQ\"],\"bankDetails\":null,\"emiDetails\":null,\"multiProdDetails\":null,\"cardDetails\":null},\"extras\":{\"udf1\":null,\"udf2\":null,\"udf3\":null,\"udf4\":null,\"udf5\":null},\"custDetails\":{\"custFirstName\":null,\"custLastName\":null,\"custEmail\":\"test@gm.com\",\"custMobile\":null,\"billingInfo\":null}}} ";
                    string hashAlgorithm = dt.Rows[0]["HASH_ALGORITHM"].ToString();
                    // Remove "encData=" and "&merchId="
                    string cleanedString = result.Replace("encData=", "").Replace("&merchId="+merchantID+"", "");

                    string encdata = cleanedString;
                    string passphrase1 = dt.Rows[0]["RESPONSE_AES_KEY"].ToString();
                    string salt1 = dt.Rows[0]["RESPONSE_AES_KEY"].ToString();
                    string Decryptval = decrypt(encdata, passphrase1, salt1, iv, iterations);

                    //   Decryptval = "{\"merchDetails\":{\"merchId\":8952,\"merchTxnId\":\"test000123\",\"merchTxnDate\":\"2021-12-03T15:24:35\"},\"payDetails\":{\"atomTxnId\":11000000174314,\"prodDetails\":[{\"prodName\":\"NSE\",\"prodAmount\":100.0}],\"amount\":100.00,\"surchargeAmount\":1.18,\"totalAmount\":101.18,\"custAccNo\":\"213232323\",\"clientCode\":\"1234\",\"txnCurrency\":\"INR\",\"signature\":\"2b12c8bfc0e3a8268eddb6f406bf4187d4d0a0064d0355446986511453922c27e38367a97fff85863d48c147a8218e9e2d5003ab121f6f61ce3914030c60caac\",\"txnInitDate\":\"2021-12-03 15:24:36\",\"txnCompleteDate\":\"2021-12-03 15:24:40\"},\"payModeSpecificData\":{\"subChannel\":[\"NB\"],\"bankDetails\":{\"otsBankId\":2001,\"bankTxnId\":\"qjUiPQ2bMQhjPXmzE1on\",\"otsBankName\":\"Atom Bank\"}},\"extras\":{\"udf1\":\"\",\"udf2\":\"\",\"udf3\":\"\",\"udf4\":\"\",\"udf5\":\"\"},\"custDetails\":{\"custEmail\":\"sagar.gopale@atomtech.in\",\"custMobile\":\"8976286911\",\"billingInfo\":{}},\"responseDetails\":{\"statusCode\":\"OTS0000\",\"message\":\"SUCCESS\",\"description\":\"TRANSACTION IS SUCCESSFUL.\"}}";
                    Payresponse.Rootobject root = new Payresponse.Rootobject();
                    Payresponse.Parent objectres = new Payresponse.Parent();
                    //objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Payresponse.Parent>(Decryptval);

                    DataTable dtcount = new DataTable();
                    dtcount = GetPaymentTransactionBYTransactionID(transactionID);
                    int datacount = int.Parse(dtcount.Rows[0][0].ToString());
                    if (datacount == 0)
                    {
                        var payInstrumentResponse = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PayInstrumentMapper>(Decryptval);
                        if (payInstrumentResponse.payInstrument[0].responseDetails.statusCode == "OTS0000")
                        {
                            string bankTxnId = payInstrumentResponse.payInstrument[0].payModeSpecificData.bankDetails.bankTxnId;
                            string bankName = payInstrumentResponse.payInstrument[0].payModeSpecificData.bankDetails.otsBankName;
                            string paymentThrough = payInstrumentResponse.payInstrument[0].payModeSpecificData.subChannel.ToString();
                            string atomTxnId = payInstrumentResponse.payInstrument[0].payDetails.atomTxnId.ToString();
                            string newsignatures = "";
                            string txnCompleteDate = payInstrumentResponse.payInstrument[0].settlementDetails.settlementDate;
                            string newamount = payInstrumentResponse.payInstrument[0].settlementDetails.settlementAmount.ToString();
                            // string qdataValue = objectres.payInstrument.extras.udf1.ToString();


                            FeesBLL feeBLL = new FeesBLL();
                            DataTable dt2 = new DataTable();
                            dt2 = feeBLL.GetFeeTransactionRequestById(int.Parse(id));
                            if (dt2.Rows.Count > 0)
                            {
                                FeeTransactionRequest ftr = new FeeTransactionRequest();
                                ftr.ScholarNumber = dt2.Rows[0]["ScholarNumber"].ToString();
                                ftr.StudentName = dt2.Rows[0]["StudentName"].ToString();
                                ftr.Amount = int.Parse(dt2.Rows[0]["Amount"].ToString());
                                ftr.TransactionID = dt2.Rows[0]["TransactionID"].ToString();
                                ftr.TransactionDate = DateTime.Parse(dt2.Rows[0]["TransactionDate"].ToString());
                                ftr.AtomId = dt2.Rows[0]["AtomID"].ToString();
                                ftr.T1 = dt2.Rows[0]["T1"].ToString();
                                ftr.T2 = dt2.Rows[0]["T2"].ToString();
                                ftr.T3 = dt2.Rows[0]["T3"].ToString();
                                ftr.T4 = dt2.Rows[0]["T4"].ToString();
                                ftr.T5 = dt2.Rows[0]["T5"].ToString();
                                ftr.T6 = dt2.Rows[0]["T6"].ToString();
                                ftr.T7 = dt2.Rows[0]["T7"].ToString();
                                ftr.T8 = dt2.Rows[0]["T8"].ToString();
                                ftr.T9 = dt2.Rows[0]["T9"].ToString();
                                ftr.T10 = dt2.Rows[0]["T10"].ToString();
                                ftr.T11 = dt2.Rows[0]["T11"].ToString();
                                ftr.T12 = dt2.Rows[0]["T12"].ToString();
                                ftr.T13 = dt2.Rows[0]["T13"].ToString();
                                ftr.T14 = dt2.Rows[0]["T14"].ToString();
                                ftr.T15 = dt2.Rows[0]["T15"].ToString();
                                ftr.T16 = dt2.Rows[0]["T16"].ToString();
                                ftr.T17 = dt2.Rows[0]["T17"].ToString();
                                ftr.T18 = dt2.Rows[0]["T18"].ToString();
                                ftr.T19 = dt2.Rows[0]["T19"].ToString();
                                ftr.T20 = dt2.Rows[0]["T20"].ToString();

                                List<string> selectedFeeMonths = new List<string>();
                                if (!string.IsNullOrWhiteSpace(ftr.T1))
                                {
                                    selectedFeeMonths.Add(ftr.T1);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T2))
                                {
                                    selectedFeeMonths.Add(ftr.T2);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T3))
                                {
                                    selectedFeeMonths.Add(ftr.T3);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T4))
                                {
                                    selectedFeeMonths.Add(ftr.T4);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T5))
                                {
                                    selectedFeeMonths.Add(ftr.T5);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T6))
                                {
                                    selectedFeeMonths.Add(ftr.T6);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T7))
                                {
                                    selectedFeeMonths.Add(ftr.T7);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T8))
                                {
                                    selectedFeeMonths.Add(ftr.T8);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T9))
                                {
                                    selectedFeeMonths.Add(ftr.T9);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T10))
                                {
                                    selectedFeeMonths.Add(ftr.T10);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T11))
                                {
                                    selectedFeeMonths.Add(ftr.T11);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T12))
                                {
                                    selectedFeeMonths.Add(ftr.T12);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T13))
                                {
                                    selectedFeeMonths.Add(ftr.T13);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T14))
                                {
                                    selectedFeeMonths.Add(ftr.T14);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T15))
                                {
                                    selectedFeeMonths.Add(ftr.T15);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T16))
                                {
                                    selectedFeeMonths.Add(ftr.T16);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T17))
                                {
                                    selectedFeeMonths.Add(ftr.T17);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T18))
                                {
                                    selectedFeeMonths.Add(ftr.T18);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T19))
                                {
                                    selectedFeeMonths.Add(ftr.T19);
                                }
                                if (!string.IsNullOrWhiteSpace(ftr.T20))
                                {
                                    selectedFeeMonths.Add(ftr.T20);
                                }
                                string selectedMonths = string.Join(",", selectedFeeMonths);

                                DataSet ds = feeBLL.StudentFeeParameterDetail(ftr.ScholarNumber);

                                Session["MyDataTable"] = ds.Tables[0];
                                DataTable feedt = (DataTable)Session["MyDataTable"];
                                string[] monthsArray = selectedMonths.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                DataTable noFineDt = feedt.Clone();
                                decimal feeSum = 0;
                                decimal fineSum = 0;
                                foreach (DataRow rowfdt in feedt.Rows)
                                {
                                    // Check if the FeeType is in the array and the FeeName is not "Fine"
                                    if (monthsArray.Contains(rowfdt["FeeType"].ToString()) && rowfdt["FeeName"].ToString() != "Fine")
                                    {
                                        feeSum += Convert.ToDecimal(rowfdt["FeeAmount"]);
                                        // Import the row into the filtered DataTable
                                        noFineDt.ImportRow(rowfdt);
                                    }

                                    // Check if the FeeName is "Fine"
                                    if (monthsArray.Contains(rowfdt["FeeType"].ToString()) && rowfdt["FeeName"].ToString() == "Fine")
                                    {
                                        fineSum += Convert.ToDecimal(rowfdt["FeeAmount"]);
                                    }
                                }

                                Session["NoFineDataTable"] = noFineDt;

                                int receiptNo = feeBLL.AddFeeReceiptPrintOnline(DateTime.Now, ftr.ScholarNumber);
                                if (receiptNo > 0)
                                {
                                    Session["ReceiptNo"] = receiptNo.ToString();
                                    decimal amountDecimaln = Convert.ToDecimal(amount);
                                    int amountIntn = Convert.ToInt32(amountDecimaln);
                                    var amountInwords = "Rupees " + ConvertNumbertoWords(amountIntn) + " Only.";
                                    FeeTransactionModel ftm = new FeeTransactionModel();
                                    ftm.ReceiptNo = receiptNo;
                                    ftm.ReceiptDt = DateTime.Now;
                                    ftm.ScholarNo = ftr.ScholarNumber;
                                    ftm.BillBookNo = "";
                                    ftm.TotFeeAmt = feeSum;
                                    ftm.FineAmt = fineSum;
                                    ftm.TotDisAmt = 0;
                                    ftm.TotRecAmt = feeSum + fineSum;
                                    ftm.OnlineAmt = feeSum + fineSum;
                                    ftm.OnlineRefNo = ftr.TransactionID;
                                    ftm.OnlineDt = ftr.TransactionDate;
                                    ftm.AmtInWords = amountInwords;
                                    ftm.StudentName = ftr.StudentName;
                                    ftm.Amount = ftr.Amount;
                                    ftm.TransactionID = ftr.TransactionID;
                                    ftm.TransactionDate = ftr.TransactionDate;
                                    ftm.IsReverified = false;
                                    ftm.CreatedBy = ftr.CreatedBy;
                                    ftm.BankName = bankName;
                                    ftm.BankTransaction = bankTxnId;
                                    ftm.AtomID = ftr.AtomId;
                                    ftm.TransactionType = paymentThrough;
                                    ftm.T1 = ftr.T1;
                                    ftm.T2 = ftr.T2;
                                    ftm.T3 = ftr.T3;
                                    ftm.T4 = ftr.T4;
                                    ftm.T5 = ftr.T5;
                                    ftm.T6 = ftr.T6;
                                    ftm.T7 = ftr.T7;
                                    ftm.T8 = ftr.T8;
                                    ftm.T9 = ftr.T9;
                                    ftm.T10 = ftr.T10;
                                    ftm.T11 = ftr.T11;
                                    ftm.T12 = ftr.T12;
                                    ftm.T13 = ftr.T13;
                                    ftm.T14 = ftr.T14;
                                    ftm.T15 = ftr.T15;
                                    ftm.T16 = ftr.T16;
                                    ftm.T17 = ftr.T17;
                                    ftm.T18 = ftr.T18;
                                    ftm.T19 = ftr.T19;
                                    ftm.T20 = ftr.T20;

                                    int feeTransactionReult = feeBLL.AddFeeTransactionOnline(ftm);
                                    if (feeTransactionReult > 0)
                                    {
                                        List<FeeTransDetailOnline> feeTransDetailOnlines = new List<FeeTransDetailOnline>();

                                        foreach (DataRow dr in noFineDt.Rows)
                                        {
                                            string feeType = dr["FeeType"].ToString();
                                            string feeName = dr["FeeName"].ToString();
                                            double feeAmount = double.Parse(dr["FeeAmount"].ToString());
                                            int feeTypeSeqNo = feeBLL.GetOrderNoByFeeType(feeType);
                                            int feeNameSeqNo = feeBLL.GetSerialNoByFeeName(feeName);

                                            feeTransDetailOnlines.Add(new FeeTransDetailOnline
                                            {
                                                ReceiptNo = receiptNo,
                                                FeeType = feeType,
                                                FeeName = feeName,
                                                PrevBalAmt = 0,
                                                FeeAmt = decimal.Parse(feeAmount.ToString()),
                                                DisAmt = 0,
                                                PaidFeeAmt = decimal.Parse(feeAmount.ToString()),
                                                FeeTypeSeqNo = feeTypeSeqNo,
                                                FeeHeadSeqNo = feeNameSeqNo
                                            });

                                        }
                                        int responseRslt = feeBLL.InsertFeeTransDetailOnline(feeTransDetailOnlines);
                                        if (responseRslt > 0)
                                        {
                                            Session["ScholarNo"] = ftr.ScholarNumber;
                                            Session["SchoolId"] = schoolID;
                                            UpdateRequestedPaymentTempAsync(int.Parse(id));
                                            //Response.Redirect(ConfigurationManager.AppSettings["ReceiptURL"].ToString() + "", false);
                                            //HttpContext.Current.ApplicationInstance.CompleteRequest(); // Stop further processing
                                            //Label1.Text = "Payment Done Successfully";
                                        }
                                        else
                                        {
                                           // Label1.Text = "Data Not Stored";
                                            string errorUrl = ConfigurationManager.AppSettings["ErrorURL"].ToString();
                                            Response.Redirect(errorUrl, false);
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            var tempUpdate = UpdateRequestedPaymentTempAsync(int.Parse(id));
                        }

                    }
                    else
                    {
                        var tempUpdate = UpdateRequestedPaymentTempAsync(int.Parse(id));
                    }
                }
                BindNonVerified();
            }
        }
        public DataTable GetPaymentTransactionBYTransactionID(string transactionId)
        {
            string databaseName=Session["databaseName"].ToString();
            string sqlServerConnectionString = @"Data Source=150.242.203.229;Initial Catalog=" + databaseName + ";User Id=dpsuser;Password=dps@123;Integrated Security=False;MultipleActiveResultSets=True;Connect Timeout=50000;";
            SqlConnection con = new SqlConnection(sqlServerConnectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "SELECT [OnlineRefNo]  FROM [FeeTransactionOnline]";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(com);
            DataTable dt2 = new DataTable();
            sda.Fill(dt2);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return dt2;
        }
        public int UpdateRequestedPaymentTempAsync(int id)
        {
            string databaseName = Session["databaseName"].ToString();
            string sqlServerConnectionString = @"Data Source=150.242.203.229;Initial Catalog=" + databaseName + ";User Id=dpsuser;Password=dps@123;Integrated Security=False;MultipleActiveResultSets=True;Connect Timeout=50000;";
            SqlConnection con = new SqlConnection(sqlServerConnectionString);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "Update FeeTransactionRequest SET IsReverified=1 where ID="+id;
            SqlCommand com = new SqlCommand(query, con);
            int result = com.ExecuteNonQuery();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return result;
        }
        public static string ConvertNumbertoWords(int number)
        {
            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        #region EncryptTransaction
        public static string byteToHexString(byte[] byData)
        {
            StringBuilder sb = new StringBuilder((byData.Length * 2));
            for (int i = 0; (i < byData.Length); i++)
            {
                int v = (byData[i] & 255);
                if ((v < 16))
                {
                    sb.Append('0');
                }

                sb.Append(v.ToString("X"));

            }

            return sb.ToString();
        }
        public String Encrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            string data = ByteArrayToHexString(Encrypt(plainBytes, GetSymmetricAlgorithm(passphrase, salt, iv, iterations))).ToUpper();


            return data;
        }
        public String decrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
        {
            byte[] str = HexStringToByte(plainText);

            string data1 = Encoding.UTF8.GetString(decrypt(str, GetSymmetricAlgorithm(passphrase, salt, iv, iterations)));
            return data1;
        }
        public byte[] Encrypt(byte[] plainBytes, SymmetricAlgorithm sa)
        {
            return sa.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        }
        public byte[] decrypt(byte[] plainBytes, SymmetricAlgorithm sa)
        {
            return sa.CreateDecryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
        public SymmetricAlgorithm GetSymmetricAlgorithm(String passphrase, String salt, Byte[] iv, int iterations)
        {
            var saltBytes = new byte[16];
            var ivBytes = new byte[16];
            Rfc2898DeriveBytes rfcdb = new System.Security.Cryptography.Rfc2898DeriveBytes(passphrase, Encoding.UTF8.GetBytes(salt), iterations, HashAlgorithmName.SHA512);
            saltBytes = rfcdb.GetBytes(32);
            var tempBytes = iv;
            Array.Copy(tempBytes, ivBytes, Math.Min(ivBytes.Length, tempBytes.Length));
            var rij = new RijndaelManaged(); //SymmetricAlgorithm.Create();
            rij.Mode = CipherMode.CBC;
            rij.Padding = PaddingMode.PKCS7;
            rij.FeedbackSize = 128;
            rij.KeySize = 128;

            rij.BlockSize = 128;
            rij.Key = saltBytes;
            rij.IV = ivBytes;
            return rij;
        }
        protected static byte[] HexStringToByte(string hexString)
        {
            try
            {
                int bytesCount = (hexString.Length) / 2;
                byte[] bytes = new byte[bytesCount];
                for (int x = 0; x < bytesCount; ++x)
                {
                    bytes[x] = Convert.ToByte(hexString.Substring(x * 2, 2), 16);
                }
                return bytes;
            }
            catch
            {
                throw;
            }
        }
        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        #endregion

    }
}