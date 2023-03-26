USE [EnManai]
GO
/****** Object:  User [DESKTOP-L1PFAQD\Amuthan]    Script Date: 26-03-2023 16:16:07 ******/
CREATE USER [DESKTOP-L1PFAQD\Amuthan] FOR LOGIN [DESKTOP-L1PFAQD\Amuthan] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NT AUTHORITY\SYSTEM]    Script Date: 26-03-2023 16:16:07 ******/
CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[db_accessadmin]
GO
ALTER ROLE [db_owner] ADD MEMBER [DESKTOP-L1PFAQD\Amuthan]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [DESKTOP-L1PFAQD\Amuthan]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [DESKTOP-L1PFAQD\Amuthan]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [DESKTOP-L1PFAQD\Amuthan]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [DESKTOP-L1PFAQD\Amuthan]
GO
ALTER ROLE [db_datareader] ADD MEMBER [DESKTOP-L1PFAQD\Amuthan]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [DESKTOP-L1PFAQD\Amuthan]
GO
/****** Object:  Table [dbo].[HouseOwner]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HouseOwner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[FatherName] [nvarchar](max) NULL,
	[MotherName] [nvarchar](max) NULL,
	[AadharNo] [nvarchar](50) NULL,
	[PhonePrimary] [nvarchar](30) NOT NULL,
	[Phone2] [nvarchar](30) NULL,
	[LandLine1] [nvarchar](30) NULL,
	[LandLine2] [nvarchar](30) NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[CIty] [nvarchar](max) NULL,
	[District] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Pincode] [nvarchar](50) NULL,
	[ResidingAddress] [bit] NOT NULL,
 CONSTRAINT [PK_HouseOwner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HouseOwnerResidingAddress]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HouseOwnerResidingAddress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HouseOwnerID] [int] NOT NULL,
	[Address1] [nvarchar](max) NOT NULL,
	[Address2] [nvarchar](max) NOT NULL,
	[CIty] [nvarchar](max) NOT NULL,
	[District] [nvarchar](max) NOT NULL,
	[State] [nvarchar](max) NOT NULL,
	[Pincode] [nchar](10) NULL,
 CONSTRAINT [PK_ouseOwnerResidingAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[HouseOwnerId] [int] NULL,
	[TenantId] [int] NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[EMailId] [nvarchar](30) NOT NULL,
	[PhoneNumberVerified] [bit] NOT NULL,
	[EmailVerified] [bit] NOT NULL,
	[ReverficationTime] [int] NOT NULL,
	[PhoneNumberVerifiedDate] [datetime] NULL,
	[EmailIdVerifiedDate] [datetime] NULL,
	[MandatoryVerification] [bit] NOT NULL,
	[ReVerification] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedBy] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[NonPaidedContactViewed] [int] NOT NULL,
	[PaidedContactViewed] [int] NOT NULL,
	[Paided] [bit] NOT NULL,
	[NoOfNonPaidedContact] [int] NOT NULL,
	[NonPaidContactList] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentForRent]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentForRent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HouseOwnerId] [int] NOT NULL,
	[RentHouseId] [int] NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[PaymentReceivedDate] [datetime] NOT NULL,
	[AmountPaid] [int] NOT NULL,
	[SchemeId] [int] NOT NULL,
	[ReferenceNumber] [nvarchar](max) NULL,
	[BankDetails] [nvarchar](max) NULL,
	[TransactionSuccessfull] [bit] NOT NULL,
	[PaymentExpiry] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentForTenant]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentForTenant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[PaymentDate] [datetime] NOT NULL,
	[PaymentReceivedDate] [datetime] NOT NULL,
	[AmountPaid] [int] NOT NULL,
	[SchemeId] [int] NOT NULL,
	[RefernceNumber] [nvarchar](max) NOT NULL,
	[BankDetails] [nvarchar](max) NOT NULL,
	[TransactonSucessfull] [bit] NOT NULL,
	[PaymentExpired] [bit] NOT NULL,
	[PaymentExpiryDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentalHouseDetails]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentalHouseDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HouseOwnerId] [int] NOT NULL,
	[FlatNoOrDoorNo] [nvarchar](50) NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[AreaOrNagar] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[District] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[Pincode] [nvarchar](20) NULL,
	[Floor] [nvarchar](10) NULL,
	[Vasuthu] [bit] NOT NULL,
	[CoOperationWater] [bit] NOT NULL,
	[BoreWater] [bit] NOT NULL,
	[SeparateEB] [bit] NOT NULL,
	[TwoWheelerParking] [bit] NOT NULL,
	[FourWheelerParking] [bit] NOT NULL,
	[SeparateHouse] [bit] NOT NULL,
	[HouseOwnerResidingInSameBuilding] [bit] NOT NULL,
	[RentalOccupied] [bit] NOT NULL,
	[Apartment] [bit] NOT NULL,
	[ApartmentFloor] [int] NOT NULL,
	[RentFrom] [int] NULL,
	[RentTo] [int] NULL,
	[PetsAllowed] [bit] NOT NULL,
	[Address3] [nvarchar](max) NULL,
	[PaymentActive] [bit] NOT NULL,
	[BHK] [int] NOT NULL,
	[Bachelor] [bit] NOT NULL,
	[NonVeg] [bit] NOT NULL,
	[Deposit] [int] NOT NULL,
	[PaymentId] [int] NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[PhoneNumberPrimary] [nvarchar](20) NULL,
	[LandLineNumber] [nvarchar](30) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentalsImage]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentalsImage](
	[id] [int] NOT NULL,
	[Filename] [nvarchar](max) NULL,
	[filebytes] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Scheme]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scheme](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SchemeName] [nvarchar](max) NOT NULL,
	[SchemeDuration] [int] NOT NULL,
	[DiscountPercentage] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[Rental] [bit] NOT NULL,
	[Tenant] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SMSVerification]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SMSVerification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[LoginId] [int] NOT NULL,
	[Phonenumber] [nvarchar](50) NOT NULL,
	[SMSSendDateTime] [datetime] NOT NULL,
	[SMSExpiryDateTime] [datetime] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[NoOfVerificationPerDay] [int] NOT NULL,
	[BlockForDays] [int] NOT NULL,
	[BlockAfterAttempt] [int] NOT NULL,
	[TotalNoAttempt] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[ModifiedBy] [nvarchar](50) NOT NULL,
	[ModifiedOn] [datetime] NULL,
	[SMSCode] [nvarchar](10) NULL,
	[LastSMSSendStatus] [bit] NULL,
 CONSTRAINT [PK_SMSVerfication] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDisplayFields]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDisplayFields](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FieldName] [nvarchar](50) NULL,
	[UserType] [nvarchar](50) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[HouseOwner] ADD  CONSTRAINT [DF_HouseOwner_ResidingAddress]  DEFAULT ((0)) FOR [ResidingAddress]
GO
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_PhoneNumberVerified]  DEFAULT ((0)) FOR [PhoneNumberVerified]
GO
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_EmailVerified]  DEFAULT ((0)) FOR [EmailVerified]
GO
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_ReverficationTime]  DEFAULT ((0)) FOR [ReverficationTime]
GO
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_MandatoryVerification]  DEFAULT ((0)) FOR [MandatoryVerification]
GO
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_ReVerification]  DEFAULT ((0)) FOR [ReVerification]
GO
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_NonPaidedContactViewed]  DEFAULT ((0)) FOR [NonPaidedContactViewed]
GO
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_PaidedContactViewed]  DEFAULT ((0)) FOR [PaidedContactViewed]
GO
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_Paided]  DEFAULT ((0)) FOR [Paided]
GO
ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_NoOfNonPaidedContact]  DEFAULT ((0)) FOR [NoOfNonPaidedContact]
GO
ALTER TABLE [dbo].[PaymentForRent] ADD  CONSTRAINT [DF_PaymentForRent_AmountPaid]  DEFAULT ((0)) FOR [AmountPaid]
GO
ALTER TABLE [dbo].[PaymentForRent] ADD  CONSTRAINT [DF_PaymentForRent_TransactionSuccessfull]  DEFAULT ((0)) FOR [TransactionSuccessfull]
GO
ALTER TABLE [dbo].[PaymentForTenant] ADD  CONSTRAINT [DF_PaymentForTenant_PaymentExpired]  DEFAULT ((0)) FOR [PaymentExpired]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_Vasuthu]  DEFAULT ((0)) FOR [Vasuthu]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_CoOperationWater]  DEFAULT ((0)) FOR [CoOperationWater]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_BoreWater]  DEFAULT ((0)) FOR [BoreWater]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_SeparateEB]  DEFAULT ((0)) FOR [SeparateEB]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_TwoWheelerParking]  DEFAULT ((0)) FOR [TwoWheelerParking]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_FourWheelerParking]  DEFAULT ((0)) FOR [FourWheelerParking]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_SeparateHouse]  DEFAULT ((0)) FOR [SeparateHouse]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_HouseOwnerResidingInSameBuilding]  DEFAULT ((0)) FOR [HouseOwnerResidingInSameBuilding]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_RentalOccupied]  DEFAULT ((0)) FOR [RentalOccupied]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_Apartment]  DEFAULT ((0)) FOR [Apartment]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_ApartmentFloor]  DEFAULT ((0)) FOR [ApartmentFloor]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_PetsAllowed]  DEFAULT ((0)) FOR [PetsAllowed]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_PaymentActive]  DEFAULT ((0)) FOR [PaymentActive]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_BHK]  DEFAULT ((1)) FOR [BHK]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_Bachelor]  DEFAULT ((0)) FOR [Bachelor]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_NonVeg]  DEFAULT ((0)) FOR [NonVeg]
GO
ALTER TABLE [dbo].[RentalHouseDetails] ADD  CONSTRAINT [DF_RentalHouseDetails_Deposit]  DEFAULT ((0)) FOR [Deposit]
GO
/****** Object:  StoredProcedure [dbo].[CREATEMODEL]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CREATEMODEL]  
(  
     @TableName SYSNAME ,  
     @CLASSNAME VARCHAR(500)   
)  
AS  
BEGIN  
    DECLARE @Result VARCHAR(MAX)  
  
    SET @Result = @CLASSNAME + ' '+ @TableName + '  
{'  
  
SELECT @Result = @Result + '  
    public ' + ColumnType + NullableSign + ' ' + ColumnName + ' { get; set; }'  
FROM  
(  
    SELECT   
        REPLACE(col.NAME, ' ', '_') ColumnName,  
        column_id ColumnId,  
        CASE typ.NAME   
            WHEN 'bigint' THEN 'long'  
            WHEN 'binary' THEN 'byte[]'  
            WHEN 'bit' THEN 'bool'  
            WHEN 'char' THEN 'string'  
            WHEN 'date' THEN 'DateTime'  
            WHEN 'datetime' THEN 'DateTime'  
            WHEN 'datetime2' then 'DateTime'  
            WHEN 'datetimeoffset' THEN 'DateTimeOffset'  
            WHEN 'decimal' THEN 'decimal'  
            WHEN 'float' THEN 'float'  
            WHEN 'image' THEN 'byte[]'  
            WHEN 'int' THEN 'int'  
            WHEN 'money' THEN 'decimal'  
            WHEN 'nchar' THEN 'char'  
            WHEN 'ntext' THEN 'string'  
            WHEN 'numeric' THEN 'decimal'  
            WHEN 'nvarchar' THEN 'string'  
            WHEN 'real' THEN 'double'  
            WHEN 'smalldatetime' THEN 'DateTime'  
            WHEN 'smallint' THEN 'short'  
            WHEN 'smallmoney' THEN 'decimal'  
            WHEN 'text' THEN 'string'  
            WHEN 'time' THEN 'TimeSpan'  
            WHEN 'timestamp' THEN 'DateTime'  
            WHEN 'tinyint' THEN 'byte'  
            WHEN 'uniqueidentifier' THEN 'Guid'  
            WHEN 'varbinary' THEN 'byte[]'  
            WHEN 'varchar' THEN 'string'  
            ELSE 'UNKNOWN_' + typ.NAME  
        END ColumnType,  
        CASE   
            WHEN col.is_nullable = 1 and typ.NAME in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier')   
            THEN '?'   
            ELSE ''   
        END NullableSign  
    FROM SYS.COLUMNS col join sys.types typ on col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id  
    where object_id = object_id(@TableName)  
) t  
ORDER BY ColumnId  
SET @Result = @Result  + '  
}'  
  
print @Result  
  
END  
GO
/****** Object:  StoredProcedure [dbo].[EM_HouseOwner]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Amuthan
-- Create date: 21.08.2022
-- Description:	HouseOwner procedure for Read, Create, Update and Delete (0,1,2,3) are completed.

--@ACTION : 
--0 - READ
--1 - CREATE
--2 - Update
--4 - DELETE
--@ResidingAddress 
--0 - House Owner is Same house
--1 - House Owner is Different house

--EXECUTE EM_HouseOwner 
--	@id='11',
--	@name = 'Ahamed',
--  @LastName = 'Mohamed',
--  @FatherName = 'Syed',
--  @MotherName = 'Bhuavam',
--  @AadharNo  = '1502',
--  @PhonePrimary = '9952401985',
--  @Phone2 = null,
--  @LandLine1  = '0458',
--  @LandLine2 = null,
--  @EmailAddress = 'ahamed@gmail.com',
--  @Address1 = '41',
--  @Address2 = 'Gandhi Road',
--  @CIty = 'Palani',
--  @District = 'Dindigul',
--  @State = 'TN',
--  @Pincode = '624601',
--  @ResidingAddress = 0,
--	@OwnerAddress1  = 'No.102',
--  @OwnerAddress2 = 'Samuvel Puram',
--  @ownerCIty  = 'Thirunelveli',
--  @OwnerDistrict  = 'Thirunelveli',
--  @OwnerState = 'Tamilnadu',
--  @OwnerPincode  = '628002',
--	@Action = '1',
--	@UserId = '1'

-- =============================================
CREATE PROCEDURE [dbo].[EM_HouseOwner] 
	-- Add the parameters for the stored procedure here
	@id int = 0,
	@name varchar(max) = null,
    @LastName varchar(max) = 'Balan',
    @FatherName varchar(max) = 'Bala Subramanian',
    @MotherName varchar(max) = 'Shanthi1',
    @AadharNo varchar(max) = '1602',
    @PhonePrimary varchar(max) = '9952401983',
    @Phone2 varchar(max) = null,
    @LandLine1 varchar(max) = '04547',
    @LandLine2 varchar(max) = null,
    @EmailAddress varchar(max) = 'amuphy1@gmail.com',
    @Address1 varchar(max) = '43',
    @Address2 varchar(max) = 'Gandhi Road1',
    @CIty varchar(max) = 'Palani1',
    @District varchar(max) = 'Dindigul1',
    @State varchar(max) = 'Tamilnadu1',
    @Pincode varchar(max) = '624602',
    @ResidingAddress bit = 1,
	@OwnerAddress1 nvarchar(max) = null,
    @OwnerAddress2 nvarchar(50) = null,
    @ownerCIty nvarchar(50) = null,
    @OwnerDistrict nvarchar(50) = null,
    @OwnerState nvarchar(50) = null,
    @OwnerPincode nvarchar(50) = null,
	@Action nvarchar(50) = '0',
	@UserId int = 0
      

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	If(@Action = '0') --Read 
	BEGIN
		SELECT * FROM HouseOwner H Left join HouseOwnerResidingAddress rd 
		ON h.Id = rd.HouseOwnerID
		JOIN Login L
		ON l.HouseOwnerId = h.Id AND L.ID = @UserId
	END
	ELSE If(@Action = '1') --  CREATE
	BEGIN
	DECLARE @newID int
	Insert into houseowner (
                                [Name],
                                LastName,
                                FatherName, 
                                MotherName,
                                AadharNo, 
                                PhonePrimary, 
                                Phone2, 
                                LandLine1,
                                LandLine2, 
                                EmailAddress, 
                                Address1, 
                                Address2, 
                                CIty, 
                                District, 
                                State, 
                                Pincode, 
                                ResidingAddress) 
                                values(@name,
                                @LastName,
                                @FatherName,
                                @MotherName,
                                @AadharNo,
                                @PhonePrimary,
                                @Phone2,
                                @LandLine1,
                                @LandLine2,
                                @EmailAddress,
                                @Address1,
                                @Address2,
                                @CIty,
                                @District,
                                @State,
                                @Pincode,
                                @ResidingAddress)
	DECLARE @IdentityValue int
	SET @IdentityValue = SCOPE_IDENTITY()

	UPDATE Login SET HouseOwnerId = @IdentityValue WHERE id=@UserId;

	IF(@ResidingAddress = 0)
	BEGIN
	PRINT 'Inserting Residing Address'
	INSERT INTO HouseOwnerResidingAddress (
	HouseOwnerID,
	Address1,
	Address2,
	CIty,
	District,
	[State],
	Pincode) 
	values(
	@IdentityValue,
	@OwnerAddress1,
	@OwnerAddress2,
	@ownerCIty,
	@OwnerDistrict,
	@OwnerState,
	@OwnerPincode)
	END
	END
	ELSE IF(@Action ='2') -- Update
	BEGIN 
	PRINT 'Updating Houseowner'
	UPDATE houseowner SET 
		[Name] = @name,
        LastName = @LastName,
        FatherName = @FatherName, 
        MotherName = @MotherName,
        AadharNo = @AadharNo, 
        PhonePrimary = @PhonePrimary, 
        Phone2 = @Phone2, 
        LandLine1 = @LandLine1,
        LandLine2 = @LandLine2, 
        EmailAddress = @EmailAddress, 
        Address1 = @Address1, 
        Address2 = @Address2, 
        CIty = @CIty, 
        District = @District, 
        [State] = @State, 
        Pincode = @Pincode, 
        ResidingAddress = @ResidingAddress 
		WHERE Id=@id
		PRINT 'Updating Houseowner Completed'
		if(@ResidingAddress=0)
		BEGIN
		PRINT 'Updating House owner Residing Address'
		IF EXISTS(select * from HouseOwnerResidingAddress where HouseOwnerID=@id)
		BEGIN
			update HouseOwnerResidingAddress set 
			Address1 = @OwnerAddress1,
			Address2 = @OwnerAddress2,
			CIty = @ownerCIty,
			District = @OwnerDistrict,
			[State] = @OwnerState,
			Pincode	=@OwnerPincode
			where HouseOwnerID=@id
			PRINT 'Updating House owner Residing Address Completed'
		END
		ELSE
		BEGIN
			INSERT INTO HouseOwnerResidingAddress (
			HouseOwnerID,
			Address1,
			Address2,
			CIty,
			District,
			[State],
			Pincode) 
			values(
			@id,
			@OwnerAddress1,
			@OwnerAddress2,
			@ownerCIty,
			@OwnerDistrict,
			@OwnerState,
			@OwnerPincode)
		END
   END
   ELSE IF (@ResidingAddress = 1)
   BEGIN
   IF EXISTS(SELECT * FROM HouseOwnerResidingAddress WHERE HouseOwnerID = @id)
   BEGIN
   DELETE FROM HouseOwnerResidingAddress WHERE HouseOwnerID = @id
   END

   END
	END
	ELSE IF (@Action = '3') --Delete
	BEGIN
	DELETE FROM HouseOwnerResidingAddress WHERE Id=@id
	DELETE FROM HouseOwner WHERE ID=@id
	END
END
GO
/****** Object:  StoredProcedure [dbo].[EM_PaymentForRent]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Amuthan
-- Create date: 28/08/2022
-- Description:	SP for Read, Create, Update and Delete of PaymentforRent
-- =============================================
CREATE PROCEDURE [dbo].[EM_PaymentForRent] 
	@Id int =0,
	@HouseOwnerId int = 0,
	@RentHouseId int = 0,
	@PaymentDate Datetime = null,
	@PaymentReceivedDate Datetime = null,
	@AmountPaid int = 0,
	@SchemeId int = 0,
	@ReferenceNumber nvarchar(max) = null,
	@BankDetails nvarchar(max) = null,
	@TransactionSuccessfull bit = 0,
	@Action int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@Action = 0) -- READ
	BEGIN
	SELECT * FROM PaymentForRent WHERE HouseOwnerId = @HouseOwnerId and RentHouseId=@RentHouseId
	END
	ELSE IF (@Action = 1) -- CREATE
	BEGIN
		INSERT INTO PaymentForRent(HouseOwnerId,RentHouseId, PaymentDate,PaymentReceivedDate,AmountPaid, SchemeId,ReferenceNumber,BankDetails,TransactionSuccessfull) 
		values(
		@HouseOwnerId,
		@RentHouseId,
		@PaymentDate,
		@PaymentReceivedDate,
		@AmountPaid,
		@SchemeId,
		@ReferenceNumber,
		@BankDetails,
		@TransactionSuccessfull)
	END
	ELSE IF (@Action = 2) -- Update
	BEGIN
		UPDATE PaymentForRent set
		HouseOwnerId = @HouseOwnerId,
		RentHouseId = @RentHouseId,
		PaymentDate = @PaymentDate,
		PaymentReceivedDate = @PaymentReceivedDate,
		AmountPaid = @AmountPaid,
		SchemeId = @SchemeId,
		ReferenceNumber = @ReferenceNumber,
		BankDetails = @BankDetails,
		TransactionSuccessfull = @TransactionSuccessfull
		WHERE HouseOwnerId = @HouseOwnerId
		AND RentHouseId = @RentHouseId
	END
	ELSE IF (@Action = 3) -- Delete
	BEGIN
		DELETE from PaymentForRent 
		WHERE 
		HouseOwnerId = @HouseOwnerId AND 
		RentHouseId = @RentHouseId
	END
END
GO
/****** Object:  StoredProcedure [dbo].[EM_RentalHouseDetails]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Amuthan
-- Create date: 21.08.2022
-- Description:	Rental House Details procedure for Read, Create, Update and Delete (0,1,2,3) in -progress.

--@ACTION : 
--0 - READ
--1 - CREATE
--2 - Update
--3 - DELETE
--execute [dbo].[EM_RentalHouseDetails] 
--@Id = 0,
--	@HouseOwnerId = 4,
--	@AreaOrNagar =null,
--	@City = 'Palani',
--	@District ='Dindigul',
--	@State = 'TN',
--	@Pincode = '624601',
--	@Floor  = 0,
--	@Vasuthu  = 1,
--	@CoOperationWater = 1,
--	@BoreWater = 1,
--	@SeparateED = 1,
--	@TwoWheelerParking = 1,
--	@FourWheelerParking = 0,
--	@SeparateHouse = 0,
--	@HouseResidingInSameBuilding = 0,
--	@apartment = 0,
--	@ResultType = null,
--	@action = 0,
--  @RentFrom = 10000,
--	@RentTo = 12000,
-- PetsAllowed = 0,
-- PaymentActive = 1
-- 
-- ==============================================
CREATE PROCEDURE [dbo].[EM_RentalHouseDetails] 
	@Id int = null,
	@HouseOwnerId int = null,
	@FlatNoOrDoorNo int = null,
	@Address1 nvarchar(max)= null,
	@Address2 nvarchar(max)= null,
	@Address3 nvarchar(max)= null,
	@AreaOrNagar nvarchar(max) = null,
	@City nvarchar(max) = null,
	@District nvarchar(max) = null,
	@State nvarchar(max) = null,
	@Pincode nvarchar(20) = null,
	@Floor nvarchar(10) = null,
	@Vasuthu bit = 0,
	@CoOperationWater bit = 0,
	@BoreWater bit = 0,
	@SeparateED bit = 0,
	@TwoWheelerParking bit = 0,
	@FourWheelerParking bit = 0,
	@SeparateHouse bit = 0,
	@HouseResidingInSameBuilding bit = 0,
	@RentalOccupied bit =0,
	@Apartment bit = 0,
	@ApartmentFloor bit = 0,
	@Action nvarchar(10) = 0,
	@ResultType nvarchar(20) = null,
	@RentFrom nvarchar(max) = null,
	@RentTo nvarchar(max) = null,
	@PetsAllowed bit = 0,
	@PaymentActive bit = 1,
	@start int = 0,
	@End int = null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	print @Action
	IF @Action = 0 --Search (Read)
	BEGIN 
		--1. Result on ID value
		IF @Id IS NOT NULL AND  @Action = 0 AND @ResultType IS NULL AND @HouseOwnerId IS NULL 
		BEGIN
		PRINT 'Search with id:'+@id
		SELECT * from RentalHouseDetails where Id = @Id
		RETURN
		END
		--2. Based on HouseOwner ID
		ELSE IF @Id IS NULL AND  @Action = 0 AND @ResultType IS NULL AND @HouseOwnerId IS NOT NULL  
		BEGIN
		PRINT 'Search with houseownerid:' + @houseownerid
		SELECT * FROM RentalHouseDetails WHERE HouseOwnerId = @HouseOwnerId
		RETURN
		END

		ELSE IF @Id IS NULL AND  @Action = 0 AND @ResultType IS NOT NULL AND @HouseOwnerId IS NULL
		BEGIN
			DECLARE @sql nvarchar(max) = 'SELECT * FROM RentalHouseDetails WHERE RentalOccupied = 0 ',
			@areaOrNagarSql nvarchar(max),
			@citySql nvarchar(max),
			@pincodeSql nvarchar(max), 
			@floorSql nvarchar(max),
			@rentAmountSql varchar(max),
			@petsAllowedSql bit,
			@finalSql nvarchar(max)
			PRINT 'SEARCH :'+@resulttype
			
			--IF @PaymentActive = 1 
			--BEGIN
			--	SET @sql ='SELECT * FROM RentalHouseDetails WHERE RentalOccupied = 0 AND PaymentActive = ''true''' 
			--END
			--ELSE 
			--BEGIN
			--	SET @sql ='SELECT * FROM RentalHouseDetails WHERE RentalOccupied = 0 AND PaymentActive = ''false'''
			--END
			IF @City IS NOT NULL
			BEGIN
				SET @citySql = ' AND City ='+''''+@City +''''
			END

			IF(@AreaOrNagar IS NOT NULL)
			BEGIN
				print 'Seach :'+@ResultType
				SET @areaOrNagarSql = ' AND AreaOrNagar =' +''''+@AreaOrNagar+''''
			END
			
			IF @Pincode IS NOT NULL
			BEGIN 
				SET @pincodeSql = ' AND PINCODE = '+''''+@Pincode+''''
			END

			IF @Floor IS NOT NULL
			BEGIN
				SET @floorSql = ' AND Floor = '+''''+@Floor+''''
			END

			IF @Rentfrom IS NOT NULL AND @Rentfrom IS NOT NULL
			BEGIN
				IF @RentFrom <> 0 and @RentTo <> 0
				BEGIN
					PRINT 'Implemented not started'
				END
				RETURN
			END
			
			SET @finalSql = @sql + @citySql 
			+ isnull(@areaOrNagarSql,'') 
			+ isnull(@pincodeSql,'')
			+ isnull(@floorSql,'')
			
			IF(@Vasuthu = 'True')
			BEGIN
			SET @finalSql = @finalSql + ' AND Vasuthu = ''true'''
			END
			SET @finalSql = @finalSql + ' order by id OFFSET '  + CAST(@start AS varchar) +' ROWS FETCH NEXT '+ CAST(@End AS varchar) +' ROWS ONLY';
			PRINT @finalSql 
			EXEC sp_executesql @finalSql 
			
		END
		Return	
	END
    -- Insert Rentail House Details
	ELSE IF @Action = 1 -- Create Rental House Details
	BEGIN 
		INSERT INTO RentalHouseDetails(
		HouseOwnerId,
		FlatNoOrDoorNo,
		Address1,
		Address2,
		AreaOrNagar,
		City,
		District,
		[State],
		Pincode,
		[Floor],
		Vasuthu,
		CoOperationWater,
		BoreWater,
		SeparateEB,
		TwoWheelerParking,
		FourWheelerParking,
		SeparateHouse,
		HouseOwnerResidingInSameBuilding,
		RentalOccupied,
		Apartment,
		ApartmentFloor,
		RentFrom,
		RentTo,
		PetsAllowed)
		Values(
		@HouseOwnerId,
		@FlatNoOrDoorNo,
		@Address1,
		@Address2,
		@AreaOrNagar,
		@City,
		@District,
		@State,
		@Pincode,
		@Floor,
		@Vasuthu,
		@CoOperationWater,
		@BoreWater,
		@SeparateED,
		@TwoWheelerParking,
		@FourWheelerParking,
		@SeparateHouse,
		@HouseResidingInSameBuilding,
		@RentalOccupied,
		@Apartment,
		@ApartmentFloor,
		@RentFrom,
		@RentTo,
		@PetsAllowed)
		RETURN
	END
	ELSE IF @Action = 2 -- Create Rental House Details
	BEGIN 
		UPDATE RentalHouseDetails SET 
		HouseOwnerId =  @HouseOwnerId,
		FlatNoOrDoorNo = @FlatNoOrDoorNo,
		Address1 =  @Address1,
		Address2 =  @Address2,
		AreaOrNagar =  @AreaOrNagar,
		City = @City,
		District = @District,
		[State] = @State,
		Pincode =  @Pincode,
		[Floor] = @Floor,
		Vasuthu = @Vasuthu,
		CoOperationWater =  @CoOperationWater,
		BoreWater = @BoreWater,
		SeparateEB = @SeparateED,
		TwoWheelerParking=  @TwoWheelerParking,
		FourWheelerParking = @FourWheelerParking,
		SeparateHouse =  @SeparateHouse,
		HouseOwnerResidingInSameBuilding =  @HouseResidingInSameBuilding,
		RentalOccupied = @RentalOccupied,
		Apartment=  @Apartment,
		ApartmentFloor = @ApartmentFloor,
		RentFrom = @RentFrom,
		RentTo = @RentTo,
		PetsAllowed=  @PetsAllowed
		WHERE id = @id
		RETURN
	END
	ElSE IF @Action = 3 -- DELETE RENTAL HOUSE DETAILS
	BEGIN
	BEGIN TRY
		DELETE FROM RentalHouseDetails WHERE Id = @Id
	END TRY
	BEGIN CATCH
		PRINT 'Delete Has Error'
	END CATCH
	END
END
GO
/****** Object:  StoredProcedure [dbo].[EM_Scheme]    Script Date: 26-03-2023 16:16:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Amuthan
-- Create date: 28/08/2022
-- Description:	SP for Read, Create, Update and Delete of Scheme
-- =============================================
CREATE PROCEDURE [dbo].[EM_Scheme] 
	@Id int =0,
	@SchemeName nvarchar(max) = null,
	@SchemeDuration int = 0,
	@DiscountPercentage int = 0,
	@Amount int = 0,
	@AmountPaid int = 0,
	@Rental bit = 0,
	@Tenant bit = 0,
	@Action int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@Action = 0) -- READ
	BEGIN
	SELECT * FROM Scheme 
	END
	ELSE IF (@Action = 1) -- CREATE
	BEGIN
		INSERT INTO Scheme(
		SchemeName,
		SchemeDuration,
		DiscountPercentage,
		Amount,
		Rental,
		Tenant) 
		values(
		@SchemeName,
		@SchemeDuration,
		@DiscountPercentage,
		@Amount,
		@Rental,
		@Tenant)
	END
	ELSE IF (@Action = 2) -- Update
	BEGIN
		UPDATE Scheme set
		SchemeName = @SchemeName,
		SchemeDuration = @SchemeDuration,
		DiscountPercentage = @DiscountPercentage,
		Amount = @Amount,
		Rental = @Rental,
		Tenant = @Tenant
		WHERE Scheme.Id = @Id
	END
	ELSE IF (@Action = 3) -- Delete
	BEGIN
		DELETE from Scheme 
		WHERE Scheme.Id = @Id
	END
END
GO
