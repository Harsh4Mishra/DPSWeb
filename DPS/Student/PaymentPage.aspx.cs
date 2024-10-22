using DPS.Student.FeeClassFile;
using DPS.SuperAdmin.PaymentConfigurationClassFIle;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.DynamicData;
using System.Web.Script.Serialization;

namespace DPS.Student
{
    public partial class PaymentPage : System.Web.UI.Page
    {
        string t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19, t20;
        string Tok_id = "";
        DateTime now = DateTime.Now;
        string TranTrackid = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMddhhmmss");
        FeesBLL fees = new FeesBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string selectedMonths = Request.QueryString["selectedMonths"];

                if (!string.IsNullOrEmpty(selectedMonths))
                {
                    DataTable feedt = (DataTable)Session["MyDataTable"];
                    // Split the selected months into an array
                    string[] monthsArray = selectedMonths.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    DataTable dt = new DataTable();
                    string scholarno = Session["StudentPayFee"].ToString();
                    dt = fees.GetStudentDetailByScholarNo(scholarno);
                    if (dt.Rows.Count > 0)
                    {
                        // Example personal details
                        txtScholarNo.Text = dt.Rows[0]["Scholarno"].ToString();
                        txtStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                        txtDOB.Text = dt.Rows[0]["DOB"].ToString();
                        txtSex.Text = dt.Rows[0]["Sex"].ToString();
                        txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                        txtFatherPhone.Text = dt.Rows[0]["FatherPhone"].ToString();
                        txtClass.Text = dt.Rows[0]["ClassName"].ToString();
                        txtSection.Text = dt.Rows[0]["SectionName"].ToString();
                    }


                    DataTable noFineDt = feedt.Clone();
                    decimal feeSum = 0;
                    decimal fineSum = 0;
                    foreach (DataRow row in feedt.Rows)
                    {
                        // Check if the FeeType is in the array and the FeeName is not "Fine"
                        if (monthsArray.Contains(row["FeeType"].ToString()) && row["FeeName"].ToString() != "Fine")
                        {
                            feeSum += Convert.ToDecimal(row["FeeAmount"]);
                            // Import the row into the filtered DataTable
                            noFineDt.ImportRow(row);
                        }

                        // Check if the FeeName is "Fine"
                        if (monthsArray.Contains(row["FeeType"].ToString()) && row["FeeName"].ToString() == "Fine")
                        {
                            fineSum += Convert.ToDecimal(row["FeeAmount"]);
                        }
                    }

                    int sumFeeValue = Convert.ToInt32(feeSum);
                    int sumFineValue = Convert.ToInt32(fineSum);


                    txtFeeAmount.Text = sumFeeValue.ToString();
                    txtFineAmount.Text = sumFineValue.ToString();

                    int FinalAmount = sumFeeValue + sumFineValue;

                    txtfinalAmount.Text = FinalAmount.ToString();


                    GridView1.DataSource = noFineDt;
                    GridView1.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MakePaymentThroughNTTDATA();
        }
        public void MakePaymentThroughNTTDATA()
        {
            //string q = txtBPNo.Text + "^" + txtFullName.Text + "^" + txtemail.Text + "^" + txtphone.Text + "^" + txttransactionId.Text + "^" + txtAmount.Text + "^" + txttransactionDate.Text;
            //string encrypted = aesOperation.EncryptString(ConfigurationManager.AppSettings["EncryptKey"].ToString(),q);

            BindVariable();

            SchoolPaymentConfigurationDAL dal = new SchoolPaymentConfigurationDAL();
            DataTable dt = new DataTable();
            int schoolID = int.Parse(Session["SchoolName"].ToString());
            dt = dal.GetSchoolPaymentConfigurationByClientId(schoolID);
            string databaseName = Session["databaseName"].ToString();

            if (dt.Rows.Count > 0)
            {
                //string q = "";
                string payInstrument = "";

                try
                {
                    int requestTransactionresult = 0;

                    Payrequest.RootObject rt = new Payrequest.RootObject();
                    Payrequest.MsgBdy mb = new Payrequest.MsgBdy();
                    Payrequest.HeadDetails hd = new Payrequest.HeadDetails();
                    // Payrequest.HeadDetails hd = new Payrequest.HeadDetails();
                    Payrequest.MerchDetails md = new Payrequest.MerchDetails();
                    Payrequest.PayDetails pd = new Payrequest.PayDetails();
                    Payrequest.CustDetails cd = new Payrequest.CustDetails();
                    Payrequest.Extras ex = new Payrequest.Extras();

                    Payrequest.Payrequest pr = new Payrequest.Payrequest();


                    hd.version = "OTSv1.1";
                    hd.api = "AUTH";
                    hd.platform = "FLASH";

                    md.merchId = dt.Rows[0]["MERCHANT_ID"].ToString();
                    md.userId = dt.Rows[0]["USER_ID"].ToString();
                    md.password = dt.Rows[0]["MERCHANT_PASSWORD"].ToString();
                    md.merchTxnDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//"2021-09-04 20:46:00";
                    md.merchTxnId = TranTrackid;


                    pd.amount = txtfinalAmount.Text;
                    pd.product = dt.Rows[0]["PRODUCT_ID"].ToString();
                    pd.custAccNo = "213232323";
                    pd.txnCurrency = dt.Rows[0]["TRANSACTION_CURRENCY"].ToString();

                    cd.custEmail = "dps.epay@gmail.com";
                    cd.custMobile = "9598487295";

                    ex.udf1 = requestTransactionresult.ToString();
                    ex.udf2 = txtScholarNo.Text;
                    ex.udf3 = schoolID.ToString();
                    ex.udf4 = databaseName;
                    ex.udf5 = "";

                    pr.headDetails = hd;
                    pr.merchDetails = md;
                    pr.payDetails = pd;
                    pr.custDetails = cd;
                    pr.extras = ex;

                    rt.payInstrument = pr;
                    var json = new JavaScriptSerializer().Serialize(rt);



                    string passphrase = dt.Rows[0]["REQUEST_AES_KEY"].ToString();
                    string salt = dt.Rows[0]["REQUEST_AES_KEY"].ToString();


                    //Payrequest.RootObject rt = new Payrequest.RootObject();
                    //Payrequest.MsgBdy mb = new Payrequest.MsgBdy();
                    //Payrequest.HeadDetails hd = new Payrequest.HeadDetails();
                    //// Payrequest.HeadDetails hd = new Payrequest.HeadDetails();
                    //Payrequest.MerchDetails md = new Payrequest.MerchDetails();
                    //Payrequest.PayDetails pd = new Payrequest.PayDetails();
                    //Payrequest.CustDetails cd = new Payrequest.CustDetails();
                    //Payrequest.Extras ex = new Payrequest.Extras();

                    //Payrequest.Payrequest pr = new Payrequest.Payrequest();


                    //hd.version = "OTSv1.1";
                    //hd.api = "AUTH";
                    //hd.platform = "FLASH";

                    //md.merchId = "317157";
                    //md.userId = "123";
                    //md.password = "Test@123";
                    //md.merchTxnDate = "2021-09-04 20:46:00";
                    //md.merchTxnId = "test000123";


                    //pd.amount = "100";
                    //pd.product = "NSE";
                    //pd.custAccNo = "213232323";
                    //pd.txnCurrency = "INR";

                    //cd.custEmail = "sagar.gopale@atomtech.in";
                    //cd.custMobile = "8976286911";

                    //ex.udf1 = "";
                    //ex.udf2 = "";
                    //ex.udf3 = "";
                    //ex.udf4 = "";
                    //ex.udf5 = "";


                    //pr.headDetails = hd;
                    //pr.merchDetails = md;
                    //pr.payDetails = pd;
                    //pr.custDetails = cd;
                    //pr.extras = ex;

                    //rt.payInstrument = pr;
                    //var json = new JavaScriptSerializer().Serialize(rt);



                    //string passphrase = "A4476C2062FFA58980DC8F79EB6A799E";
                    //string salt = "A4476C2062FFA58980DC8F79EB6A799E";

                    byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                    int iterations = 65536;
                    int keysize = 256;
                    // string plaintext = "{\"payInstrument\":{\"headDetails\":{\"version\":\"OTSv1.1\",\"payMode\":\"SL\",\"channel\":\"ECOMM\",\"api\":\"SALE\",\"stage\":1,\"platform\":\"WEB\"},\"merchDetails\":{\"merchId\":8952,\"userId\":\"\",\"password\":\"Test@123\",\"merchTxnId\":\"1234567890\",\"merchType\":\"R\",\"mccCode\":562,\"merchTxnDate\":\"2019-12-24 20:46:00\"},\"payDetails\":{\"prodDetails\":[{\"prodName\": \"NSE\",\"prodAmount\": 10.00}],\"amount\":10.00,\"surchargeAmount\":0.00,\"totalAmount\":10.00,\"custAccNo\":null,\"custAccIfsc\":null,\"clientCode\":\"12345\",\"txnCurrency\":\"INR\",\"remarks\":null,\"signature\":\"7c643bbd9418c23e972f5468377821d9f0486601e1749930816c409fddbc7beb5d2943d832b6382d3d4a8bd7755e914922fb85aa8c234210bf2993566686a46a\"},\"responseUrls\":{\"returnUrl\":\"http://172.21.21.136:9001/payment/ots/v1/merchresp\",\"cancelUrl\":null,\"notificationUrl\":null},\"payModeSpecificData\":{\"subChannel\":[\"BQ\"],\"bankDetails\":null,\"emiDetails\":null,\"multiProdDetails\":null,\"cardDetails\":null},\"extras\":{\"udf1\":null,\"udf2\":null,\"udf3\":null,\"udf4\":null,\"udf5\":null},\"custDetails\":{\"custFirstName\":null,\"custLastName\":null,\"custEmail\":\"test@gm.com\",\"custMobile\":null,\"billingInfo\":null}}} ";
                    string hashAlgorithm = dt.Rows[0]["HASH_ALGORITHM"].ToString();
                    string Encryptval = Encrypt(json, passphrase, salt, iv, iterations);

                    string testurleq = ConfigurationManager.AppSettings["AuthURL"].ToString() + md.merchId.ToString() + "&encData=" + Encryptval;//"https://caller.atomtech.in/ots/aipay/auth?merchId="+ md.merchId.ToString() + "&encData=" + Encryptval;
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
                    //// var result = "{\"initiateDigiOrderResponse\":{ \"msgHdr\":{ \"rslt\":\"OK\"},\"msgBdy\":{ \"sts\":\"ACPT\",\"txnId\":\"DIG2019039816365405440004\"}}}";
                    //  var  objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Payverify.Payverify>(result);

                    var uri = new Uri("http://atom.in?" + result);


                    var query = HttpUtility.ParseQueryString(uri.Query);

                    string encData = query.Get("encData");
                    // string  encData="5500FEA2F09DA7EF128CFE7D2D01F2533B8D8211ACDCEEE850A7943CF46D4A18FF153971B83983A1EBF8B48F36315222E33FED142A05BE8FD890492ED759983B173801C801A79B390C17E01354CA0752087CF1E71316E5F442FADA985C46B06DB8462928DB18BC8E7714EC6128340CB8690A185F590E47658C293FA2E73ADC77899D6E7B119E17005E625CF2258A6A74363EAA59A43FF785505A77D163DA232B1D2250C4A1A1C755E10D5991A2DB5B3C";
                    string passphrase1 = dt.Rows[0]["RESPONSE_AES_KEY"].ToString();//ConfigurationManager.AppSettings["ResAESKey"].ToString();
                    string salt1 = dt.Rows[0]["RESPONSE_AES_KEY"].ToString();//ConfigurationManager.AppSettings["ResAESKey"].ToString();
                    string Decryptval = decrypt(encData, passphrase1, salt1, iv, iterations);
                    //{ "atomTokenId":15000000085830,"responseDetails":{ "txnStatusCode":"OTS0000","txnMessage":"SUCCESS","txnDescription":"ATOM TOKEN ID HAS BEEN GENERATED SUCCESSFULLY"} }
                    Payverify.Payverify objectres = new Payverify.Payverify();
                    objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Payverify.Payverify>(Decryptval);
                    string txnMessage = objectres.responseDetails.txnMessage;
                    Tok_id = objectres.atomTokenId.ToString();



                    //byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                    //int iterations = 65536;
                    //int keysize = 256;
                    //// string plaintext = "{\"payInstrument\":{\"headDetails\":{\"version\":\"OTSv1.1\",\"payMode\":\"SL\",\"channel\":\"ECOMM\",\"api\":\"SALE\",\"stage\":1,\"platform\":\"WEB\"},\"merchDetails\":{\"merchId\":8952,\"userId\":\"\",\"password\":\"Test@123\",\"merchTxnId\":\"1234567890\",\"merchType\":\"R\",\"mccCode\":562,\"merchTxnDate\":\"2019-12-24 20:46:00\"},\"payDetails\":{\"prodDetails\":[{\"prodName\": \"NSE\",\"prodAmount\": 10.00}],\"amount\":10.00,\"surchargeAmount\":0.00,\"totalAmount\":10.00,\"custAccNo\":null,\"custAccIfsc\":null,\"clientCode\":\"12345\",\"txnCurrency\":\"INR\",\"remarks\":null,\"signature\":\"7c643bbd9418c23e972f5468377821d9f0486601e1749930816c409fddbc7beb5d2943d832b6382d3d4a8bd7755e914922fb85aa8c234210bf2993566686a46a\"},\"responseUrls\":{\"returnUrl\":\"http://172.21.21.136:9001/payment/ots/v1/merchresp\",\"cancelUrl\":null,\"notificationUrl\":null},\"payModeSpecificData\":{\"subChannel\":[\"BQ\"],\"bankDetails\":null,\"emiDetails\":null,\"multiProdDetails\":null,\"cardDetails\":null},\"extras\":{\"udf1\":null,\"udf2\":null,\"udf3\":null,\"udf4\":null,\"udf5\":null},\"custDetails\":{\"custFirstName\":null,\"custLastName\":null,\"custEmail\":\"test@gm.com\",\"custMobile\":null,\"billingInfo\":null}}} ";
                    //string hashAlgorithm = "SHA1";
                    //string Encryptval = Encrypt(json, passphrase, salt, iv, iterations);



                    ////Response.Redirect("https://caller.atomtech.in/ots/payment/txn?merchId=8952&encData=" + Encryptval);



                    ////  string Decryptval = decrypt(Encryptval, passphrase, salt, iv, iterations);

                    ////   Response.Redirect("https://caller.atomtech.in/ots/payment/txn?merchId=8952&encData=" + Encryptval);


                    //// string data="{\"payrequest\":{\"merchanyDetails\":{\"merchId\":\"8952\",\"userId\":\"\",\"password\":\"NCA@1234\",\"merchTxnDate\":\"2021-09-04 20:46:00\",\"merchTxnId\":\"test000123\"},\"payDetails\":{\"amount\":\"100\",\"product\":\"NSE\",\"custAccNo\":\"213232323\",\"txnCurrency\":\"INR\"},\"custDetails\":{\"custEmail\":\"sagar.gopale @atomtech.in\",\"custMobile\":\"8976286911\"},\"extras\":{\"udf1\":\"\",\"udf2\":\"\",\"udf3\":\"\",\"udf4\":\"\",\"udf5\":\"\"},\"headDetails\":{\"version\":\"OTSv1.1\",\"api\":\"AUTH\",\"platform\":\"FLASH\"}}}";




                    //string testurleq = "https://caller.atomtech.in/ots/aipay/auth?merchId=317157&encData=" + Encryptval;
                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(testurleq);
                    //ServicePointManager.Expect100Continue = true;
                    //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                    //request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                    //Encoding encoding = new UTF8Encoding();
                    //byte[] data = encoding.GetBytes(json);
                    //request.ProtocolVersion = HttpVersion.Version11;
                    //request.Method = "POST";
                    //request.ContentType = "application/json";
                    //request.ContentLength = data.Length;
                    ////request.Timeout = 600000;
                    //Stream stream = request.GetRequestStream();
                    //stream.Write(data, 0, data.Length);
                    ////Console.WriteLine(stream);
                    //// Console.WriteLine(json);
                    //stream.Close();
                    //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    //string jsonresponse = response.ToString();

                    //StreamReader reader = new StreamReader(response.GetResponseStream());
                    //////  string jsonresponse = post;
                    //string temp = null;
                    //string status = "";
                    //while ((temp = reader.ReadLine()) != null)
                    //{
                    //    jsonresponse += temp;
                    //}
                    ////InitiateOrderResEq.RootObject objectres = new InitiateOrderResEq.RootObject();
                    //JavaScriptSerializer serializer = new JavaScriptSerializer();
                    //var result = jsonresponse.Replace("System.Net.HttpWebResponse", "");
                    ////// var result = "{\"initiateDigiOrderResponse\":{ \"msgHdr\":{ \"rslt\":\"OK\"},\"msgBdy\":{ \"sts\":\"ACPT\",\"txnId\":\"DIG2019039816365405440004\"}}}";
                    ////  var  objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Payverify.Payverify>(result);

                    //var uri = new Uri("http://atom.in?" + result);


                    //var query = HttpUtility.ParseQueryString(uri.Query);

                    //string encData = query.Get("encData");
                    //// string  encData="5500FEA2F09DA7EF128CFE7D2D01F2533B8D8211ACDCEEE850A7943CF46D4A18FF153971B83983A1EBF8B48F36315222E33FED142A05BE8FD890492ED759983B173801C801A79B390C17E01354CA0752087CF1E71316E5F442FADA985C46B06DB8462928DB18BC8E7714EC6128340CB8690A185F590E47658C293FA2E73ADC77899D6E7B119E17005E625CF2258A6A74363EAA59A43FF785505A77D163DA232B1D2250C4A1A1C755E10D5991A2DB5B3C";
                    //string passphrase1 = "75AEF0FA1B94B3C10D4F5B268F757F11";
                    //string salt1 = "75AEF0FA1B94B3C10D4F5B268F757F11";
                    //string Decryptval = decrypt(encData, passphrase1, salt1, iv, iterations);
                    ////{ "atomTokenId":15000000085830,"responseDetails":{ "txnStatusCode":"OTS0000","txnMessage":"SUCCESS","txnDescription":"ATOM TOKEN ID HAS BEEN GENERATED SUCCESSFULLY"} }
                    //Payverify.Payverify objectres = new Payverify.Payverify();
                    //objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Payverify.Payverify>(Decryptval);
                    //string txnMessage = objectres.responseDetails.txnMessage;
                    //Tok_id = objectres.atomTokenId;

                    DateTime now = DateTime.Now;
                    // string TranTrackid = Convert.ToDateTime(DateTime.Now).ToString("yyyyMMddhhmmss");
                    FeeTransactionRequest ftr = new FeeTransactionRequest();
                    ftr.ScholarNumber = txtScholarNo.Text;
                    ftr.StudentName = txtStudentName.Text;
                    ftr.Amount = int.Parse(txtfinalAmount.Text);
                    ftr.TransactionID = md.merchTxnId;
                    ftr.TransactionDate = DateTime.Now;
                    ftr.AtomId = Tok_id;
                    ftr.T1 = t1;
                    ftr.T2 = t2;
                    ftr.T3 = t3;
                    ftr.T4 = t4;
                    ftr.T5 = t5;
                    ftr.T6 = t6;
                    ftr.T7 = t7;
                    ftr.T8 = t8;
                    ftr.T9 = t9;
                    ftr.T10 = t10;
                    ftr.T11 = t11;
                    ftr.T12 = t12;
                    ftr.T13 = t13;
                    ftr.T14 = t14;
                    ftr.T15 = t15;
                    ftr.T16 = t16;
                    ftr.T17 = t17;
                    ftr.T18 = t18;
                    ftr.T19 = t19;
                    ftr.T20 = t20;
                    ftr.CreatedBy = txtScholarNo.Text;

                    FeesBLL feesBLL = new FeesBLL();
                    int tempResult = feesBLL.AddFeeTransactionRequest(ftr);

                    if (tempResult > 0 && tempResult != 0)
                    {
                        ex.udf1 = tempResult.ToString();
                        string merchId = dt.Rows[0]["MERCHANT_ID"].ToString();// "8952";//txtmer.Text;
                        string custEmail = "dps.epay@gmail.com";
                        string custMobile = txtFatherPhone.Text;
                        //string returnUrl = ConfigurationManager.AppSettings["ResponseURL"].ToString() + "?id={" + tempResult +"}&q={"+encrypted+"}";
                        //string returnUrl = $"{ConfigurationManager.AppSettings["ResponseURL"].ToString()}?id={Uri.EscapeDataString(tempResult.ToString())}&q={Uri.EscapeDataString(encrypted)}";

                        string queryString = $"?id={Uri.EscapeDataString(tempResult.ToString())}";
                        string baseUrl = ConfigurationManager.AppSettings["ResponseURL"].ToString();
                        // Combine base URL with query string
                        string returnUrl = baseUrl + queryString;

                        // JavaScript to be executed
                        string script = $@"
                var options = {{
                    atomTokenId: '{Tok_id}',
                    merchId: '{merchId}',
                    custEmail: '{custEmail}',
                    custMobile: '{custMobile}',
                    returnUrl: '{returnUrl}'
                }};
                let atom = new AtomPaynetz(options, 'uat');
            ";
                        feesBLL.AddFeeTransactionValue(json, Encryptval);
                        ClientScript.RegisterStartupScript(this.GetType(), "openPay", script, true);
                        // return script;
                    }
                    else
                    {
                        string errorUrl = ConfigurationManager.AppSettings["ErrorURL"].ToString();
                        Response.Redirect(errorUrl);
                        throw new Exception(txnMessage = "Transaction is not Updated");

                    }
                }
                catch (Exception ex)
                {

                    string errorUrl = ConfigurationManager.AppSettings["ErrorURL"].ToString();
                    Response.Redirect(errorUrl);

                }
                finally
                {
                }
            }
            else
            {
                string script = "alert('Payment Configuration not found.');";
                // Register the JavaScript code to be executed
                ClientScript.RegisterStartupScript(this.GetType(), "ShowAlert", script, true);
                string errorUrl = ConfigurationManager.AppSettings["ErrorURL"].ToString();
                Response.Redirect(errorUrl);
            }
        }
        #region EncryptTransaction

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
        public void BindVariable()
        {
            string selectedMonths = Request.QueryString["selectedMonths"];
            string[] monthsArray = selectedMonths.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (monthsArray.Count() == 1)
            {
                t1 = monthsArray[0].ToString();
            }
            if (monthsArray.Count() == 2)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
            }
            if (monthsArray.Count() == 3)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
            }
            if (monthsArray.Count() == 4)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
            }
            if (monthsArray.Count() == 5)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
            }
            if (monthsArray.Count() == 6)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
            }
            if (monthsArray.Count() == 7)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
            }
            if (monthsArray.Count() == 8)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
            }
            if (monthsArray.Count() == 9)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
                t9 = monthsArray[8].ToString();
            }
            if (monthsArray.Count() == 10)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
                t9 = monthsArray[8].ToString();
                t10 = monthsArray[9].ToString();
            }
            if (monthsArray.Count() == 11)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
                t9 = monthsArray[8].ToString();
                t10 = monthsArray[9].ToString();
                t11 = monthsArray[10].ToString();
            }
            if (monthsArray.Count() == 12)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
                t9 = monthsArray[8].ToString();
                t10 = monthsArray[9].ToString();
                t11 = monthsArray[10].ToString();
                t12 = monthsArray[11].ToString();
            }
            if (monthsArray.Count() == 13)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
                t9 = monthsArray[8].ToString();
                t10 = monthsArray[9].ToString();
                t11 = monthsArray[10].ToString();
                t12 = monthsArray[11].ToString();
                t13 = monthsArray[12].ToString();
            }
            if (monthsArray.Count() == 14)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
                t9 = monthsArray[8].ToString();
                t10 = monthsArray[9].ToString();
                t11 = monthsArray[10].ToString();
                t12 = monthsArray[11].ToString();
                t13 = monthsArray[12].ToString();
                t14 = monthsArray[13].ToString();
            }
            if (monthsArray.Count() == 15)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
                t9 = monthsArray[8].ToString();
                t10 = monthsArray[9].ToString();
                t11 = monthsArray[10].ToString();
                t12 = monthsArray[11].ToString();
                t13 = monthsArray[12].ToString();
                t14 = monthsArray[13].ToString();
                t15 = monthsArray[14].ToString();
            }
            if (monthsArray.Count() == 16)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
                t9 = monthsArray[8].ToString();
                t10 = monthsArray[9].ToString();
                t11 = monthsArray[10].ToString();
                t12 = monthsArray[11].ToString();
                t13 = monthsArray[12].ToString();
                t14 = monthsArray[13].ToString();
                t15 = monthsArray[14].ToString();
                t16 = monthsArray[15].ToString();
            }
            if (monthsArray.Count() == 17)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
                t9 = monthsArray[8].ToString();
                t10 = monthsArray[9].ToString();
                t11 = monthsArray[10].ToString();
                t12 = monthsArray[11].ToString();
                t13 = monthsArray[12].ToString();
                t14 = monthsArray[13].ToString();
                t15 = monthsArray[14].ToString();
                t16 = monthsArray[15].ToString();
                t17 = monthsArray[16].ToString();
            }
            if (monthsArray.Count() == 18)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
                t9 = monthsArray[8].ToString();
                t10 = monthsArray[9].ToString();
                t11 = monthsArray[10].ToString();
                t12 = monthsArray[11].ToString();
                t13 = monthsArray[12].ToString();
                t14 = monthsArray[13].ToString();
                t15 = monthsArray[14].ToString();
                t16 = monthsArray[15].ToString();
                t17 = monthsArray[16].ToString();
                t18 = monthsArray[17].ToString();
            }
            if (monthsArray.Count() == 19)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
                t9 = monthsArray[8].ToString();
                t10 = monthsArray[9].ToString();
                t11 = monthsArray[10].ToString();
                t12 = monthsArray[11].ToString();
                t13 = monthsArray[12].ToString();
                t14 = monthsArray[13].ToString();
                t15 = monthsArray[14].ToString();
                t16 = monthsArray[15].ToString();
                t17 = monthsArray[16].ToString();
                t18 = monthsArray[17].ToString();
                t19 = monthsArray[18].ToString();
            }
            if (monthsArray.Count() == 20)
            {
                t1 = monthsArray[0].ToString();
                t2 = monthsArray[1].ToString();
                t3 = monthsArray[2].ToString();
                t4 = monthsArray[3].ToString();
                t5 = monthsArray[4].ToString();
                t6 = monthsArray[5].ToString();
                t7 = monthsArray[6].ToString();
                t8 = monthsArray[7].ToString();
                t9 = monthsArray[8].ToString();
                t10 = monthsArray[9].ToString();
                t11 = monthsArray[10].ToString();
                t12 = monthsArray[11].ToString();
                t13 = monthsArray[12].ToString();
                t14 = monthsArray[13].ToString();
                t15 = monthsArray[14].ToString();
                t16 = monthsArray[15].ToString();
                t17 = monthsArray[16].ToString();
                t18 = monthsArray[17].ToString();
                t19 = monthsArray[18].ToString();
                t20 = monthsArray[19].ToString();
            }
        }

        #endregion


    }
}