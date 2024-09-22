using System;

namespace DPS.SuperAdmin.PaymentConfigurationClassFIle
{
    public class SchoolPaymentConfiguration
    {
        public int Id { get; set; }                // Primary key
        public int ClientId { get; set; }           // Foreign key to client
        public string MccCode { get; set; }         // Merchant Category Code
        public string MerchantId { get; set; }      // Merchant ID
        public string UserId { get; set; }          // User ID
        public string MerchantPassword { get; set; } // Merchant Password
        public string ProductId { get; set; }       // Product ID
        public string TransactionCurrency { get; set; } // Transaction Currency
        public string RequestAesKey { get; set; }   // Request AES Key
        public string RequestHashKey { get; set; }   // Request Hash Key
        public string ResponseAesKey { get; set; }  // Response AES Key
        public string ResponseHashKey { get; set; } // Response Hash Key
        public string HashAlgorithm { get; set; }   // Hash Algorithm
        public string CustomerAccountNumber { get; set; } // Customer Account Number
        public bool IsActive { get; set; }          // Active status
        public string CreatedBy { get; set; }       // Created by
        public DateTime CreatedOn { get; set; }     // Creation date
        public string UpdatedBy { get; set; }       // Updated by
        public DateTime? UpdatedOn { get; set; }    // Update date
        public string DeletedBy { get; set; }       // Deleted by
        public DateTime? DeletedOn { get; set; }    // Deletion date
        public bool IsDeleted { get; set; }         // Soft delete flag
    }
}