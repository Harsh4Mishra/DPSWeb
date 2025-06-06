
/****** Object:  Table [dbo].[FeesOnlineTransaction]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeesOnlineTransaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ScholarNumber] [varchar](max) NULL,
	[StudentName] [varchar](max) NULL,
	[Amount] [int] NULL,
	[TransactionID] [varchar](max) NULL,
	[TransactionDate] [datetime] NULL,
	[IsReverified] [bit] NULL,
	[T1] [varchar](max) NULL,
	[T2] [varchar](max) NULL,
	[T3] [varchar](max) NULL,
	[T4] [varchar](max) NULL,
	[T5] [varchar](max) NULL,
	[T6] [varchar](max) NULL,
	[T7] [varchar](max) NULL,
	[T8] [varchar](max) NULL,
	[T9] [varchar](max) NULL,
	[T10] [varchar](max) NULL,
	[T11] [varchar](max) NULL,
	[T12] [varchar](max) NULL,
	[T13] [varchar](max) NULL,
	[T14] [varchar](max) NULL,
	[T15] [varchar](max) NULL,
	[T16] [varchar](max) NULL,
	[T17] [varchar](max) NULL,
	[T18] [varchar](max) NULL,
	[T19] [varchar](max) NULL,
	[T20] [varchar](max) NULL,
	[CreatedBy] [varchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
	[BankName] [varchar](max) NULL,
	[BankTransaction] [varchar](max) NULL,
	[AtomID] [varchar](max) NULL,
	[TransactionType] [varchar](max) NULL,
 CONSTRAINT [PK_FeesOnlineTransaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO
/****** Object:  Table [dbo].[FeeTransactionRequest]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeeTransactionRequest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ScholarNumber] [varchar](max) NULL,
	[StudentName] [varchar](max) NULL,
	[Amount] [int] NULL,
	[AtomID] [varchar](max) NULL,
	[TransactionID] [varchar](max) NULL,
	[TransactionDate] [datetime] NULL,
	[IsReverified] [bit] NULL,
	[T1] [varchar](max) NULL,
	[T2] [varchar](max) NULL,
	[T3] [varchar](max) NULL,
	[T4] [varchar](max) NULL,
	[T5] [varchar](max) NULL,
	[T6] [varchar](max) NULL,
	[T7] [varchar](max) NULL,
	[T8] [varchar](max) NULL,
	[T9] [varchar](max) NULL,
	[T10] [varchar](max) NULL,
	[T11] [varchar](max) NULL,
	[T12] [varchar](max) NULL,
	[T13] [varchar](max) NULL,
	[T14] [varchar](max) NULL,
	[T15] [varchar](max) NULL,
	[T16] [varchar](max) NULL,
	[T17] [varchar](max) NULL,
	[T18] [varchar](max) NULL,
	[T19] [varchar](max) NULL,
	[T20] [varchar](max) NULL,
	[CreatedBy] [varchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [varchar](max) NULL,
	[UpdatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO
/****** Object:  Table [dbo].[FeeTransactionValue]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeeTransactionValue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TextValue] [varchar](max) NULL,
	[EncryptedValue] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO
/****** Object:  Table [dbo].[SyncMaster]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SyncMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InitialSyncOn] [datetime] NULL,
	[LastStudentSyncOn] [datetime] NULL,
	[LastTransactionSyncOn] [datetime] NULL,
	[LastTransactionIDSync] [int] NULL,
	[IsDownloaded] [bit] NULL,
 CONSTRAINT [PK_SyncMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY];
GO
/****** Object:  StoredProcedure [dbo].[ADD_FEE_TRANSACTION_REQUEST]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ADD_FEE_TRANSACTION_REQUEST]
    @ScholarNumber VARCHAR(MAX),
    @StudentName VARCHAR(MAX),
    @Amount INT,
    @TransactionID VARCHAR(MAX),
    @TransactionDate DATETIME,
    @AtomID VARCHAR(MAX),
    @T1 VARCHAR(MAX) = NULL,
    @T2 VARCHAR(MAX) = NULL,
    @T3 VARCHAR(MAX) = NULL,
    @T4 VARCHAR(MAX) = NULL,
    @T5 VARCHAR(MAX) = NULL,
    @T6 VARCHAR(MAX) = NULL,
    @T7 VARCHAR(MAX) = NULL,
    @T8 VARCHAR(MAX) = NULL,
    @T9 VARCHAR(MAX) = NULL,
    @T10 VARCHAR(MAX) = NULL,
    @T11 VARCHAR(MAX) = NULL,
    @T12 VARCHAR(MAX) = NULL,
    @T13 VARCHAR(MAX) = NULL,
    @T14 VARCHAR(MAX) = NULL,
    @T15 VARCHAR(MAX) = NULL,
    @T16 VARCHAR(MAX) = NULL,
    @T17 VARCHAR(MAX) = NULL,
    @T18 VARCHAR(MAX) = NULL,
    @T19 VARCHAR(MAX) = NULL,
    @T20 VARCHAR(MAX) = NULL,
    @CreatedBy VARCHAR(MAX),
    @NewID INT OUTPUT  -- Add an output parameter for the new ID
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[FeeTransactionRequest] (
        ScholarNumber,
        StudentName,
        Amount,
        TransactionID,
        TransactionDate,
        AtomID,
        IsReverified,
        T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,
        T11, T12, T13, T14, T15, T16, T17, T18, T19, T20,
        CreatedBy,
        CreatedOn
    )
    VALUES (
        @ScholarNumber,
        @StudentName,
        @Amount,
        @TransactionID,
        @TransactionDate,
        @AtomID,
        0,
        @T1, @T2, @T3, @T4, @T5, @T6, @T7, @T8, @T9, @T10,
        @T11, @T12, @T13, @T14, @T15, @T16, @T17, @T18, @T19, @T20,
        @CreatedBy,
        GETDATE()  -- Automatically set CreatedOn to the current date and time
    );

    -- Get the ID of the newly inserted record
    SET @NewID = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[ADD_FEE_TRANSACTION_VALUE]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ADD_FEE_TRANSACTION_VALUE]
    @TextValue VARCHAR(MAX),
    @EncryptedValue VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[FeeTransactionValue] (TextValue, EncryptedValue)
    VALUES (@TextValue, @EncryptedValue);
END
GO
/****** Object:  StoredProcedure [dbo].[GET_FEE_TRANSACTION_REQUEST_BY_ID]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GET_FEE_TRANSACTION_REQUEST_BY_ID]
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ID,
        ScholarNumber,
        StudentName,
        Amount,
        AtomID,
        TransactionID,
        TransactionDate,
        IsReverified,
        T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,
        T11, T12, T13, T14, T15, T16, T17, T18, T19, T20,
        CreatedBy,
        CreatedOn,
        UpdatedBy,
        UpdatedOn
    FROM [dbo].[FeeTransactionRequest]
    WHERE ID = @ID;
END
GO
/****** Object:  StoredProcedure [dbo].[GetDistinctClassNames]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDistinctClassNames]
AS
BEGIN
    SET NOCOUNT ON;

        SELECT 
            ClassName
        FROM 
            ClassMaster
       ORDER BY Cast(ClassSerialNo as int);
END;
GO
/****** Object:  StoredProcedure [dbo].[GetFeeTransactionSummaryBoth]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetFeeTransactionSummaryBoth]
    @ClassName NVARCHAR(100) = NULL,
    @SectionName NVARCHAR(100) = NULL,
    @FromDate DATE = NULL,
    @ToDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Temporary table to hold the results
    CREATE TABLE #TempResults (
        ScholarNo NVARCHAR(50),
        StudentName NVARCHAR(100),
		ClassName varchar(max),
		SectionName varchar(max),
        CashRecAmt DECIMAL(18, 2),  -- Added CashRecAmt
        ChequeAmt DECIMAL(18, 2),    -- Added ChequeAmt
        FineAmt DECIMAL(18, 2),
        ReceiptNo NVARCHAR(50),
        ReceiptDt VARCHAR(MAX),
        TotDisAmt DECIMAL(18, 2),
        TotFeeAmt DECIMAL(18, 2),
        TotRecAmt DECIMAL(18, 2),
        OnlineAmt DECIMAL(18, 2),    -- New column for OnlineAmt
        OnlineRefNo NVARCHAR(50)      -- New column for OnlineRefNo
    );

    INSERT INTO #TempResults (ScholarNo, StudentName, ClassName, SectionName, CashRecAmt, ChequeAmt, FineAmt, ReceiptNo, ReceiptDt, TotDisAmt, TotFeeAmt, TotRecAmt, OnlineAmt, OnlineRefNo)
    SELECT 
        ft.ScholarNo,
        ss.StudentName,
		ss.ClassName,
		ss.SectionName,
        ft.CashRecAmt,            -- Include CashRecAmt
        ft.ChequeAmt,             -- Include ChequeAmt
        ft.FineAmt,
        ft.ReceiptNo,
        ft.ReceiptDt,
        ft.TotDisAmt,
        ft.TotFeeAmt,
        ft.TotRecAmt,
        ft.OnlineAmt,             -- Include OnlineAmt
        ft.OnlineRefNo            -- Include OnlineRefNo
    FROM 
        FeeTransaction ft
    INNER JOIN 
        studentstatus ss ON ft.ScholarNo = ss.ScholarNo
    WHERE 
        (@ClassName IS NULL OR ss.ClassName = @ClassName) AND
        (@SectionName IS NULL OR ss.SectionName = @SectionName) AND
        (@FromDate IS NULL OR CAST(ft.CreatedOn AS DATETIME) >= @FromDate) AND
        (@ToDate IS NULL OR CAST(ft.CreatedOn AS DATETIME) <= @ToDate);

    -- Select the results and include the sums
    SELECT 
        ScholarNo,
        StudentName,
		ClassName,
		SectionName,
        CashRecAmt,            -- Include CashRecAmt in the results
        ChequeAmt,             -- Include ChequeAmt in the results
        FineAmt,
        ReceiptNo,
        ReceiptDt,
        TotDisAmt,
        TotFeeAmt,
        TotRecAmt,
        OnlineAmt,             -- Include OnlineAmt in the results
        OnlineRefNo            -- Include OnlineRefNo in the results
    FROM 
        #TempResults

    --UNION ALL

    --SELECT 
    --    NULL AS ScholarNo,
    --    NULL AS StudentName,
    --    SUM(CashRecAmt) AS CashRecAmt,        -- Sum of CashRecAmt
    --    SUM(ChequeAmt) AS ChequeAmt,          -- Sum of ChequeAmt
    --    SUM(FineAmt) AS FineAmt,
    --    NULL AS ReceiptNo,
    --    NULL AS ReceiptDt,
    --    SUM(TotDisAmt) AS TotDisAmt,
    --    SUM(TotFeeAmt) AS TotFeeAmt,
    --    SUM(TotRecAmt) AS TotRecAmt,
    --    SUM(OnlineAmt) AS OnlineAmt,          -- Sum of OnlineAmt
    --    NULL AS OnlineRefNo                     -- OnlineRefNo can't be summed, keeping it NULL
    --FROM 
    --    #TempResults;

    -- Clean up temporary table
    DROP TABLE #TempResults;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetFeeTransactionSummaryOffLine]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetFeeTransactionSummaryOffLine]
    @ClassName NVARCHAR(100) = NULL,
    @SectionName NVARCHAR(100) = NULL,
    @FromDate DATE = NULL,
    @ToDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Temporary table to hold the results
    CREATE TABLE #TempResults (
        ScholarNo NVARCHAR(50),
        StudentName NVARCHAR(100),
		ClassName varchar(max),
		SectionName varchar(max),
        CashRecAmt DECIMAL(18, 2),
        ChequeAmt DECIMAL(18, 2),
        FineAmt DECIMAL(18, 2),
        ReceiptNo NVARCHAR(50),
		ReceiptDt varchar(max),
        TotDisAmt DECIMAL(18, 2),
        TotFeeAmt DECIMAL(18, 2),
        TotRecAmt DECIMAL(18, 2)
    );

    INSERT INTO #TempResults (ScholarNo, StudentName, ClassName, SectionName, CashRecAmt, ChequeAmt, FineAmt, ReceiptNo,ReceiptDt , TotDisAmt, TotFeeAmt, TotRecAmt)
    SELECT 
        ft.ScholarNo,
        ss.StudentName,
		ss.ClassName,
		ss.SectionName,
        ft.CashRecAmt,
        ft.ChequeAmt,
        ft.FineAmt,
        ft.ReceiptNo,
		ft.ReceiptDt,
        ft.TotDisAmt,
        ft.TotFeeAmt,
        ft.TotRecAmt
    FROM 
        FeeTransaction ft
    INNER JOIN 
        studentstatus ss ON ft.ScholarNo = ss.ScholarNo
    WHERE 
	 CAST(ft.OnlineAmt as int)=0 AND
        (@ClassName IS NULL OR ss.ClassName = @ClassName) AND
        (@SectionName IS NULL OR ss.SectionName = @SectionName) AND
        (@FromDate IS NULL OR Cast(ft.CreatedOn as datetime) >= @FromDate) AND
        (@ToDate IS NULL OR Cast(ft.CreatedOn as datetime) <= @ToDate);

    -- Select the results and include the sums
    SELECT 
        ScholarNo,
        StudentName,
		ClassName,
		SectionName,
        CashRecAmt,
        ChequeAmt,
        FineAmt,
        ReceiptNo,
		ReceiptDt,
        TotDisAmt,
        TotFeeAmt,
        TotRecAmt
    FROM 
        #TempResults

  --  UNION ALL

  --  SELECT 
  --      NULL AS ScholarNo,
  --      NULL AS StudentName,
  --      SUM(CashRecAmt) AS CashRecAmt,
  --      SUM(ChequeAmt) AS ChequeNo,
  --      SUM(FineAmt) AS FineAmt,
  --      NULL AS ReceiptNo,
		--Null AS ReceiptDt,
  --      SUM(TotDisAmt) AS TotDisAmt,
  --      SUM(TotFeeAmt) AS TotFeeAmt,
  --      SUM(TotRecAmt) AS TotRecAmt
  --  FROM 
  --      #TempResults;

    -- Clean up temporary table
    DROP TABLE #TempResults;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetPaidFeeByScholarNo]    Script Date: 23-Nov-24 3:09:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetPaidFeeByScholarNo] 
	@ScholarNo varchar(max)
AS
BEGIN
	SELECT distinct  ftd.FeeType
FROM FeeTransDetail ftd
INNER JOIN FeeTransaction ft ON ftd.ReceiptNo = ft.ReceiptNo
WHERE ft.ScholarNo = @ScholarNo
END;
GO
/****** Object:  StoredProcedure [dbo].[GetFeeTransactionSummaryONLine]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetFeeTransactionSummaryONLine]
    @ClassName NVARCHAR(100) = NULL,
    @SectionName NVARCHAR(100) = NULL,
    @FromDate DATE = NULL,
    @ToDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Temporary table to hold the results
    CREATE TABLE #TempResults (
        ScholarNo NVARCHAR(50),
        StudentName NVARCHAR(100),
		ClassName varchar(max),
		SectionName varchar(max),
        FineAmt DECIMAL(18, 2),
        ReceiptNo NVARCHAR(50),
        ReceiptDt VARCHAR(MAX),
        TotDisAmt DECIMAL(18, 2),
        TotFeeAmt DECIMAL(18, 2),
        TotRecAmt DECIMAL(18, 2),
        OnlineAmt DECIMAL(18, 2),  -- New column for OnlineAmt
        OnlineRefNo NVARCHAR(50)    -- New column for OnlineRefNo
    );

    INSERT INTO #TempResults (ScholarNo, StudentName, ClassName, SectionName, FineAmt, ReceiptNo, ReceiptDt, TotDisAmt, TotFeeAmt, TotRecAmt, OnlineAmt, OnlineRefNo)
    SELECT 
        ft.ScholarNo,
        ss.StudentName,
		ss.ClassName,
		ss.SectionName,
        ft.FineAmt,
        ft.ReceiptNo,
        ft.ReceiptDt,
        ft.TotDisAmt,
        ft.TotFeeAmt,
        ft.TotRecAmt,
        ft.OnlineAmt,          -- Include OnlineAmt
        ft.OnlineRefNo        -- Include OnlineRefNo
    FROM 
        FeeTransaction ft
    INNER JOIN 
        studentstatus ss ON ft.ScholarNo = ss.ScholarNo
    WHERE 
        CAST(ft.OnlineAmt AS INT) > 0 AND    -- Updated condition
        (@ClassName IS NULL OR ss.ClassName = @ClassName) AND
        (@SectionName IS NULL OR ss.SectionName = @SectionName) AND
        (@FromDate IS NULL OR CAST(ft.CreatedOn AS DATETIME) >= @FromDate) AND
        (@ToDate IS NULL OR CAST(ft.CreatedOn AS DATETIME) <= @ToDate);

    -- Select the results and include the sums
    SELECT 
        ScholarNo,
        StudentName,
		ClassName,
		SectionName,
        FineAmt,
        ReceiptNo,
        ReceiptDt,
        TotDisAmt,
        TotFeeAmt,
        TotRecAmt,
        OnlineAmt,          -- Include OnlineAmt in the results
        OnlineRefNo         -- Include OnlineRefNo in the results
    FROM 
        #TempResults

    --UNION ALL

    --SELECT 
    --    NULL AS ScholarNo,
    --    NULL AS StudentName,
    --    SUM(FineAmt) AS FineAmt,
    --    NULL AS ReceiptNo,
    --    NULL AS ReceiptDt,
    --    SUM(TotDisAmt) AS TotDisAmt,
    --    SUM(TotFeeAmt) AS TotFeeAmt,
    --    SUM(TotRecAmt) AS TotRecAmt,
    --    SUM(OnlineAmt) AS OnlineAmt,        -- Sum of OnlineAmt
    --    NULL AS OnlineRefNo                   -- OnlineRefNo can't be summed, keeping it NULL
    --FROM 
    --    #TempResults;

    -- Clean up temporary table
    DROP TABLE #TempResults;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetOrderNoByFeeType]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[GetOrderNoByFeeType]
    @FeeType VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Orderno
    FROM FeeTypeMaster
    WHERE FeeType = @FeeType;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetSectionNamesByClassName]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE PROCEDURE [dbo].[GetSectionNamesByClassName]
    @ClassName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        SectionName
    FROM 
        ClassSectionAllotment
    WHERE 
        ClassName = @ClassName;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetSerialNoByFeeName]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSerialNoByFeeName]
    @FeeName VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT SNo
    FROM FeeNameMaster
    WHERE FeeName = @FeeName;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetStudentDetailByScholarNo]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentDetailByScholarNo](@ScholarNo varchar(max))
AS
BEGIN
    select sm.Scholarno,sm.StudentName,sm.Caste,sm.AppliedStream,sm.DOB,sm.Sex,sm.FatherName,sm.FatherPhone,ss.ClassName
	,ss.SectionName from StudentMaster sm inner join studentstatus ss 
	on sm.Scholarno=ss.ScholarNo 
	where sm.Scholarno=@ScholarNo
END;
GO

/****** Object:  StoredProcedure [dbo].[INSERT_FEE_RECEIPT_PRINT_ONLINE]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[INSERT_FEE_RECEIPT_PRINT_ONLINE]
    @ReceiptDt DATETIME,
    @ScholarNo VARCHAR(MAX),
    @ReceiptNo INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [FeeReceiptPrintOnline] (
        ReceiptDt,
        ScholarNo
    )
    VALUES (
        @ReceiptDt,
        @ScholarNo
    );

    -- Get the last inserted ReceiptNo
    SET @ReceiptNo = SCOPE_IDENTITY();
END;
GO
/****** Object:  StoredProcedure [dbo].[INSERT_FEE_TRANS_DETAIL_ONLINE]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[INSERT_FEE_TRANS_DETAIL_ONLINE]
    @ReceiptNo INT,
    @FeeType VARCHAR(MAX),
    @FeeName VARCHAR(MAX),
    @PrevBalAmt DECIMAL(10, 2),
    @FeeAmt DECIMAL(10, 2),
    @DisAmt DECIMAL(10, 2),
    @PaidFeeAmt DECIMAL(10, 2),
    @FeeTypeSeqNo INT,
    @FeeHeadSeqNo INT,
	@RowsAffected INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[FeeTransDetailOnline] (
        ReceiptNo,
        FeeType,
        FeeName,
        PrevBalAmt,
        FeeAmt,
        DisAmt,
        PaidFeeAmt,
        FeeTypeSeqNo,
        FeeHeadSeqNo
    )
    VALUES (
        @ReceiptNo,
        @FeeType,
        @FeeName,
        @PrevBalAmt,
        @FeeAmt,
        @DisAmt,
        @PaidFeeAmt,
        @FeeTypeSeqNo,
        @FeeHeadSeqNo
    );

	SET @RowsAffected = @@ROWCOUNT; 
END;
GO
/****** Object:  StoredProcedure [dbo].[INSERT_FEE_TRANSACTION_ONLINE]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INSERT_FEE_TRANSACTION_ONLINE]
    @ReceiptNo INT,
    @ReceiptDt DATETIME,
    @ScholarNo VARCHAR(MAX),
    @BillBookNo VARCHAR(MAX),
    @TotFeeAmt DECIMAL(10, 2),
    @FineAmt DECIMAL(10, 2),
    @TotDisAmt DECIMAL(10, 2),
    @TotRecAmt DECIMAL(10, 2),
    @OnlineAmt DECIMAL(10, 2),
    @OnlineRefNo VARCHAR(MAX),
    @OnlineDt DATETIME,
    @AmtInWords VARCHAR(MAX),
    @StudentName VARCHAR(MAX),
    @Amount INT,
    @TransactionID VARCHAR(MAX),
    @TransactionDate DATETIME,
    @IsReverified BIT,
    @CreatedBy VARCHAR(MAX),
    @BankName VARCHAR(MAX),
    @BankTransaction VARCHAR(MAX),
    @AtomID VARCHAR(MAX),
    @TransactionType VARCHAR(MAX),
    @T1 VARCHAR(MAX),
    @T2 VARCHAR(MAX),
    @T3 VARCHAR(MAX),
    @T4 VARCHAR(MAX),
    @T5 VARCHAR(MAX),
    @T6 VARCHAR(MAX),
    @T7 VARCHAR(MAX),
    @T8 VARCHAR(MAX),
    @T9 VARCHAR(MAX),
    @T10 VARCHAR(MAX),
    @T11 VARCHAR(MAX),
    @T12 VARCHAR(MAX),
    @T13 VARCHAR(MAX),
    @T14 VARCHAR(MAX),
    @T15 VARCHAR(MAX),
    @T16 VARCHAR(MAX),
    @T17 VARCHAR(MAX),
    @T18 VARCHAR(MAX),
    @T19 VARCHAR(MAX),
    @T20 VARCHAR(MAX),
	@RowsAffected INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Insert into FeeTransactionOnline
    INSERT INTO [dbo].[FeeTransactionOnline] (
        ReceiptNo,
        ReceiptDt,
        ScholarNo,
        BillBookNo,
        TotFeeAmt,
        FineAmt,
        TotDisAmt,
        TotRecAmt,
        OnlineAmt,
        OnlineRefNo,
        OnlineDt,
        AmtInWords,
        CreatedOn,
        CreatedTime
    )
    VALUES (
        @ReceiptNo,
        @ReceiptDt,
        @ScholarNo,
        @BillBookNo,
        @TotFeeAmt,
        @FineAmt,
        @TotDisAmt,
        @TotRecAmt,
        @OnlineAmt,
        @OnlineRefNo,
        @OnlineDt,
        @AmtInWords,
        GETDATE(),  -- Automatically set CreatedOn to current date
        GETDATE()   -- Automatically set CreatedTime to current time
    );

	SET @RowsAffected = @@ROWCOUNT;

    -- Insert into FeesOnlineTransaction
    INSERT INTO [dbo].[FeesOnlineTransaction] (
        ScholarNumber,
        StudentName,
        Amount,
        TransactionID,
        TransactionDate,
        IsReverified,
        CreatedBy,
        CreatedOn,
        BankName,
        BankTransaction,
        AtomID,
        TransactionType,
        T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,
        T11, T12, T13, T14, T15, T16, T17, T18, T19, T20
    )
    VALUES (
        @ScholarNo,
        @StudentName,
        @Amount,
        @TransactionID,
        @TransactionDate,
        @IsReverified,
        @CreatedBy,
        GETDATE(),  -- Automatically set CreatedOn to current date
        @BankName,
        @BankTransaction,
        @AtomID,
        @TransactionType,
        @T1, @T2, @T3, @T4, @T5, @T6, @T7, @T8, @T9, @T10,
        @T11, @T12, @T13, @T14, @T15, @T16, @T17, @T18, @T19, @T20
    );
	SET @RowsAffected = @RowsAffected + @@ROWCOUNT;
END;
GO
/****** Object:  StoredProcedure [dbo].[StudentFeeParameterDetail]    Script Date: 27-Oct-24 1:36:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StudentFeeParameterDetail] 
(
    @ScholarNo VARCHAR(MAX)
)
AS
BEGIN
    CREATE TABLE #StudentFeeDetails (ScholarNo VARCHAR(MAX), FeeName VARCHAR(MAX), FeeAmount INT);
    CREATE TABLE #FeeNameList (FeeName VARCHAR(MAX), OptionalFee BIT, Conveyance BIT);

    DECLARE @Mode VARCHAR(MAX), @Area VARCHAR(MAX), @Category VARCHAR(MAX);
    DECLARE @LeftSchool VARCHAR(MAX), @ClassName VARCHAR(MAX), @SectionName VARCHAR(MAX);
    DECLARE @TransportFeeName VARCHAR(MAX), @AreaWise BIT, @TransportFee INT;

    -- Fetch student details
    SELECT 
        @Mode = s.Mode, 
        @Area = s.Area, 
        @Category = s.Caste, 
        @LeftSchool = st.LeftSchool, 
        @ClassName = st.ClassName, 
        @SectionName = st.SectionName
    FROM StudentMaster s
    JOIN studentstatus st ON s.ScholarNo = st.ScholarNo
    WHERE s.ScholarNo = @ScholarNo and st.LeftSchool<>'Yes';

   
        -- Insert compulsory FeeName into FeeNameList
        INSERT INTO #FeeNameList (FeeName, OptionalFee, Conveyance)
        SELECT FeeName, OptionalFee, Conveyance FROM FeeNameMaster WHERE OptionalFee = 0;

        -- Insert OptionalFee for specific ScholarNo into FeeNameList
        INSERT INTO #FeeNameList (FeeName, OptionalFee, Conveyance)
        SELECT sft.OptionalFee, fnm.OptionalFee,fnm.Conveyance FROM StudentFeeType sft inner join FeeNameMaster fnm on 
sft.OptionalFee=fnm.FeeName  WHERE ScholarNo = @ScholarNo;

        -- Check for transport fee
        DECLARE @transportCount INT;
        SELECT @transportCount = COUNT(*) FROM #FeeNameList WHERE OptionalFee = 1 AND Conveyance = 1;

        IF (@transportCount > 0)
        BEGIN
            SELECT @TransportFeeName = FeeName FROM #FeeNameList WHERE OptionalFee = 1 AND Conveyance = 1;
        END

        -- Select fees into #feeStructure
        SELECT FeeName, FeeNameAmount, FeeType 
        INTO #feeStructure 
        FROM FeeStructure
        WHERE ClassName = @ClassName AND 
              Category = @Category AND FeeName IN (SELECT FeeName FROM #FeeNameList);

		

        IF (@transportCount > 0)
        BEGIN
            SET @AreaWise = (SELECT AreaWise FROM FeeParameter);
			
            IF (@AreaWise = 1)
            BEGIN
                DECLARE @conveyanceFee INT;
                SELECT @conveyanceFee = Charges FROM AreaMaster WHERE AreaName = @Area AND Mode = @Mode;

                -- Loop through #feeStructure
                DECLARE @Counter INT = 1, @Max INT;
                SELECT @Max = COUNT(*) FROM #feeStructure;

                WHILE @Counter <= @Max
                BEGIN
                    DECLARE @FeeName VARCHAR(100), @FeeType VARCHAR(100);
                    SELECT @FeeName = FeeName, @FeeType = FeeType
                    FROM (
                        SELECT FeeName, FeeType, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum
                        FROM #feeStructure
                    ) AS T
                    WHERE RowNum = @Counter;

                    IF (@FeeName = @TransportFeeName)
                    BEGIN
                        DECLARE @monthValue INT;
                        SELECT @monthValue = NoOfMonth FROM FeeMonth WHERE FeeMonth = @FeeType;
                        DECLARE @TransportFeeAmount INT = @conveyanceFee * @monthValue;

                        UPDATE #feeStructure
                        SET FeeNameAmount = @TransportFeeAmount
                        WHERE FeeName = @FeeName AND FeeType = @FeeType;
                    END

                    SET @Counter = @Counter + 1;
                END
            END
        END

        -- Handling fines
        DECLARE @FineApplicable VARCHAR(MAX);
        SELECT @FineApplicable = FineType FROM FeeParameter;
		
        IF (@FineApplicable = 'Applicable')
        BEGIN
            DECLARE @fineAmt INT;
            SELECT @fineAmt = FineAmount FROM FeeParameter;
            INSERT INTO #feeStructure (FeeName, FeeNameAmount, FeeType) 
            VALUES ('Fine', @fineAmt, '');
        END
        ELSE IF (@FineApplicable = 'Per Day')
        BEGIN
            DECLARE @fineAmount DECIMAL(10,2), @finalFineAmount INT, @diffCount INT;
            SELECT @fineAmount = FineAmount FROM FeeParameter;

            DECLARE @Counterpd INT = 1, @Maxpd INT;
            SELECT @Maxpd = COUNT(*) FROM FeeMonth;

            WHILE @Counterpd <= @Maxpd
            BEGIN
                DECLARE @dueDate DATETIME, @FeeTypepd VARCHAR(100);

                SELECT @FeeTypepd = FeeMonth
                FROM (
                    SELECT FeeMonth, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum
                    FROM FeeMonth
                ) AS T
                WHERE RowNum = @Counterpd;

				set @dueDate=(select top 1 DueDate from FeeTypeMaster where FeeType=@FeeTypepd and ClassName=@ClassName)

                SET @diffCount = DATEDIFF(DAY, @dueDate, GETDATE());
                SET @finalFineAmount = 0;
				--select @diffCount,@dueDate,@FeeTypepd
                IF (@diffCount > 0)
                BEGIN
                    SET @finalFineAmount = @fineAmount * @diffCount;
                END

                INSERT INTO #feeStructure (FeeName, FeeNameAmount, FeeType) 
                VALUES ('Fine', @finalFineAmount, @FeeTypepd);

                SET @Counterpd = @Counterpd + 1;
            END
        END

        -- Looping through FeeMonths
        DECLARE @Counterfm INT = 1, @Maxfm INT;
        CREATE TABLE #FeeDetails (FeeName VARCHAR(MAX), FeeType VARCHAR(MAX), FeeAmount DECIMAL(10,2));
        SELECT FeeMonth INTO #FeeMonths FROM FeeMonth;

        SELECT @Maxfm = COUNT(*) FROM #FeeMonths;

        WHILE @Counterfm <= @Maxfm
        BEGIN
            DECLARE @CurrentFeeMonth VARCHAR(50);
            SELECT @CurrentFeeMonth = FeeMonth
            FROM (
                SELECT FeeMonth, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum
                FROM #FeeMonths
            ) AS T
            WHERE RowNum = @Counterfm;

            DECLARE @writeoffCount INT;
            SET @writeoffCount = (SELECT COUNT(*) FROM StudentFeeWriteOff WHERE ScholarNo = @ScholarNo AND FeeType = @CurrentFeeMonth);

            IF (@writeoffCount=0)
            BEGIN
                INSERT INTO #FeeDetails (FeeName, FeeType, FeeAmount)
                SELECT FeeName, FeeType, FeeNameAmount FROM #feeStructure WHERE FeeType = @CurrentFeeMonth;
            END

            SET @Counterfm = @Counterfm + 1;
        END
   

    SELECT * FROM #FeeDetails;
	SELECT 
    fm.FeeMonth,
    COALESCE(SUM(fd.FeeAmount), 0) AS TotalFee
FROM 
    FeeMonth fm
LEFT JOIN 
    #FeeDetails fd ON fm.FeeMonth = fd.FeeType AND fd.FeeName != 'Fine'
GROUP BY 
    fm.FeeMonth
ORDER BY 
    CASE fm.FeeMonth 
        WHEN 'April' THEN 1
        WHEN 'May' THEN 2
        WHEN 'June' THEN 3
        WHEN 'July' THEN 4
        WHEN 'August' THEN 5
        WHEN 'September' THEN 6
        WHEN 'October' THEN 7
        WHEN 'November' THEN 8
        WHEN 'December' THEN 9
        WHEN 'January' THEN 10
        WHEN 'February' THEN 11
        WHEN 'March' THEN 12
    END

	--select * from #feeStructure;

    -- Cleanup
    DROP TABLE #StudentFeeDetails;
    DROP TABLE #FeeNameList;
    DROP TABLE #FeeStructure;
    DROP TABLE #FeeDetails;
    DROP TABLE #FeeMonths;
END;
GO
